using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KidsPos.Object;
using KidsPos.Object.Database;
using KidsPos.Setting;
using KidsPos.Util;
using Microsoft.VisualBasic.FileIO;

namespace DBRegister.Sources
{
    public partial class TopScreenForm : Form
    {
        public enum LoadType
        {
            Item,
            User
        }

        public TopScreenForm()
        {
            InitializeComponent();
            new Database().CreateTable();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MinimizeBox = false;
            MaximizeBox = false;
            PosInformation.GetInstance().Init(this);
        }

        private int CntSuccess { get; set; }
        private int CntMissing { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            var currentYear = DateTime.Now.ToString("yyyy");
            lKidsDate.Text = $"""
                              {currentYear} 年のキッズ用に登録されます
                              出力ユーザバーコード例: {BarcodeConfig.Prefix}{BarcodeConfig.Staff}{currentYear.Substring(2)}0001
                              出力商品バーコード例　: {BarcodeConfig.Prefix}{BarcodeConfig.Item}010001
                              """;
        }

        private void load_csv_file_Click(object sender, EventArgs e)
        {
            load_csv(LoadType.Item);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            load_csv(LoadType.User);
        }

        private void load_csv(LoadType type)
        {
            var al = new ArrayList();

            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK) return;
            var parser = new TextFieldParser(ofd.FileName, Encoding.GetEncoding("Shift_JIS"))
            {
                TextFieldType = FieldType.Delimited
            };
            // 区切り文字はコンマ
            parser.SetDelimiters(",", "\t");

            CntSuccess = 0;
            CntMissing = 0;

            while (!parser.EndOfData)
            {
                // 1行読み込み
                var row = parser.ReadFields();

                switch (type)
                {
                    case LoadType.Item:
                        if (row != null)
                            foreach (var field in row)
                            {
                                var f = field;
                                f = f.Replace("\r\n", "n");
                                f = f.Replace(" ", "");
                                if (f != "") al.Add(f);
                            }

                        break;
                    case LoadType.User:
                        if (row != null && CheckFormatUser(row))
                        {
                            CntSuccess++;
                            var builder = new StringBuilder();
                            builder.Append(row[0]).Append(",").Append(row[1]);
                            al.Add(builder.ToString());
                        }
                        else
                        {
                            CntMissing++;
                        }

                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }

            switch (type)
            {
                case LoadType.Item:
                    RegistItem(al);
                    break;
                case LoadType.User:
                    RegistrationUser(al);
                    break;
            }
        }

        private static bool CheckFormatUser(IList<string> row)
        {
            var staffId = int.Parse(row[0]);
            return staffId > 0 && 10000 > staffId && !row[1].Equals("");
        }

