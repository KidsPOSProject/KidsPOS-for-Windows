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

        SocketClient cn;

        #endregion
        #region HashTableなど

        Hashtable day_of_week = new Hashtable();
        public static Hashtable genre = new Hashtable();
        public static Hashtable ip_list = new Hashtable();

        //バーコード関係の変数

        //読み取った数値格納
        public static string[] input = new string[PosSystem.Setting.Barcode.BARCODE_NUM];

        //現在読み取っている数値の場所
        public int input_count = 0;

        public static int reg_item_price_sum = 0;

        #endregion
        public Form1()
        {
            InitializeComponent();
            InitializeValues();
            InitializeListView(reg_goods_list);

            PosInformation.getInstance().init(new StoreObject("デパート") , "");

            this.KeyPreview = !this.KeyPreview;
            reg_goods_list_SizeChanged(reg_goods_list, new EventArgs());
            take_mode.Enabled = false;
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
            #region HashTable day_or_week
            day_of_week.Add(DayOfWeek.Sunday, "日");
            day_of_week.Add(DayOfWeek.Monday, "月");
            day_of_week.Add(DayOfWeek.Tuesday, "火");
            day_of_week.Add(DayOfWeek.Wednesday, "水");
            day_of_week.Add(DayOfWeek.Thursday, "木");
            day_of_week.Add(DayOfWeek.Friday, "金");
            day_of_week.Add(DayOfWeek.Saturday, "土");
            #endregion

            genre.Add("食品", 01);
            genre.Add("汎用", 02);


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
            reg_goods_list.Items.Clear();
            reg_item_price_sum = 0;
            reg_goods_sum.Text = "0";
            lScanItemName.Text = "";
            lScanItemPrice.Text = "";
        }

        #region DB_SHORI
        //スキャンしたときの処理
        public void scan_goods(string itemBarcode)
        {
            ItemObject item = new Database().selectSingle<ItemObject>(string.Format("WHERE barcode = '{0}'", itemBarcode));
            if (item != null)
            {
                lScanItemName.Text = item.name;
                lScanItemPrice.Text = item.price.ToString();
                reg_goods_list.Items.Add(new ListViewItem(new string[] { (item.id.ToString("D4")), item.name, "1", item.price.ToString(), "×" }));
                reg_item_price_sum += item.price;
                reg_goods_sum.Text = reg_item_price_sum.ToString();
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
            //if(isActive) this.Activate();
            DateTime dt = DateTime.Now;
            disp_now_time.Text = dt.ToString("yyyy年MM月dd日(" + day_of_week[dt.DayOfWeek] + ") HH時mm分ss秒");
        }        
        
        private void reg_clear_Click(object sender, EventArgs e)
        {
            InitializeREG();
        }
        private void reg_goods_list_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            debug_Test.Text = reg_goods_list.SelectedItems[0].SubItems[1].Text;
        }
        private void reg_account_Click(object sender, EventArgs e)
        {
            if (reg_goods_sum.Text != "" && reg_goods_sum.Text != "0")
            {
                // 売上用のアイテムリスト生成
                for (int i = 0; i < reg_goods_list.Items.Count; i++)
                {
                    item_list += reg_goods_list.Items[i].SubItems[0].Text + ((i != reg_goods_list.Items.Count - 1) ? "," : "");
                }
                Account ac = new Account(reg_goods_list);
                ac.ShowDialog(this);
                ac.Dispose();
            }
            //売上テーブルにインサート処理

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
            if (key_check(e) || input_count == PosSystem.Setting.Barcode.BARCODE_NUM) init_input();

            input[input_count] = e.KeyCode.ToString();
            input_count++;

            if (input_count == PosSystem.Setting.Barcode.BARCODE_NUM)
            {
                string temp_barcode = comb_input_barcode();
                
                switch (temp_barcode[PosSystem.Setting.Barcode.PREFIX.Length].ToString()+
                    temp_barcode[PosSystem.Setting.Barcode.PREFIX.Length+1].ToString())
                {
                    
                    case PosSystem.Setting.Barcode.ITEM:
                        scan_goods(temp_barcode);
                        break;

                    case PosSystem.Setting.Barcode.SALE:
                        
                        Sales sl = new Sales(temp_barcode);
                        sl.ShowDialog(this);
                        sl.Dispose();

                        break;

                    //従業員のバーコードを読み込んだとき
                    case PosSystem.Setting.Barcode.STAFF:
                        StaffObject staff = new Database().selectSingle<StaffObject>(string.Format("WHERE barcode = '{0}'", temp_barcode));
                        if (staff != null)
                        {
                            reg_user.Text = staff.name;
                            shop_person = staff.name;
                        }
                        else
                        {
                            StaffRegistWindow staffRegist = new StaffRegistWindow(cn,temp_barcode);
                            staffRegist.ShowDialog(this);
                            staffRegist.Dispose();
                        }
                        break;

                    //商品リストを読み込んだ時
                    case PosSystem.Setting.Barcode.ITEM_LIST:
                        
                        Item_List il = new Item_List();
                        il.ShowDialog(this);
                        il.Dispose();
                        
                        break;

                    //売上リストを読み込んだ時
                    case PosSystem.Setting.Barcode.SALE_LIST:
                        
                        Sales_List sll = new Sales_List();
                        sll.ShowDialog(this);
                        sll.Dispose();
                        
                        break;

                    //会計を読み込んだ時
                    case PosSystem.Setting.Barcode.ACCOUNT:
                        if (reg_goods_sum.Text != "" && reg_goods_sum.Text != "0")
                        {
                            for (int i = 0; i < reg_goods_list.Items.Count; i++)
                            {
                                // 売上用のアイテムリスト生成
                                item_list += reg_goods_list.Items[i].SubItems[0].Text + ((i != reg_goods_list.Items.Count - 1) ? "," : "");
                            }
                            
                            Account ac = new Account(reg_goods_list);
                            ac.ShowDialog(this);
                            ac.Dispose();
                        }

                        InitializeREG();
                        break;

                    //スタッフリストを読み込んだ時
                    case PosSystem.Setting.Barcode.STAFF_LIST:
                        
                        Staff_List stf = new Staff_List();
                        stf.ShowDialog(this);
                        stf.Dispose();
                        
                        break;

                    //ツールバー表示を読み込んだ時
                    case PosSystem.Setting.Barcode.SHOW_TOOLBAR:
                            top_menu.Visible = true;
                        break;

                    //ツールバー非表示を読み込んだ時
                    case PosSystem.Setting.Barcode.HIDE_TOOLBAR:
                        top_menu.Visible = false;
                        break;
                }
            }
        }

        public void init_input()
        {
            //配列の初期化
            input = new string[PosSystem.Setting.Barcode.BARCODE_NUM];
            //参照要素ナンバーの初期化
            input_count = 0;
        }
        public bool key_check(KeyEventArgs e)
        {
            string input_text = e.KeyCode.ToString();
            //プリフィックスの長さ分だけ見るのよ

            if (PosSystem.Setting.Barcode.PREFIX.Length > input_count){ 
                 if(
                     !input_text.Equals("D" + PosSystem.Setting.Barcode.PREFIX[input_count])
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
            for (int i = 0; i < PosSystem.Setting.Barcode.BARCODE_NUM; i++)
            {
                ret += input[i][1];
            }
            return ret;
        }
        #endregion

        #endregion

        #region ツールメニュー
        private void 印刷ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(Print.getInstance().printSystemBarcode);
            pd.Print();
        }

        private void 商品リストToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Item_List il = new Item_List();
            il.ShowDialog();
            il.Dispose();
        }

        private void 売上リストToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sales_List sl = new Sales_List();
            sl.ShowDialog();
            sl.Dispose();
        }

        private void ユーザリストToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Staff_List sl = new Staff_List();
            sl.ShowDialog(this);
            sl.Dispose();
        }
        #endregion

        #endregion
        #endregion

        private void 接続する(object sender, EventArgs e)
        {
            if (sender.GetType() == this.接続先ToolStripMenuItem.GetType())
            {
                ToolStripItem tsi = (ToolStripItem)sender;

                SocketClient _cn = new SocketClient(new SocketListener());
                if (!_cn.ClientStart()) MessageBox.Show("接続に失敗しました");
                else
                {
                    this.Text += tsi.Text + " へ接続中";
                    for (int i = 0; i < 接続先ToolStripMenuItem.DropDownItems.Count; i++)
                    {
                        接続先ToolStripMenuItem.DropDownItems[i].Enabled = false;
                    }
                    cn = _cn;
                }

            }
        }
        public void change()
        {
            this.Text = form_name + " 接続が解除されました。";
            for (int i = 0; i < 接続先ToolStripMenuItem.DropDownItems.Count; i++)
            {
                接続先ToolStripMenuItem.DropDownItems[i].Enabled = true;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            display_timer.Enabled = false;
            if (cn != null) cn.StopSock();
        }

    }
}
