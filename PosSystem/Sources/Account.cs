using System;
using System.Windows.Forms;

namespace PosSystem.Source
{
    public partial class Account : Form
    {
        private readonly ListView _mainList;

        //フォームの名前
        public string FormName = "かいけい";

        public Account(ListView mainList)
        {
            InitializeComponent();
            Form1.InitializeListView(reg_goods_list);
            reg_goods_list_SizeChanged(reg_goods_list, new EventArgs());
            _mainList = mainList;

            MaximizeBox = !MaximizeBox;
            MinimizeBox = !MinimizeBox;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            reg_goods_sum.Text = Form1.RegItemPriceSum.ToString();

            ActiveControl = received_money;
        }

        private void Account_Load(object sender, EventArgs e)
        {
            copy_listview(_mainList);
        }

        private void copy_listview(ListView seme)
        {
            for (var icnt = 0; icnt < seme.Items.Count; icnt++)
            {
                var items = new ListViewItem();
                items = seme.Items[icnt];
                reg_goods_list.Items.Insert(icnt, (ListViewItem)items.Clone());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var check = 0 >= int.Parse(reg_goods_sum.Text) - int.Parse(received_money.Text);
            if (reg_goods_sum.Text != "" && received_money.Text != "" && check)
            {
                var ac = new AccountChange(received_money.Text, reg_goods_list, Form1.ItemList);
                Form1.ItemList = "";
                ac.ShowDialog();
                ac.Dispose();
                Dispose();
            }
        }

        private void Account_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button1.PerformClick();
        }

        #region リストのカラム幅調整

        private bool _resizing;

        private void reg_goods_list_SizeChanged(object sender, EventArgs e)
        {
            if (!_resizing)
            {
                _resizing = true;

                var listView = sender as ListView;
                if (listView != null)
                {
                    float totalColumnWidth = 0;

                    for (var i = 0; i < listView.Columns.Count; i++)
                        totalColumnWidth += Convert.ToInt32(listView.Columns[i].Tag);

                    for (var i = 0; i < listView.Columns.Count; i++)
                    {
                        var colPercentage = Convert.ToInt32(listView.Columns[i].Tag) / totalColumnWidth;
                        listView.Columns[i].Width = (int)(colPercentage * listView.ClientRectangle.Width);
                    }
                }
            }

            _resizing = false;
        }

        #endregion
    }
}