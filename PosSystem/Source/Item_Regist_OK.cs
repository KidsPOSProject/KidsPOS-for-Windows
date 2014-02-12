﻿using System;
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

            Barcode bar = new Barcode(BarCode_Prefix.ITEM, Form1.store_num, atsumi_pos.read_count_num(Form1.db_file, "item_list").ToString("D5"));

            reg_barcode = bar.show();

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
                if (atsumi_pos.Insert(new atsumi_pos.ItemTable(reg_barcode,reg_item_name,reg_item_price.ToString(),Form1.store_num.ToString())))
                {
                    regist_status_text.Text = "商品の登録に成功しました。";
                }
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



        private void Item_Regist_OK_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void image_barcode_Click(object sender, EventArgs e)
        {

        }

    }
}
