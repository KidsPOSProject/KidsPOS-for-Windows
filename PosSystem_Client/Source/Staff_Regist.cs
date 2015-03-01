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
    public partial class Staff_Regist : Form
    {
        string barcode;
        Connect cn;
        public Staff_Regist(Connect _cn, string barcode)
        {
            InitializeComponent();
            this.MaximizeBox = !this.MaximizeBox;
            this.MinimizeBox = !this.MinimizeBox;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.barcode = barcode;
            cn = _cn;
        }

        private void Staff_Regist_Load(object sender, EventArgs e)
        {
            label2.Text = "スタッフ名を追加します。"+ Environment.NewLine +"入力が終わったらEnterキー。";
            textBox2.Text = barcode;
        }
        private void Staff_Regist_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && textBox1.Text != "")
            {
                DialogResult result = MessageBox.Show("このなまえでとうろくしますか？"+Environment.NewLine+textBox1.Text, "かくにん", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    atsumi_pos.regist_user(cn, textBox1.Text, barcode);
                    this.Close();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            print_template.print_user(barcode, textBox1.Text, e);
        }


    }
}
