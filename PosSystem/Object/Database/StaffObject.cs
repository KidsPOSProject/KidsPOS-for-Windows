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
            if (_staffID.Length != BarcodeConfig.DATA_LENGTH) throw new InvalidDataException();
            barcode = gen(staffID.ToString());
            name = _name;
            genQuery();
        }
        public StaffObject(string staffID, string name)
            : base(DBPath.STAFF)
        {
            this.barcode = BarcodeConfig.DATA_LENGTH >= staffID.Length?gen(staffID):staffID;
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
                BarcodeConfig.PREFIX + //10
                BarcodeConfig.STAFF +   //00
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
