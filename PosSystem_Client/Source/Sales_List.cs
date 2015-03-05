using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PosSystem.Object.Database;
using PosSystem.Util;
using PosSystem.Setting;
using PosSystem.Object;

namespace PosSystem_Client
{
    public partial class Sales_List : Form
    {
        private DataTable table = new DataTable();
        List<SaleObject> list = new List<SaleObject>();
        public Sales_List()
        {
            InitializeComponent();
            this.MaximizeBox = !this.MaximizeBox;
            this.MinimizeBox = !this.MinimizeBox;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            mGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            mGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }
        protected override void OnLoad(EventArgs e)
        {
            mGridView.DataSource = table;
            base.OnLoad(e);
        }

        private void Sales_List_Load(object sender, EventArgs e)
        {
            Database db = new Database();
            db.insertView<SaleObject>(table," SELECT barcode, created_at, price, points FROM " + TableList.SALE);
            list = db.selectMulti<SaleObject>(string.Format("WHERE store = '{0}'", Config.getInstance().store.id));
            turn_over.Text = calc_turnover();
        }

        public string calc_turnover()
        {
            int sum = 0;
            foreach (SaleObject item in list)
            {
                sum += item.price;
            }
            return sum.ToString();
        }
        private void Sales_List_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) this.Close();
        }

        private void mGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Sales sl = new Sales(
                new Database().selectSingle<SaleObject>(
                string.Format("WHERE barcode = '{0}'", mGridView[0, mGridView.CurrentCell.RowIndex].Value.ToString())));
            sl.ShowDialog(this); 
            sl.Dispose();
        }

    }
}
