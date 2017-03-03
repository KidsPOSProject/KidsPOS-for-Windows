using System;
using System.Windows.Forms;
using KidsPos.Object.Database;
using KidsPos.Setting;
using KidsPos.Util;

namespace PosSystem.Source
{
    public partial class StaffRegistWindow : Form
    {
        private readonly string _barcode;
        public StaffRegistWindow(string barcode)
        {
            InitializeComponent();
            MaximizeBox = !MaximizeBox;
            MinimizeBox = !MinimizeBox;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            _barcode = barcode;
        }

        private void Staff_Regist_Load(object sender, EventArgs e)
        {
            label2.Text = @"スタッフ名を追加します。"+ Environment.NewLine +@"入力が終わったらEnterキー。";
            textBox2.Text = _barcode;
        }
        private void Staff_Regist_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && textBox1.Text != "")
            {
                var result = MessageBox.Show(@"このなまえでとうろくしますか？"+Environment.NewLine+textBox1.Text, @"読み込みエラー", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    StaffObject staff = new StaffObject(_barcode, textBox1.Text);
                    SocketClient.GetInstance().RegistUser(staff);
                    PosInformation.GetInstance().SetStaff(staff);
                    Close();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
