using System;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Drawing.Printing;
using ZXing;

namespace PosSystem
{
    public partial class Item_Regist_OK : Form
    {        
        string reg_item_price = "";
        string reg_item_name = "";
        string reg_barcode = "";

        public Item_Regist_OK(string _category, string _name, string _price)
        {
            InitializeComponent();
            reg_item_name = _name;
            reg_item_price = _price;

            reg_barcode =
                Form1.BARCODE_PREFIX +
                Form1.store_num +
                int.Parse(Form1.genre[_category].ToString()).ToString("D2") +
                atsumi_pos.read_count_num(Form1.db_file,"item_list").ToString("D3");
            
            reg_barcode = reg_barcode + create_check_digit(reg_barcode);

            ireg_ok_name.Text = _name;
            ireg_ok_price.Text = _price;
            this.MaximizeBox = !this.MaximizeBox;
            this.MinimizeBox = !this.MinimizeBox;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void Item_Regist_OK_Load(object sender, EventArgs e)
        {
            // 4:prifix 3:store_num 2:ganre 3:item 1:check
            if (Form1.isPractice)
            {
                regist_status_text.Text = "練習モードなので\n商品の登録は出来ません。";
            }
            else
            {
                if (Insert(new ItemTable(reg_barcode, reg_item_name, reg_item_price.ToString(), Form1.store_num.ToString())))
                {
                    regist_status_text.Text = "商品の登録に成功しました。";
                }
            }
        }

        //商品のテーブル
        private class ItemTable
        {
            public string id = null;
            public string barcode;
            public string name;
            public string price;
            public string shop;

            public ItemTable(string _barcode, string _name, string _price, string _shop)
            {
                barcode = _barcode;
                name = _name;
                price = _price;
                shop = _shop;
            }
        }
        //データベースにインサート
        private bool Insert(ItemTable it)
        {
            try
            {
                using (var conn = new SQLiteConnection("Data Source=" + Form1.db_file))
                {
                    conn.Open();
                    using (SQLiteTransaction sqlt = conn.BeginTransaction())
                    {
                        using (SQLiteCommand command = conn.CreateCommand())
                        {
                            string query = "insert into item_list (barcode, name, price, shop) values('" + it.barcode + "', '" + it.name + "', '" + it.price + "', '" + it.shop + "')";
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
                return false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void print_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            print_template.print_temple(reg_barcode, ireg_ok_name.Text, e);
        }

        private string create_check_digit(string barcode)
        {
            int even = 0;
            int odd = 0;

            for (int i = 0; i < barcode.Length; i++)
            {
                if (i == 0 || i % 2 == 0) odd += int.Parse(barcode[i].ToString());
                else even += int.Parse(barcode[i].ToString());
            }

            int check_digit = 10 - (even * 3 + odd) % 10; if (check_digit == 10) check_digit = 0;

            return check_digit.ToString();
        }

        private void Item_Regist_OK_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void image_barcode_Click(object sender, EventArgs e)
        {

        }

    }
}
