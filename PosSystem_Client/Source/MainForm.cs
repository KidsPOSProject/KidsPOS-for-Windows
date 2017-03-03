using System;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using KidsPos.Object;
using KidsPos.Object.Database;
using KidsPos.Setting;
using KidsPos.Util;

namespace PosSystem.Source
{
    public partial class Form1 : Form
    {
        //フォームの名前
        public string FormName = "POSシステム";
        public static string ItemList = "";

        //読み取った数値格納
        public string[] ReadTextArray = new string[BarcodeConfig.BarcodeNum];
        public int InputCount;

        public static int RegItemPriceSum;

        public Form1()
        {
            InitializeComponent();
            InitializeListView(tItemList);
            Init();
        }

        public void Init()
        {
            new Database().CreateTable();
            PosInformation.GetInstance().Init(this);
            new Csv().LoadConfig();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            MinimizeBox = false;
            if (Config.GetInstance().Store == null)
            {
                MessageBox.Show(
                    @"config.csvの " + Csv.ConfigHead.StoreNum + @" の値が正しくありません。
ソフトウェアを終了します。",
                    @"読み込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Csv.RunNotePad();
                Environment.Exit(0);
            }

            if (Config.IsClient)
            {
                if (Config.GetInstance().TargetIp.Count == 0)
                {
                    MessageBox.Show(@"config.csvの " + Csv.ConfigHead.TargetIp + @" の値が正しくありません。
ソフトウェアを終了します。",
                    @"読み込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Csv.RunNotePad();
                    Environment.Exit(0);
                }
            }
            else
            {
                if (Config.IsPrintEnable && !Print.PingToPrinter("192.168.0.100"))
                {
                    var builder = new StringBuilder();
                    builder.Append("192.168.0.100 にあるプリンターに到達出来ませんでした\n");
                    builder.Append("ネットワーク接続を確認するか、config.csvの " + Csv.ConfigHead.Mode + " と " + 
                        Csv.ConfigHead.PrintEnable + " の項目を確認してください\n");
                    builder.Append("ソフトウェアを終了します。");
                    MessageBox.Show(builder.ToString(),
                    @"読み込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Csv.RunNotePad();
                    Environment.Exit(0);
                }
            }
            SetToolMenuItem();
            SocketServer.GetInstance().Init(new StreamCallback(this));
            SocketClient.GetInstance().Init(new StreamCallback(this));

            KeyPreview = !KeyPreview;
            OnSizeChangedReadItemList(tItemList, new EventArgs());
            if (Config.IsClient)
            {
                toolMenuClient.Visible = true;
                Text = FormName + @" " + Config.GetInstance().Store.Name;
            }
            else
            {
                toolMenuServer.Visible = true;
                Text = FormName + @" " + Config.GetInstance().Store.Name + @" サーバ";
            }
        }

        public void SetToolMenuItem()
        {
            foreach (string key in Config.GetInstance().TargetIp.Keys)
            {
                接続先ToolStripMenuItem.DropDownItems.Add(key);
            }
            foreach (ToolStripDropDownItem item in 接続先ToolStripMenuItem.DropDownItems)
            {
                item.Click += 接続する;
            }
        }
        public static void InitializeListView(ListView listview)
        {
            // ListViewコントロールのプロパティを設定
            listview.FullRowSelect = true;
            listview.GridLines = true;
            listview.Sorting = SortOrder.Ascending;
            listview.View = View.Details;

            // 列（コラム）ヘッダの作成
            var goodsId = new ColumnHeader();
            var goodsOrder = new ColumnHeader();
            var goodsItem = new ColumnHeader();
            var goodsPrice = new ColumnHeader();

            goodsId.Text = "ID";
            goodsId.Width = 20;
            goodsId.Tag = 1;
            goodsId.TextAlign = HorizontalAlignment.Center;

            goodsOrder.Text = "商品名";
            goodsOrder.Width = 100;
            goodsOrder.Tag = 4;
            goodsOrder.TextAlign = HorizontalAlignment.Center;
            
            goodsItem.Text = "個数";
            goodsItem.Width = 60;
            goodsItem.Tag = 1;
            goodsItem.TextAlign = HorizontalAlignment.Center;

            goodsPrice.Text = "金額";
            goodsPrice.Width = 150;
            goodsPrice.Tag = 2;
            goodsPrice.TextAlign = HorizontalAlignment.Right;

            ColumnHeader[] colHeaderRegValue = {goodsId, goodsOrder, goodsItem, goodsPrice };
            listview.Columns.AddRange(colHeaderRegValue);
        }
        public void InitializeReg()
        {
            tItemList.Items.Clear();
            RegItemPriceSum = 0;
            tSumItemPrice.Text = "0";
            lScanItemName.Text = "";
            lScanItemPrice.Text = "";
        }

        //スキャンしたときの処理
        public void OnReadItem(string itemBarcode)
        {
            var item = new Database().SelectSingle<ItemObject>($"WHERE barcode = '{itemBarcode}'");
            if (item != null)
            {
                lScanItemName.Text = item.Name;
                lScanItemPrice.Text = item.Price.ToString();
                tItemList.Items.Add(new ListViewItem(new[] { (item.Id.ToString()), item.Name, "1", item.Price.ToString(), "×" }));
                RegItemPriceSum += item.Price;
                tSumItemPrice.Text = RegItemPriceSum.ToString();
            }
            else
            {
                MessageBox.Show(@"何らかの原因で登録できませんでした。");
            }
        }
        
        //タイマー  ステータスバーの日付等更新
        private void display_timer_Tick(object sender, EventArgs e)
        {
            disp_now_time.Text = new Time().GetTime();
        }        
        
        private void reg_clear_Click(object sender, EventArgs e)
        {
            InitializeReg();
        }
        private void 会計()
        {
            if (tSumItemPrice.Text != "" && tSumItemPrice.Text != "0")
            {
                var builder = new StringBuilder();
                foreach (ListViewItem item in tItemList.Items)
                {
                    builder.Append(item.SubItems[0].Text).Append(",");
                }
                var items = builder.ToString();
                if (items.IndexOf(',') > -1)
                {
                    items = items.Substring(0, items.Length - 1);
                }
                ItemList = items;
                var ac = new Account(tItemList);
                ac.ShowDialog(this);
                ac.Dispose();
            }
            InitializeReg();
        }

        private bool _resizing;
        private void OnSizeChangedReadItemList(object sender, EventArgs e)
        {
            if (!_resizing)
            {
                _resizing = true;

                var listView = sender as ListView;
                if (listView != null)
                {
                    float totalColumnWidth = 0;

                    for (var i = 0; i < listView.Columns.Count; i++)
                        totalColumnWidth += Convert.ToInt32(listView.Columns[i].Tag);

                    for (var i = 0; i < listView.Columns.Count; i++)
                    {
                        var colPercentage = (Convert.ToInt32(listView.Columns[i].Tag) / totalColumnWidth);
                        listView.Columns[i].Width = (int)(colPercentage * listView.ClientRectangle.Width);
                    }
                }
            }
            _resizing = false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) return;

            if (KeyCheck(e) || InputCount == BarcodeConfig.BarcodeNum) init_input();

            ReadTextArray[InputCount] = e.KeyCode.ToString();
            InputCount++;

            if (InputCount == BarcodeConfig.BarcodeNum)
            {
                var bar = GenBarcode(ReadTextArray);
                init_input();
                var barHead = bar.Substring(BarcodeConfig.Prefix.Length, BarcodeConfig.PrefixLength - BarcodeConfig.Prefix.Length);

                Database db = new Database();

                switch (barHead)
                {

                    case BarcodeConfig.Item:
                        OnReadItem(bar);
                        break;

                    case BarcodeConfig.Sale:
                        var sl = new Sales(db.SelectSingle<SaleObject>($"WHERE barcode = '{bar}'"));
                        sl.ShowDialog(this);
                        sl.Dispose();
                        break;

                    case BarcodeConfig.Staff:
                        OnReadStaff(bar);
                        break;

                    case BarcodeConfig.ItemList:
                        OpenItemList();
                        break;

                    case BarcodeConfig.SaleList:
                        OpenSalesList();
                        break;

                    case BarcodeConfig.Account:
                        会計();
                        break;

                    case BarcodeConfig.ChangeVisibleDebugToolbar:
                        toolMenuDebug.Visible = !toolMenuDebug.Visible;
                        break;

                    case BarcodeConfig.ChangeVisibleToolbar:
                        if (Config.IsClient)
                        {
                            toolMenuClient.Visible = !toolMenuClient.Visible;
                        }
                        else
                        {
                            toolMenuServer.Visible = !toolMenuServer.Visible;
                        }
                        break;
                }
            }
            else if(InputCount > BarcodeConfig.BarcodeNum)
            {
                init_input();
            }
        }

        public void init_input()
        {
            ReadTextArray = new string[BarcodeConfig.BarcodeNum];
            InputCount = 0;
        }
        public bool KeyCheck(KeyEventArgs e)
        {
            return BarcodeConfig.Prefix.Length > InputCount
                && !e.KeyCode.ToString().Equals("D" + BarcodeConfig.Prefix[InputCount]);
        }

        public string GenBarcode(string[] readValueArray)
        {
            var ret = "";
            for (var i = 0; i < BarcodeConfig.BarcodeNum; i++)
            {
                ret += readValueArray[i][1];
            }
            return ret;
        }
        private void OpenItemList()
        {
            var il = new ItemList();
            il.ShowDialog(this);
            il.Dispose();
        }
        private void OpenSalesList()
        {
            var sll = new SalesList();
            sll.ShowDialog(this);
            sll.Dispose();
        }

        private void 商品リストToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var il = new ItemList();
            il.ShowDialog();
            il.Dispose();
        }

        private void 売上リストToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sl = new SalesList();
            sl.ShowDialog();
            sl.Dispose();
        }
        private void 接続する(object sender, EventArgs e)
        {
            if (sender.GetType() == 接続先ToolStripMenuItem.GetType())
            {
                string targetIp = Config.GetInstance().TargetIp[((ToolStripItem)sender).Text].ToString();
                if (!SocketClient.GetInstance().ClientStart(targetIp)) MessageBox.Show(@"何らかの原因で登録できませんでした。");
                else
                {
                    Text += ((ToolStripItem)sender).Text + @" へ接続中";
                    for (var i = 0; i < 接続先ToolStripMenuItem.DropDownItems.Count; i++)
                    {
                        接続先ToolStripMenuItem.DropDownItems[i].Enabled = false;
                    }
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            display_timer.Enabled = false;
            SocketClient.GetInstance().StopSock();
            SocketServer.GetInstance().CloseServer();
        }

        private void OnReadStaff(string barcode)
        {
            var staff = new Database().SelectSingle<StaffObject>($"WHERE barcode = '{barcode}'");
            if (staff != null)
            {
                reg_user.Text = staff.Name;
                PosInformation.GetInstance().SetStaff(staff);
            }
            else
            {
                var staffRegist = new StaffRegistWindow(barcode);
                staffRegist.ShowDialog(this);
                staffRegist.Dispose();
            }
        }
        public class StreamCallback : SocketListener
        {
            public StreamCallback(Form context) : base(context) { }
            public override void OnReceive(string text)
            {
                var data = text.Split(',');
                switch (data[0])
                {
                    // staff, [name], [barcode]
                    case "staff":
                        new Database().Insert(new StaffObject(data[2], data[1]));
                        break;
                }
            }
            public override void OnClose(SocketCloseType closeType)
            {
                //MessageBox.Show("接続解除");
            }
        }

        private void 商リストToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenItemList();
        }

        // テストメニュー
        private void 商品ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OnReadItem("1001010001");
        }

