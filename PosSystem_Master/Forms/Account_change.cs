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
using PosSystem.Setting;
using PosSystem.Object;

namespace PosSystem_Master
{
    public partial class Account_change : Form
    {
        ListView item_list = null;
        SaleObject sale;

        public Account_change(string _rec_money, ListView _rec_points, string _rec_items)
        {
            InitializeComponent();

            this.MaximizeBox = !this.MaximizeBox;
            this.MinimizeBox = !this.MinimizeBox;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            reg_goods_sum.Text = Form1.sumItemPrice.ToString();
            received_money.Text = _rec_money;
            change.Text =   (int.Parse(received_money.Text) - int.Parse(reg_goods_sum.Text)).ToString();

            item_list = _rec_points;

            sale = new SaleObject(
                _rec_points.Items.Count,
                Form1.sumItemPrice,
                _rec_items,
                Config.getInstance().store.id,
                PosInformation.getInstance().getStaffBarcode());
            new Database().insert<SaleObject>(sale);
        }

        private void Account_change_Load(object sender, EventArgs e)
        {
            //レシートの印刷

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

            PrintDialog pdlg = new PrintDialog();
            pdlg.Document = pd;
            pd.Print();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ひだり みぎ うえ した
            printDocument1.DefaultPageSettings.Margins = new Margins(0, 300, 0, 0);
            printDocument1.OriginAtMargins = true;
            
            this.printPreviewDialog1.ShowDialog();
            
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Print.getInstance().printReceipt(item_list, received_money.Text, e, sale.barcode);
        }

        private void Account_change_KeyDown(object sender, KeyEventArgs e)
        {
            this.Close();
        }
    }
}
