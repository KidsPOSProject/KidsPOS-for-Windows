using System.Data.SQLite;
using KidsPos.Sources.Setting;

namespace KidsPos.Sources.Database
{
    public class Item : Record
    {
        public Item(string barcode, string name, int price, int storeNum, int genreNum) : base(DbPath.Item)
        {
            Barcode = barcode;
            Name = name;
            Price = price;
            StoreNum = storeNum;
            GenreNum = genreNum;
            GenerateInsertQuery();
        }

        public Item(SQLiteDataReader reader) : base(DbPath.Item, reader)
        {
            SetData();
        }

        public string Barcode { get; private set; }
        public string Name { get; private set; }
        public int Price { get; private set; }
        public int StoreNum { get; private set; }
        public int GenreNum { get; private set; }
        public string QueryUpdate { get; private set; }

        public sealed override void SetData()
        {
            Id = Record.GetInt("id");
            Barcode = Record.GetString("barcode");
            Name = Record.GetString("name");
            Price = Record.GetInt("price");
            StoreNum = Record.GetInt("shop");
            GenreNum = Record.GetInt("genre");
            GenerateInsertQuery();
        }

        public sealed override void GenerateInsertQuery()
        {
            SetQueryInsert(
                "INSERT INTO " + TableList.Item +
                $" (barcode,name,price,shop,genre) VALUES ('{Barcode}','{Name}','{Price}','{StoreNum}','{GenreNum}')"
            );
            QueryUpdate =
                $"UPDATE item_list SET name = '{Name}', price = '{Price}' WHERE id = '{Id}'";
        }
    }
}