using System.Collections;
using KidsPos.Sources.Database;

namespace KidsPos.Sources
{
    public class Config
    {
        public static bool IsPrintEnable = false;
        public static bool IsClient = true;
        private static readonly Config Instance = new Config();
        public Hashtable TargetIp = new Hashtable();

        private Config()
        {
        }

        public int StoreNumber { get; set; }
        public int TargetPort { get; set; }
        public StoreObject Store { get; set; }

        public static Config GetInstance()
        {
            return Instance;
        }

        public void Init(int storeNumber, int targetPort, Hashtable hostList)
        {
            TargetPort = targetPort;
            StoreNumber = storeNumber;
            TargetIp = hostList;
            if (IsClient)
            {
                Store = new StoreObject("練習用クライアント");
                StoreNumber = 0;
            }
            else
            {
                Store = new Util.Database().SelectSingle<StoreObject>($"WHERE id = '{storeNumber}'");
            }
        }
    }
}