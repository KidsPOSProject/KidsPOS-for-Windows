using System.Data.SQLite;
using KidsPos.Setting;
using PosSystem.Object.Database;

namespace KidsPos.Object.Database
{
    public class ItemGenreObject : RecordObject
    {
        public string Name { get; private set; }
        public int StoreNum { get; private set; }

        public ItemGenreObject(string name, int storeNum)
            : base(DbPath.ItemGenre)
        {
            this.Name = name;
            this.StoreNum = storeNum;
            GenerateInsertQuery();
        }

        public ItemGenreObject(SQLiteDataReader reader) : base(DbPath.ItemGenre, reader) { setData(); }
        public sealed override void setData()
        {
            id = record.getInt("id");
            this.Name = record.getString("name");
            this.StoreNum = record.getInt("store");
            GenerateInsertQuery();
        }
        public sealed override void GenerateInsertQuery()
        {
            setQueryInsert(
                "INSERT INTO " + TableList.ItemGenre + $" (name,store) values('{this.Name}','{this.StoreNum}')"
            );
        }
    }
}
