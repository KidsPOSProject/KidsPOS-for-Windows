using System.Data.SQLite;
using PosSystem.Setting;

namespace PosSystem.Object.Database
{
    public class ItemObject : RecordObject
    {
        public string barcode { get; private set; }
        public string name { get; private set; }
        public int price { get; private set; }
        public int storeNum { get; private set; }
        public int genreNum { get; private set; }

        public ItemObject(string barcode, string name, int price, int storeNum, int genreNum)
            : base(DBPath.ITEM)
        {
            this.barcode = barcode;
            this.name = name;
            this.price = price;
            this.storeNum = storeNum;
            this.genreNum = genreNum;
            genQuery();
        }
        public ItemObject(SQLiteDataReader reader) : base(DBPath.ITEM, reader) { setData(); }
        public override void setData()
        {
            id = record.getInt("id");
            this.barcode = record.getString("barcode");
            this.name = record.getString("name");
            this.price = record.getInt("price");
            this.storeNum = record.getInt("shop");
            this.genreNum = record.getInt("genre");
            genQuery();
        }

        public override void genQuery()
        {
            setQueryInsert(
                string.Format("INSERT INTO " + TableList.ITEM + " (barcode,name,price,shop,genre) VALUES ('{0}','{1}','{2}','{3}','{4}')",
                    this.barcode, this.name, this.price, this.storeNum, this.genreNum)
            );
        }
    }
}
