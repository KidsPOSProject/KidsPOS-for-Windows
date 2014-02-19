using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PosSystem_Master
{
    public partial class Staff_Regist : Form
    {
        string isBarcode = "";
        public Staff_Regist(string _isBarcode = "")
        {
            InitializeComponent();
            this.MaximizeBox = !this.MaximizeBox;
            this.MinimizeBox = !this.MinimizeBox;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            isBarcode = _isBarcode;
        }

        private void Staff_Regist_Load(object sender, EventArgs e)
        {
            label2.Text = "スタッフを追加します。"+ Environment.NewLine +"入力が終わったらEnterキー。";
            if (isBarcode != "")
            {
                textBox2.Visible = true;
                textBox2.Text = isBarcode;
                label3.Visible = true;
            }
        }
        private void Staff_Regist_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && textBox1.Text != "")
            {
                DialogResult result = MessageBox.Show("このなまえでとうろくしますか？"+Environment.NewLine+textBox1.Text, "かくにん", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    string res = "";

                    if (isBarcode != "") res = atsumi_pos.regist_user(textBox1.Text, textBox2.Text);
                    else res = atsumi_pos.regist_user(textBox1.Text);
                    
                    if (res != "")
                    {
                        isBarcode = res;
                        print_template.check_default_printer(true);
                        this.printPreviewDialog1.ShowDialog();
                    }else{
                        MessageBox.Show("なんらかの原因で登録できませんでした。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
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
            print_template.print_user(isBarcode, textBox1.Text, e);
        }


    }
}
