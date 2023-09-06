using System.Data.SQLite;
using KidsPos.Util;

namespace KidsPos.Object.Database
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

        public string Db { get; private set; }
        public string QueryInsert { get; private set; }
        public SqLiteItem Record { get; private set; }
        public int Id { get; set; }
        public abstract void GenerateInsertQuery();

        public void SetQueryInsert(string query)
        {
            QueryInsert = query;
        }

        public abstract void SetData();
    }
}