using System.Collections;
using KidsPos.Object.Database;

namespace KidsPos.Object
{
    public class Config
    {
        public static bool IsPrintEnable = false;
        public static bool IsClient = true;
        private static readonly Config Instance = new Config();
        private Config()
        {

        }

        public static Config GetInstance()
        {
            return Instance;
        }
        public int StoreNumber { get; set; }
        public Hashtable TargetIp = new Hashtable();
        public int TargetPort { get; set; }
        public StoreObject Store { get; set; }
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
