using System;
using System.Data.SQLite;
using KidsPos.Setting;
using PosSystem.Object.Database;
using PosSystem.Setting;

namespace KidsPos.Object.Database
{
    public class StaffObject : RecordObject
    {
        public string Barcode { get; private set; }
        public string Name { get; private set; }

        public StaffObject(int staffId, string name) : base(DbPath.Staff)
        {
            Barcode = CreateStaffBarcode(staffId);
            this.Name = name;
            GenerateInsertQuery();
        }

        public StaffObject(string staffId, string name) : base(DbPath.Staff)
        {
            Barcode = CreateStaffBarcode(Int32.Parse(staffId));
            this.Name = name;
            GenerateInsertQuery();
        }

        public StaffObject(SQLiteDataReader reader) : base(DbPath.Staff, reader)
        {
            setData();
        }

        public sealed override void setData()
        {
            this.Barcode = record.getString("barcode");
            this.Name = record.getString("name");
            GenerateInsertQuery();
        }

        private static string CreateStaffBarcode(int staffId)
        {
            return
                $"{BarcodeConfig.PREFIX}{BarcodeConfig.STAFF}{PosInformation.GetInstance().Year}{staffId:D4}";
        }

        public sealed override void GenerateInsertQuery()
        {
            setQueryInsert(
                "INSERT INTO " + TableList.Staff + $" (barcode,name) VALUES('{this.Barcode}', '{this.Name}')"
            );
        }
    }
}
