using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PosSystem.Util;
using PosSystem.Object.Database;
using PosSystem.Setting;

namespace PosSystem_Client
{
    public partial class StaffRegistWindow : Form
    {
        string barcode;
        public StaffRegistWindow(string barcode)
        {
            InitializeComponent();
            this.MaximizeBox = !this.MaximizeBox;
            this.MinimizeBox = !this.MinimizeBox;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.barcode = barcode;
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
                    StaffObject staff = new StaffObject(barcode, textBox1.Text);
                    SocketClient.getInstance().registUser(staff);
                    PosInformation.getInstance().setStaff(staff);
                    this.Close();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
