using System;
using System.Windows.Forms;
using System.Collections;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Printing;
using ZXing;
using Microsoft.VisualBasic.FileIO;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;


namespace PosSystem_Client
{

    class BarCode_Prefix
    {
        public const int BARCODE_NUM = 10;
        public const string PREFIX = "10";

        public const string STAFF = "00";
        public const string ITEM = "01";
        public const string SALE = "02";

        //画面遷移
        public const string ITEM_REGIST = "17";
        public const string ITEM_LIST = "11";
        public const string SALE_LIST = "12";
        public const string ACCOUNT = "13";
        public const string STAFF_REGIST = "14";
        public const string STAFF_LIST = "15";
        public const string ITEM_LIST_EDIT = "16";

        //操作
        public const string ENTER = "20";
        public const string BACK = "21";
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

    class atsumi_pos
    {
        string barcode = "";
        string name = "";
        public static ArrayList loadSettings()
        {
            ArrayList al = new ArrayList();
            try
            {
                TextFieldParser parser = new TextFieldParser("ip.csv", System.Text.Encoding.GetEncoding("Shift_JIS"));
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
            catch
            {
                MessageBox.Show("設定ファイルが正しく読み込まれませんでした。");
            }
            return al;
        }


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
            
            using (var conn = new SQLiteConnection("Data Source=" + Form1.db_file_item))
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

            using (var conn = new SQLiteConnection("Data Source=" + Form1.db_file_item))
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
        

        public static string[,] find_user(string db_file_path, string _barcode)
        {
            string[,] ret = new string[1, 3];

            using (var conn = new SQLiteConnection("Data Source=" + db_file_path))
            {
                conn.Open();
                using (SQLiteCommand command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM staff_list WHERE barcode ='" + _barcode + "'";

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ret[0, 0] = reader.GetInt32(0).ToString();
                        ret[0, 1] = reader.GetString(1);
                        ret[0, 2] = reader.GetString(2);
                    }
                }
                conn.Close();
            }
            return ret;
        }
        
