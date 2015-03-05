using PosSystem.Setting;
using System.Data.SQLite;
using System.IO;

namespace PosSystem.Object.Database
{
    public class StaffObject : RecordObject
    {
        public string barcode { get; private set; }
        public string name { get; private set; }

        public StaffObject(int staffID, string _name)
            : base(DBPath.STAFF)
        {
            string _staffID = staffID.ToString();
            if (_staffID.Length != Barcode.DATA_LENGTH) throw new InvalidDataException();
            barcode = gen(staffID.ToString());
            name = _name;
            genQuery();
        }
        public StaffObject(string staffID, string name)
            : base(DBPath.STAFF)
        {
            this.barcode = gen(staffID);
            this.name = name;
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
                Barcode.STAFF +   //00
                PosInformation.getInstance().year +
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
