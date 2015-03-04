using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Printing;

using PosSystem.Util;
using PosSystem.Setting;
using PosSystem.Object.Database;
using PosSystem.Object;

namespace DBRegister
{
    public partial class Registed_List : Form
    {
        List<PrintItemObject> selectedItem = new List<PrintItemObject>();

        public Registed_List()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private DataTable dataTable = new DataTable();
        private DataTable dataTable2 = new DataTable();
        private DataTable dataTable3 = new DataTable();
        private DataTable dataTable4 = new DataTable();
        protected override void OnLoad(EventArgs e)
        {
            dataGridView1.DataSource = dataTable;
            dataGridView2.DataSource = dataTable2;
            dataGridView3.DataSource = dataTable3;
            dataGridView4.DataSource = dataTable4;
            base.OnLoad(e);
        }
        private void Registed_List_Load(object sender, EventArgs e)
        {
            Database db = new Database();
            db.insertView<ItemObject>(dataTable,
                "SELECT il.id,il.barcode AS バーコード,sk.name AS お店, il.name AS 商品名,ig.name AS ジャンル,il.price AS 値段 FROM " 
                + TableList.ITEM + " AS il," 
                + TableList.ITEM_GENRE + " AS ig," + TableList.STORE +" AS sk WHERE il.genre = ig.id AND il.shop = sk.id;");
            db.insertView<ItemObject>(dataTable2);
            db.insertView<ItemGenreObject>(dataTable3);
            db.insertView<StoreObject>(dataTable4);
        }

        int count = 0;
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Print.getInstance().printItemBarcode(selectedItem[count], print_grid.Checked, e);
            bool b = count != selectedItem.Count - 1;
            e.HasMorePages = b;
            count++;
            if (!b) count = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            selectedItem.Clear();
            List<int> li = new List<int>();
            Database db = new Database();
            foreach (DataGridViewCell r in dataGridView2.SelectedCells)
            {
                if(0 > li.BinarySearch(r.RowIndex)){
                    int index = r.RowIndex;
                    li.Add(r.RowIndex);
                    li.Sort();

                    StoreObject store = db.find_store(int.Parse(dataGridView2.Rows[index].Cells[4].Value.ToString()));

                    selectedItem.Add(
                        new PrintItemObject(
                            dataGridView2.Rows[index].Cells[1].Value.ToString(),
                            dataGridView2.Rows[index].Cells[2].Value.ToString(),
                            store.name));
                }
            }
            if (selectedItem.Count > 0)
            {
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                PrintDialog pdlg = new PrintDialog();
                pdlg.Document = pd;
                if (pdlg.ShowDialog() == DialogResult.OK) pd.Print();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            selectedItem.Clear();
            Database db = new Database();
            foreach (DataGridViewRow r in dataGridView2.Rows)
            {
                if (r.Cells[1].Value == null) break;
                StoreObject store = db.find_store(int.Parse(r.Cells[4].Value.ToString()));
                selectedItem.Add(
                    new PrintItemObject(
                        r.Cells[1].Value.ToString(),
                        r.Cells[2].Value.ToString(),
                        store.name));
            }
            if (selectedItem.Count > 0)
            {
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

                PrintDialog pdlg = new PrintDialog();
                pdlg.Document = pd;
                if (pdlg.ShowDialog() == DialogResult.OK) pd.Print();
            }
        }
    }
}
