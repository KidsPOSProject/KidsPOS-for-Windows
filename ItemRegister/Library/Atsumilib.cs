using System;
using System.Windows.Forms;
using System.Collections;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Printing;
using ZXing;
using Microsoft.VisualBasic.FileIO;
using System.Drawing.Drawing2D;


namespace ItemRegister
{

    class BarCode_Prefix
    {
        public const int BARCODE_NUM = 13;

        public const string PREFIX = "49";

        public const string ITEM        = "00";
        public const string SALE = "01";
        public const string STAFF       = "02";

        //画面遷移
        public const string ITEM_REGIST = "10";
        public const string ITEM_LIST   = "11";
        public const string SALE_LIST   = "12";
        public const string ACCOUNT     = "13";
        public const string STAFF_REGIST = "14";
        public const string STAFF_LIST  ="15";
        public const string ITEM_LIST_EDIT = "16";
        
        //操作
        public const string ENTER       = "20";
        public const string BACK        = "21";
        public const string SHOW_TOOLBAR = "22";
        public const string HIDE_TOOLBAR = "23";

        //動作モード変更
        public const string MODE_TAKE = "30";
        public const string MODE_PRACTICE = "31";

        //MONEY
        public const string M100 = "80";
        public const string M200 = "81";
        public const string M300 = "82";
        public const string M400 = "83";

        //ダミーデータ登録
        public const string DUMMY_ITEM = "90";
        public const string DUMMY_USER = "91";
    }

    class Barcode
    {
        public bool isSet = false;
        public bool isCreated = false;

        private string prefix = "";
        private string store = "";
        private string item_num = "";
        private string barcode = "";

        Barcode(string _prefix)
        {
            this.prefix = _prefix;
        }
        
        /// <summary>
        /// バーコードを作成します。
        /// </summary>
        /// <param name="_prefix">2桁 Barcode_Prefix</param>
        /// <param name="_store_num">3桁 001 Form1.store_num</param>
        /// <param name="_item_num">5桁 データベースとか見てね</param>
        public Barcode(string _prefix, string _store_num, string _item_num)
        {
            this.prefix = _prefix.ToString();
            this.store = _store_num;
            this.item_num = _item_num;
            this.isSet = true;
            this.comb_barcode();
        }
        public void comb_barcode()
        {
            if (this.isSet)
            {
                string temp = BarCode_Prefix.PREFIX + this.prefix + this.store + this.item_num;
                temp += atsumi_pos.create_check_digit(temp);
                if (temp.Length == BarCode_Prefix.BARCODE_NUM)
                {
                    this.barcode = temp;
                    this.isCreated = true;
                }
                else
                {
                    throw new Exception("作られたバーコードの長さがおかしいなぁ・・"+Environment.NewLine+
                    temp);
                }
            }
        }
        public string show()
        {
            return (this.isCreated) ? this.barcode : "";
        }
    }

    class atsumi_pos
    {


        //商品のテーブル
        public class ItemTable
        {
            public string id = null;
            public string barcode;
            public string name;
            public string price;
            public string shop;
            public string genre;

            public ItemTable(string _barcode, string _name, string _price, string _shop, string _genre)
            {
                barcode = _barcode;
                name = _name;
                price = _price;
                shop = _shop;
                genre = _genre;
            }
        }
        //従業員のテーブル
        public class StaffTable
        {
            public string id = null;
            public string barcode;
            public string name;

            public StaffTable(string _barcode, string _name)
            {
                barcode = _barcode;
                name = _name;
            }
        }

        //店名のテーブル
        public class StoreNameTable
        {
            public string id = null;
            public string name;

            public StoreNameTable(string _name)
            {
                name = _name;
            }
        }

        //商品ジャンルのテーブル
        public class ItemGenreTable
        {
            public string id = null;
            public string name;
            public string store;

            public ItemGenreTable(string _name, string _store)
            {
                name = _name;
                store = _store;
            }
        }
        /// <summary>
        /// table に含まれるレコードの数を表示
        /// </summary>
        /// <param name="table">テーブル名</param>
        /// <returns>個数  失敗時 -1</returns>
        public static int read_count_num(string db_file_path, string table)
        {
            int id_count = -1;
            using (var conn = new SQLiteConnection("Data Source=" + db_file_path))
            {
                conn.Open();
                using (SQLiteCommand command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT COUNT(*) FROM " + table;
                    id_count = int.Parse(command.ExecuteScalar().ToString());
                }
                conn.Close();
            }
            return id_count;
        }

