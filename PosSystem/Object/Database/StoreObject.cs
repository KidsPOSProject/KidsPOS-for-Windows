using PosSystem.Setting;
using System.Data.SQLite;

namespace PosSystem.Object.Database
{
    public class StoreObject : RecordObject
    {
        public string name { get; private set; }

        public StoreObject(string name)
            : base(DBPath.STORE)
        {
            this.name = name;
            genQuery();
        }
        public StoreObject(SQLiteDataReader reader) : base(DBPath.STORE, reader) { setData(); }
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
}
