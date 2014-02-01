using System;
using System.Windows.Forms;
using System.Collections;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Printing;
using ZXing;
using Microsoft.VisualBasic.FileIO;

namespace PosSystem
{


    class BarCode_Prefix
    {
        public static int NUM = 4;
        public static string ITEM = "4903";
        public static string STAFF = "4904";
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

            public ItemTable(string _barcode, string _name, string _price, string _shop)
            {
                barcode = _barcode;
                name = _name;
                price = _price;
                shop = _shop;
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

        /// <summary>
        /// table に含まれるレコードの数を表示
        /// </summary>
        /// <param name="table">テーブル名</param>
        /// <returns>個数  失敗時 -1</returns>
        public static int read_count_num(string db_file_path ,string table)
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
            int c_num = read_count_num(db_file_path,"item_list");
            string[,] ret = new string[c_num,4];
            
            using (var conn = new SQLiteConnection("Data Source=" + Form1.db_file))
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
            int c_num = read_count_num(db_file_path,"sales_list");
            string[,] ret = new string[c_num, 3];

            using (var conn = new SQLiteConnection("Data Source=" + Form1.db_file))
            {
                conn.Open();
                using (SQLiteCommand command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT registdated_at,price,points FROM sales_list";

                    var reader = command.ExecuteReader();

                    int count = 0;
                    while (reader.Read())
                    {
                        ret[count, 0] = reader.GetString(0);
                        ret[count, 1] = reader.GetInt32(1).ToString();
                        ret[count, 2] = reader.GetInt32(2).ToString();
                        count++;
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
                using (var conn = new SQLiteConnection("Data Source=" + Form1.db_file))
                {
                    conn.Open();
                    using (SQLiteTransaction sqlt = conn.BeginTransaction())
                    {
                        using (SQLiteCommand command = conn.CreateCommand())
                        {
                            string query = "insert into item_list (barcode, name, price, shop) values('" + it.barcode + "', '" + it.name + "', '" + it.price + "', '" + it.shop + "')";
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
            try
            {
                using (var conn = new SQLiteConnection("Data Source=" + Form1.db_file))
                {
                    conn.Open();
                    using (SQLiteTransaction sqlt = conn.BeginTransaction())
                    {
                        using (SQLiteCommand command = conn.CreateCommand())
                        {
                            string query = "insert into item_list (barcode, name) values('" + it.barcode + "', '" + it.name + "')";
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
    }
    class System_log{
        public static void ShowDialog(string msg)
        {
            MessageBox.Show(msg, "例外発生", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
    }
}
