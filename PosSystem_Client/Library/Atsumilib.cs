using System;
using System.Windows.Forms;
using System.Collections;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Printing;
using ZXing;
using Microsoft.VisualBasic.FileIO;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;


namespace PosSystem_Client
{

    class BarCode_Prefix
    {
        public const int BARCODE_NUM = 10;
        public const string PREFIX = "10";

        public const string STAFF = "00";
        public const string ITEM = "01";
        public const string SALE = "02";

        //画面遷移
        public const string ITEM_REGIST = "17";
        public const string ITEM_LIST = "11";
        public const string SALE_LIST = "12";
        public const string ACCOUNT = "13";
        public const string STAFF_REGIST = "14";
        public const string STAFF_LIST = "15";
        public const string ITEM_LIST_EDIT = "16";

        //操作
        public const string ENTER = "20";
        public const string BACK = "21";
        public const string SHOW_TOOLBAR = "22";
        public const string HIDE_TOOLBAR = "23";

        //動作モード変更
        public const string MODE_TAKE = "30";
        public const string MODE_PRACTICE = "31";

    }

    class atsumi_pos
    {
        public static void regist_user(Connect cn, string _name, string _barcode = "")
        {
            if (cn != null) cn.SendStringData("staff_list," + _barcode);

            atsumi_pos.Insert(new atsumi_pos.StaffTable(_barcode, _name));
        }


