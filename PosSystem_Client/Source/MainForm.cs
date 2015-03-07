using System;
using System.Windows.Forms;
using PosSystem.Object.Database;
using PosSystem.Util;
using PosSystem.Setting;
using System.Text;
using PosSystem.Object;
using System.Drawing.Printing;

namespace PosSystem_Client
{
    public partial class Form1 : Form
    {
        //フォームの名前
        public string form_name = "POSシステム";
        public static string item_list = "";

        //読み取った数値格納
        public string[] readTextArray = new string[BarcodeConfig.BARCODE_NUM];
        public int inputCount = 0;

        public static int reg_item_price_sum = 0;

        public Form1()
        {
            InitializeComponent();
            InitializeListView(tItemList);
            init();
        }

        public void init()
        {
            new Database().createTable();
            PosInformation.getInstance().init(this);
            new CSV().loadConfig();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.MinimizeBox = false;
            if (Config.getInstance().store == null)
            {
                MessageBox.Show(
                    "config.csvの " + CSV.ConfigHead.STORE_NUM + " の値が正しくありません。\nソフトウェアを終了します。",
                    "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CSV.runNotePad();
                Environment.Exit(0);
            }

            if (Config.isClient)
            {
                if (Config.getInstance().targetIP.Count == 0)
                {
                    MessageBox.Show("config.csvの " + CSV.ConfigHead.TARGET_IP + " の値が正しくありません。\nソフトウェアを終了します。",
                    "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CSV.runNotePad();
                    Environment.Exit(0);
                }
            }
            else
            {
                if (Config.isPrintEnable && !Print.pingToPrinter("192.168.0.100"))
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append("192.168.0.1 にあるプリンターに到達出来ませんでした\n");
                    builder.Append("ネットワーク接続を確認するか、config.csvの " + CSV.ConfigHead.MODE + " と " + CSV.ConfigHead.PRINT_ENABLE + " の項目を確認してください\n");
                    builder.Append("ソフトウェアを終了します。");
                    MessageBox.Show(builder.ToString(),
                    "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CSV.runNotePad();
                    Environment.Exit(0);
                }
            }
            setToolMenuItem();
            SocketServer.getInstance().init(new StreamCallback(this));
            SocketClient.getInstance().init(new StreamCallback(this));

            this.KeyPreview = !this.KeyPreview;
            onSizeChangedReadItemList(tItemList, new EventArgs());
            if (Config.isClient)
            {
                toolMenuClient.Visible = true;
                this.Text = form_name + " " + Config.getInstance().store.name;
            }
            else
            {
                toolMenuServer.Visible = true;
                this.Text = form_name + " " + Config.getInstance().store.name + " サーバ";
            }
        }

        public void setToolMenuItem()
        {
            foreach (string key in Config.getInstance().targetIP.Keys)
            {
                接続先ToolStripMenuItem.DropDownItems.Add(key);
            }
            foreach (ToolStripDropDownItem item in 接続先ToolStripMenuItem.DropDownItems)
            {
                item.Click += new EventHandler(this.接続する);
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
            ColumnHeader goods_id = new ColumnHeader();
            ColumnHeader goods_order = new ColumnHeader();
            ColumnHeader goods_item = new ColumnHeader();
            ColumnHeader goods_price = new ColumnHeader();

            goods_id.Text = "ID";
            goods_id.Width = 20;
            goods_id.Tag = 1;
            goods_id.TextAlign = HorizontalAlignment.Center;

            goods_order.Text = "商品名";
            goods_order.Width = 100;
            goods_order.Tag = 4;
            goods_order.TextAlign = HorizontalAlignment.Center;
            
            goods_item.Text = "個数";
            goods_item.Width = 60;
            goods_item.Tag = 1;
            goods_item.TextAlign = HorizontalAlignment.Center;

            goods_price.Text = "金額";
            goods_price.Width = 150;
            goods_price.Tag = 2;
            goods_price.TextAlign = HorizontalAlignment.Right;

            ColumnHeader[] colHeaderRegValue = {goods_id, goods_order, goods_item, goods_price };
            listview.Columns.AddRange(colHeaderRegValue);
        }
        public void InitializeREG()
        {
            tItemList.Items.Clear();
            reg_item_price_sum = 0;
            tSumItemPrice.Text = "0";
            lScanItemName.Text = "";
            lScanItemPrice.Text = "";
        }

        //スキャンしたときの処理
        public void onReadItem(string itemBarcode)
        {
            ItemObject item = new Database().selectSingle<ItemObject>(string.Format("WHERE barcode = '{0}'", itemBarcode));
            if (item != null)
            {
                lScanItemName.Text = item.name;
                lScanItemPrice.Text = item.price.ToString();
                tItemList.Items.Add(new ListViewItem(new string[] { (item.id.ToString()), item.name, "1", item.price.ToString(), "×" }));
                reg_item_price_sum += item.price;
                tSumItemPrice.Text = reg_item_price_sum.ToString();
            }
            else
            {
                MessageBox.Show("登録されていない商品です。");
            }
        }
        
        //タイマー  ステータスバーの日付等更新
        private void display_timer_Tick(object sender, EventArgs e)
        {
            disp_now_time.Text = new Time().getTime();
        }        
        
        private void reg_clear_Click(object sender, EventArgs e)
        {
            InitializeREG();
        }
        private void 会計()
        {
            if (tSumItemPrice.Text != "" && tSumItemPrice.Text != "0")
            {
                StringBuilder builder = new StringBuilder();
                foreach (ListViewItem item in tItemList.Items)
                {
                    builder.Append(item.SubItems[0].Text).Append(",");
                }
                string items = builder.ToString();
                if (items.IndexOf(',') > -1)
                {
                    items = items.Substring(0, items.Length - 1);
                }
                item_list = items;
                Account ac = new Account(tItemList);
                ac.ShowDialog(this);
                ac.Dispose();
            }
            InitializeREG();
        }

        private bool Resizing = false;
        private void onSizeChangedReadItemList(object sender, EventArgs e)
        {
            if (!Resizing)
            {
                Resizing = true;

                ListView listView = sender as ListView;
                if (listView != null)
                {
                    float totalColumnWidth = 0;

                    for (int i = 0; i < listView.Columns.Count; i++)
                        totalColumnWidth += Convert.ToInt32(listView.Columns[i].Tag);

                    for (int i = 0; i < listView.Columns.Count; i++)
                    {
                        float colPercentage = (Convert.ToInt32(listView.Columns[i].Tag) / totalColumnWidth);
                        listView.Columns[i].Width = (int)(colPercentage * listView.ClientRectangle.Width);
                    }
                }
            }
            Resizing = false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) return;

            if (keyCheck(e) || inputCount == BarcodeConfig.BARCODE_NUM) init_input();

            readTextArray[inputCount] = e.KeyCode.ToString();
            inputCount++;

            if (inputCount == BarcodeConfig.BARCODE_NUM)
            {
                string bar = genBarcode(readTextArray);
                init_input();
                string barHead = bar.Substring(BarcodeConfig.PREFIX.Length, BarcodeConfig.PREFIX_LENGTH - BarcodeConfig.PREFIX.Length);

                Database db = new Database();

                switch (barHead)
                {

                    case BarcodeConfig.ITEM:
                        onReadItem(bar);
                        break;

                    case BarcodeConfig.SALE:
                        Sales sl = new Sales(db.selectSingle<SaleObject>(string.Format("WHERE barcode = '{0}'", bar)));
                        sl.ShowDialog(this);
                        sl.Dispose();
                        break;

                    case BarcodeConfig.STAFF:
                        onReadStaff(bar);
                        break;

                    case BarcodeConfig.ITEM_LIST:
                        openItemList();
                        break;

                    case BarcodeConfig.SALE_LIST:
                        openSalesList();
                        break;

                    case BarcodeConfig.ACCOUNT:
                        会計();
                        break;

                    case BarcodeConfig.CHANGE_VISIBLE_DEBUG_TOOLBAR:
                        toolMenuDebug.Visible = !toolMenuDebug.Visible;
                        break;

                    case BarcodeConfig.CHANGE_VISIBLE_TOOLBAR:
                        if (Config.isClient)
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
            else if(inputCount > BarcodeConfig.BARCODE_NUM)
            {
                init_input();
            }
        }

        public void init_input()
        {
            readTextArray = new string[PosSystem.Setting.BarcodeConfig.BARCODE_NUM];
            inputCount = 0;
        }
        public bool keyCheck(KeyEventArgs e)
        {
            return
                PosSystem.Setting.BarcodeConfig.PREFIX.Length > inputCount
                && !e.KeyCode.ToString().Equals("D" + PosSystem.Setting.BarcodeConfig.PREFIX[inputCount]);
        }

        public string genBarcode(string[] readValueArray)
        {
            string ret = "";
            for (int i = 0; i < PosSystem.Setting.BarcodeConfig.BARCODE_NUM; i++)
            {
                ret += readValueArray[i][1];
            }
            return ret;
        }
        private void openItemList()
        {
            ItemList il = new ItemList();
            il.ShowDialog(this);
            il.Dispose();
        }
        private void openSalesList()
        {
            Sales_List sll = new Sales_List();
            sll.ShowDialog(this);
            sll.Dispose();
        }

        private void 商品リストToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ItemList il = new ItemList();
            il.ShowDialog();
            il.Dispose();
        }

        private void 売上リストToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sales_List sl = new Sales_List();
            sl.ShowDialog();
            sl.Dispose();
        }
        private void 接続する(object sender, EventArgs e)
        {
            if (sender.GetType() == this.接続先ToolStripMenuItem.GetType())
            {
                string targetIP = Config.getInstance().targetIP[((ToolStripItem)sender).Text].ToString();
                if (!SocketClient.getInstance().ClientStart(targetIP)) MessageBox.Show("接続に失敗しました");
                else
                {
                    this.Text += ((ToolStripItem)sender).Text + " へ接続中";
                    for (int i = 0; i < 接続先ToolStripMenuItem.DropDownItems.Count; i++)
                    {
                        接続先ToolStripMenuItem.DropDownItems[i].Enabled = false;
                    }
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            display_timer.Enabled = false;
            SocketClient.getInstance().StopSock();
            SocketServer.getInstance().CloseServer();
        }

        private void onReadStaff(string barcode)
        {
            StaffObject staff = new Database().selectSingle<StaffObject>(string.Format("WHERE barcode = '{0}'", barcode));
            if (staff != null)
            {
                reg_user.Text = staff.name;
                PosInformation.getInstance().setStaff(staff);
            }
            else
            {
                StaffRegistWindow staffRegist = new StaffRegistWindow(barcode);
                staffRegist.ShowDialog(this);
                staffRegist.Dispose();
            }
        }
        public class StreamCallback : SocketListener
        {
            public StreamCallback(Form context) : base(context) { }
            public override void onReceive(string text)
            {
                string[] data = text.Split(',');
                switch (data[0])
                {
                    // staff, [name], [barcode]
                    case "staff":
                        new Database().insert<StaffObject>(new StaffObject(data[2], data[1]));
                        break;
                    default:
                        break;
                }
            }
            public override void onClose(SocketCloseType closeType)
            {
                //MessageBox.Show("接続解除");
            }
        }

        private void 商リストToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openItemList();
        }

        // テストメニュー
        private void 商品ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            onReadItem("1001010001");
        }

        private void スタッフToolStripMenuItem_Click(object sender, EventArgs e)
        {
            onReadStaff("1000150050");
        }

        private void 未登録スタッフToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int count = new Database().count<StaffObject>() + 1;
            onReadStaff("100015" + count.ToString("D4"));
        }

        private void 売上リストToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            openSalesList();
        }

        private void サーバーを建てるToolStripMenuItem_Click(object sender, EventArgs e)
        {
            serverStart();
        }
        private void serverStart()
        {
            if (!SocketServer.getInstance().ServerStart()) MessageBox.Show("サーバ起動に失敗しました。");
            else
            {
                this.Text += " サーバー起動中";
                サーバーを建てるToolStripMenuItem.Enabled = false;
            }
        }

        private void reg_account_MouseClick(object sender, MouseEventArgs e)
        {
            会計();
        }

        private void システムバーコード印刷ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            PrintDialog pdlg = new PrintDialog();
            pdlg.Document = pd;
            pd.Print();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Print.getInstance().printSystemBarcode(sender, e);
        }

        private void ダミーユーザ印刷ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(Print.getInstance().printDummyUserBarcode);
            PrintDialog pdlg = new PrintDialog();
            pdlg.Document = pd;
            pd.Print();
        }
    }
}
