using System;
using System.Windows.Forms;
using System.Collections;
using System.Data.SQLite;
using System.IO;
using System.Xml.Serialization;

namespace PosSystem_Master
{
    public partial class Form1 : Form
    {

        #region 定数

        //フォームの名前
        public string form_name = "POSシステム 練習用クライアント";

        //後々サーバーと通信する
        public static string store_num = "001";
        public static string store_name = "デパート";
        //public static string store_kind = "食品";

        //変数
        public static string db_file_item = "KidsDB-ITEM.db";
        public static string db_file_staff = "KidsDB-STAFF.db";
        public static string db_file_master = "KidsDB-STORE.db";
        public static string item_sum = "";
        public static string item_list = "";

        public static string shop_person = "";


        #endregion
        #region HashTableなど

        Hashtable day_of_week = new Hashtable();
        public static Hashtable genre = new Hashtable();

        //バーコード関係の変数

        //読み取った数値格納
        public static string[] input = new string[BarCode_Prefix.BARCODE_NUM];

        //現在読み取っている数値の場所
        public int input_count = 0;

        public static int reg_item_price_sum = 0;

        #endregion
        public Form1()
        {
            InitializeComponent();
            InitializeValues();
            InitializeListView(reg_goods_list);
            this.KeyPreview = !this.KeyPreview;
            reg_goods_list_SizeChanged(reg_goods_list, new EventArgs());
            this.WindowState = FormWindowState.Maximized;

            this.Text = form_name;

            //this.MaximizeBox = false;
            this.MinimizeBox = false;
            //this.ControlBox = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            CreateTable();
            disp_store_name.Text = Form1.store_name;
        }

