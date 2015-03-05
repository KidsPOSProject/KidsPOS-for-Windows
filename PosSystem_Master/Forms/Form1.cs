using System;
using System.Windows.Forms;
using System.Collections;
using System.Data.SQLite;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Net;
using PosSystem.Setting;
using PosSystem.Util;
using PosSystem.Object.Database;
using PosSystem_Master.Source;
using PosSystem_Master.Forms;

namespace PosSystem_Master
{
    public partial class Form1 : Form
    {
        const bool DEBUG = false;
        #region 定数

        //フォームの名前
        public string form_name = "POSシステム 練習用クライアント";

        public const string config_file = "config.csv";

        //変数
        public static string item_sum = "";
        public static string item_list = "";

        public static string shop_person = "";

        #endregion
        #region HashTableなど

        //バーコード関係の変数

        //読み取った数値格納
        public static string[] input = new string[PosSystem.Setting.BarcodeConfig.BARCODE_NUM];

        //現在読み取っている数値の場所
        public int input_count = 0;

        public static int sumItemPrice = 0;

        #endregion
        public Form1()
        {
            InitializeComponent();
            InitializeListView(readItemList);
            PosInformation.getInstance().init(this, new StoreObject("デパート"));
            SocketServer.getInstance().init(new StreamCallback(this));

            this.KeyPreview = !this.KeyPreview;
            reg_goods_list_SizeChanged(readItemList, new EventArgs());
            this.WindowState = FormWindowState.Maximized;

            this.Text = form_name;

            //this.MaximizeBox = false;
            this.MinimizeBox = false;
            //this.ControlBox = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (!load_settings())
            {
                MessageBox.Show("設定ファイルが間違っています。","エラー",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
            }
        }

        #region Initialize
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
            readItemList.Items.Clear();
            sumItemPrice = 0;
            tSumItemPrice.Text = "0";
            lScanItemName.Text = "";
            lScanItemPrice.Text = "";
        }
        #endregion


        public void onReadItem(string itemBarcode)
        {
            ItemObject item = new Database().selectSingle<ItemObject>(string.Format("WHERE barcode = '{0}'", itemBarcode));
            if (item != null)
            {
                lScanItemName.Text = item.name;
                lScanItemPrice.Text = item.price.ToString();
                readItemList.Items.Add(new ListViewItem(new string[] { (item.id.ToString()), item.name, "1", item.price.ToString(), "×" }));
                sumItemPrice += item.price;
                tSumItemPrice.Text = sumItemPrice.ToString();
            }
            else
            {
                debug_Test.Text = "登録されていない商品です。";
            }
        }
        #region Event

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
            debug_Test.Text = readItemList.SelectedItems[0].SubItems[1].Text;
        }
        private void bAccount_Click(object sender, EventArgs e)
        {
            if (tSumItemPrice.Text != "" && tSumItemPrice.Text != "0")
            {
                for (int i = 0; i < readItemList.Items.Count; i++)
                {
                    item_list += readItemList.Items[i].SubItems[0].Text + ((i != readItemList.Items.Count - 1) ? "," : "");
                }
                Account ac = new Account(readItemList);
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

        string[] aaa = new string[15];

        //バーコードが入力されたとき
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (key_check(e) || input_count == PosSystem.Setting.BarcodeConfig.BARCODE_NUM) init_input();

            input[input_count] = e.KeyCode.ToString();
            input_count++;

            if (input_count == PosSystem.Setting.BarcodeConfig.BARCODE_NUM)
            {
                string temp_barcode = comb_input_barcode();

                Database db = new Database();

                switch (temp_barcode[PosSystem.Setting.BarcodeConfig.PREFIX.Length].ToString() +
                    temp_barcode[PosSystem.Setting.BarcodeConfig.PREFIX.Length + 1].ToString())
                {

                    case PosSystem.Setting.BarcodeConfig.ITEM:
                        onReadItem(temp_barcode);
                        break;

                    case PosSystem.Setting.BarcodeConfig.SALE:
                        Sales sl = new Sales(db.selectSingle<SaleObject>(string.Format("WHERE barcode = '{0}'", temp_barcode)));
                        sl.ShowDialog(this);
                        sl.Dispose();

                        break;

                    //従業員のバーコードを読み込んだとき
                    case PosSystem.Setting.BarcodeConfig.STAFF:
                        onReadStaff(temp_barcode);
                        break;

                    //商品リストを読み込んだ時
                    case PosSystem.Setting.BarcodeConfig.ITEM_LIST:

                        ItemList il = new ItemList();
                        il.ShowDialog(this);
                        il.Dispose();

                        break;

                    //売上リストを読み込んだ時
                    case PosSystem.Setting.BarcodeConfig.SALE_LIST:
                        
                        Sales_List sll = new Sales_List();
                        sll.ShowDialog(this);
                        sll.Dispose();
                        
                        break;

                    //会計を読み込んだ時
                    case PosSystem.Setting.BarcodeConfig.ACCOUNT:
                        if (tSumItemPrice.Text != "" && tSumItemPrice.Text != "0")
                        {
                            for (int i = 0; i < readItemList.Items.Count; i++)
                            {
                                item_list += readItemList.Items[i].SubItems[0].Text + ((i != readItemList.Items.Count - 1) ? "," : "");
                            }
                            
                            Account ac = new Account(readItemList);
                            ac.ShowDialog(this);
                            ac.Dispose();
                            
                        }
                        //売上テーブルにインサート処理

                        InitializeREG();
                        break;

                    //ツールバー表示を読み込んだ時
                    case PosSystem.Setting.BarcodeConfig.SHOW_TOOLBAR:
                            top_menu.Visible = true;
                        break;

                    //ツールバー非表示を読み込んだ時
                    case PosSystem.Setting.BarcodeConfig.HIDE_TOOLBAR:
                        top_menu.Visible = false;
                        break;

                    //商品リストEditを読み込んだとき
                    case PosSystem.Setting.BarcodeConfig.ITEM_LIST_EDIT:
                         
                        ItemList ile = new ItemList(true);
                        ile.ShowDialog(this);
                        ile.Dispose();
                        
                        break;
                    default:
                        break;
                }
            }
        }

        public void init_input()
        {
            input = new string[PosSystem.Setting.BarcodeConfig.BARCODE_NUM];
            input_count = 0;
        }
        public bool key_check(KeyEventArgs e)
        {
            string input_text = e.KeyCode.ToString();
            //プリフィックスの長さ分だけ見るのよ

            if (PosSystem.Setting.BarcodeConfig.PREFIX.Length > input_count)
            {
                if (
                    !input_text.Equals("D" + PosSystem.Setting.BarcodeConfig.PREFIX[input_count])
                    )
                {
                    return true;
                }
            }
            return false;
        }

        public string comb_input_barcode()
        {
            string ret = "";
            for (int i = 0; i < PosSystem.Setting.BarcodeConfig.BARCODE_NUM; i++)
            {
                ret += input[i][1];
            }
            return ret;
        }
        #endregion

        #endregion

        #region ツールメニュー

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

        private void 商品リストEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ItemList il = new ItemList(true);
            il.ShowDialog();
            il.Dispose();
        }

