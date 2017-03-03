using System;
using System.Data.SQLite;
using PosSystem.Setting;
using System.Collections.Generic;
using PosSystem.Object.Database;
using System.Data;
using KidsPos.Object.Database;
using KidsPos.Setting;

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
            queryImpl(DbPath.Item, DBQueryCreate.ITEM);
            queryImpl(DbPath.ItemGenre, DBQueryCreate.ITEM_GENRE);
            queryImpl(DbPath.Sale, DBQueryCreate.SALE);
            queryImpl(DbPath.Store, DBQueryCreate.STORE);
            queryImpl(DbPath.Staff, DBQueryCreate.STAFF);
        }
        public List<ItemObject> getItem(string barcode)
        {
            return selectMulti<ItemObject>("");
        }

        public T selectSingle<T>(string where = "") where T : RecordObject
        {
            return querySelectImplSingle<T>(
                DbPath.GetPath<T>(),
                "SELECT * FROM " + TableList.GetTableName<T>() + " " + where);
        }
        public List<T> selectMulti<T>(string where = "") where T : RecordObject
        {
            return querySelectImpl<T>(
                DbPath.GetPath<T>(),
                "SELECT * FROM " + TableList.GetTableName<T>() + " " + where);
        }

        public int count<T>(string where = "")
        {
            int ret = 0;
            using (var conn = new SQLiteConnection("Data Source=" + DbPath.GetPath<T>()))
            {
                conn.Open();
                using (SQLiteCommand command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT COUNT(*) FROM " + TableList.GetTableName<T>() + " " + where;
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
        public void updateItem(ItemObject item)
        {
            queryImpl(item.db, "UPDATE " + TableList.Item + string.Format(" SET name = '{0}' , price = '{1}' WHERE id = '{2}'", item.id));
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
        public bool updateItem(int id, string name, int price)
        {
            return queryImpl(DbPath.Item,
                string.Format("UPDATE " + TableList.Item + " SET name = '{0}', price = '{1}' WHERE id = '{2}'",
                    name, price, id));
        }
        // Insertview
        public void insertView<T>(DataTable dt, string query = "") where T : RecordObject
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=" + DbPath.GetPath<T>()))
            {
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(
                    (query.Equals("")) ? "SELECT * FROM " + TableList.GetTableName<T>() : query, con))
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
            if (reader == null || reader[item] == null || reader[item].Equals("")) return "";
            return reader[item].ToString();
        }
        public int getInt(string item)
        {
            if (reader == null || reader[item] == null || reader[item].Equals("")) return 0;
            string st = reader[item].ToString();
            return Convert.ToInt32(st);
        }
    }

    public class DBQueryCreate
    {
        public const string ITEM = "CREATE TABLE " + TableList.Item + "(id INTEGER  PRIMARY KEY AUTOINCREMENT, barcode INTEGER UNIQUE, name TEXT, price INTEGER, shop INT, genre TEXT)";
        public const string ITEM_GENRE = "CREATE TABLE " + TableList.ItemGenre + "(id INTEGER  PRIMARY KEY AUTOINCREMENT, name TEXT, store TEXT)";
        public const string SALE = "CREATE TABLE " + TableList.Sale + "(id INTEGER  PRIMARY KEY AUTOINCREMENT, barcode TEXT UNIQUE, created_at TEXT, points INTEGER, price INTEGER, items TEXT, store INTEGER, staff INTEGER)";
        public const string STORE = "CREATE TABLE " + TableList.Store + "(id INTEGER  PRIMARY KEY AUTOINCREMENT, name TEXT)";
        public const string STAFF = "CREATE TABLE " + TableList.Staff + "(barcode INTEGER PRIMARY KEY, name TEXT)";
    }
}
