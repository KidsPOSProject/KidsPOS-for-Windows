using System.Data.SQLite;
using KidsPos.Sources.Setting;

namespace KidsPos.Sources.Database
{
    public class ItemGenre : Record
    {
        public ItemGenre(string name, int storeNum)
            : base(DbPath.ItemGenre)
        {
            Name = name;
            StoreNum = storeNum;
            GenerateInsertQuery();
        }

        public ItemGenre(SQLiteDataReader reader) : base(DbPath.ItemGenre, reader)
        {
            SetData();
        }

        public string Name { get; private set; }
        public int StoreNum { get; private set; }

        public sealed override void SetData()
        {
            Id = Record.GetInt("id");
            Name = Record.GetString("name");
            StoreNum = Record.GetInt("store");
            GenerateInsertQuery();
        }

        public sealed override void GenerateInsertQuery()
        {
            SetQueryInsert(
                "INSERT INTO " + TableList.ItemGenre + $" (name,store) values('{Name}','{StoreNum}')"
            );
        }
    }
}