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

namespace ItemRegister
{
    public partial class Form1 : Form
    {
        public static string db_file_pos = "KidsDB-ITEM.db";


        public Form1()
        {
            InitializeComponent();
            CreateTable();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void load_csv_file_Click(object sender, EventArgs e)
        {
            do_load(load_csv());
        }
        

        public static int find_store(string _store_name)
        {
            int res = -1;
            using (var conn = new SQLiteConnection("Data Source=" + db_file_pos))
            {
                conn.Open();
                using (SQLiteCommand command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT id FROM store_kind WHERE name = '" + _store_name + "'";

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        res = reader.GetInt32(0);
                    }
                }
                conn.Close();
            }
            return res;
        }
        public static int find_ganre(string _store_name, string _genre)
        {
            int res = -1;
            using (var conn = new SQLiteConnection("Data Source=" + db_file_pos))
            {
                conn.Open();
                using (SQLiteCommand command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT id FROM item_genre WHERE name = '" + _genre + "' AND store = '"+ _store_name +"'";

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        res = reader.GetInt32(0);
                    }
                }
                conn.Close();
            }
            return res;
        }

        public static int find_item(string _store_num, string _item_name, string _genre, string _price)
        {
            int res = -1;
            using (var conn = new SQLiteConnection("Data Source=" + db_file_pos))
            {
                conn.Open();
                using (SQLiteCommand command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT id FROM item_list WHERE name = '" + _item_name + "' AND price = '" + _price + "' AND shop = '" + _store_num + "'";

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        res = reader.GetInt32(0);
                    }
                }
                conn.Close();
            }
            return res;
        }

        public static int read_count_num(string db_file_path, string table, string _where ="")
        {
            int id_count = -1;
            using (var conn = new SQLiteConnection("Data Source=" + db_file_path))
            {
                conn.Open();
                using (SQLiteCommand command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT COUNT(*) FROM " + table +" "+ _where;
                    id_count = int.Parse(command.ExecuteScalar().ToString());
                }
                conn.Close();
            }
            return id_count;
        }

        public ArrayList load_csv()
        {
            ArrayList al = new ArrayList();

            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                TextFieldParser parser = new TextFieldParser(ofd.FileName, System.Text.Encoding.GetEncoding("Shift_JIS"));
                parser.TextFieldType = FieldType.Delimited;
                // 区切り文字はコンマ
                parser.SetDelimiters(",");

                
                while (!parser.EndOfData)
                {
                    // 1行読み込み
                    string[] row = parser.ReadFields();
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
                }
            }
            return al;
        }
        public bool do_load(ArrayList csv)
        {
            bool isFirst = false;
            try
            {
                string store_name = "";
                int store_num = 0;

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
                    MessageBox.Show(
                        "ファイルがおかしいよ" + Environment.NewLine +
                        "・CSV形式であるか" + Environment.NewLine +
                        "・しっかりフォーマットに沿っているか" + Environment.NewLine +
                        "を確認してください", "読み込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Question
                        );
                    return false;
                }
                store_name = csv[store_position + 1].ToString();

                int res = find_store(store_name);
                if (res > -1)
                {
                    //登録されていたら
                    store_num = res;
                }
                else
                {
                    isFirst = true;
                    //登録されてなかったら
                    DialogResult dr = MessageBox.Show("新しく " + store_name + " を追加します。", "追加", MessageBoxButtons.OKCancel);
                    if (dr == DialogResult.OK)
                        atsumi_pos.Insert(db_file_pos, "INSERT INTO store_kind (name) values('" + store_name + "')");

                    //登録できていたら
                    res = find_store(store_name);
                    if (res > -1)
                    {
                        store_num = res;
                    }
                    else
                    {
                        MessageBox.Show("何らかの原因で登録できませんでした。");
                    }
                }

                //データベースに登録されているgenreの数
                int genre_count = read_count_num(db_file_pos, "item_genre", "WHERE store = " + res.ToString("000"));

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
                    MessageBox.Show(
                        "ファイルがおかしいよ" + Environment.NewLine +
                        "・CSV形式であるか" + Environment.NewLine +
                        "・しっかりフォーマットに沿っているか" + Environment.NewLine +
                        "を確認してください", "読み込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Question
                        );
                    return false;
                }

                int add_genre_num = item_list - (item_position + 1);



                for (int i = item_position + 1; i < item_list; i++)
                {
                    int search = find_ganre(res.ToString("000"), csv[i].ToString());
                    if (search == -1)
                    {
                        atsumi_pos.Insert(db_file_pos, "INSERT INTO item_genre (name,store) values('" + csv[i].ToString() + "','" + res.ToString("000") + "')");
                    }
                }

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
                if (item_position == -1)
                {
                    MessageBox.Show(
                        "ファイルがおかしいよ" + Environment.NewLine +
                        "・CSV形式であるか" + Environment.NewLine +
                        "・しっかりフォーマットに沿っているか" + Environment.NewLine +
                        "を確認してください", "読み込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Question
                        );
                    return false;
                }
                int column_count = 0;
                for (int i = item_position + 1; i < csv.Count; i++)
                {
                    if (!csv[i].ToString().StartsWith("#")) continue;
                    column_count++;
                }

                if (column_count == 0)
                {
                    MessageBox.Show(
                        "ファイルがおかしいよ" + Environment.NewLine +
                        "・しっかりフォーマットに沿っているか" + Environment.NewLine +
                        "を確認してください", "読み込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Question
                        );
                    return false;
                }
                for (int i = item_position + column_count + 1; i < csv.Count; i += column_count)
                {
                    int item_num = find_item(store_num.ToString("000"), csv[i].ToString(), res.ToString("000"), csv[i + 2].ToString());
                    if (item_num == -1)
                    {
                        //atsumi_pos.Insert(db_file_item, "INSERT INTO item_genre (name,store) values('" + csv[i].ToString() + "','" + res.ToString("000") + "')");
                        if (find_ganre(res.ToString("000"), csv[i + 1].ToString()) == -1)
                        {
                            //商品登録の時に、変なジャンルが混ざってたら・・・
                            DialogResult dr = MessageBox.Show("未定義のジャンルがありました。追加しますか？" + Environment.NewLine + csv[i + 1].ToString(),"エラー",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                            if (dr == System.Windows.Forms.DialogResult.Yes)
                            {
                                atsumi_pos.Insert(db_file_pos, "INSERT INTO item_genre (name,store) values('" + csv[i + 1].ToString() + "','" + res.ToString("000") + "')");
                            }
                            else
                            {
                                continue;
                            }
                        }


                        Barcode bar = new Barcode(
                            BarCode_Prefix.ITEM,
                            res.ToString("000"), atsumi_pos.read_count_num(db_file_pos, "item_list").ToString("D5"));

                        atsumi_pos.Insert(
                            db_file_pos,
                            "INSERT INTO item_list (barcode,name,price,shop,genre) VALUES ('" +
                            bar.show() + "','" +
                            csv[i].ToString() + "','" +
                            csv[i + 2].ToString() + "','" +
                            res.ToString() + "','" +
                            find_ganre(res.ToString("000"), csv[i + 1].ToString()) + "')");
                    }
                }
                /* ---- ここまで　商品リストの追加 ---- */
            }
            finally
            {
                if(isFirst) MessageBox.Show("アイテムの登録が完了いたしました");
                else MessageBox.Show("更新が完了いたしました");
            }
            return true;
        }
        public void CreateTable()
        {
            if (!File.Exists(db_file_pos))
            {
                using (var conn = new SQLiteConnection("Data Source=" + db_file_pos))
                {
                    conn.Open();
                    using (SQLiteCommand command = conn.CreateCommand())
                    {
                        //商品ジャンルリスト
                        command.CommandText = "create table item_genre(id INTEGER  PRIMARY KEY AUTOINCREMENT, name TEXT, store TEXT)";
                        command.ExecuteNonQuery();

                        //商品リスト
                        command.CommandText = "create table item_list(id INTEGER  PRIMARY KEY AUTOINCREMENT, barcode INTEGER UNIQUE, name TEXT, price INTEGER, shop INT, genre TEXT)";
                        command.ExecuteNonQuery();

                        //売上リスト
                        command.CommandText = "create table sales_list(id INTEGER  PRIMARY KEY AUTOINCREMENT, buycode TEXT UNIQUE, created_at TEXT, points INTEGER, price INTEGER, items TEXT)";
                        command.ExecuteNonQuery();

                        //お店リスト
                        command.CommandText = "create table store_kind(id INTEGER  PRIMARY KEY AUTOINCREMENT, name TEXT)";
                        command.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Registed_List rl = new Registed_List();
            rl.ShowDialog(this);
            rl.Dispose();
        }
    }
}
