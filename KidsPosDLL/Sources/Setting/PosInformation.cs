using System;
using System.Windows.Forms;
using KidsPos.Object.Database;

namespace KidsPos.Setting
{
    public class PosInformation
    {
        public string Year { get;private set; }
        private StaffObject CurrentStaff { get;set; }
        private static readonly PosInformation Instance = new PosInformation();
        private PosInformation() { }
        public static PosInformation GetInstance()
        {
            return Instance;
        }
        public void Init(Form context)
        {
            Year = DateTime.Now.ToString("yyyy").Substring(2);
        }
        public void SetStaff(StaffObject staffObject)
        {
            CurrentStaff = staffObject;
        }
        public string GetStaffName()
        {
            return CurrentStaff == null ? "" : CurrentStaff.Name;
        }
        public string GetStaffBarcode()
        {
            return CurrentStaff == null ? "" : CurrentStaff.Barcode;
        }
    }
}
