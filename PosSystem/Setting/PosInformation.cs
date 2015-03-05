using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PosSystem.Object.Database;
using System.Windows.Forms;

namespace PosSystem.Setting
{
    public class PosInformation
    {
        Form context;

        public string year { get;private set; }
        private StaffObject reg { get;set; }
        static PosInformation instance = new PosInformation();
        PosInformation() { }
        public static PosInformation getInstance()
        {
            return instance;
        }
        public void init(Form context)
        {
            this.context = context;
            this.year = DateTime.Now.ToString("yyyy").Substring(2);
        }
        public void setStaff(StaffObject obj)
        {
            this.reg = obj;
        }
        public string getStaffName()
        {
            return this.reg == null ? "" : this.reg.name;
        }
        public string getStaffBarcode()
        {
            return this.reg == null ? "" : this.reg.barcode;
        }
    }
}
