using System.Data.SQLite;
using KidsPos.Sources.Setting;

namespace KidsPos.Sources.Database
{
    public class Store : Record
    {
        public Store(string name) : base(DbPath.Store)
        {
            Name = name;
            GenerateInsertQuery();
        }

        public Store(SQLiteDataReader reader) : base(DbPath.Store, reader)
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