using System;
using System.Data;
using System.Windows.Forms;
using PosSystem.Object.Database;
using PosSystem.Util;
using PosSystem.Setting;

namespace PosSystem_Client
{
    public partial class ItemList : Form
    {
        private DataTable table = new DataTable();

        public ItemList()
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

        private void ItemList_Load(object sender, EventArgs e)
        {
            new Database().insertView<ItemObject>(table,
                "SELECT il.barcode AS バーコード,il.name AS 商品名,il.price AS 値段,sk.name AS お店 FROM " 
                + TableList.ITEM + " AS il," 
                + TableList.ITEM_GENRE + " AS ig," + TableList.STORE +" AS sk WHERE il.genre = ig.id AND il.shop = sk.id;");
        }
    }
}
