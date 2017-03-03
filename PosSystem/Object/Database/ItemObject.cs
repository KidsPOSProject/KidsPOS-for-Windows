using System.Data.SQLite;
using KidsPos.Setting;
using PosSystem.Object.Database;

namespace KidsPos.Object.Database
{
    public class ItemObject : RecordObject
    {
        public string barcode { get; private set; }
        public string name { get; private set; }
        public int price { get; private set; }
        public int storeNum { get; private set; }
        public int genreNum { get; private set; }
        public string queryUpdate { get; private set; }

        public ItemObject(string barcode, string name, int price, int storeNum, int genreNum)
            : base(DbPath.Item)
        {
            this.barcode = barcode;
            this.name = name;
            this.price = price;
            this.storeNum = storeNum;
            this.genreNum = genreNum;
            GenerateInsertQuery();
        }
        public ItemObject(SQLiteDataReader reader) : base(DbPath.Item, reader) { setData(); }
        public override void setData()
        {
            id = record.getInt("id");
            this.barcode = record.getString("barcode");
            this.name = record.getString("name");
            this.price = record.getInt("price");
            this.storeNum = record.getInt("shop");
            this.genreNum = record.getInt("genre");
            GenerateInsertQuery();
        }

        public override void GenerateInsertQuery()
        {
            setQueryInsert(
                string.Format("INSERT INTO " + TableList.Item + " (barcode,name,price,shop,genre) VALUES ('{0}','{1}','{2}','{3}','{4}')",
                    this.barcode, this.name, this.price, this.storeNum, this.genreNum)
            );
            queryUpdate =
                string.Format("UPDATE item_list SET name = '{0}', price = '{1}' WHERE id = '{2}'",
                    this.name, this.price, this.id);
        }
    }
}
