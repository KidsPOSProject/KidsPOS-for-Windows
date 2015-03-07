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
        public static bool isPrintEnable = false;
        public static bool isClient = true;
        static Config instance = new Config();
        private Config()
        {

        }

        public static Config getInstance()
        {
            return instance;
        }
        public int storeNumber { get; set; }
        public Hashtable targetIP = new Hashtable();
        public int targetPort { get; set; }
        public StoreObject store { get; set; }
        public void init(int storeNumber, int targetPort, Hashtable hostList)
        {
            this.targetPort = targetPort;
            this.storeNumber = storeNumber;
            this.targetIP = hostList;
            if (isClient)
            {
                this.store = new StoreObject("練習用クライアント");
                this.storeNumber = 0;
            }
            else
            {
                this.store = new PosSystem.Util.Database().selectSingle<StoreObject>(string.Format("WHERE id = '{0}'", storeNumber));
            }
        }
    }
}
