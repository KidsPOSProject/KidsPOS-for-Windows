using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Collections;
using Microsoft.VisualBasic.FileIO;
using System.IO;

namespace DBRegister
{
    public partial class Form1 : Form
    {
        public enum LoadType
        {
            ITEM,
            USER
        }

        public Form1()
        {
            InitializeComponent();
            DataBase.getInstance().createTable();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            string stPrompt1 = dt.ToString("yyyy");
            lKidsDate.Text = stPrompt1 + " 年のキッズ用に登録されます" + Environment.NewLine;
            lKidsDate.Text += "出力ユーザバーコード例: " + BarCode_Prefix.PREFIX + BarCode_Prefix.USER + stPrompt1.Substring(2) + "0001" + Environment.NewLine;
            lKidsDate.Text += "出力商品バーコード例　: " + BarCode_Prefix.PREFIX + BarCode_Prefix.SALE + "010001";
        }

        private void load_csv_file_Click(object sender, EventArgs e)
        {
            load_csv(LoadType.ITEM);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            load_csv(LoadType.USER);
        }



        int cntSuccess = 0;
        int cntMissing = 0;

        public void load_csv(LoadType type)
        {
            ArrayList al = new ArrayList();

            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                TextFieldParser parser = new TextFieldParser(ofd.FileName, System.Text.Encoding.GetEncoding("Shift_JIS"));
                parser.TextFieldType = FieldType.Delimited;
                // 区切り文字はコンマ
                parser.SetDelimiters(",", "\t");

                cntSuccess = 0;
                cntMissing = 0;

                while (!parser.EndOfData)
                {
                    // 1行読み込み
                    string[] row = parser.ReadFields();
                    switch (type)
                    {
                        case LoadType.ITEM:
                            foreach (string field in row)
                            {
                                string f = field;
                                // 改行をnで表示
                                f = f.Replace("\r\n", "n");
                                // 空白を_で表示 
                                f = f.Replace(" ", "");
                                // TAB区切りで出力 
                                if (!(f == "")) al.Add(f);
                            }
                            break;
                        case LoadType.USER:
                            if (checkFormatUser(row))
                            {
                                cntSuccess++;
                                StringBuilder builder = new StringBuilder();
                                builder.Append(row[0]).Append(",").Append(row[1]);
                                al.Add(builder.ToString());
                            }
                            else
                            {
                                cntMissing++;
                            }
                            break;
                        default:
                            break;
                    }
                } 
                switch (type)
                {
                    case LoadType.ITEM:
                        registItem(al);
                        break;
                    case LoadType.USER:
                        registUser(al);
                        break;
                    default:
                        break;
                }
            }
        }
        private Boolean checkFormatUser(string[] row)
        {
            if (row[0].Length == BarCode_Prefix.DATA_LENGTH && !row[1].Equals(""))
            {
                return true;
            }
            return false;
        }

