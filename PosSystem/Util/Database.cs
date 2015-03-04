using System;
using System.Data.SQLite;
using PosSystem.Setting;
using System.Collections.Generic;
using PosSystem.Object.Database;
using System.Data;

namespace PosSystem.Util
{
    public class Database
    {
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
        public List<ItemObject> getItem(string barcode)
        {
            return selectMulti<ItemObject>("");
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

        public StoreObject find_store(int id)
        {
            StoreObject ret = selectSingle<StoreObject>(string.Format("WHERE id = '{0}'", id));
            return ret == null ? null : ret;
        }
        public int find_store(string storeName)
        {
            StoreObject ret = selectSingle<StoreObject>(string.Format("WHERE name = '{0}'", storeName));
            return ret == null ? -1 : ret.id;
        }
        public int find_ganre(string genre, int storeNum)
        {
            ItemGenreObject ret = selectSingle<ItemGenreObject>(string.Format("WHERE name = '{0}' AND store = '{1}'", genre, storeNum));
            return ret == null ? -1 : ret.id;
        }

        public int find_item(string itemName, int price, int storeNum)
        {
            ItemObject ret = selectSingle<ItemObject>(string.Format("WHERE name = '{0}' AND price = '{1}' AND shop = '{2}'", itemName, price, storeNum));
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
        // Insertview
        public void insertView<T>(DataTable dt, string query = "") where T : RecordObject
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=" + DBPath.getPath<T>()))
            {
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(
                    (query.Equals("")) ? "SELECT * FROM " + TableList.getTableName<T>() : query, con))
                {
                    adapter.Fill(dt);
                }
                con.Dispose();
            }
        }
    }

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

    public class DBQueryCreate
    {
        public const string ITEM = "CREATE TABLE " + TableList.ITEM + "(id INTEGER  PRIMARY KEY AUTOINCREMENT, barcode INTEGER UNIQUE, name TEXT, price INTEGER, shop INT, genre TEXT)";
        public const string ITEM_GENRE = "CREATE TABLE " + TableList.ITEM_GENRE + "(id INTEGER  PRIMARY KEY AUTOINCREMENT, name TEXT, store TEXT)";
        public const string SALE = "CREATE TABLE " + TableList.SALE + "(id INTEGER  PRIMARY KEY AUTOINCREMENT, buycode TEXT UNIQUE, created_at TEXT, points INTEGER, price INTEGER, items TEXT)";
        public const string STORE = "CREATE TABLE " + TableList.STORE + "(id INTEGER  PRIMARY KEY AUTOINCREMENT, name TEXT)";
        public const string STAFF = "CREATE TABLE " + TableList.STAFF + "(barcode INTEGER PRIMARY KEY, name TEXT)";
    }
}
