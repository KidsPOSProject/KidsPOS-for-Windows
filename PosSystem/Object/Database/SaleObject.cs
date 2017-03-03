using System.Data.SQLite;
using KidsPos.Setting;
using KidsPos.Util;

namespace KidsPos.Object.Database
{
    public class SaleObject : RecordObject
    {
        public string Barcode { get; private set; }
        public string CreatedAt { get; private set; }
        public int Points { get; private set; }
        public int Price { get; private set; }

        // カンマ区切りのアイテムID  1, 42, 124, とか
        public string Items { get; private set; }
        public int StoreId {get; private set;}
        public string StaffId {get; private set;}
        public SaleObject(int points, int price, string items, int storeId, string staffId) : base(DbPath.Sale)
        {
            CreatedAt = new Time().GetTime();
            Points = points;
            Price = price;
            Items = items;
            StoreId = storeId;
            Barcode = GenBarcode();
            StaffId = staffId;
            GenerateInsertQuery();
        }
        public SaleObject(SQLiteDataReader reader) : base(DbPath.Sale, reader) { SetData(); }
        public sealed override void SetData()
        {
            Id = Record.GetInt("id");
            Barcode = Record.GetString("barcode");
            CreatedAt = Record.GetString("created_at");
            Points = Record.GetInt("points");
            Price = Record.GetInt("price");
            Items = Record.GetString("items");
            StoreId = Record.GetInt("store");
            StaffId = Record.GetString("staff");
            GenerateInsertQuery();
        }
        private string GenBarcode()
        {
            return 
                BarcodeConfig.Prefix + 
                BarcodeConfig.Sale + 
                StoreId.ToString("D" + BarcodeConfig.DataMidLength) +
                new Util.Database().Count<SaleObject>(
                    $"where store = '{StoreId}'").ToString("D" + BarcodeConfig.DataLength);
        }
        public sealed override void GenerateInsertQuery()
        {
            SetQueryInsert(
                "INSERT INTO " + TableList.Sale +
                $" (barcode,created_at,points,price,items,store,staff) VALUES('{Barcode}','{CreatedAt}','{Points}','{Price}','{Items}','{StoreId}','{StaffId}')"
            );
        }
    }
}
