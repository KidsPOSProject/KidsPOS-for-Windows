using System;
using System.Windows.Forms;
using System.Collections;
using System.Data.SQLite;
using System.IO;
using System.Xml.Serialization;
using PosSystem.Object.Database;
using PosSystem.Util;
using PosSystem.Setting;
using System.Drawing.Printing;
using Microsoft.VisualBasic.FileIO;
using System.Text;

namespace PosSystem_Client
{
    public partial class Form1 : Form
    {

        #region 定数

        //フォームの名前
        public string form_name = "POSシステム 練習用クライアント";

        //後々サーバーと通信する

        public static string store_num = "001";
        public static string store_name = "デパート";

        //変数
        public static string item_sum = "";
        public static string item_list = "";

        public static string shop_person = "";

        #endregion

        public static Hashtable ip_list = new Hashtable();

        //バーコード関係の変数

        //読み取った数値格納
        public static string[] readTextArray = new string[PosSystem.Setting.BarcodeConfig.BARCODE_NUM];

        //現在読み取っている数値の場所
        public int inputCount = 0;

        public static int reg_item_price_sum = 0;

        public Form1()
        {
            InitializeComponent();
            InitializeValues();
            InitializeListView(tItemList);

            PosInformation.getInstance().init(this, new StoreObject("デパート"), "");
            SocketClient.getInstance().init(new StreamCallback(this));

            this.KeyPreview = !this.KeyPreview;
            reg_goods_list_SizeChanged(tItemList, new EventArgs());
            this.Text = form_name;

            this.WindowState = FormWindowState.Maximized;
            this.MinimizeBox = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        #region Initialize
        public void InitializeValues()
        {


            ArrayList al = loadSettings();
            if (1 > al.Count)
            {
                MessageBox.Show("設定ファイルが見つかりませんでした。ソフトウェアを終了いたします。");
                this.Close();
            }
            try
            {
                for (int i = 2; i < al.Count; i+=2)
                {
                    接続先ToolStripMenuItem.DropDownItems.Add(al[i].ToString());
                    ip_list.Add(al[i], al[i + 1]);
                }
                for (int i = 0; i < 接続先ToolStripMenuItem.DropDownItems.Count; i++)
                {
                    接続先ToolStripMenuItem.DropDownItems[i].Click += new System.EventHandler(this.接続する);
                }
            }
            catch
            {
                MessageBox.Show("正しくIPアドレスが読み込まれませんでした。");
                this.Close();
            }
        }
        public static ArrayList loadSettings()
        {
            ArrayList al = new ArrayList();
            try
            {
                TextFieldParser parser = new TextFieldParser("ip.csv", System.Text.Encoding.GetEncoding("Shift_JIS"));
                parser.TextFieldType = FieldType.Delimited;
                // 区切り文字はコンマ
                parser.SetDelimiters(",");

                while (!parser.EndOfData)
                {
                    // 1行読み込み
                    string[] row = parser.ReadFields();
                    foreach (string field in row)
                    {
                        string f = field;
                        // 改行をnで表示
                        f = f.Replace("\r\n", "n");
                        // 空白を_で表示 
                        f = f.Replace(" ", "");
                        // TAB区切りで出力 
                        if (!(f == "")) al.Add(f);
                    }
                }
            }
            catch
            {
                MessageBox.Show("設定ファイルが正しく読み込まれませんでした。");
            }
            return al;
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

        #region DB_SHORI
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
                debug_Test.Text = "登録されていない商品です。";
            }
            //debug_Test.Text = reg_goods_list.Items.Count.ToString();
        }
        
        #region Event

        //タイマー  ステータスバーの日付等更新
        private void display_timer_Tick(object sender, EventArgs e)
        {
            disp_now_time.Text = new Time().getTime();
        }        
        
        private void reg_clear_Click(object sender, EventArgs e)
        {
            InitializeREG();
        }
        private void reg_goods_list_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            debug_Test.Text = tItemList.SelectedItems[0].SubItems[1].Text;
        }
        private void reg_account_Click(object sender, EventArgs e)
        {
            会計();
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
                    items = items.Substring(0, items.Length - 2);
                }
                item_list = items;
                Account ac = new Account(tItemList);
                ac.ShowDialog(this);
                ac.Dispose();
            }
            InitializeREG();
        }

        #region リストのカラム幅調整
        private bool Resizing = false;
        private void reg_goods_list_SizeChanged(object sender, EventArgs e)
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
        #endregion

        #region キー入力の処理

        //bool F_key_check = false;

        //バーコードが入力されたとき
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (keyCheck(e) || inputCount == BarcodeConfig.BARCODE_NUM) init_input();

            readTextArray[inputCount] = e.KeyCode.ToString();
            inputCount++;

            if (inputCount == BarcodeConfig.BARCODE_NUM)
            {
                string bar = genBarcode();
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
                        
                        ItemList il = new ItemList();
                        il.ShowDialog(this);
                        il.Dispose();
                        
                        break;

                    case BarcodeConfig.SALE_LIST:
                        Sales_List sll = new Sales_List();
                        sll.ShowDialog(this);
                        sll.Dispose();
                        
                        break;

                    case BarcodeConfig.ACCOUNT:
                        会計();
                        break;

                    case BarcodeConfig.SHOW_TOOLBAR:
                        top_menu.Visible = true;
                        break;

                    case BarcodeConfig.HIDE_TOOLBAR:
                        top_menu.Visible = false;
                        break;
                }
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

        public string genBarcode()
        {
            string ret = "";
            for (int i = 0; i < PosSystem.Setting.BarcodeConfig.BARCODE_NUM; i++)
            {
                ret += readTextArray[i][1];
            }
            return ret;
        }
        #endregion

        #endregion
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

        #endregion
        #endregion

        private void 接続する(object sender, EventArgs e)
        {
            if (sender.GetType() == this.接続先ToolStripMenuItem.GetType())
            {
                if (!SocketClient.getInstance().ClientStart()) MessageBox.Show("接続に失敗しました");
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
        }

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

        private void onReadStaff(string barcode)
        {
            StaffObject staff = new Database().selectSingle<StaffObject>(string.Format("WHERE barcode = '{0}'", barcode));
            if (staff != null)
            {
                reg_user.Text = staff.name;
                shop_person = staff.name;
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
            public StreamCallback(Form context) : base(context){}
            public override void onReceive(string text)
            {
                MessageBox.Show(text);
            }
            public override void onClose(SocketCloseType closeType)
            {
                MessageBox.Show("接続解除");
            }
        }
    }
}