        #region Initialize
        public void InitializeValues(){
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
            scan_goods_name.Text = "";
            scan_goods_price.Text = "";
        }
        public void CreateTable()
        {
            if (!File.Exists(db_file_item))
            {
                using (var conn = new SQLiteConnection("Data Source=" + db_file_item))
                {
                    conn.Open();
                    using (SQLiteCommand command = conn.CreateCommand())
                    {
                        //商品ジャンルリスト
                        command.CommandText = "create table item_genre(id INTEGER  PRIMARY KEY AUTOINCREMENT, name TEXT, store TEXT)";
                        command.ExecuteNonQuery();

                        //商品リスト
                        command.CommandText = "create table item_list(id INTEGER  PRIMARY KEY AUTOINCREMENT, barcode INTEGER UNIQUE, name TEXT, price INTEGER, shop TEXT, genre TEXT)";
                        command.ExecuteNonQuery();

                        //売上リスト
                        command.CommandText = "create table sales_list(id INTEGER  PRIMARY KEY AUTOINCREMENT, buycode TEXT UNIQUE, created_at TEXT, points INTEGER, price INTEGER, items TEXT)";
                        command.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
            if (!File.Exists(db_file_staff))
            {
                using (var conn = new SQLiteConnection("Data Source=" + db_file_staff))
                {
                    conn.Open();
                    using (SQLiteCommand command = conn.CreateCommand())
                    {
                        //商品リスト
                        command.CommandText = "create table staff_list(id INTEGER  PRIMARY KEY AUTOINCREMENT, barcode TEXT UNIQUE, name TEXT)";
                        command.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
            if (!File.Exists(db_file_master))
            {
                using (var conn = new SQLiteConnection("Data Source=" + db_file_master))
                {
                    conn.Open();
                    using (SQLiteCommand command = conn.CreateCommand())
                    {
                        //商品リスト 現在、お店の名前は完全ユニークだと考えています。
                        command.CommandText = "create table store_kind(id INTEGER  PRIMARY KEY AUTOINCREMENT, name TEXT)";
                        command.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                Item_Regist ir = new Item_Regist();
                ir.ShowDialog();
                ir.Dispose();
            }

        }
        #endregion

        public void empty_item_database()
        {
            atsumi_pos.read_count_num(db_file_master, "store_kind");
        }

        //スキャンしたときの処理
        public void scan_goods(string item_num)
        {
            string[] data = read_items(item_num);
            if (data[0] != "" && data[1] != "" && data[2] != "")
            {
                //TODO item_num をデータベースから検索し表示
                string read_items_id = data[0];
                string read_items_name = data[1];
                string read_items_price = data[2];

                scan_goods_name.Text = read_items_name;
                scan_goods_price.Text = read_items_price;

                string[] item1 = {read_items_id, scan_goods_name.Text, "1", scan_goods_price.Text, "×" };
                reg_goods_list.Items.Add(new ListViewItem(item1));

                reg_item_price_sum += int.Parse(scan_goods_price.Text);

                reg_goods_sum.Text = reg_item_price_sum.ToString();
            }
            else
            {
                debug_Test.Text = "登録されていない商品です。";
            }
            //debug_Test.Text = reg_goods_list.Items.Count.ToString();
        }
        
        /// <summary>
        /// 商品リストから商品データを読み込む
        /// </summary>
        /// <param name="barcode">読み取ったバーコード</param>
        /// <returns>バーコード、商品名、値段</returns>
        public static string[] read_items(string barcode)
        {
            string[] ret = {"","",""};
            using (var conn = new SQLiteConnection("Data Source=" + db_file_item))
            {
                conn.Open();
                using (SQLiteCommand command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT id,name,price FROM item_list WHERE barcode ='" +barcode + "'";

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ret[0] = reader.GetInt32(0).ToString();
                        ret[1] = reader.GetString(1);
                        ret[2] = reader.GetInt32(2).ToString();
                    }
                }
            }
            return ret;
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

            if (key_check(e) || input_count == BarCode_Prefix.BARCODE_NUM) init_input();

            input[input_count] = e.KeyCode.ToString();
            input_count++;

            if (input_count == BarCode_Prefix.BARCODE_NUM)
            {
                string temp_barcode = comb_input_barcode();
                
                switch (temp_barcode[BarCode_Prefix.PREFIX.Length].ToString()+
                    temp_barcode[BarCode_Prefix.PREFIX.Length+1].ToString())
                {
                    
                    case BarCode_Prefix.ITEM:
                        scan_goods(temp_barcode);
                        break;
                    
                    case BarCode_Prefix.SALE:
                        
                        Sales sl = new Sales(temp_barcode);
                        sl.ShowDialog(this);
                        sl.Dispose();

                        break;

                    //従業員のバーコードを読み込んだとき
                    case BarCode_Prefix.STAFF:
                        string[,] ret = atsumi_pos.find_user(Form1.db_file_staff, temp_barcode);
                        if (ret[0, 0] != "")
                        {
                            reg_user.Text = ret[0, 2];
                            shop_person = ret[0, 2];
                        }
                        break;

                    //商品リストを読み込んだ時
                    case BarCode_Prefix.ITEM_LIST:
                        
                        Item_List il = new Item_List();
                        il.ShowDialog(this);
                        il.Dispose();
                        
                        break;

                    //売上リストを読み込んだ時
                    case BarCode_Prefix.SALE_LIST:
                        
                        Sales_List sll = new Sales_List();
                        sll.ShowDialog(this);
                        sll.Dispose();
                        
                        break;

                    //会計を読み込んだ時
                    case BarCode_Prefix.ACCOUNT:
                        if (reg_goods_sum.Text != "" && reg_goods_sum.Text != "0")
                        {
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
                        break;

                    //スタッフリストを読み込んだ時
                    case BarCode_Prefix.STAFF_LIST:
                        
                        Staff_List stf = new Staff_List();
                        stf.ShowDialog(this);
                        stf.Dispose();
                        
                        break;

                    //ツールバー表示を読み込んだ時
                    case BarCode_Prefix.SHOW_TOOLBAR:
                            top_menu.Visible = true;
                        break;

                    //ツールバー非表示を読み込んだ時
                    case BarCode_Prefix.HIDE_TOOLBAR:
                        top_menu.Visible = false;
                        break;

                    //ダミーアイテムを読み込んだ時
                    case BarCode_Prefix.DUMMY_ITEM:
                        Barcode bc = new Barcode(BarCode_Prefix.ITEM, Form1.store_num, "00000");
                        string[] data = read_items(bc.show());
                        if (data[0] == "")
                        {
                            atsumi_pos.Insert(new atsumi_pos.ItemTable(bc.show(), "十文字のダミーデータ", "100", "デパート", "00000"));
                        }
                        break;

                    //ダミーユーザーを読み込んだ時
                    case BarCode_Prefix.DUMMY_USER:
                        atsumi_pos.regist_user("千葉 商太郎");
                        break;
                    
                    //商品リストEditを読み込んだとき
                    case BarCode_Prefix.ITEM_LIST_EDIT:
                         
                        Item_List ile = new Item_List(true);
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
            //配列の初期化
            input = new string[BarCode_Prefix.BARCODE_NUM];
            //参照要素ナンバーの初期化
            input_count = 0;
        }
        public bool key_check(KeyEventArgs e)
        {
            string input_text = e.KeyCode.ToString();
            //プリフィックスの長さ分だけ見るのよ

            if (BarCode_Prefix.PREFIX.Length > input_count){ 
                 if(
                     !input_text.Equals("D" + BarCode_Prefix.PREFIX[input_count])
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
            for (int i = 0; i < BarCode_Prefix.BARCODE_NUM - 1; i++)
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
            System.Drawing.Printing.PrintDocument pd =
                new System.Drawing.Printing.PrintDocument();
            pd.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(print_template.print_system_barcode);
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

        private void 商品リストEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Item_List il = new Item_List(true);
            il.ShowDialog();
            il.Dispose();
        }

        private void ユーザリストToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Staff_List sl = new Staff_List();
            sl.ShowDialog(this);
            sl.Dispose();
        }

        private void ユーザーToolStripMenuItem_Click(object sender, EventArgs e)
        {
            atsumi_pos.regist_user("千葉 商太郎");
        }

        private void 商品ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Barcode bc = new Barcode(BarCode_Prefix.ITEM, Form1.store_num, "00000");
            string[] data = read_items(bc.show());
            if (data[0] == "")
            {
                atsumi_pos.Insert(new atsumi_pos.ItemTable(bc.show(), "十文字のダミーデータ", "100", "デパート", "00000"));
            }
        }
        #endregion


    }
}
