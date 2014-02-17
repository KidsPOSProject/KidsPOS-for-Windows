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

namespace PosSystem
{
    public partial class Item_List : Form
    {
        int selected_item_id = -1;
        string selected_item_barcode = "";

        public Item_List()
        {
            InitializeComponent();
            InitializeListView(reg_goods_list);

            this.MaximizeBox = !this.MaximizeBox;
            this.MinimizeBox = !this.MinimizeBox;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            reg_goods_list_SizeChanged(reg_goods_list, new EventArgs());
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

            ColumnHeader[] colHeaderRegValue = { goods_id, goods_order, goods_item, goods_price };
            listview.Columns.AddRange(colHeaderRegValue);
        }

        public void InsertListView()
        {
            reg_goods_list.Items.Clear();
            string[,] st = atsumi_pos.read_item_list(Form1.db_file_item);
            for (int i = 0; i < st.GetLength(0); i++)
            {
                reg_goods_list.Items.Add(new ListViewItem(new string[] { st[i, 0], st[i, 1], st[i, 2], st[i, 3] }));
            }
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

        private void reg_goods_list_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {

            //reg_goods_list_SizeChanged(reg_goods_list, new EventArgs());
        }

        private void reg_goods_list_MouseClick(object sender, MouseEventArgs e)
        {
            selected_item_id = int.Parse(reg_goods_list.SelectedItems[0].SubItems[0].Text);
            selected_item_barcode = reg_goods_list.SelectedItems[0].SubItems[1].Text;
            change_item_name.Text = reg_goods_list.SelectedItems[0].SubItems[2].Text;
            change_item_price.Text = reg_goods_list.SelectedItems[0].SubItems[3].Text;
            change_item_name.Enabled = true;
            change_item_price.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = true;
        }

        private class ItemTable
        {
            public string id = null;
            public string name;
            public string price;

            public ItemTable(string _id, string _name, string _price)
            {
                id = _id;
                name = _name;
                price = _price;
            }
        }
        private bool Update(ItemTable it)
        {
            try
            {
                using (var conn = new SQLiteConnection("Data Source=" + Form1.db_file_item))
                {
                    conn.Open();
                    using (SQLiteTransaction sqlt = conn.BeginTransaction())
                    {
                        using (SQLiteCommand command = conn.CreateCommand())
                        {
                            string query = string.Format("UPDATE item_list SET name = '{0}', price = '{1}' WHERE id = '{2}'",
                            it.name,it.price,it.id);

                            command.CommandText = query;
                            command.ExecuteNonQuery();
                        }
                        sqlt.Commit();
                    }
                    conn.Close();
                }
                return true;
            }
            catch (Exception e)
            {
                System_log.ShowDialog(e.ToString());
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (change_item_name.Text != "" && change_item_price.Text != "")
            {
                Update(new ItemTable(selected_item_id.ToString(), change_item_name.Text, change_item_price.Text));
                InsertListView();
                change_item_name.Text = "";
                change_item_price.Text = "";
                change_item_name.Enabled = false;
                change_item_price.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            print_template.print_temple(selected_item_barcode, change_item_name.Text, e);
        }
        private void button2_Click(object sender, EventArgs e)
        {

            this.printPreviewDialog1.ShowDialog();
        }
    }
}