        public bool registItem(ArrayList csv)
        {
            DataBase db = DataBase.getInstance();
            bool isFirst = false;
            try
            {
                string store_name = "";
                int storeNum;

                /* ---- ここから　店名の追加 ---- */
                int store_position = -1;
                for (int i = 0; i < csv.Count; i++)
                {
                    if (csv[i].ToString().StartsWith("#店名"))
                    {
                        store_position = i;
                        break;
                    }
                }
                if (store_position == -1)
                {
                    throw new InvalidDataException();
                }
                store_name = csv[store_position + 1].ToString();

                storeNum = db.find_store(store_name);
                if (storeNum == -1)
                {
                    isFirst = true;
                    //登録されてなかったら
                    DialogResult dr = MessageBox.Show("新しく " + store_name + " を追加します。", "追加", MessageBoxButtons.OKCancel);
                    if (dr == DialogResult.OK)
                        db.insert<DataBase.Store>(new DataBase.Store(store_name));

                    //登録できていたら
                    storeNum = db.find_store(store_name);
                    if (storeNum == -1) MessageBox.Show("何らかの原因で登録できませんでした。");
                }

                //データベースに登録されているgenreの数
                int genre_count = db.count<DataBase.ItemGenre>(string.Format("WHERE store = '{0}'", storeNum));

                /* ---- ここまで　店名の追加 ---- */

                /* ---- ここから　ジャンルの追加 ---- */

                //商品ジャンルの行から、商品リストまでの行を読み取って、その間をデータベースに追加する。
                int item_position = -1;

                for (int i = store_position; i < csv.Count; i++)
                {
                    if (csv[i].ToString().StartsWith("#商品ジャンル"))
                    {
                        item_position = i;
                        break;
                    }
                }
                int item_list = -1;
                for (int i = store_position; i < csv.Count; i++)
                {
                    if (csv[i].ToString().StartsWith("##商品リスト"))
                    {
                        item_list = i;
                        break;
                    }
                }

                if (item_position == item_list)
                {
                    throw new InvalidDataException();
                }

                int add_genre_num = item_list - (item_position + 1);

                List<DataBase.ItemGenre> _g = db.selectMulti<DataBase.ItemGenre>(string.Format("WHERE store = '{0}'", storeNum));
                List<string> g = new List<string>();
                foreach (DataBase.ItemGenre _ in _g)
                {
                    g.Add(_.name);
                }
                List<DataBase.ItemGenre> tempGenre = new List<DataBase.ItemGenre>();
                for (int i = item_position + 1; i < item_list; i++)
                {
                    string genreName = csv[i].ToString();
                    if (g.IndexOf(genreName) == -1)
                    {
                        tempGenre.Add(new DataBase.ItemGenre(genreName, storeNum));
                    }
                }
                db.insert(tempGenre);

                /* ---- ここまで　ジャンルの追加 ---- */

                /* ---- ここから　商品リストの追加 ---- */

                item_position = -1;
                for (int i = 0; i < csv.Count; i++)
                {
                    if (csv[i].ToString().StartsWith("##商品リスト"))
                    {
                        item_position = i;
                        break;
                    }
                }
                if (item_position == -1) throw new InvalidDataException();

                int column_count = 0;
                for (int i = item_position + 1; i < csv.Count; i++)
                {
                    if (!csv[i].ToString().StartsWith("#")) continue;
                    column_count++;
                }

                if (column_count == 0) throw new InvalidDataException();
                List<DataBase.Item> list = new List<DataBase.Item>();
                int count = db.count<DataBase.Item>();
                for (int i = item_position + column_count + 1; i < csv.Count; i += column_count)
                {
                    string name = csv[i].ToString();
                    String genreName = csv[i + 1].ToString();
                    int price = Convert.ToInt32(csv[i + 2].ToString());
                    int item_num = db.find_item(name, price, storeNum);
                    if (item_num == -1)
                    {
                        count++;
                        if (db.find_ganre(genreName, storeNum) == -1)
                        {
                            //商品登録の時に、変なジャンルが混ざってたら・・・
                            DialogResult dr = MessageBox.Show("未定義のジャンルがありました。追加しますか？" + Environment.NewLine + csv[i + 1].ToString(), "エラー", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dr == System.Windows.Forms.DialogResult.Yes)
                            {
                                db.insert<DataBase.ItemGenre>(new DataBase.ItemGenre(csv[i * 1].ToString(), storeNum));
                            }
                            else
                            {
                                continue;
                            }
                        }
                        list.Add(new DataBase.Item(
                            new Barcode(storeNum, count).getBarcode(), //barcode
                                name, //name
                                price, //price
                                storeNum, //shop
                                db.find_ganre(genreName, storeNum)) //genre
                        );
                    }
                }
                db.insert<DataBase.Item>(list);
                /* ---- ここまで　商品リストの追加 ---- */
            }
            catch (InvalidDataException)
            {
                MessageBox.Show(
                    "ファイルがおかしいよ" + Environment.NewLine +
                    "・CSV形式であるか" + Environment.NewLine +
                    "・しっかりフォーマットに沿っているか" + Environment.NewLine +
                    "を確認してください", "読み込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Question
                    );
                return false;
            }
            finally
            {
                if(isFirst) MessageBox.Show("アイテムの登録が完了いたしました");
                else MessageBox.Show("更新が完了いたしました");
            }
            return true;
        }
        public void registUser(ArrayList csv)
        {
            List<DataBase.Staff> arr = new List<DataBase.Staff>();
            foreach (string row in csv)
            {
                string[] s = row.Split(',');
                arr.Add(new DataBase.Staff(s[0], s[1]));
            }
            DataBase.getInstance().insert(arr);
            MessageBox.Show("完了しました");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Registed_List rl = new Registed_List();
            rl.ShowDialog(this);
            rl.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RegistedUser rl = new RegistedUser();
            rl.ShowDialog(this);
            rl.Dispose();
        }

    }
}
