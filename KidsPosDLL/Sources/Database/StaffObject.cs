using System.Data.SQLite;
using KidsPos.Setting;

namespace KidsPos.Object.Database
{
    public class StaffObject : RecordObject
    {
        public StaffObject(int staffId, string name) : base(DbPath.Staff)
        {
            Barcode = CreateStaffBarcode(staffId);
            Name = name;
            GenerateInsertQuery();
        }

        public StaffObject(string staffId, string name) : base(DbPath.Staff)
        {
            Barcode = CreateStaffBarcode(int.Parse(staffId));
            Name = name;
            GenerateInsertQuery();
        }

        public StaffObject(SQLiteDataReader reader) : base(DbPath.Staff, reader)
        {
            SetData();
        }

        public string Barcode { get; private set; }
        public string Name { get; private set; }

        public sealed override void SetData()
        {
            Barcode = Record.GetString("barcode");
            Name = Record.GetString("name");
            GenerateInsertQuery();
        }

        private static string CreateStaffBarcode(int staffId)
        {
            return
                $"{BarcodeConfig.Prefix}{BarcodeConfig.Staff}{PosInformation.GetInstance().Year}{staffId:D4}";
        }

        public sealed override void GenerateInsertQuery()
        {
            SetQueryInsert(
                "INSERT INTO " + TableList.Staff + $" (barcode,name) VALUES('{Barcode}', '{Name}')"
            );
        }
    }
}