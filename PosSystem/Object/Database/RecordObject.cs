using System.Data.SQLite;
using PosSystem.Util;

namespace PosSystem.Object.Database
{
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
}
