using System;
using System.Data;
using System.Windows.Forms;
using KidsPos.Sources.Database;
using KidsPos.Sources.Setting;
using KidsPos.Sources.Util;

namespace PosSystem.Sources
{
    public partial class ItemList : Form
    {
        private readonly DataTable _table = new DataTable();

        public ItemList()
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

        private void ItemList_Load(object sender, EventArgs e)
        {
            new Database().InsertView<ItemObject>(_table,
                "SELECT il.barcode AS バーコード,il.Name AS 商品名,il.price AS 値段,sk.Name AS お店 FROM "
                + TableList.Item + " AS il,"
                + TableList.ItemGenre + " AS ig," + TableList.Store +
                " AS sk WHERE il.genre = ig.id AND il.shop = sk.id;");
        }
    }
}