using System;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Printing;
using ZXing;

namespace PosSystem
{
    class atsumi_pos
    {
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
}