        public static string[,] read_item_list(string db_file_path)
        {
            int c_num = read_count_num(db_file_path, "item_list");
            string[,] ret = new string[c_num, 4];

            using (var conn = new SQLiteConnection("Data Source=" + db_file_path))
            {
                conn.Open();
                using (SQLiteCommand command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT id, barcode, name, price FROM item_list";

                    var reader = command.ExecuteReader();

                    int count = 0;
                    while (reader.Read())
                    {
                        ret[count, 0] = reader.GetInt32(0).ToString();
                        ret[count, 1] = reader.GetInt64(1).ToString();
                        ret[count, 2] = reader.GetString(2);
                        ret[count, 3] = reader.GetInt32(3).ToString();
                        count++;
                    }
                }
                conn.Close();
            }
            return ret;
        }
        public static string[,] read_sales_list(string db_file_path)
        {
            int c_num = read_count_num(db_file_path, "sales_list");
            string[,] ret = new string[c_num, 4];

            using (var conn = new SQLiteConnection("Data Source=" + db_file_path))
            {
                conn.Open();
                using (SQLiteCommand command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT buycode, created_at,price,points FROM sales_list";

                    var reader = command.ExecuteReader();

                    int count = 0;
                    while (reader.Read())
                    {
                        ret[count, 0] = reader.GetString(0);
                        ret[count, 1] = reader.GetString(1);
                        ret[count, 2] = reader.GetInt32(2).ToString();
                        ret[count, 3] = reader.GetInt32(3).ToString();
                        count++;
                    }
                }
                conn.Close();
            }
            return ret;
        }
        public static string[,] read_staff_list(string db_file_path)
        {
            int c_num = read_count_num(db_file_path, "staff_list");
            string[,] ret = new string[c_num, 3];

            using (var conn = new SQLiteConnection("Data Source=" + db_file_path))
            {
                conn.Open();
                using (SQLiteCommand command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM staff_list";

                    var reader = command.ExecuteReader();

                    int count = 0;
                    while (reader.Read())
                    {
                        ret[count, 0] = reader.GetInt32(0).ToString();
                        ret[count, 1] = reader.GetString(1);
                        ret[count, 2] = reader.GetString(2);
                        count++;
                    }
                }
                conn.Close();
            }
            return ret;
        }