        public bool RegistItem(ArrayList csv)
        {
            var db = new Database();
            var isFirst = false;
            try
            {
                /* ---- ここから　店名の追加 ---- */
                var storePosition = -1;
                for (var i = 0; i < csv.Count; i++)
                {
                    if (!csv[i].ToString().StartsWith("#店名")) continue;
                    storePosition = i;
                    break;
                }

                if (storePosition == -1) throw new InvalidDataException();
                var storeName = csv[storePosition + 1].ToString();
                var storeNum = db.find_store(storeName);
                if (storeNum == -1)
                {
                    isFirst = true;
                    //登録されてなかったら
                    var dr = MessageBox.Show(@"新しく " + storeName + @" を追加します。", @"追加", MessageBoxButtons.OKCancel);
                    if (dr == DialogResult.OK)
                        db.Insert(new StoreObject(storeName));

                    //登録できていたら
                    storeNum = db.find_store(storeName);
                    if (storeNum == -1) MessageBox.Show(@"何らかの原因で登録できませんでした。");
                }


                //データベースに登録されているgenreの数
                db.Count<ItemGenreObject>($"WHERE store = '{storeNum}'");
                /* ---- ここまで　店名の追加 ---- */

                /* ---- ここから　ジャンルの追加 ---- */

                //商品ジャンルの行から、商品リストまでの行を読み取って、その間をデータベースに追加する。
                var itemPosition = -1;
                for (var i = storePosition; i < csv.Count; i++)
                    if (csv[i].ToString().StartsWith("#商品ジャンル"))
                    {
                        itemPosition = i;
                        break;
                    }

                var itemList = -1;
                for (var i = storePosition; i < csv.Count; i++)
                    if (csv[i].ToString().StartsWith("##商品リスト"))
                    {
                        itemList = i;
                        break;
                    }

                if (itemPosition == itemList) throw new InvalidDataException();

                var g = db.SelectMulti<ItemGenreObject>($"WHERE store = '{storeNum}'").Select(_ => _.Name).ToList();
                var tempGenre = new List<ItemGenreObject>();
                for (var i = itemPosition + 1; i < itemList; i++)
                {
                    var genreName = csv[i].ToString();
                    if (g.IndexOf(genreName) == -1) tempGenre.Add(new ItemGenreObject(genreName, storeNum));
                }

                db.Insert(tempGenre);

                /* ---- ここまで　ジャンルの追加 ---- */

                /* ---- ここから　商品リストの追加 ---- */

                itemPosition = -1;
                for (var i = 0; i < csv.Count; i++)
                {
                    if (!csv[i].ToString().StartsWith("##商品リスト")) continue;
                    itemPosition = i;
                    break;
                }

                if (itemPosition == -1) throw new InvalidDataException();

                var columnCount = 0;
                for (var i = itemPosition + 1; i < csv.Count; i++)
                {
                    if (!csv[i].ToString().StartsWith("#")) continue;
                    columnCount++;
                }

                if (columnCount == 0) throw new InvalidDataException();
                var list = new List<ItemObject>();
                var count = db.Count<ItemObject>();
                for (var i = itemPosition + columnCount + 1; i < csv.Count; i += columnCount)
                {
                    var name = csv[i].ToString();
                    var genreName = csv[i + 1].ToString();
                    var price = Convert.ToInt32(csv[i + 2].ToString());
                    var itemNum = db.find_item(name, price, storeNum);
                    if (itemNum == -1)
                    {
                        count++;
                        if (db.find_ganre(genreName, storeNum) == -1)
                        {
                            //商品登録の時に、変なジャンルが混ざってたら・・・
                            var dr = MessageBox.Show(@"未定義のジャンルがありました。追加しますか？" + Environment.NewLine + csv[i + 1],
                                @"読み込みエラー", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dr == DialogResult.Yes)
                                db.Insert(new ItemGenreObject(csv[i * 1].ToString(), storeNum));
                            else
                                continue;
                        }

                        list.Add(new ItemObject(
                                new BarcodeObject(storeNum, count).Barcode, //barcode
                                name, //name
                                price, //price
                                storeNum, //shop
                                db.find_ganre(genreName, storeNum)) //genre
                        );
                    }
                }

                db.Insert(list);
                /* ---- ここまで　商品リストの追加 ---- */
            }
            catch (InvalidDataException)
            {
                MessageBox.Show(
                    @"ファイルがおかしいよ" + Environment.NewLine +
                    @"・CSV形式であるか" + Environment.NewLine +
                    @"・しっかりフォーマットに沿っているか" + Environment.NewLine +
                    @"を確認してください", @"読み込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Question
                );
                return false;
            }
            finally
            {
                if (isFirst) MessageBox.Show(@"何らかの原因で登録できませんでした。");
                else MessageBox.Show(@"何らかの原因で登録できませんでした。");
            }

            return true;
        }

        public void RegistrationUser(ArrayList csv)
        {
            var result = new Database()
                .Insert(
                    (from string row in csv select row.Split(',') into s select new StaffObject(s[0], s[1])).ToList());
            MessageBox.Show(result ? @"商品リストを更新しました" : @"??");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var rl = new RegistedList();
            rl.ShowDialog(this);
            rl.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var rl = new RegistedUser();
            rl.ShowDialog(this);
            rl.Dispose();
        }
    }
}