using System;
using System.Drawing.Printing;
using System.Windows.Forms;
using KidsPos.Sources;
using KidsPos.Sources.Database;
using KidsPos.Sources.Setting;
using KidsPos.Sources.Util;

namespace PosSystem.Sources
{
    public partial class AccountChange : Form
    {
        private readonly ListView _itemList;
        private readonly SaleObject _sale;

        public AccountChange(string recMoney, ListView recPoints, string recItems)
        {
            InitializeComponent();

            MaximizeBox = !MaximizeBox;
            MinimizeBox = !MinimizeBox;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            reg_goods_sum.Text = Form1.RegItemPriceSum.ToString();
            received_money.Text = recMoney;
            change.Text = (int.Parse(received_money.Text) - int.Parse(reg_goods_sum.Text)).ToString();

            _itemList = recPoints;

            _sale = new SaleObject(
                recPoints.Items.Count,
                Form1.RegItemPriceSum,
                recItems,
                Config.GetInstance().Store.Id,
                PosInformation.GetInstance().GetStaffBarcode());
            new Database().Insert(_sale);
        }

        private void Account_change_Load(object sender, EventArgs e)
        {
            if (!Config.IsClient && Config.IsPrintEnable)
            {
                var pd = new PrintDocument();
                pd.PrintPage += printDocument1_PrintPage;
                var pdlg = new PrintDialog();
                pdlg.Document = pd;
                pd.Print();
            }
        }

        private void Account_change_KeyDown(object sender, KeyEventArgs e)
        {
            Close();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Print.GetInstance().PrintReceipt(_itemList, received_money.Text, e, _sale.Barcode);
        }
    }
}