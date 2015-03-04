using System.Data.SQLite;
using PosSystem.Setting;

namespace PosSystem.Object.Database
{
    public class SaleObject : RecordObject
    {
        public string buycode { get; private set; }
        public string createdAt { get; private set; }
        public string points { get; private set; }
        public string price { get; private set; }
        public string items { get; private set; }
        public SaleObject(string buycode, string createdAt, string points, string price, string items)
            : base(DBPath.SALE)
        {
            this.buycode = buycode;
            this.createdAt = createdAt;
            this.points = points;
            this.price = points;
            this.items = items;
            genQuery();
        }
        public SaleObject(SQLiteDataReader reader) : base(DBPath.SALE, reader) { setData(); }
        public override void setData()
        {
            id = record.getInt("id");
            this.buycode = record.getString("buycode");
            this.createdAt = record.getString("created_at");
            this.points = record.getString("points");
            this.price = record.getString("price");
            this.items = record.getString("items");
            genQuery();
        }
        public override void genQuery()
        {
            setQueryInsert(
                string.Format("INSERT INTO " + TableList.SALE + " (buycode,created_at,points,price,items) VALUES('{0}','{1}','{2}','{3}','{4}')",
                    this.buycode, this.createdAt, this.points, this.price, this.items)
            );
        }
    }
}
