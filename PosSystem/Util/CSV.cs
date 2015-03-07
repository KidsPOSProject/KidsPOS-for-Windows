using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PosSystem.Object.Database;
using Microsoft.VisualBasic.FileIO;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using PosSystem.Object;
using PosSystem.Setting;
using System.Diagnostics;

namespace PosSystem.Util
{
    public class CSV
    {
        public const string CONFIG_PATH = "config.csv";
        public static class ConfigHead
        {
            public const string STORE_NUM = "#StoreNum";
            public const string TARGET_IP = "#TargetIP";
            public const string TARGET_PORT = "#TargetPort";
            public const string MODE = "#Mode";
            public const string PRINT_ENABLE = "#PrintReceipt";
        }

        public void loadConfig()
        {
            List<String> list = loadCSV(CONFIG_PATH);
            if (list == null || list.Count == 0)
            {
                using (StreamWriter sw = new StreamWriter(CONFIG_PATH, false, Encoding.GetEncoding("Shift_JIS")))
                {
                    sw.WriteLine("-- お店番号 DBRegister.exe のお店リスト参照");
                    sw.WriteLine(ConfigHead.STORE_NUM + ", 0");
                    sw.WriteLine("");
                    sw.WriteLine("-- サーバかクライアントか\n-- 0 = Client,  1 = Server");
                    sw.WriteLine(ConfigHead.MODE + ", 0");
                    sw.WriteLine("");
                    sw.WriteLine("-- レシートをプリントするか\n-- クライアントはプリント出来ない\n-- 0 = しない, 1 = する");
                    sw.WriteLine(ConfigHead.PRINT_ENABLE + ",0");
                    sw.WriteLine("");
                    sw.WriteLine("-- クライアントの接続先");
                    sw.WriteLine(ConfigHead.TARGET_IP + ", デフォルト1, 127.0.0.1");
                    sw.WriteLine(ConfigHead.TARGET_IP + ", デフォルト2, 127.0.0.1");
                    sw.WriteLine("");
                    sw.WriteLine("-- 接続先ポート (基本的に変えない)");
                    sw.WriteLine(ConfigHead.TARGET_PORT + ", 10800");
                }
                MessageBox.Show("設定ファイルが見つかりませんでした。" + Environment.NewLine + "新しくファイルを作成しました。");
                Config.getInstance().init(0, 10800, new Hashtable());
            }

            int storeNum = 0;
            Hashtable table = new Hashtable();
            int targetHost = 10800;
            foreach (string item in list)
            {
                string[] str = item.Split(',');
                switch (str[0])
                {
                    case ConfigHead.STORE_NUM:
                        storeNum = Convert.ToInt32(str[1]);
                        break;
                    case ConfigHead.TARGET_IP:
                        table.Add(str[1], str[2]);
                        break;
                    case ConfigHead.TARGET_PORT:
                        targetHost = Convert.ToInt32(str[1]);
                        break;
                    case ConfigHead.MODE:
                        Config.isClient = Convert.ToInt32(str[1]) == 0;
                        break;
                    case ConfigHead.PRINT_ENABLE:
                        Config.isPrintEnable = Convert.ToInt32(str[1]) == 1;
                        break;
                    default:
                        break;
                }
            }
            Config.getInstance().init(storeNum, targetHost, table);
        }
        private List<String> loadCSV(string path)
        {
            List<String> ret = new List<string>();
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path, Encoding.GetEncoding("Shift_JIS")))
                {
                    while (!sr.EndOfStream)
                    {
                        ret.Add(sr.ReadLine().Replace(" ","").Replace("\t",""));
                    }
                }
            }
            return ret;
        }
        public static void runNotePad()
        {
            Process.Start("Notepad", Environment.CurrentDirectory + "\\" +CONFIG_PATH);
        }
    }
}
