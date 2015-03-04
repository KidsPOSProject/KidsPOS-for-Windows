using System;
using PosSystem.Object.Database;

namespace PosSystem.Setting
{
    public class TableList
    {
        public const string ITEM = "item";
        public const string ITEM_GENRE = "item_genre";
        public const string SALE = "sale";
        public const string STORE = "store";
        public const string STAFF = "staff";
        public static string getTableName<T>()
        {
            Type type = typeof(T);
            if (typeof(ItemObject) == type)
            {
                return ITEM;
            }
            else if (typeof(ItemGenreObject) == type)
            {
                return ITEM_GENRE;
            }
            else if (typeof(SaleObject) == type)
            {
                return SALE;
            }
            else if (typeof(StoreObject) == type)
            {
                return STORE;
            }
            else if (typeof(StaffObject) == type)
            {
                return STAFF;
            }
            else
            {
                return "";
            }
        }
    }
    public class DBPath
    {
        public const string ITEM = "item.db";
        public const string ITEM_GENRE = "item.db";
        public const string SALE = "item.db";
        public const string STORE = "item.db";
        public const string STAFF = "staff.db";
        public static string getPath<T>()
        {
            Type type = typeof(T);
            if (typeof(ItemObject) == type)
            {
                return ITEM;
            }
            else if (typeof(ItemGenreObject) == type)
            {
                return ITEM_GENRE;
            }
            else if (typeof(SaleObject) == type)
            {
                return SALE;
            }
            else if (typeof(StoreObject) == type)
            {
                return STORE;
            }
            else if (typeof(StaffObject) == type)
            {
                return STAFF;
            }
            else
            {
                return "";
            }
        }
    }
}
