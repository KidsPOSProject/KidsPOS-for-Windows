using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace KidsPos.Sources.Util
{
    public class Csv
    {
        public const string ConfigPath = "config.csv";

        public void LoadConfig()
        {
            var list = LoadCsv(ConfigPath);
            if (list == null || list.Count == 0)
            {
                using (var sw = new StreamWriter(ConfigPath, false, Encoding.GetEncoding("Shift_JIS")))
                {
                    sw.WriteLine("-- お店番号 DBRegister.exe のお店リスト参照");
                    sw.WriteLine(ConfigHead.StoreNum + ", 0");
                    sw.WriteLine("");
                    sw.WriteLine("-- サーバかクライアントか\n-- 0 = Client,  1 = Server");
                    sw.WriteLine(ConfigHead.Mode + ", 0");
                    sw.WriteLine("");
                    sw.WriteLine("-- レシートをプリントするか\n-- クライアントはプリント出来ない\n-- 0 = しない, 1 = する");
                    sw.WriteLine(ConfigHead.PrintEnable + ",0");
                    sw.WriteLine("");
                    sw.WriteLine("-- クライアントの接続先");
                    sw.WriteLine(ConfigHead.TargetIp + ", デフォルト1, 127.0.0.1");
                    sw.WriteLine(ConfigHead.TargetIp + ", デフォルト2, 127.0.0.1");
                    sw.WriteLine("");
                    sw.WriteLine("-- 接続先ポート (基本的に変えない)");
                    sw.WriteLine(ConfigHead.TargetPort + ", 10800");
                }
                MessageBox.Show("設定ファイルが見つかりませんでした。" + Environment.NewLine + "新しくファイルを作成しました。");
                Config.GetInstance().Init(0, 10800, new Hashtable());
            }

            var storeNum = 0;
            var table = new Hashtable();
            var targetHost = 10800;
            foreach (var item in list)
            {
                var str = item.Split(',');
                switch (str[0])
                {
                    case ConfigHead.StoreNum:
                        storeNum = Convert.ToInt32(str[1]);
                        break;
                    case ConfigHead.TargetIp:
                        table.Add(str[1], str[2]);
                        break;
                    case ConfigHead.TargetPort:
                        targetHost = Convert.ToInt32(str[1]);
                        break;
                    case ConfigHead.Mode:
                        Config.IsClient = Convert.ToInt32(str[1]) == 0;
                        break;
                    case ConfigHead.PrintEnable:
                        Config.IsPrintEnable = Convert.ToInt32(str[1]) == 1;
                        break;
                }
            }
            Config.GetInstance().Init(storeNum, targetHost, table);
        }

        private static List<string> LoadCsv(string path)
        {
            var ret = new List<string>();
            if (!File.Exists(path)) return ret;
            using (var sr = new StreamReader(path, Encoding.GetEncoding("Shift_JIS")))
            {
                while (!sr.EndOfStream)
                    ret.Add(sr.ReadLine()?.Replace(" ", "").Replace("\t", ""));
            }
            return ret;
        }

        public static void RunNotePad()
        {
            Process.Start("Notepad", Environment.CurrentDirectory + "\\" + ConfigPath);
        }

        public static class ConfigHead
        {
            public const string StoreNum = "#StoreNum";
            public const string TargetIp = "#TargetIP";
            public const string TargetPort = "#TargetPort";
            public const string Mode = "#Mode";
            public const string PrintEnable = "#PrintReceipt";
        }
    }
}