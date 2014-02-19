using System;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Drawing.Printing;
using ZXing;

namespace PosSystem_Master
{
    public partial class Item_Regist_OK : Form
    {        
        string reg_item_price = "";
        string reg_item_name = "";
        string reg_barcode = "";

        string category = "";
        public Item_Regist_OK(string _category, string _name, string _price)
        {
            InitializeComponent();
            reg_item_name = _name;
            reg_item_price = _price;
            category = _category;


            Barcode bar = new Barcode(BarCode_Prefix.ITEM, Form1.store_num, atsumi_pos.read_count_num(Form1.db_file_item, "item_list").ToString("D5"));

            reg_barcode = bar.show();

            ireg_ok_name.Text = _name;
            ireg_ok_price.Text = _price;
            this.MaximizeBox = !this.MaximizeBox;
            this.MinimizeBox = !this.MinimizeBox;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void Item_Regist_OK_Load(object sender, EventArgs e)
        {
            if (atsumi_pos.Insert(new atsumi_pos.ItemTable(reg_barcode,reg_item_name,reg_item_price.ToString(),Form1.store_num.ToString(),"00000")))
            {
                regist_status_text.Text = "商品の登録に成功しました。";
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void print_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

            PrintDialog pdlg = new PrintDialog();
            pdlg.Document = pd;
            if (pdlg.ShowDialog() == DialogResult.OK)
            {
                pd.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            print_template.print_temple(reg_barcode, ireg_ok_name.Text, e);
        }

    }
}
