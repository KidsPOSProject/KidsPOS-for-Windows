using System.Data.SQLite;
using KidsPos.Setting;

namespace KidsPos.Object.Database
{
    public class ItemGenreObject : RecordObject
    {
        public string Name { get; private set; }
        public int StoreNum { get; private set; }

        public ItemGenreObject(string name, int storeNum)
            : base(DbPath.ItemGenre)
        {
            Name = name;
            StoreNum = storeNum;
            GenerateInsertQuery();
        }

        public ItemGenreObject(SQLiteDataReader reader) : base(DbPath.ItemGenre, reader) { SetData(); }
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
