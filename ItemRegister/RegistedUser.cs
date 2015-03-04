using System;
using System.Data;
using System.Windows.Forms;
using PosSystem.Util;
using PosSystem.Object.Database;

namespace DBRegister
{
    public partial class RegistedUser : Form
    {
        private DataTable table = new DataTable();
        public RegistedUser()
        {
            InitializeComponent();
            mGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            mGridView.DataSource = table;
            base.OnLoad(e);
        }
        private void RegistedUser_Load(object sender, EventArgs e)
        {
            new Database().insertView<StaffObject>(table);
        }
    }
}
