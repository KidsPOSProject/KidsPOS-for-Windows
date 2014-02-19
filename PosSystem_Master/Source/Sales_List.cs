using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PosSystem_Master
{
    public partial class Sales_List : Form
    {
        public Sales_List()
        {
            InitializeComponent();
            InitializeListView(reg_goods_list);
            this.MaximizeBox = !this.MaximizeBox;
            this.MinimizeBox = !this.MinimizeBox;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            reg_goods_list_SizeChanged(reg_goods_list, new EventArgs());
        }

        private void Sales_List_Load(object sender, EventArgs e)
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
            ColumnHeader goods_barcode = new ColumnHeader();
            ColumnHeader goods_id = new ColumnHeader();
            ColumnHeader goods_order = new ColumnHeader();
            ColumnHeader goods_price = new ColumnHeader();

            goods_barcode.Text = "バーコード";
            goods_barcode.Tag = 3;
            goods_barcode.TextAlign = HorizontalAlignment.Center;

            goods_id.Text = "日時";
            goods_id.Tag = 6;
            goods_id.TextAlign = HorizontalAlignment.Center;

            goods_order.Text = "金額";
            goods_order.Tag = 2;
            goods_order.TextAlign = HorizontalAlignment.Center;

            goods_price.Text = "点数";
            goods_price.Tag = 2;
            goods_price.TextAlign = HorizontalAlignment.Right;

            ColumnHeader[] colHeaderRegValue = {goods_barcode, goods_id, goods_order,  goods_price };
            listview.Columns.AddRange(colHeaderRegValue);
        }

        public void InsertListView()
        {
            reg_goods_list.Items.Clear();
            string[,] st = atsumi_pos.read_sales_list(Form1.db_file_item);
            for (int i = 0; i < st.GetLength(0); i++)
            {
                reg_goods_list.Items.Add(new ListViewItem(new string[] {st[i,0], Unix_Time.FromUnixTime(long.Parse(st[i, 1])).ToLocalTime().ToString(), st[i, 2], st[i, 3] }));
            }
            turn_over.Text = calc_turnover();
        }

        public string calc_turnover()
        {
            int sum = 0;
            for (int i = 0; i < reg_goods_list.Items.Count; i++)
            {
                sum += int.Parse(reg_goods_list.Items[i].SubItems[2].Text);
            }
            return sum.ToString();
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void reg_goods_list_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void reg_goods_list_DoubleClick(object sender, EventArgs e)
        {

            Sales sl = new Sales(reg_goods_list.SelectedItems[0].SubItems[0].Text);
            sl.ShowDialog(this);
            sl.Dispose();
        }

        private void Sales_List_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) this.Close();
        }
        
    }
}