        private void スタッフToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnReadStaff("1000150050");
        }

        private void 未登録スタッフToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var count = new Database().Count<StaffObject>() + 1;
            OnReadStaff("100015" + count.ToString("D4"));
        }

        private void 売上リストToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenSalesList();
        }

        private void サーバーを建てるToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ServerStart();
        }
        private void ServerStart()
        {
            if (!SocketServer.GetInstance().ServerStart()) MessageBox.Show(@"何らかの原因で登録できませんでした。");
            else
            {
                Text += " サーバー起動中";
                サーバーを建てるToolStripMenuItem.Enabled = false;
            }
        }

        private void reg_account_MouseClick(object sender, MouseEventArgs e)
        {
            会計();
        }

        private void システムバーコード印刷ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var pd = new PrintDocument();
            pd.PrintPage += printDocument1_PrintPage;
            var pdlg = new PrintDialog();
            pdlg.Document = pd;
            pd.Print();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Print.GetInstance().PrintSystemBarcode(sender, e);
        }

        private void ダミーユーザ印刷ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var pd = new PrintDocument();
            pd.PrintPage += Print.GetInstance().PrintDummyUserBarcode;
            var pdlg = new PrintDialog();
            pdlg.Document = pd;
            pd.Print();
        }
    }
}