        public void print()
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            PrintDialog pdlg = new PrintDialog();
            pdlg.Document = pd;
            pd.Print();
        }
    }
    class Unix_Time
    {
        // unix epochをDateTimeで表した定数
        public readonly static DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        // DateTimeをUNIX時間に変換するメソッド
        public static long ToUnixTime(DateTime dateTime)
        {
            // 時刻をUTCに変換
            dateTime = dateTime.ToUniversalTime();

            // unix epochからの経過秒数を求める
            return (long)dateTime.Subtract(UnixEpoch).TotalSeconds;
        }

        // UNIX時間からDateTimeに変換するメソッド
        public static DateTime FromUnixTime(long unixTime)
        {
            // unix epochからunixTime秒だけ経過した時刻を求める
            return UnixEpoch.AddSeconds(unixTime);
        }
    }
    class print_template
    {
        /// <summary>
        /// 「通常使うプリンタ」に設定する
        /// </summary>
        /// <param name="printerName">プリンタ名</param>
        public static void SetDefaultPrinter(string printerName)
        {
            //WshNetworkオブジェクトを作成する
            Type t = Type.GetTypeFromProgID("WScript.Network");
            object wshNetwork = Activator.CreateInstance(t);
            //SetDefaultPrinterメソッドを呼び出す
            t.InvokeMember("SetDefaultPrinter",
                System.Reflection.BindingFlags.InvokeMethod,
                null, wshNetwork, new object[] { printerName });
        }

        public static void print_system_barcode(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int margin_min = 3;
            int margin_max = 70;
            int align_center = 27;
            int line_height = 7;
            int draw_height_position = 0;

            Graphics g = e.Graphics;
            g.PageUnit = GraphicsUnit.Millimeter;
            Font f = new Font("MS UI Gothic", 10);
            Font f_big = new Font("MS UI Gothic", 13);

            g.DrawImage(Image.FromFile(@"Kids.jpg"), 3, 3, 67, 20);

            draw_height_position += line_height + 22;

            drawString(g, f_big, "<システムバーコード>", align_center - 20, draw_height_position);

            draw_height_position += line_height + 3;

            g.DrawLine(new Pen(Brushes.Black),
                new Point(margin_min, draw_height_position),
                new Point(margin_max, draw_height_position));

            draw_height_position += line_height + 2;

            int margin_str_barcode = 1;
            int margin_barcode = 40;

            /* ---  バーコード生成  --- */
            /* 2つずつ書いています。もうなんか読みづらくてすみませ・・・・、、 */

            hoge("商品登録", BarCode_Prefix.ITEM_REGIST, "商品リスト", BarCode_Prefix.ITEM_LIST, g, f, margin_min, margin_barcode, line_height, margin_str_barcode, ref draw_height_position);
            
            hoge("売上リスト", BarCode_Prefix.SALE_LIST, "会計", BarCode_Prefix.ACCOUNT, g, f, margin_min, margin_barcode, line_height, margin_str_barcode, ref draw_height_position);
            
            hoge("スタッフリスト", BarCode_Prefix.STAFF_LIST, "スタッフ登録", BarCode_Prefix.STAFF_REGIST, g, f, margin_min, margin_barcode, line_height, margin_str_barcode, ref draw_height_position);

            hoge("ツールバー表示", BarCode_Prefix.SHOW_TOOLBAR, "ツールバー非表示", BarCode_Prefix.HIDE_TOOLBAR, g, f, margin_min, margin_barcode, line_height, margin_str_barcode, ref draw_height_position);

            g.DrawLine(new Pen(Brushes.Black),
                new Point(margin_min, draw_height_position),
                new Point(margin_max, draw_height_position));

            e.HasMorePages = false;
        }

        public static void hoge(
            string fir, string fir_p,
            string sec, string sec_p,
            Graphics g, Font f,
            int margin_min, 
            int margin_barcode,
            int line_height,
            int margin_str_barcode,
            ref int height)
        {
            drawString(g, f, fir, margin_min, height);
            drawString(g, f, sec, margin_min + margin_barcode, height);
            height += line_height + margin_str_barcode;
            create(g, fir_p, margin_min, height);
            create(g, sec_p, margin_min + margin_barcode, height);
            height += line_height + 25;
        }

        public static void create(Graphics g, string _barcode_prefix, int weight, int height)
        {
            Barcode bc = new Barcode(_barcode_prefix, "000", "00000");
            BarcodeWriter bw = new BarcodeWriter();
            bw.Format = BarcodeFormat.EAN_13;
            Bitmap barcode = bw.Write(bc.show());
            g.DrawImage(barcode, new Point(weight, height));
        }
    }
    class System_log{
        public static void ShowDialog(string msg)
        {
            MessageBox.Show(msg, "例外発生", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
    }

    public class Connect
    {

        //別スレッドからログを書き込むデリゲート
        public delegate void dlgExeFuntion();

        int port_num = 10800;
        string ip_address;
        Form1 fom;
        public Connect(string _ip_address, Form1 _fom)
        {
            this.ip_address = _ip_address;
            this.fom = _fom;
        }
        Encoding ecUni = Encoding.GetEncoding("utf-16");
        Encoding ecSjis = Encoding.GetEncoding("shift-jis");

        //TcpClient server = null;

        TcpClient client = null;
        Thread threadClient = null;

        delegate void dlgMydelegate();

        //ソケット通信開始
        public bool StartSock()
        {
            bool openflg = false;
            openflg = ClientStart();
            
            return openflg;
        }

        //***********************************************************
        //セカンドスレッドの作成とクライアントのスタート
        //***********************************************************    
        public bool ClientStart()
        {
            try
            {
                client = new TcpClient(ip_address, port_num);
                threadClient = new Thread(new ThreadStart(this.ClientListen));
                threadClient.Start();

                return (true);
            }
            catch
            {
                return (false);
            }
        }
        //***********************************************************
        //別スレッドで実行されるクライアント側の処理
        //ここの処理はServerと同じなのでそちらを参照のこと
        //***********************************************************

        string barcode = "";
        string name = "";
        public void ClientListen()
        {
            NetworkStream stream = client.GetStream();
            Byte[] bytes = new Byte[100];
            while (true)
            {
                try
                {
                    int intCount = stream.Read(bytes, 0, bytes.Length);
                    if (intCount>0)
                    {
                        Byte[] getByte = new byte[intCount];
                        for (int i = 0; i < intCount; i++)
                            getByte[i] = bytes[i];

                        byte[] uniBytes;
                        uniBytes = Encoding.Convert(ecSjis, ecUni, bytes);
                        string strGetText = ecUni.GetString(uniBytes);
                        strGetText = strGetText.Substring(0, strGetText.IndexOf((char)0));

                        string[] rec = strGetText.Split(',');
                        if (rec[0] == "receive")
                        {
                            if (rec[1] == "barcode")
                            {
                                //MessageBox.Show("バーコードが発行されました"+Environment.NewLine+rec[2]);
                                atsumi_pos.Insert(new atsumi_pos.StaffTable(rec[2],rec[3]));
                                this.barcode = rec[2];
                                this.name = rec[3];
                                this.print();
                            }
                        }
                    }
                    else
                    {
                        stream.Close();
                        stream = null;
                        Thread.Sleep(20);//これを入れないとNullReferenceExceptionが起きる

                        MessageBox.Show("サーバーから切断されました");
                        StopSock();
                        fom.change();
                    }
                }
                catch
                {
                    return;
                }
            }
        }

        public void print()
        {
            System.Drawing.Printing.PrintDocument pd =
                new System.Drawing.Printing.PrintDocument();
            pd.PrintPage +=
                new System.Drawing.Printing.PrintPageEventHandler(printDocument1_PrintPage);
            PrintDialog pdlg = new PrintDialog();
            pdlg.Document = pd;
            if (pdlg.ShowDialog() == DialogResult.OK)
            {
                pd.Print();
            }
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            print_template.print_user(this.barcode, this.name , e);
        }
        public void StopSock()
        {
            CloseClient();
        }
        //クライアントのクローズ
        public void CloseClient()
        {
            NetworkStream ns = client.GetStream();

            if (client != null && client.Connected)
                ns.Close();
                client.Close();

            if (threadClient != null)
                threadClient.Abort();
        }
        //***********************************************************
        //別スレッドから抜けて、メインスレッドからStop　Startを実行
        //***********************************************************
        //文字データーの送信
        public void SendStringData(string _send_text)
        {
            Byte[] data = ecSjis.GetBytes(_send_text);
            NetworkStream stream = null;
            try
            {
                stream = client.GetStream();

                stream.Write(data, 0, data.Length);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString()+Environment.NewLine+"送信できませんでした。", "送信エラー");
            }
        }
        //***********************************************************
        //別スレッドから抜けて、メインスレッドからStop　Startを実行
        //***********************************************************
        public void RestartServer()
        {
            StopSock();
            StartSock();
        }
    }
    class Barcode
    {
        public bool isSet = false;
        public bool isCreated = false;

        private string prefix = "";
        private string store = "";
        private string item_num = "";
        private string barcode = "";

        Barcode(string _prefix)
        {
            this.prefix = _prefix;
        }

        /// <summary>
        /// バーコードを作成します。
        /// </summary>
        /// <param name="_prefix">2桁 Barcode_Prefix</param>
        /// <param name="_store_num">3桁 001 Form1.store_num</param>
        /// <param name="_item_num">5桁 データベースとか見てね</param>
        public Barcode(string _prefix, string _store_num, string _item_num)
        {
            this.prefix = _prefix.ToString();
            this.store = _store_num;
            this.item_num = _item_num;
            this.isSet = true;
            this.comb_barcode();
        }
        public void comb_barcode()
        {
            if (this.isSet)
            {
                string temp = BarCode_Prefix.PREFIX + this.prefix + this.store + this.item_num;
                temp += atsumi_pos.create_check_digit(temp);
                if (temp.Length == BarCode_Prefix.BARCODE_NUM)
                {
                    this.barcode = temp;
                    this.isCreated = true;
                }
                else
                {
                    throw new Exception("作られたバーコードの長さがおかしいなぁ・・" + Environment.NewLine +
                    temp);
                }
            }
        }
        public string show()
        {
            return (this.isCreated) ? this.barcode : "";
        }
    }
}
