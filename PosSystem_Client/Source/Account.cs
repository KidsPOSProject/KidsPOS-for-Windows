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
    public partial class Account : Form
    {
        //フォームの名前
        public string form_name = "かいけい";

        ListView main_list;
        public Account(ListView _main_list)
        {
            
            InitializeComponent();
            Form1.InitializeListView(reg_goods_list);
            reg_goods_list_SizeChanged(reg_goods_list, new EventArgs());
            main_list = _main_list;

            this.MaximizeBox = !this.MaximizeBox;
            this.MinimizeBox = !this.MinimizeBox;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            reg_goods_sum.Text = Form1.reg_item_price_sum.ToString();

            this.ActiveControl = this.received_money;

        }

        private void Account_Load(object sender, EventArgs e)
        {
            copy_listview(main_list);
        }
        private void copy_listview(ListView seme)
        {
            for (int icnt = 0; icnt < seme.Items.Count; icnt++)
            {
                ListViewItem items = new ListViewItem();
                items = seme.Items[icnt];
                reg_goods_list.Items.Insert(icnt, (ListViewItem)items.Clone());
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

        private void button1_Click(object sender, EventArgs e)
        {
            bool check = 0 >= (int.Parse(reg_goods_sum.Text) - int.Parse(received_money.Text));
            if (reg_goods_sum.Text != "" && received_money.Text != "" && check)
            {
                Account_change ac = new Account_change(received_money.Text, reg_goods_list, Form1.item_list);
                ac.ShowDialog();
                ac.Dispose();
                this.Dispose();
            }
        }

        private void Account_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }
    }
}
