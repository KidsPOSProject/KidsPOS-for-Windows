using PosSystem.Setting;
using System.Data.SQLite;
using KidsPos.Setting;

namespace PosSystem.Object.Database
{
    public class StoreObject : RecordObject
    {
        public string name { get; private set; }

        public StoreObject(string name)
            : base(DbPath.Store)
        {
            this.name = name;
            GenerateInsertQuery();
        }
        public StoreObject(SQLiteDataReader reader) : base(DbPath.Store, reader) { setData(); }
        public override void setData()
        {
            id = record.getInt("id");
            this.name = record.getString("name");
            GenerateInsertQuery();
        }
        public override void GenerateInsertQuery()
        {
            setQueryInsert(
                string.Format("INSERT INTO " + TableList.Store + " (name) values('{0}')",
                    this.name)
            );
        }
        public string getID()
        {
            return id.ToString("D" + BarcodeConfig.DATA_MID_LENGTH);
        }
    }
}
