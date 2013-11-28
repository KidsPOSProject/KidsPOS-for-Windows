using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace PosSystem
{
    public partial class Account_change : Form
    {
        string form_name = "おつり";

        public Account_change(string _rec_money, string _rec_points, string _rec_items)
        {
            InitializeComponent();

            this.MaximizeBox = !this.MaximizeBox;
            this.MinimizeBox = !this.MinimizeBox;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            reg_goods_sum.Text = Form1.reg_item_price_sum.ToString();
            received_money.Text = _rec_money;
            change.Text =   (int.Parse(received_money.Text) - int.Parse(reg_goods_sum.Text)).ToString();
            practice_status.Text = (Form1.isPractice) ? "練習モードなので売上は記録されません。" : "";

            if (!Form1.isPractice)
            {
                Insert(new SalesTable(
                    Form1.BARCODE_PREFIX + Form1.store_num + atsumi_pos.read_count_num(Form1.db_file, "sales_list").ToString("D3"),
                    (Unix_Time.ToUnixTime(DateTime.Now)).ToString(),
                    _rec_points,
                    Form1.reg_item_price_sum.ToString(),
                    _rec_items));
            }
            Form1.change_form_text(this, form_name);
        }

        private void Account_change_Load(object sender, EventArgs e)
        {

        }

        //売上のテーブル
        private class SalesTable
        {
            public string id = null;
            public string buycode;
            public string registdated_at;
            public string points;
            public string price;
            public string items;

            public SalesTable(string _buycode, string _registdated_at, string _points, string _price,string _items)
            {
                buycode = _buycode;
                registdated_at = _registdated_at;
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
                using (var conn = new SQLiteConnection("Data Source=" + Form1.db_file))
                {
                    conn.Open();
                    using (SQLiteTransaction sqlt = conn.BeginTransaction())
                    {
                        using (SQLiteCommand command = conn.CreateCommand())
                        {
                            string query = string.Format("INSERT INTO sales_list (buycode,registdated_at,points,price,items) VALUES('{0}','{1}','{2}','{3}','{4}')",
                                st.buycode,
                                st.registdated_at,
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
                return false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
