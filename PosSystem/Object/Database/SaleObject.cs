using System.Data.SQLite;
using PosSystem.Setting;

namespace PosSystem.Object.Database
{
    public class SaleObject : RecordObject
    {
        public string barcode { get; private set; }
        public string createdAt { get; private set; }
        public int points { get; private set; }
        public int price { get; private set; }

        // カンマ区切りのアイテムID  1, 42, 124, とか
        public string items { get; private set; }
        public int storeID {get; private set;}
        public SaleObject(string createdAt, int points, int price, string items, int storeID)
            : base(DBPath.SALE)
        {
            this.createdAt = createdAt;
            this.points = points;
            this.price = points;
            this.items = items;
            this.storeID = storeID;
            this.barcode = genBarcode();
            genQuery();
        }
        public SaleObject(SQLiteDataReader reader) : base(DBPath.SALE, reader) { setData(); }
        public override void setData()
        {
            id = record.getInt("id");
            this.barcode = record.getString("barcode");
            this.createdAt = record.getString("created_at");
            this.points = record.getInt("points");
            this.price = record.getInt("price");
            this.items = record.getString("items");
            this.storeID = record.getInt("store");
            genQuery();
        }
        private string genBarcode()
        {
            return Barcode.PREFIX + Barcode.SALE + storeID.ToString("D" + Barcode.DATA_MID_LENGTH) + new Util.Database().count<SaleObject>(string.Format("where store = '{0}'", storeID)).ToString("D" + Barcode.DATA_LENGTH);
        }
        public override void genQuery()
        {
            setQueryInsert(
                string.Format("INSERT INTO " + TableList.SALE + " (barcode,created_at,points,price,items,store) VALUES('{0}','{1}','{2}','{3}','{4}','{5}')",
                    this.barcode, this.createdAt, this.points, this.price, this.items, this.storeID)
            );
        }
    }
}