        #endregion

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            serverStop();
        }

        private void サーバーを建てるToolStripMenuItem_Click(object sender, EventArgs e)
        {
            serverStart();
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
                        new Database().insert<StaffObject>(new StaffObject( data[2],data[1]));
                        break;
                    default:
                        break;
                }
            }
            public override void onClose(SocketCloseType closeType)
            {
                MessageBox.Show("接続解除");
            }
        }
        public bool load_settings()
        {

            if (File.Exists(config_file))
            {
                ArrayList al = new ArrayList();

                TextFieldParser parser = new TextFieldParser(config_file, System.Text.Encoding.GetEncoding("Shift_JIS"));
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    string[] row = parser.ReadFields();
                    foreach (string field in row)
                    {
                        string f = field;
                        f = f.Replace("\r\n", "n");
                        f = f.Replace(" ", "");
                        if (!(f == "")) al.Add(f);
                    }
                }
                if (al.Count > 0)
                {
                    for (int i = 0; i < al.Count; i++)
                    {
                        if (al[i].ToString().StartsWith("#store_number"))
                        {
                            int storeNum = int.Parse(al[i + 1].ToString());
                            StoreObject store = new Database().selectSingle<StoreObject>(string.Format("WHERE id = '{0}'", storeNum));
                            if(store == null) return false;
                            PosInformation.getInstance().setStore(store);
                            disp_store_name.Text = store.name;
                            return true;
                        }
                    }
                }
            }
            else
            {
                using (var sw = new StreamWriter(config_file, false, Encoding.GetEncoding("Shift_JIS")))
                {
                    sw.WriteLine("#store_number,0");
                }
                MessageBox.Show("設定ファイルが見つかりませんでした。"+Environment.NewLine+"新しくファイルを作成しました。");
                this.Close();
            }
            return false;
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
        }

        private void serverStart()
        {
            if (!SocketServer.getInstance().ServerStart()) MessageBox.Show("サーバ起動に失敗しました。");
            else
            {
                this.Text += " サーバー起動中";
                サーバーを建てるToolStripMenuItem.Enabled = false;
                接続者確認ToolStripMenuItem.Enabled = true;
            }
        }
        private void serverStop()
        {
            SocketServer.getInstance().CloseServer();
            接続者確認ToolStripMenuItem.Enabled = false;
        }

        private void 接続者確認ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            foreach (SocketServer.ClientHandler c in SocketServer.getInstance().getClient())
            {
                builder.Append(c.ClientHandle.ToString()).Append(Environment.NewLine);
            }
            MessageBox.Show(builder.ToString());
        }

    }
}
