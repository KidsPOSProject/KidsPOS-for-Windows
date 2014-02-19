using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Collections;

namespace PosSystem_Master
{
    public partial class Sales : Form
    {
        string buycode = "";
        public Sales(string _buycode)
        {
            InitializeComponent();
            InitializeListView(sales_list);
            this.MaximizeBox = !this.MaximizeBox;
            this.MinimizeBox = !this.MinimizeBox;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            sales_list_SizeChanged(sales_list, new EventArgs());
            this.buycode = _buycode;
        }

        private void Sales_Load(object sender, EventArgs e)
        {
            ArrayList sdr = read_sales(buycode);
            if (sdr.Count == 0)
            {
                MessageBox.Show("無効な売り上げIDです。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Question);
                this.Close();
            }
            buy_time.Text = Unix_Time.FromUnixTime(long.Parse(sdr[2].ToString())).ToLocalTime().ToString();
            scan_goods(sdr[5].ToString().Split(','));
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
            goods_id.Width = 100;
            goods_id.Tag = 1;
            goods_id.TextAlign = HorizontalAlignment.Center;

            goods_order.Text = "商品名";
            goods_order.Width = 430;
            goods_order.Tag = 4;
            goods_order.TextAlign = HorizontalAlignment.Center;

            goods_item.Text = "個数";
            goods_item.Width = 150;
            goods_item.Tag = 1;
            goods_item.TextAlign = HorizontalAlignment.Center;

            goods_price.Text = "金額";
            goods_price.Width = 150;
            goods_price.Tag = 2;
            goods_price.TextAlign = HorizontalAlignment.Right;

            ColumnHeader[] colHeaderRegValue = { goods_id, goods_order, goods_item, goods_price };
            listview.Columns.AddRange(colHeaderRegValue);
        }
        public static string[] read_items(int item_id)
        {
            string[] ret = { "", "", "" };
            using (var conn = new SQLiteConnection("Data Source=" + Form1.db_file_item))
            {
                conn.Open();
                using (SQLiteCommand command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT id,name,price FROM item_list WHERE id ='" + item_id + "'";

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

        public void scan_goods(string[] item_num)
        {
            int bad_item = 0;

            for (int i = 0; i < item_num.Length; i++)
            {
                string[] data = read_items(int.Parse(item_num[i]));

                if (data[0] != "" && data[1] != "" && data[2] != "")
                {
                    //TODO item_num をデータベースから検索し表示
                    string read_items_id = data[0];
                    string read_items_name = data[1];
                    string read_items_price = data[2];

                    string[] item1 = { read_items_id, read_items_name, "1", read_items_price, "×" };
                    sales_list.Items.Add(new ListViewItem(item1));
                }
                else
                {
                    bad_item++;
                }
            }
            if (bad_item > 0) MessageBox.Show("現在は登録されていない商品がありました", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private ArrayList read_sales(string buycode)
        {
            ArrayList al = new ArrayList();
            using (var conn = new SQLiteConnection("Data Source=" + Form1.db_file_item))
            {
                conn.Open();
                using (SQLiteCommand command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM sales_list WHERE buycode ='" + buycode + "'";

                    SQLiteDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        al.Add(reader.GetInt32(0).ToString());
                        al.Add(reader.GetString(1));
                        al.Add(reader.GetString(2));
                        al.Add(reader.GetInt32(3).ToString());
                        al.Add(reader.GetInt32(4).ToString());
                        al.Add(reader.GetString(5));
                    }
                }
                conn.Close();
            }
            return al;
        }

        private void sales_list_SizeChanged(object sender, EventArgs e)
        {

        }

        private void Sales_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) this.Close();
        }
    }
}
