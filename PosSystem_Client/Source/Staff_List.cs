using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PosSystem_Client
{
    public partial class Staff_List : Form
    {
        public Staff_List()
        {
            InitializeComponent();
            this.MaximizeBox = !this.MaximizeBox;
            this.MinimizeBox = !this.MinimizeBox;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            InitializeListView(reg_staff_list);
        }

        private void Staff_List_Load(object sender, EventArgs e)
        {
            reg_staff_list.Items.Clear();
            string[,] st = atsumi_pos.read_staff_list(Form1.db_file_staff);
            for (int i = 0; i < st.GetLength(0); i++)
            {
                reg_staff_list.Items.Add(new ListViewItem(new string[] { st[i, 0], st[i, 1], st[i, 2] }));
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
            ColumnHeader staff_id = new ColumnHeader();
            ColumnHeader staff_code = new ColumnHeader();
            ColumnHeader staff_name = new ColumnHeader();

            staff_id.Text = "ID";
            staff_id.Tag = 1;
            staff_id.Width = 70;
            staff_id.TextAlign = HorizontalAlignment.Center;

            staff_code.Text = "バーコード";
            staff_code.Tag = 4;
            staff_code.Width = 250;
            staff_code.TextAlign = HorizontalAlignment.Center;

            staff_name.Text = "なまえ";
            staff_name.Tag = 5;
            staff_name.Width = 400;
            staff_name.TextAlign = HorizontalAlignment.Center;

            ColumnHeader[] colHeaderRegValue = { staff_id, staff_code, staff_name };
            listview.Columns.AddRange(colHeaderRegValue);
        }
        private void reg_staff_list_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void Staff_List_KeyDown(object sender, KeyEventArgs e)
        {
            this.Close();
        }
    }
}
