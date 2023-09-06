using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Windows.Forms;
using KidsPos.Object;
using KidsPos.Object.Database;
using KidsPos.Setting;
using KidsPos.Util;

namespace DBRegister
{
    public partial class RegistedList : Form
    {
        private readonly DataTable _dataTable = new DataTable();
        private readonly DataTable _dataTable2 = new DataTable();
        private readonly DataTable _dataTable3 = new DataTable();
        private readonly DataTable _dataTable4 = new DataTable();
        private readonly List<PrintItemObject> _selectedItem = new List<PrintItemObject>();

        private int _count;

        public RegistedList()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MinimizeBox = false;
            MaximizeBox = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        protected override void OnLoad(EventArgs e)
        {
            dataGridView1.DataSource = _dataTable;
            dataGridView2.DataSource = _dataTable2;
            dataGridView3.DataSource = _dataTable3;
            dataGridView4.DataSource = _dataTable4;
            base.OnLoad(e);
        }

        private void Registed_List_Load(object sender, EventArgs e)
        {
            var db = new Database();
            db.InsertView<ItemObject>(_dataTable,
                "SELECT il.id,il.barcode AS バーコード,sk.Name AS お店, il.Name AS 商品名,ig.Name AS ジャンル,il.price AS 値段 FROM "
                + TableList.Item + " AS il,"
                + TableList.ItemGenre + " AS ig," + TableList.Store +
                " AS sk WHERE il.genre = ig.id AND il.shop = sk.id;");
            db.InsertView<ItemObject>(_dataTable2);
            db.InsertView<ItemGenreObject>(_dataTable3);
            db.InsertView<StoreObject>(_dataTable4);
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Print.GetInstance().PrintItemBarcode(_selectedItem[_count], print_grid.Checked, e);
            var b = _count != _selectedItem.Count - 1;
            e.HasMorePages = b;
            _count++;
            if (!b) _count = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _selectedItem.Clear();
            var li = new List<int>();
            var db = new Database();
            foreach (DataGridViewCell r in dataGridView2.SelectedCells)
                if (0 > li.BinarySearch(r.RowIndex))
                {
                    var index = r.RowIndex;
                    li.Add(r.RowIndex);
                    li.Sort();

                    var store = db.find_store(int.Parse(dataGridView2.Rows[index].Cells[4].Value.ToString()));

                    _selectedItem.Add(
                        new PrintItemObject(
                            dataGridView2.Rows[index].Cells[1].Value.ToString(),
                            dataGridView2.Rows[index].Cells[2].Value.ToString(),
                            store.Name));
                }

            if (_selectedItem.Count > 0)
            {
                var pd = new PrintDocument();
                pd.PrintPage += printDocument1_PrintPage;
                var pdlg = new PrintDialog();
                pdlg.Document = pd;
                if (pdlg.ShowDialog() == DialogResult.OK) pd.Print();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _selectedItem.Clear();
            var db = new Database();
            foreach (DataGridViewRow r in dataGridView2.Rows)
            {
                if (r.Cells[1].Value == null) break;
                var store = db.find_store(int.Parse(r.Cells[4].Value.ToString()));
                _selectedItem.Add(
                    new PrintItemObject(
                        r.Cells[1].Value.ToString(),
                        r.Cells[2].Value.ToString(),
                        store.Name));
            }

            if (_selectedItem.Count > 0)
            {
                var pd = new PrintDocument();
                pd.PrintPage += printDocument1_PrintPage;

                var pdlg = new PrintDialog();
                pdlg.Document = pd;
                if (pdlg.ShowDialog() == DialogResult.OK) pd.Print();
            }
        }
    }
}