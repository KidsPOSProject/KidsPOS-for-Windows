using System.Data.SQLite;
using KidsPos.Sources.Util;

namespace KidsPos.Sources.Database
{
    public abstract class RecordObject
    {
        protected RecordObject(string path)
        {
            Db = path;
        }

        protected RecordObject(string path, SQLiteDataReader reader)
        {
            Db = path;
            Record = new SqLiteItem(reader);
        }

        public string Db { get; }
        public string QueryInsert { get; private set; }
        public SqLiteItem Record { get; }
        public int Id { get; set; }
        public abstract void GenerateInsertQuery();

        public void SetQueryInsert(string query)
        {
            QueryInsert = query;
        }

        public abstract void SetData();
    }
}