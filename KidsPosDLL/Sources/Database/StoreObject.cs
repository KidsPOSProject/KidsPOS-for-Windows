using System.Data.SQLite;
using KidsPos.Setting;

namespace KidsPos.Object.Database
{
    public class StoreObject : RecordObject
    {
        public StoreObject(string name)　: base(DbPath.Store)
        {
            Name = name;
            GenerateInsertQuery();
        }

        public StoreObject(SQLiteDataReader reader) : base(DbPath.Store, reader)
        {
            SetData();
        }

        public string Name { get; private set; }

        public sealed override void SetData()
        {
            Id = Record.GetInt("id");
            Name = Record.GetString("name");
            GenerateInsertQuery();
        }

        public sealed override void GenerateInsertQuery()
        {
            SetQueryInsert(
                "INSERT INTO " + TableList.Store + $" (name) values('{Name}')"
            );
        }

        public string GetId()
        {
            return Id.ToString("D" + BarcodeConfig.DataMidLength);
        }
    }
}