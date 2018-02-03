using KidsPos.Sources.Database;

namespace KidsPos.Sources.Setting
{
    public class TableList
    {
        public const string Item = "item";
        public const string ItemGenre = "item_genre";
        public const string Sale = "sale";
        public const string Store = "store";
        public const string Staff = "staff";

        public static string GetTableName<T>()
        {
            var type = typeof(T);
            if (typeof(ItemObject) == type)
                return Item;
            if (typeof(ItemGenreObject) == type)
                return ItemGenre;
            if (typeof(SaleObject) == type)
                return Sale;
            if (typeof(StoreObject) == type)
                return Store;
            return typeof(StaffObject) == type ? Staff : "";
        }
    }

    public class DbPath
    {
        public const string Item = "item.db";
        public const string ItemGenre = "item.db";
        public const string Sale = "item.db";
        public const string Store = "item.db";
        public const string Staff = "staff.db";

        public static string GetPath<T>()
        {
            var type = typeof(T);
            if (typeof(ItemObject) == type)
                return Item;
            if (typeof(ItemGenreObject) == type)
                return ItemGenre;
            if (typeof(SaleObject) == type)
                return Sale;
            if (typeof(StoreObject) == type)
                return Store;
            return typeof(StaffObject) == type ? Staff : "";
        }
    }
}