using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using KidsPos.Object;
using KidsPos.Object.Database;
using KidsPos.Setting;
using KidsPos.Util;

namespace PosSystem.Source
{
    public partial class SalesList : Form
    {
        private readonly DataTable _table = new DataTable();
        private List<SaleObject> _list = new List<SaleObject>();
        public SalesList()
        {
            InitializeComponent();
            MaximizeBox = !MaximizeBox;
            MinimizeBox = !MinimizeBox;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            mGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            mGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }
        protected override void OnLoad(EventArgs e)
        {
            mGridView.DataSource = _table;
            base.OnLoad(e);
        }

        private void Sales_List_Load(object sender, EventArgs e)
        {
            Database db = new Database();
            db.InsertView<SaleObject>(_table,
                $"SELECT barcode, created_at, price, points FROM {TableList.Sale} WHERE store ='{Config.GetInstance().Store.Id}'");
            _list = db.SelectMulti<SaleObject>(string.Format("WHERE store = '{0}'", Config.GetInstance().Store.Id));
            turn_over.Text = calc_turnover();
        }

        public string calc_turnover()
        {
            var sum = 0;
            foreach (SaleObject item in _list)
            {
                sum += item.Price;
            }
            return sum.ToString();
        }

        private void mGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var sl = new Sales(
                new Database().SelectSingle<SaleObject>(
                    $"WHERE barcode = '{mGridView[0, mGridView.CurrentCell.RowIndex].Value}'"));
            sl.ShowDialog(this); 
            sl.Dispose();
        }

    }
}
