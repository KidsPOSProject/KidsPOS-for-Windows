using System;
using System.Windows.Forms;
using System.Collections;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Printing;
using ZXing;
using Microsoft.VisualBasic.FileIO;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.IO;


namespace DBRegister
{
    
    class BarCode_Prefix
    {
        // 00 11 22 33 4444
        public const int BARCODE_NUM = 10;
        public const int PREFIX_LENGTH = 4;
        public const int DATA_LENGTH = 4;

        public const int DATA_MID_LENGTH = BARCODE_NUM - PREFIX_LENGTH - DATA_LENGTH;
        // prefix
        public const string PREFIX = "10";
        public const string SALE = "01";
        public const string USER = "00";
    }

    class Barcode
    {
        string store;
        string itemNum;
        string barcode;
        public Barcode(string barcode)
        {
            this.barcode = barcode;
        }

        public Barcode(int storeNum, int itemNum)
        {
            this.store = storeNum.ToString("D" + BarCode_Prefix.DATA_MID_LENGTH);
            this.itemNum = itemNum.ToString("D" + BarCode_Prefix.DATA_LENGTH);
            this.comb_barcode();
        }
        public void comb_barcode()
        {
            string barcode = BarCode_Prefix.PREFIX + BarCode_Prefix.SALE + store + itemNum;
            if (barcode.Length == BarCode_Prefix.BARCODE_NUM)
            {
                this.barcode = barcode;
            }
            else
            {
                throw new Exception("作られたバーコードの長さがおかしいなぁ・・" + Environment.NewLine + barcode);
            }
        }
        public string getBarcode()
        {
            return this.barcode;
        }
    }

    class atsumi_pos
    {
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
            int c_num = read_count_num(db_file_path, DataBase.TableList.ITEM);
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
            int c_num = read_count_num(db_file_path, DataBase.TableList.SALE);
            string[,] ret = new string[c_num, 4];

