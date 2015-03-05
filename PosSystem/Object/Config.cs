using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using PosSystem.Object.Database;

namespace PosSystem.Object
{
    public class Config
    {
        static Config instance = new Config();
        Config() { }
        public static Config getInstance()
        {
            return instance;
        }
        public int storeNumber { get; set; }
        public Hashtable  targetIP = new Hashtable();
        public int targetPort { get; set; }
        public StoreObject store { get; set; }
        public void init(int storeNumber,int targetPort, Hashtable hostList)
        {
            this.targetPort = targetPort;
            this.storeNumber = storeNumber;
            this.targetIP = hostList;
            StoreObject s = new PosSystem.Util.Database().selectSingle<StoreObject>(string.Format("WHERE id = '{0}'", storeNumber));
            if(s == null) store = new StoreObject("デパート");
        }
    }
}
