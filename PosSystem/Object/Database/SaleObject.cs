using System.Data.SQLite;
using KidsPos.Setting;
using PosSystem.Object.Database;
using PosSystem.Setting;
using PosSystem.Util;

namespace KidsPos.Object.Database
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
        public string staffID {get; private set;}
        public SaleObject(int points, int price, string items, int storeID, string staffID)
            : base(DbPath.Sale)
        {
            createdAt = new Time().getTime();
            this.points = points;
            this.price = price;
            this.items = items;
            this.storeID = storeID;
            barcode = genBarcode();
            this.staffID = staffID;
            GenerateInsertQuery();
        }
        public SaleObject(SQLiteDataReader reader) : base(DbPath.Sale, reader) { setData(); }
        public override void setData()
        {
            id = record.getInt("id");
            barcode = record.getString("barcode");
            createdAt = record.getString("created_at");
            points = record.getInt("points");
            price = record.getInt("price");
            items = record.getString("items");
            storeID = record.getInt("store");
            staffID = record.getString("staff");
            GenerateInsertQuery();
        }
        private string genBarcode()
        {
            return 
                BarcodeConfig.PREFIX + 
                BarcodeConfig.SALE + 
                storeID.ToString("D" + BarcodeConfig.DATA_MID_LENGTH) +
                new PosSystem.Util.Database().count<SaleObject>(
                    $"where store = '{storeID}'").ToString("D" + BarcodeConfig.DATA_LENGTH);
        }
        public sealed override void GenerateInsertQuery()
        {
            setQueryInsert(
                "INSERT INTO " + TableList.Sale +
                $" (barcode,created_at,points,price,items,store,staff) VALUES('{barcode}','{createdAt}','{points}','{price}','{items}','{storeID}','{staffID}')"
            );
        }
    }
}