        public static string[,] find_store(string db_file_path, string _store_name)
        {
            string[,] ret = new string[1, 3];

            using (var conn = new SQLiteConnection("Data Source=" + db_file_path))
            {
                conn.Open();
                using (SQLiteCommand command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM store_kind WHERE name ='" + _store_name + "'";

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ret[0, 0] = reader.GetInt32(0).ToString();
                        ret[0, 1] = reader.GetString(1);
                    }
                }
                conn.Close();
            }
            return ret;
        }
        public static string[,] find_store(string db_file_path, int _store_id)
        {
            string[,] ret = new string[1, 3];

            using (var conn = new SQLiteConnection("Data Source=" + db_file_path))
            {
                conn.Open();
                using (SQLiteCommand command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM store_kind WHERE id ='" + _store_id + "'";

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ret[0, 0] = reader.GetInt32(0).ToString();
                        ret[0, 1] = reader.GetString(1);
                    }
                }
                conn.Close();
            }
            return ret;
        }

        public static string find_genre(string db_file_path, string _genre_name)
        {
            string ret = "";
            using (var conn = new SQLiteConnection("Data Source=" + db_file_path))
            {
                conn.Open();
                using (SQLiteCommand command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT id FROM item_genre WHERE name ='" + _genre_name + "'";

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ret = reader.GetInt32(0).ToString();
                    }
                }
                conn.Close();
            }
            return ret;
        }
        //データベースにインサート
        public static bool Insert(string _db_file, string _query)
        {
            try
            {
                using (var conn = new SQLiteConnection("Data Source=" + _db_file))
                {
                    conn.Open();
                    using (SQLiteTransaction sqlt = conn.BeginTransaction())
                    {
                        using (SQLiteCommand command = conn.CreateCommand())
                        {
                            command.CommandText = _query;
                            command.ExecuteNonQuery();
                        }
                        sqlt.Commit();
                    }
                    conn.Close();
                }
                return true;
            }
            catch (Exception e)
            {
                System_log.ShowDialog(e.ToString());
                return false;
            }
        }
        //データベースにインサート
        public static bool Insert(ItemGenreTable igt, string _db_item)
        {
            try
            {
                using (var conn = new SQLiteConnection("Data Source=" + _db_item))
                {
                    conn.Open();
                    using (SQLiteTransaction sqlt = conn.BeginTransaction())
                    {
                        using (SQLiteCommand command = conn.CreateCommand())
                        {
                            string query = "insert into item_genre (name,store) values('" + igt.name + "','" + igt.store + "')";
                            command.CommandText = query;
                            command.ExecuteNonQuery();
                        }
                        sqlt.Commit();
                    }
                    conn.Close();
                }
                return true;
            }
            catch (Exception e)
            {
                System_log.ShowDialog(e.ToString());
                return false;
            }
        }

        public static string create_check_digit(string barcode)
        {
            int even = 0;
            int odd = 0;

            for (int i = 0; i < barcode.Length; i++)
            {
                if (i == 0 || i % 2 == 0) odd += int.Parse(barcode[i].ToString());
                else even += int.Parse(barcode[i].ToString());
            }

            int check_digit = 10 - (even * 3 + odd) % 10; if (check_digit == 10) check_digit = 0;

            return check_digit.ToString();
        }
        public static ArrayList file_load()
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
    }
    class Unix_Time
    {
        // unix epochをDateTimeで表した定数
        public readonly static DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        // DateTimeをUNIX時間に変換するメソッド
        public static long ToUnixTime(DateTime dateTime)
        {
            // 時刻をUTCに変換
            dateTime = dateTime.ToUniversalTime();

            // unix epochからの経過秒数を求める
            return (long)dateTime.Subtract(UnixEpoch).TotalSeconds;
        }

        // UNIX時間からDateTimeに変換するメソッド
        public static DateTime FromUnixTime(long unixTime)
        {
            // unix epochからunixTime秒だけ経過した時刻を求める
            return UnixEpoch.AddSeconds(unixTime);
        }
    }
    class print_template
    {
        public static void print_temple(string _barcode, string item_name, string store_name, CheckBox cb, PrintPageEventArgs e)
        {
            //TODO Apache fop とか使えたらいいかも・・・

            BarcodeWriter bw = new BarcodeWriter();
            bw.Format = BarcodeFormat.EAN_13;
            Bitmap barcode = bw.Write(_barcode);


            int print_row_num = 4;
            int print_col_num = 11;

            //ミリメートルで指定
            
            //全体の余白
            float MARGIN_PAGE_TOP = 8f;
            float MARGIN_PAGE_LEFT = 8.4f;

            //一つ一つのサイズ
            float MARGIN_PRINT_HEIGHT = 25.4f;
            float MARGIN_PRINT_WEIGHT = 48.3f;


            Graphics g = e.Graphics;
            g.PageUnit = GraphicsUnit.Millimeter;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            if (cb.Checked)
            {
                //線引いてみる
                for (int j = 0; j < print_row_num + 1; j++)
                {
                    for (int i = 0; i < print_col_num + 1; i++)
                    {
                        float hei = i * MARGIN_PRINT_HEIGHT;
                        float wei = j * MARGIN_PRINT_WEIGHT;

                        float pen_size = 0.1f;

                        //横の線
                        g.DrawLine(new Pen(Brushes.Black, pen_size),
                            new Point((int)MARGIN_PAGE_LEFT, (int)(MARGIN_PAGE_TOP + (MARGIN_PRINT_HEIGHT * i))),
                            new Point((int)(MARGIN_PAGE_LEFT + (MARGIN_PRINT_WEIGHT * print_row_num)), (int)(MARGIN_PAGE_TOP + (MARGIN_PRINT_HEIGHT * i))));

                        //縦の線
                        g.DrawLine(new Pen(Brushes.Black, pen_size),
                            new Point((int)(MARGIN_PAGE_LEFT + (MARGIN_PRINT_WEIGHT * j)), (int)(MARGIN_PAGE_TOP)),
                            new Point((int)(MARGIN_PAGE_LEFT + (MARGIN_PRINT_WEIGHT * j)), (int)(MARGIN_PAGE_TOP + (MARGIN_PRINT_HEIGHT * print_col_num))));
                    }
                }
            }


            //バーコード
            for (int j = 0; j < print_row_num; j++)
            {
                for (int i = 0; i < print_col_num; i++)
                {
                    float hei = i * MARGIN_PRINT_HEIGHT;
                    float wei = j * MARGIN_PRINT_WEIGHT;

                    g.DrawImage(
                        barcode,
                        MARGIN_PAGE_LEFT + wei + 3.2f,
                        MARGIN_PAGE_TOP + hei + 7f,
                        barcode.Width * 0.4f, barcode.Height * 0.14f
                        );

                    g.DrawString(
                        item_name,
                        new Font("MS UI Gothic", 9),
                        Brushes.Black,
                        new PointF(
                            MARGIN_PAGE_LEFT + wei + 1f,
                            MARGIN_PAGE_TOP + hei + 0.2f
                            )
                        );

                    g.DrawString(
                        "おみせ: " + store_name,
                        new Font("MS UI Gothic", 8),
                        Brushes.Black,
                        new PointF(
                            MARGIN_PAGE_LEFT + wei + 1f,
                            MARGIN_PAGE_TOP + hei + 3.5f
                            )
                        );
                }
            }
        }

        private static void drawString(Graphics g, Font f, string s, int x, int y)
        {
            g.DrawString(s, f, Brushes.Black, new PointF(x, y));
        }
    }
    class System_log{
        public static void ShowDialog(string msg)
        {
            MessageBox.Show(msg, "例外発生", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
    }
}
