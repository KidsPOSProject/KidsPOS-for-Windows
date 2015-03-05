using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PosSystem.Object.Database;

namespace PosSystem.Setting
{
    public class PosInformation
    {
        public const int port = 10800;
        public string targetIP { get; private set; }
        public string year { get;private set; }
        public StoreObject store { get; set; }
        public string regUserName { get;set; }
        static PosInformation instance = new PosInformation();
        PosInformation() { }
        public static PosInformation getInstance()
        {
            return instance;
        }
        public void init(StoreObject _store, string _regUserName, string _targetIP = "")
        {
            this.store = _store;
            this.year = DateTime.Now.ToString("yyyy").Substring(2);
            this.targetIP = _targetIP;
        }
    }
}