        //データベースにインサート
        public static bool Insert(ItemTable it)
        {
            try
            {
                using (var conn = new SQLiteConnection("Data Source=" + Form1.db_file_item))
                {
                    conn.Open();
                    using (SQLiteTransaction sqlt = conn.BeginTransaction())
                    {
                        using (SQLiteCommand command = conn.CreateCommand())
                        {
                            string query = "insert into item_list (barcode, name, price, shop, genre) values('" + it.barcode + "', '" + it.name + "', '" + it.price + "', '" + it.shop + "', '" + it.genre + "')";
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

        //データベースにインサート
        public static bool Insert(StoreNameTable snt)
        {
            try
            {
                using (var conn = new SQLiteConnection("Data Source=" + Form1.db_file_item))
                {
                    conn.Open();
                    using (SQLiteTransaction sqlt = conn.BeginTransaction())
                    {
                        using (SQLiteCommand command = conn.CreateCommand())
                        {
                            string query = "insert into store_kind (name) values('"+ snt.name + "')";
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
        //データベースにインサート
        public static bool Insert(ItemGenreTable igt)
        {
            try
            {
                using (var conn = new SQLiteConnection("Data Source=" + Form1.db_file_item))
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
        
        //データベースにインサート
        public static bool Insert(StaffTable it)
        {
            //重複チェック
            string[,] fu = find_user(Form1.db_file_staff, it.barcode);
            try
            {
                using (var conn = new SQLiteConnection("Data Source=" + Form1.db_file_staff))
                {
                    conn.Open();
                    using (SQLiteTransaction sqlt = conn.BeginTransaction())
                    {
                        using (SQLiteCommand command = conn.CreateCommand())
                        {
                            string query;
                            if (fu[0, 2] == null)
                            {
                                query = "insert into staff_list (barcode, name) values('" + it.barcode + "', '" + it.name + "')";
                            }
                            else
                            {
                                MessageBox.Show("バーコードが重複しました。練習用PCのデータを上書きします。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                query = "update into staff_list set name = '"+it.name+"' WHERE barcode = '"+it.barcode+"'";
                            }

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
        public static void regist_user(Connect cn, string _name, string _barcode = "")
        {
            if (cn != null) cn.SendStringData("staff_list," + _barcode);

            atsumi_pos.Insert(new atsumi_pos.StaffTable(_barcode, _name));
        }


        public void print()
        {
            System.Drawing.Printing.PrintDocument pd = new System.Drawing.Printing.PrintDocument();
            pd.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printDocument1_PrintPage);

            PrintDialog pdlg = new PrintDialog();
            pdlg.Document = pd;
            pd.Print();
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            print_template.print_user(this.barcode, this.name, e);
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
        /// <summary>
        /// デフォルトのプリンタをチェックします。
        /// </summary>
        /// <param name="receipt">レシート印刷が目的かどうか</param>
        /*public static bool check_default_printer(bool receipt = true)
        {
            System.Drawing.Printing.PrintDocument pd = new System.Drawing.Printing.PrintDocument();
            string printer_name = pd.PrinterSettings.PrinterName;
            if (printer_name == "PRP-250")
            {
                if (receipt)return true;
            }
            else
            {
                if (!receipt) return true;
                //レシート印刷なのに、ぷりんたがPRP-250じゃない場合
                try
                {
                    SetDefaultPrinter("PRP-250");
                    if (check_default_printer()) return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
         * */

        /// <summary>
        /// 「通常使うプリンタ」に設定する
        /// </summary>
        /// <param name="printerName">プリンタ名</param>
        public static void SetDefaultPrinter(string printerName)
        {
            //WshNetworkオブジェクトを作成する
            Type t = Type.GetTypeFromProgID("WScript.Network");
            object wshNetwork = Activator.CreateInstance(t);
            //SetDefaultPrinterメソッドを呼び出す
            t.InvokeMember("SetDefaultPrinter",
                System.Reflection.BindingFlags.InvokeMethod,
                null, wshNetwork, new object[] { printerName });
        }

        public static void print_temple(string _barcode, string item_name, PrintPageEventArgs e)
        {
            //TODO Apache fop とか使えたらいいかも・・・

            BarcodeWriter bw = new BarcodeWriter();
            bw.Format = BarcodeFormat.EAN_13;
            Bitmap barcode = bw.Write(_barcode);

            int print_row_num = 4;
            int print_col_num = 3;

            int margin_weight = 70;
            int margin_height = 50;
            int barcode_margin_weight = 5;
            int barcode_margin_height = 100;

            Graphics g = e.Graphics;
            g.PageUnit = GraphicsUnit.Millimeter;
            for (int j = 0; j < print_row_num; j++)
            {
                for (int i = 0; i < print_col_num; i++)
                {
                    g.DrawImage(
                        barcode,
                        barcode_margin_weight + i * margin_weight,
                        barcode_margin_height + j * margin_height
                        );
                    g.DrawString(
                        item_name,
                        new Font("MS UI Gothic", 60),
                        Brushes.Black,
                        new PointF(20, 40)
                        );
                }
            }
            e.HasMorePages = false;
        }

        private static void drawString(Graphics g, Font f, string s, int x, int y)
        {
            g.DrawString(s, f, Brushes.Black, new PointF(x, y));
        }

        public static void print_receipt(ListView _item_list, string _deposit, PrintPageEventArgs e, string ACCOUNT_CODE)
        {
            int margin_min = 3;
            int margin_max = 70;
            int align_center = 27;
            int line_height = 7;

            int draw_height_position = 0;

            Graphics g = e.Graphics;
            g.PageUnit = GraphicsUnit.Millimeter;
            Font f = new Font("MS UI Gothic", 10);
            Font f_big = new Font("MS UI Gothic", 13);

            g.DrawImage(Image.FromFile(@"Kids.jpg"), 3, 3, 67, 20);

            draw_height_position += line_height + 22;

            drawString(g, f_big, "<レシート>", align_center, draw_height_position);

            draw_height_position += line_height + 3;

            DateTime dt = DateTime.Now;
            drawString(g, f, dt.ToString("yyyy年MM月dd日 HH時mm分ss秒"),
                margin_min,
                draw_height_position);

            draw_height_position += line_height;

            g.DrawLine(new Pen(Brushes.Black),
                new Point(margin_min, draw_height_position),
                new Point(margin_max, draw_height_position));

            draw_height_position += line_height;

            for (int i = 0; i < _item_list.Items.Count; i++)
            {
                ListViewItem lvi = _item_list.Items[i];
                drawString(g, f_big, lvi.SubItems[0].Text + "  " + lvi.SubItems[1].Text, margin_min,draw_height_position);
                drawString(g, f_big, "\t\t\\" + lvi.SubItems[3].Text, margin_min + 15, draw_height_position);
                draw_height_position += line_height;
            }
            g.DrawLine(new Pen(Brushes.Black),
                new Point(margin_min, draw_height_position),
                new Point(margin_max, draw_height_position));
            draw_height_position += line_height;
            
            int sum = 0;
            foreach (ListViewItem v in _item_list.Items)
                sum += int.Parse(v.SubItems[3].Text);

            drawString(g, f_big, "ごうけい", margin_min, draw_height_position);
            drawString(g, f_big, "\t\t\\" + sum.ToString(), margin_min + 15, draw_height_position);
            draw_height_position += line_height;

            drawString(g, f_big, "おあずかり", margin_min, draw_height_position);
            drawString(g, f_big, "\t\t\\" + _deposit, margin_min + 15, draw_height_position);
            draw_height_position += line_height;

            drawString(g, f_big, "おつり", margin_min, draw_height_position);
            drawString(g, f_big, "\t\t\\" + (int.Parse(_deposit) - sum).ToString(), margin_min + 15, draw_height_position);
            draw_height_position += line_height;
            draw_height_position += line_height;

            drawString(g, f_big, "おみせ：　" + Form1.store_name, margin_min, draw_height_position);
            draw_height_position += line_height;

            drawString(g, f_big, "れじのたんとう：　" + Form1.shop_person, margin_min, draw_height_position);
            draw_height_position += line_height + 5;


            drawString(g, f_big, 
                "印字保護のためこちらの面を"+Environment.NewLine+
                "内側に折って保管してください", margin_min + 3, draw_height_position);
            draw_height_position += line_height;
            draw_height_position += line_height;
            
            BarcodeWriter bw = new BarcodeWriter();
            bw.Format = BarcodeFormat.EAN_13;
            Bitmap barcode = bw.Write(ACCOUNT_CODE);

            g.DrawImage(barcode, new Point(align_center - 5, draw_height_position));
            draw_height_position += line_height + 30;

            g.DrawLine(new Pen(Brushes.Black),
                new Point(margin_min, draw_height_position),
                new Point(margin_max, draw_height_position));

            e.HasMorePages = false;
        }

        public static void print_system_barcode(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int margin_min = 3;
            int margin_max = 70;
            int align_center = 27;
            int line_height = 7;
            int draw_height_position = 0;

            Graphics g = e.Graphics;
            g.PageUnit = GraphicsUnit.Millimeter;
            Font f = new Font("MS UI Gothic", 10);
            Font f_big = new Font("MS UI Gothic", 13);

            g.DrawImage(Image.FromFile(@"Kids.jpg"), 3, 3, 67, 20);

            draw_height_position += line_height + 22;

            drawString(g, f_big, "<システムバーコード>", align_center - 20, draw_height_position);

            draw_height_position += line_height + 3;

            g.DrawLine(new Pen(Brushes.Black),
                new Point(margin_min, draw_height_position),
                new Point(margin_max, draw_height_position));

            draw_height_position += line_height + 2;

            int margin_str_barcode = 1;
            int margin_barcode = 40;

            /* ---  バーコード生成  --- */
            /* 2つずつ書いています。もうなんか読みづらくてすみませ・・・・、、 */

            hoge("商品登録", BarCode_Prefix.ITEM_REGIST, "商品リスト", BarCode_Prefix.ITEM_LIST, g, f, margin_min, margin_barcode, line_height, margin_str_barcode, ref draw_height_position);
            
            hoge("売上リスト", BarCode_Prefix.SALE_LIST, "会計", BarCode_Prefix.ACCOUNT, g, f, margin_min, margin_barcode, line_height, margin_str_barcode, ref draw_height_position);
            
            hoge("スタッフリスト", BarCode_Prefix.STAFF_LIST, "スタッフ登録", BarCode_Prefix.STAFF_REGIST, g, f, margin_min, margin_barcode, line_height, margin_str_barcode, ref draw_height_position);

            hoge("ツールバー表示", BarCode_Prefix.SHOW_TOOLBAR, "ツールバー非表示", BarCode_Prefix.HIDE_TOOLBAR, g, f, margin_min, margin_barcode, line_height, margin_str_barcode, ref draw_height_position);

            hoge("ダミーアイテム", BarCode_Prefix.DUMMY_ITEM, "ダミーユーザー", BarCode_Prefix.DUMMY_USER, g, f, margin_min, margin_barcode, line_height, margin_str_barcode, ref draw_height_position);

            hoge("商品リストEdit", BarCode_Prefix.ITEM_LIST_EDIT, "意味無", BarCode_Prefix.M100, g, f, margin_min, margin_barcode, line_height, margin_str_barcode, ref draw_height_position);


            g.DrawLine(new Pen(Brushes.Black),
                new Point(margin_min, draw_height_position),
                new Point(margin_max, draw_height_position));

            e.HasMorePages = false;
        }

        public static void hoge(
            string fir, string fir_p,
            string sec, string sec_p,
            Graphics g, Font f,
            int margin_min, 
            int margin_barcode,
            int line_height,
            int margin_str_barcode,
            ref int height)
        {
            drawString(g, f, fir, margin_min, height);
            drawString(g, f, sec, margin_min + margin_barcode, height);
            height += line_height + margin_str_barcode;
            create(g, fir_p, margin_min, height);
            create(g, sec_p, margin_min + margin_barcode, height);
            height += line_height + 25;
        }

        public static void create(Graphics g, string _barcode_prefix, int weight, int height)
        {
            Barcode bc = new Barcode(_barcode_prefix, "000", "00000");
            BarcodeWriter bw = new BarcodeWriter();
            bw.Format = BarcodeFormat.EAN_13;
            Bitmap barcode = bw.Write(bc.show());
            g.DrawImage(barcode, new Point(weight, height));
        }
        public static void print_user(string _barcode, string _name, PrintPageEventArgs e)
        {
            int margin_min = 3;
            int margin_max = 70;
            int align_center = 27;
            int line_height = 7;
            int draw_height_position = 0;
            Font f = new Font("MS UI Gothic", 10);
            Font f_big = new Font("MS UI Gothic", 13);
            Graphics g = e.Graphics;
            g.PageUnit = GraphicsUnit.Millimeter;
            BarcodeWriter bw = new BarcodeWriter();
            bw.Format = BarcodeFormat.EAN_13;
            Bitmap barcode = bw.Write(_barcode);


            g.DrawImage(Image.FromFile(@"Kids.jpg"), 3, 3, 67, 20);

            draw_height_position += line_height + 22;

            drawString(g, f_big, "< " + _name + " さん >", align_center - 20, draw_height_position);

            draw_height_position += line_height + 3;

            g.DrawLine(new Pen(Brushes.Black),
                new Point(margin_min, draw_height_position),
                new Point(margin_max, draw_height_position));

            draw_height_position += line_height + 2;

            drawString(g, f, "担当するお店 : " + Form1.store_name, margin_min, draw_height_position);

            draw_height_position += line_height;

            g.DrawImage(barcode, new Point(align_center, draw_height_position));
            

            g.DrawLine(new Pen(Brushes.Black),
                new Point(margin_min, draw_height_position + 30 ),
                new Point(margin_max, draw_height_position + 30)
                );

            e.HasMorePages = false;
        }
    }
    class System_log{
        public static void ShowDialog(string msg)
        {
            MessageBox.Show(msg, "例外発生", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
    }

    public class Connect
    {

        //別スレッドからログを書き込むデリゲート
        public delegate void dlgExeFuntion();

        int port_num = 10800;
        string ip_address;
        Form1 fom;
        public Connect(string _ip_address, Form1 _fom)
        {
            this.ip_address = _ip_address;
            this.fom = _fom;
        }
        Encoding ecUni = Encoding.GetEncoding("utf-16");
        Encoding ecSjis = Encoding.GetEncoding("shift-jis");

        //TcpClient server = null;

        TcpClient client = null;
        Thread threadClient = null;

        delegate void dlgMydelegate();

        //ソケット通信開始
        public bool StartSock()
        {
            bool openflg = false;
            openflg = ClientStart();
            
            return openflg;
        }

        //***********************************************************
        //セカンドスレッドの作成とクライアントのスタート
        //***********************************************************    
        public bool ClientStart()
        {
            try
            {
                client = new TcpClient(ip_address, port_num);
                threadClient = new Thread(new ThreadStart(this.ClientListen));
                threadClient.Start();

                return (true);
            }
            catch
            {
                return (false);
            }
        }
        //***********************************************************
        //別スレッドで実行されるクライアント側の処理
        //ここの処理はServerと同じなのでそちらを参照のこと
        //***********************************************************

        string barcode = "";
        string name = "";
        public void ClientListen()
        {
            NetworkStream stream = client.GetStream();
            Byte[] bytes = new Byte[100];
            while (true)
            {
                try
                {
                    int intCount = stream.Read(bytes, 0, bytes.Length);
                    if (intCount>0)
                    {
                        Byte[] getByte = new byte[intCount];
                        for (int i = 0; i < intCount; i++)
                            getByte[i] = bytes[i];

                        byte[] uniBytes;
                        uniBytes = Encoding.Convert(ecSjis, ecUni, bytes);
                        string strGetText = ecUni.GetString(uniBytes);
                        strGetText = strGetText.Substring(0, strGetText.IndexOf((char)0));

                        string[] rec = strGetText.Split(',');
                        if (rec[0] == "receive")
                        {
                            if (rec[1] == "barcode")
                            {
                                //MessageBox.Show("バーコードが発行されました"+Environment.NewLine+rec[2]);
                                atsumi_pos.Insert(new atsumi_pos.StaffTable(rec[2],rec[3]));
                                this.barcode = rec[2];
                                this.name = rec[3];
                                this.print();
                            }
                        }
                    }
                    else
                    {
                        stream.Close();
                        stream = null;
                        Thread.Sleep(20);//これを入れないとNullReferenceExceptionが起きる

                        MessageBox.Show("サーバーから切断されました");
                        StopSock();
                        fom.change();
                    }
                }
                catch
                {
                    return;
                }
            }
        }

        public void print()
        {
            System.Drawing.Printing.PrintDocument pd =
                new System.Drawing.Printing.PrintDocument();
            pd.PrintPage +=
                new System.Drawing.Printing.PrintPageEventHandler(printDocument1_PrintPage);
            PrintDialog pdlg = new PrintDialog();
            pdlg.Document = pd;
            if (pdlg.ShowDialog() == DialogResult.OK)
            {
                pd.Print();
            }
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            print_template.print_user(this.barcode, this.name , e);
        }
        public void StopSock()
        {
            CloseClient();
        }
        //クライアントのクローズ
        public void CloseClient()
        {
            NetworkStream ns = client.GetStream();

            if (client != null && client.Connected)
                ns.Close();
                client.Close();

            if (threadClient != null)
                threadClient.Abort();
        }
        //***********************************************************
        //別スレッドから抜けて、メインスレッドからStop　Startを実行
        //***********************************************************
        //文字データーの送信
        public void SendStringData(string _send_text)
        {
            Byte[] data = ecSjis.GetBytes(_send_text);
            NetworkStream stream = null;
            try
            {
                stream = client.GetStream();

                stream.Write(data, 0, data.Length);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString()+Environment.NewLine+"送信できませんでした。", "送信エラー");
            }
        }
        //***********************************************************
        //別スレッドから抜けて、メインスレッドからStop　Startを実行
        //***********************************************************
        public void RestartServer()
        {
            StopSock();
            StartSock();
        }
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
                    throw new Exception("作られたバーコードの長さがおかしいなぁ・・" + Environment.NewLine +
                    temp);
                }
            }
        }
        public string show()
        {
            return (this.isCreated) ? this.barcode : "";
        }
    }
}
