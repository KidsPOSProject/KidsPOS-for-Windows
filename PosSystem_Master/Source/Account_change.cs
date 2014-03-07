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

namespace PosSystem_Master
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

            Barcode bc = new Barcode(BarCode_Prefix.SALE,Form1.store_num,atsumi_pos.read_count_num(Form1.db_file_item, "sales_list").ToString("D5"));
            barcode = bc.show();
            Insert(new SalesTable(bc.show(),
                (Unix_Time.ToUnixTime(DateTime.Now)).ToString(),
                _rec_points.Items.Count.ToString(),
                Form1.reg_item_price_sum.ToString(),
                _rec_items));
        }

        private void Account_change_Load(object sender, EventArgs e)
        {
            //レシートの印刷

            System.Drawing.Printing.PrintDocument pd = new System.Drawing.Printing.PrintDocument();
            pd.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printDocument1_PrintPage);

            PrintDialog pdlg = new PrintDialog();
            pdlg.Document = pd;
            pd.Print();
        }

        //売上のテーブル
        private class SalesTable
        {
            public string id = null;
            public string buycode;
            public string created_at;
            public string points;
            public string price;
            public string items;

            public SalesTable(string _buycode, string _created_at, string _points, string _price,string _items)
            {
                buycode = _buycode;
                created_at = _created_at;
                points = _points;
                price = _price;
                items = _items;
            }
        }
        //データベースにインサート
        private bool Insert(SalesTable st)
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
                            string query = string.Format("INSERT INTO sales_list (buycode,created_at,points,price,items) VALUES('{0}','{1}','{2}','{3}','{4}')",
                                st.buycode,
                                st.created_at,
                                st.points,
                                st.price,
                                st.items);
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
            
            print_template.print_receipt(item_list, received_money.Text, e, barcode);
        }

        private void Account_change_KeyDown(object sender, KeyEventArgs e)
        {
            this.Close();
        }
    }
}
