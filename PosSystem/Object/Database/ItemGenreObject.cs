using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using PosSystem.Setting;

namespace PosSystem.Object.Database
{
    public class ItemGenreObject : RecordObject
    {
        public string name { get; private set; }
        public int storeNum { get; private set; }

        public ItemGenreObject(string name, int storeNum)
            : base(DBPath.ITEM_GENRE)
        {
            this.name = name;
            this.storeNum = storeNum;
            genQuery();
        }

        public ItemGenreObject(SQLiteDataReader reader) : base(DBPath.ITEM_GENRE, reader) { setData(); }
        public override void setData()
        {
            id = record.getInt("id");
            this.name = record.getString("name");
            this.storeNum = record.getInt("store");
            genQuery();
        }
        public override void genQuery()
        {
            setQueryInsert(
                string.Format("INSERT INTO " + TableList.ITEM_GENRE + " (name,store) values('{0}','{1}')",
                    this.name, this.storeNum)
            );
        }
    }
}
