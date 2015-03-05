using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Drawing.Printing;
using PosSystem.Object.Database;
using PosSystem.Util;

namespace PosSystem_Client
{
    public partial class Item_List : Form
    {
        string selected_item_barcode = "";

        public Item_List()
        {
            InitializeComponent();
            InitializeListView(reg_goods_list);

            this.MaximizeBox = !this.MaximizeBox;
            this.MinimizeBox = !this.MinimizeBox;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

        }

        private void Item_List_Load(object sender, EventArgs e)
        {
            InsertListView();
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
            goods_id.Tag = 1;
            goods_id.TextAlign = HorizontalAlignment.Center;

            goods_order.Text = "バーコード";
            goods_order.Tag = 4;
            goods_order.TextAlign = HorizontalAlignment.Center;

            goods_item.Text = "商品名";
            goods_item.Tag = 5;
            goods_item.TextAlign = HorizontalAlignment.Center;

            goods_price.Text = "金額";
            goods_price.Tag = 2;
            goods_price.TextAlign = HorizontalAlignment.Right;

            ColumnHeader[] colHeaderRegValue = { goods_id, goods_item,goods_order,  goods_price };
            listview.Columns.AddRange(colHeaderRegValue);
        }

        public void InsertListView()
        {
            reg_goods_list.Items.Clear();
            List<ItemObject> item = new Database().selectMulti<ItemObject>();
            foreach (ItemObject obj in item)
            {
                reg_goods_list.Items.Add(new ListViewItem(new string[] { obj.id.ToString(), obj.barcode, obj.name, obj.price.ToString() }));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            for (int index = 0; index < printDocument1.PrinterSettings.PaperSizes.Count; index++)
            {
                if (printDocument1.PrinterSettings.PaperSizes[index].PaperName.Contains("A4") == true)
                {
                    printDocument1.DefaultPageSettings.PaperSize = printDocument1.PrinterSettings.PaperSizes[index];
                    break;
                }
            }

            PrintDialog pdlg = new PrintDialog();
            printDocument1.DocumentName = selected_item_barcode;
            pdlg.Document = printDocument1;
            if (pdlg.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void Item_List_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) this.Close();
        }
    }
}
