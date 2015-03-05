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

        public const int port = 10800;
        public string targetIP { get; private set; }
        public string year { get;private set; }
        public StoreObject store { get;private set; }
        private StaffObject reg { get;set; }
        static PosInformation instance = new PosInformation();
        PosInformation() { }
        public static PosInformation getInstance()
        {
            return instance;
        }
        public void init(Form context,StoreObject _store, string _targetIP = "")
        {
            this.context = context;
            this.store = _store;
            this.year = DateTime.Now.ToString("yyyy").Substring(2);
            this.targetIP = _targetIP;
        }
        public void setStaff(StaffObject obj)
        {
            this.reg = obj;
        }
        public void setStore(StoreObject store)
        {
            this.store = store;
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