            using (var conn = new SQLiteConnection("Data Source=" + db_file_path))
            {
                conn.Open();
                using (SQLiteCommand command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT buycode, created_at,price,points FROM " + DataBase.TableList.ITEM;

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
            int c_num = read_count_num(db_file_path, DataBase.TableList.STAFF);
            string[,] ret = new string[c_num, 3];

            using (var conn = new SQLiteConnection("Data Source=" + db_file_path))
            {
                conn.Open();
                using (SQLiteCommand command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM " + DataBase.TableList.STAFF;

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
                    command.CommandText = "SELECT * FROM " + DataBase.TableList.STORE + " WHERE name ='" + _store_name + "'";

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
                    command.CommandText = "SELECT id FROM " + DataBase.TableList.ITEM_GENRE + "  WHERE name ='" + _genre_name + "'";

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

        public static bool insertImpl(string _db_file, string _query)
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
            catch (SQLiteException e)
            {
                return false;
            }
        }

        // for JAN_13 Barcode
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
            bw.Format = BarcodeFormat.CODABAR;
            Bitmap barcode = bw.Write("A" + _barcode + "A");


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
                        barcode.Width * 0.3f, barcode.Height * 0.14f
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
    class CSV
    {
    }

    class DataBase
    {
        #region prop
        // prop
        private static string year = "";

        #endregion
        #region const class
        public class TableList
        {
            public const string ITEM = "item";
            public const string ITEM_GENRE = "item_genre";
            public const string SALE = "sale";
            public const string STORE = "store";
            public const string STAFF = "staff";
            public static string getTableName<T>()
            {
                Type type = typeof(T);
                if (typeof(Item) == type)
                {
                    return ITEM;
                }
                else if (typeof(ItemGenre) == type)
                {
                    return ITEM_GENRE;
                }
                else if (typeof(Sale) == type)
                {
                    return SALE;
                }
                else if (typeof(Store) == type)
                {
                    return STORE;
                }
                else if (typeof(Staff) == type)
                {
                    return STAFF;
                }
                else
                {
                    return "";
                }
            }
        }
        public class DBPath
        {
            public const string ITEM = "item.db";
            public const string ITEM_GENRE = "item.db";
            public const string SALE = "item.db";
            public const string STORE = "item.db";
            public const string STAFF = "staff.db";
            public static string getPath<T>()
            {
                Type type = typeof(T);
                if (typeof(Item) == type)
                {
                    return ITEM;
                }
                else if (typeof(ItemGenre) == type)
                {
                    return ITEM_GENRE;
                }
                else if (typeof(Sale) == type)
                {
                    return SALE;
                }
                else if (typeof(Store) == type)
                {
                    return STORE;
                }
                else if (typeof(Staff) == type)
                {
                    return STAFF;
                }
                else
                {
                    return "";
                }
            }
        }
        private class DBQueryCreate
        {
            public const string ITEM = "CREATE TABLE " + TableList.ITEM + "(id INTEGER  PRIMARY KEY AUTOINCREMENT, barcode INTEGER UNIQUE, name TEXT, price INTEGER, shop INT, genre TEXT)";
            public const string ITEM_GENRE = "CREATE TABLE " + TableList.ITEM_GENRE + "(id INTEGER  PRIMARY KEY AUTOINCREMENT, name TEXT, store TEXT)";
            public const string SALE = "CREATE TABLE " + TableList.SALE + "(id INTEGER  PRIMARY KEY AUTOINCREMENT, buycode TEXT UNIQUE, created_at TEXT, points INTEGER, price INTEGER, items TEXT)";
            public const string STORE = "CREATE TABLE " + TableList.STORE + "(id INTEGER  PRIMARY KEY AUTOINCREMENT, name TEXT)";
            public const string STAFF = "CREATE TABLE " + TableList.STAFF + "(barcode INTEGER PRIMARY KEY, name TEXT)";
        }
        #endregion
        // db class
        #region record class
        // 親クラス
        abstract public class RecordObject
        {
            public string db { get; private set; }
            public string queryInsert { get; private set; }
            public SQLiteItem record { get; private set; }
            public int id { get; set; }
            public RecordObject(string path)
            {
                this.db = path;
            }
            public RecordObject(string path, SQLiteDataReader reader)
            {
                this.db = path;
                this.record = new SQLiteItem(reader);
            }
            abstract public void genQuery();

            public void setQueryInsert(string query)
            {
                this.queryInsert = query;
            }

            abstract public void setData();
        }
        // 商品
        public class Item : RecordObject
        {
            public string barcode { get; private set; }
            public string name { get; private set; }
            public int price { get; private set; }
            public int storeNum { get; private set; }
            public int genreNum { get; private set; }

            public Item(string barcode, string name, int price, int storeNum, int genreNum)
                : base(DBPath.ITEM)
            {
                this.barcode = barcode;
                this.name = name;
                this.price = price;
                this.storeNum = storeNum;
                this.genreNum = genreNum;
                genQuery();
            }
            public Item(SQLiteDataReader reader) : base(DBPath.ITEM, reader){setData();}
            public override void setData()
            {
                id = record.getInt("id");
                this.barcode = record.getString("barcode");
                this.name = record.getString("name");
                this.price = record.getInt("price");
                this.storeNum = record.getInt("shop");
                this.genreNum = record.getInt("genre");
                genQuery();
            }
            
            public override void genQuery()
            {
                setQueryInsert(
                    string.Format("INSERT INTO " + TableList.ITEM + " (barcode,name,price,shop,genre) VALUES ('{0}','{1}','{2}','{3}','{4}')",
                        this.barcode, this.name, this.price, this.storeNum, this.genreNum)
                );
            }
        }
        // 商品のジャンル
        public class ItemGenre : RecordObject
        {
            public string name { get; private set; }
            public int storeNum { get; private set; }

            public ItemGenre(string name, int storeNum)
                : base(DBPath.ITEM_GENRE)
            {
                this.name = name;
                this.storeNum = storeNum;
                genQuery();
            }
            
            public ItemGenre(SQLiteDataReader reader) : base(DBPath.ITEM_GENRE, reader){setData();}
            public override void setData()
            {
                id = record.getInt("id");
                this.name = record.getString("name");
                this.storeNum = record.getInt("store");
                genQuery();
            }
            public override void genQuery()
            {
                setQueryInsert(
                    string.Format("INSERT INTO " + TableList.ITEM_GENRE + " (name,store) values('{0}','{1}')",
                        this.name, this.storeNum)
                );
            }
        }
        // 売上
        public class Sale : RecordObject
        {
            public string buycode { get; private set; }
            public string createdAt { get; private set; }
            public string points { get; private set; }
            public string price { get; private set; }
            public string items { get; private set; }
            public Sale(string buycode, string createdAt, string points, string price, string items)
                : base(DBPath.SALE)
            {
                this.buycode = buycode;
                this.createdAt = createdAt;
                this.points = points;
                this.price = points;
                this.items = items;
                genQuery();
            }
            public Sale(SQLiteDataReader reader) : base(DBPath.SALE, reader){setData();}
            public override void setData()
            {
                id = record.getInt("id");
                this.buycode = record.getString("buycode");
                this.createdAt = record.getString("created_at");
                this.points = record.getString("points");
                this.price = record.getString("price");
                this.items = record.getString("items");
                genQuery();
            }
            public override void genQuery()
            {
                setQueryInsert(
                    string.Format("INSERT INTO " + TableList.SALE + " (buycode,created_at,points,price,items) VALUES('{0}','{1}','{2}','{3}','{4}')",
                        this.buycode, this.createdAt, this.points, this.price, this.items)
                );
            }
        }
        // お店の名前
        public class Store : RecordObject
        {
            public string name { get; private set; }

            public Store(string name)
                : base(DBPath.STORE)
            {
                this.name = name;
                genQuery();
            }
            public Store(SQLiteDataReader reader) : base(DBPath.STORE, reader){setData();}
            public override void setData()
            {
                id = record.getInt("id");
                this.name = record.getString("name");
                genQuery();
            }
            public override void genQuery()
            {
                setQueryInsert(
                    string.Format("INSERT INTO " + TableList.STORE + " (name) values('{0}')",
                        this.name)
                );
            }
        }
        // 従業員
        public class Staff : RecordObject
        {
            public string barcode { get; private set; }
            public string name { get; private set; }

            public Staff(string staffID, string _name)
                : base(DBPath.STAFF)
            {
                barcode = gen(staffID);
                name = _name;
                genQuery();
            }
            public Staff(SQLiteDataReader reader) : base(DBPath.STAFF, reader){setData();}
            public override void setData()
            {
                this.barcode = record.getString("barcode");
                this.name = record.getString("name");
                genQuery();
            }
            private string gen(string sID)
            {
                return
                    BarCode_Prefix.PREFIX + //10
                    BarCode_Prefix.USER +   //00
                    year +
                    sID; //0001
            }
            public override void genQuery()
            {
                setQueryInsert(
                    string.Format("INSERT INTO " + TableList.STAFF + " (barcode,name) VALUES('{0}', '{1}')",
                        this.barcode, this.name)
                );
            }
        }
        #endregion
        #region util class
        public class SQLiteItem
        {
            private SQLiteDataReader reader { get; set; }
            public SQLiteItem(SQLiteDataReader reader)
            {
                this.reader = reader;
            }
            public string getString(string item)
            {
                if (reader == null || reader[item] == null) return "";
                return reader[item].ToString();
            }
            public int getInt(string item)
            {
                if (reader == null || reader[item] == null) return 0;
                return Convert.ToInt32(reader[item].ToString());
            }
        }
        #endregion
        // instance
        static DataBase instance = new DataBase();
        private DataBase()
        {
            DateTime dt = DateTime.Now;
            year = dt.ToString("yyyy").Substring(2);
        }
        public static DataBase getInstance()
        {
            return instance;
        }
        // method
        public bool insert<T>(T obj) where T : RecordObject
        {
            return queryImpl(obj.db, obj.queryInsert);
        }
        public bool insert<T>(List<T> obj) where T : RecordObject
        {
            return insertImpl(obj);
        }
        public void createTable()
        {
            queryImpl(DBPath.ITEM, DBQueryCreate.ITEM);
            queryImpl(DBPath.ITEM_GENRE, DBQueryCreate.ITEM_GENRE);
            queryImpl(DBPath.SALE, DBQueryCreate.SALE);
            queryImpl(DBPath.STORE, DBQueryCreate.STORE);
            queryImpl(DBPath.STAFF, DBQueryCreate.STAFF);
        }
        public List<Item> getItem(string barcode)
        {
            return selectMulti<Item>("");
        }

        public T selectSingle<T>(string where = "") where T : RecordObject
        {
            return querySelectImplSingle<T>(
                DBPath.getPath<T>(), 
                "SELECT * FROM " + TableList.getTableName<T>() + " " + where);
        }
        public List<T> selectMulti<T>(string where = "") where T : RecordObject
        {
            return querySelectImpl<T>(
                DBPath.getPath<T>(), 
                "SELECT * FROM " + TableList.getTableName<T>() + " " + where);
        }

        public int count<T>(string where = "")
        {
            int ret = -1;
            using (var conn = new SQLiteConnection("Data Source=" + DBPath.getPath<T>()))
            {
                conn.Open();
                using (SQLiteCommand command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT COUNT(*) FROM " + TableList.getTableName<T>() + " " + where;
                    ret = int.Parse(command.ExecuteScalar().ToString());
                }
                conn.Close();
            }
            return ret;
        }

        public DataBase.Store find_store(int id)
        {
            Store ret = selectSingle<Store>(string.Format("WHERE id = '{0}'", id));
            return ret == null ? null : ret;
        }
        public int find_store(string storeName)
        {
            Store ret = selectSingle<Store>(string.Format("WHERE name = '{0}'", storeName));
            return ret == null ? -1 : ret.id;
        }
        public int find_ganre(string genre, int storeNum)
        {
            ItemGenre ret = selectSingle<ItemGenre>(string.Format("WHERE name = '{0}' AND store = '{1}'", genre, storeNum));
            return ret == null ? -1 : ret.id;
        }

        public int find_item(string itemName, int price, int storeNum)
        {
            Item ret = selectSingle<Item>(string.Format("WHERE name = '{0}' AND price = '{1}' AND shop = '{2}'", itemName, price, storeNum));
            return ret == null ? -1 : ret.id;
        }

        private bool queryImpl(string db, string query)
        {
            try
            {
                using (var conn = new SQLiteConnection("Data Source=" + db))
                {
                    conn.Open();
                    using (SQLiteTransaction sqlt = conn.BeginTransaction())
                    {
                        using (SQLiteCommand com = conn.CreateCommand())
                        {
                            com.CommandText = query;
                            com.ExecuteNonQuery();
                        }
                        sqlt.Commit();
                    }
                    conn.Close();
                }
            }
            catch (SQLiteException)
            {
                return false;
            }
            return true;
        }
        private T querySelectImplSingle<T>(string db, string where) where T : RecordObject
        {
            T ret = null;
            using (var conn = new SQLiteConnection("Data Source=" + db))
            {
                conn.Open();
                using (SQLiteTransaction sqlt = conn.BeginTransaction())
                {
                    using (SQLiteCommand com = conn.CreateCommand())
                    {
                        com.CommandText = where;
                        SQLiteDataReader reader = com.ExecuteReader();
                        while (reader.Read())
                        {
                            ret = (T)Activator.CreateInstance(typeof(T), new object[] { reader });
                        }
                        com.Dispose();
                    }
                }
                conn.Close();
            }
            return ret;
        }
        private List<T> querySelectImpl<T>(string db, string where) where T : RecordObject
        {
            List<T> ret = new List<T>();
            using (var conn = new SQLiteConnection("Data Source=" + db))
            {
                conn.Open();
                using (SQLiteTransaction sqlt = conn.BeginTransaction())
                {
                    using (SQLiteCommand com = conn.CreateCommand())
                    {
                        com.CommandText = where;
                        SQLiteDataReader reader = com.ExecuteReader();
                        while (reader.Read())
                        {
                            ret.Add((T)Activator.CreateInstance(typeof(T), new object[] { reader }));
                        }
                        com.Dispose();
                    }
                }
                conn.Close();
            }
            return ret;
        }
        /// <summary>
        /// 大量のファイルをインサートする時のみ使う
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tbl">List</param>
        /// <returns></returns>
        private bool insertImpl<T>(List<T> tbl) where T : RecordObject
        {
            if (1 > tbl.Count) return true;
            try
            {
                using (var conn = new SQLiteConnection("Data Source=" + tbl[0].db))
                {
                    conn.Open();
                    using (SQLiteTransaction sqlt = conn.BeginTransaction())
                    {
                        using (SQLiteCommand com = conn.CreateCommand())
                        {
                            foreach (T obj in tbl)
                            {
                                com.CommandText = obj.queryInsert;
                                com.ExecuteNonQuery();
                            }
                        }
                        sqlt.Commit();
                    }
                    conn.Close();
                }
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }        
    }
}
