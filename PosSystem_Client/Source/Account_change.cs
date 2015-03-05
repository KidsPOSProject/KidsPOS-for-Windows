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
using PosSystem.Object;
using PosSystem.Setting;
using PosSystem.Object.Database;
using PosSystem.Util;

namespace PosSystem_Client
{
    public partial class Account_change : Form
    {
        ListView item_list = null;
        string barcode = "";

        public Account_change(string _rec_money, ListView _rec_points, string _rec_items)
        {
            InitializeComponent();

            this.MaximizeBox = !this.MaximizeBox;
            this.MinimizeBox = !this.MinimizeBox;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            reg_goods_sum.Text = Form1.reg_item_price_sum.ToString();
            received_money.Text = _rec_money;
            change.Text =   (int.Parse(received_money.Text) - int.Parse(reg_goods_sum.Text)).ToString();

            item_list = _rec_points;

            new Database().insert<SaleObject>(new SaleObject(
                UnixTime.ToUnixTime(DateTime.Now).ToString(),
                _rec_points.Items.Count,
                Form1.reg_item_price_sum,
                _rec_items,
                PosInformation.getInstance().store.id));
        }

        private void Account_change_Load(object sender, EventArgs e)
        {
        }
        private void Account_change_KeyDown(object sender, KeyEventArgs e)
        {
            this.Close();
        }
    }
}
