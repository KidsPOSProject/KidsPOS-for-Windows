using PosSystem.Setting;
using System.Data.SQLite;

namespace PosSystem.Object.Database
{
    public class StaffObject : RecordObject
    {
        public string barcode { get; private set; }
        public string name { get; private set; }

        public StaffObject(string staffID, string _name)
            : base(DBPath.STAFF)
        {
            barcode = gen(staffID);
            name = _name;
            genQuery();
        }
        public StaffObject(SQLiteDataReader reader) : base(DBPath.STAFF, reader) { setData(); }
        public override void setData()
        {
            this.barcode = record.getString("barcode");
            this.name = record.getString("name");
            genQuery();
        }
        private string gen(string sID)
        {
            return
                Barcode.PREFIX + //10
                Barcode.USER +   //00
                PosInformation.year +
                sID; //0001
        }
        public override void genQuery()
        {
            setQueryInsert(
                string.Format("INSERT INTO " + TableList.STAFF + " (barcode,name) VALUES('{0}', '{1}')",
                    this.barcode, this.name)
            );
        }
    }
}
