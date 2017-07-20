using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using KidsPos.Object.Database;
using KidsPos.Setting;

namespace KidsPos.Util
{
    public class Database
    {
        // method
        public bool Insert<T>(T obj) where T : RecordObject
        {
            return QueryImpl(obj.Db, obj.QueryInsert);
        }
        public bool Insert<T>(List<T> obj) where T : RecordObject
        {
            return InsertImpl(obj);
        }
        public void CreateTable()
        {
            QueryImpl(DbPath.Item, DbQueryCreate.Item);
            QueryImpl(DbPath.ItemGenre, DbQueryCreate.ItemGenre);
            QueryImpl(DbPath.Sale, DbQueryCreate.Sale);
            QueryImpl(DbPath.Store, DbQueryCreate.Store);
            QueryImpl(DbPath.Staff, DbQueryCreate.Staff);
        }
        public List<ItemObject> GetItem(string barcode)
        {
            return SelectMulti<ItemObject>();
        }

        public T SelectSingle<T>(string where = "") where T : RecordObject
        {
            return QuerySelectImplSingle<T>(
                DbPath.GetPath<T>(),
                "SELECT * FROM " + TableList.GetTableName<T>() + " " + where);
        }
        public List<T> SelectMulti<T>(string where = "") where T : RecordObject
        {
            return QuerySelectImpl<T>(
                DbPath.GetPath<T>(),
                "SELECT * FROM " + TableList.GetTableName<T>() + " " + where);
        }

        public int Count<T>(string where = "")
        {
            int ret;
            using (var conn = new SQLiteConnection("Data Source=" + DbPath.GetPath<T>()))
            {
                conn.Open();
                using (var command = conn.CreateCommand())
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
            var ret = SelectSingle<StoreObject>($"WHERE id = '{id}'");
            return ret;
        }
        public int find_store(string storeName)
        {
            var ret = SelectSingle<StoreObject>($"WHERE name = '{storeName}'");
            return ret?.Id ?? -1;
        }
        public int find_ganre(string genre, int storeNum)
        {
            var ret = SelectSingle<ItemGenreObject>($"WHERE name = '{genre}' AND store = '{storeNum}'");
            return ret?.Id ?? -1;
        }

        public int find_item(string itemName, int price, int storeNum)
        {
            var ret = SelectSingle<ItemObject>(
                $"WHERE name = '{itemName}' AND price = '{price}' AND shop = '{storeNum}'");
            return ret?.Id ?? -1;
        }
        public void UpdateItem(ItemObject item)
        {
            QueryImpl(item.Db, "UPDATE " + TableList.Item +
                               $" SET name = '{item.Name}' , price = '{item.Price}' WHERE id = '{item.Id}'");
        }

        private bool QueryImpl(string db, string query)
        {
            try
            {
                using (var conn = new SQLiteConnection("Data Source=" + db))
                {
                    conn.Open();
                    using (var sqlt = conn.BeginTransaction())
                    {
                        using (var com = conn.CreateCommand())
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
        private T QuerySelectImplSingle<T>(string db, string where) where T : RecordObject
        {
            T ret = null;
            using (var conn = new SQLiteConnection("Data Source=" + db))
            {
                conn.Open();
                using (conn.BeginTransaction())
                {
                    using (var com = conn.CreateCommand())
                    {
                        com.CommandText = where;
                        var reader = com.ExecuteReader();
                        while (reader.Read())
                        {
                            ret = (T)Activator.CreateInstance(typeof(T), reader);
                        }
                        com.Dispose();
                    }
                }
                conn.Close();
            }
            return ret;
        }
        private static List<T> QuerySelectImpl<T>(string db, string where) where T : RecordObject
        {
            var ret = new List<T>();
            using (var conn = new SQLiteConnection("Data Source=" + db))
            {
                conn.Open();
                using (conn.BeginTransaction())
                {
                    using (var com = conn.CreateCommand())
                    {
                        com.CommandText = where;
                        var reader = com.ExecuteReader();
                        while (reader.Read())
                        {
                            ret.Add((T)Activator.CreateInstance(typeof(T), reader));
                        }
                        com.Dispose();
                    }
                }
                conn.Close();
            }
            return ret;
        }
        private static bool InsertImpl<T>(IList<T> tbl) where T : RecordObject
        {
            if (1 > tbl.Count) return true;
            try
            {
                using (var conn = new SQLiteConnection("Data Source=" + tbl[0].Db))
                {
                    conn.Open();
                    using (var sqlt = conn.BeginTransaction())
                    {
                        using (var com = conn.CreateCommand())
                        {
                            foreach (var obj in tbl)
                            {
                                com.CommandText = obj.QueryInsert;
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
        public bool UpdateItem(int id, string name, int price)
        {
            return QueryImpl(DbPath.Item,
                "UPDATE " + TableList.Item + $" SET name = '{name}', price = '{price}' WHERE id = '{id}'");
        }
        // Insertview
        public void InsertView<T>(DataTable dt, string query = "") where T : RecordObject
        {
            using (var con = new SQLiteConnection("Data Source=" + DbPath.GetPath<T>()))
            {
                using (var adapter = new SQLiteDataAdapter(
                    (query.Equals("")) ? "SELECT * FROM " + TableList.GetTableName<T>() : query, con))
                {
                    adapter.Fill(dt);
                }
                con.Dispose();
            }
        }
    }

    public class SqLiteItem
    {
        private SQLiteDataReader Reader { get; set; }
        public SqLiteItem(SQLiteDataReader reader)
        {
            Reader = reader;
        }
        public string GetString(string item)
        {
            if (Reader?[item] == null || Reader[item].Equals("")) return "";
            return Reader[item].ToString();
        }
        public int GetInt(string item)
        {
            if (Reader?[item] == null || Reader[item].Equals("")) return 0;
            var st = Reader[item].ToString();
            return Convert.ToInt32(st);
        }
    }

    public class DbQueryCreate
    {
        public const string Item = "CREATE TABLE " + TableList.Item + "(id INTEGER  PRIMARY KEY AUTOINCREMENT, barcode INTEGER UNIQUE, name TEXT, price INTEGER, shop INT, genre TEXT)";
        public const string ItemGenre = "CREATE TABLE " + TableList.ItemGenre + "(id INTEGER  PRIMARY KEY AUTOINCREMENT, name TEXT, store TEXT)";
        public const string Sale = "CREATE TABLE " + TableList.Sale + "(id INTEGER  PRIMARY KEY AUTOINCREMENT, barcode TEXT UNIQUE, created_at TEXT, points INTEGER, price INTEGER, items TEXT, store INTEGER, staff INTEGER)";
        public const string Store = "CREATE TABLE " + TableList.Store + "(id INTEGER  PRIMARY KEY AUTOINCREMENT, name TEXT)";
        public const string Staff = "CREATE TABLE " + TableList.Staff + "(barcode INTEGER PRIMARY KEY, name TEXT)";
    }
}
