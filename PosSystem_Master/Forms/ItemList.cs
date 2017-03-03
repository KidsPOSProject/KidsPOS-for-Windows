using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Drawing.Printing;
using PosSystem.Util;
using PosSystem.Object.Database;
using PosSystem.Setting;

namespace PosSystem_Master
{
    public partial class ItemList : Form
    {
        private ItemObject item;
        private DataTable table = new DataTable();
        public ItemList(bool _isEdit = false)
        {
            InitializeComponent();
            this.MaximizeBox = !this.MaximizeBox;
            this.MinimizeBox = !this.MinimizeBox;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            mGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            mGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            if (_isEdit) edit_panel.Visible = true;
        }
        protected override void OnLoad(EventArgs e)
        {
            mGridView.DataSource = table;
            base.OnLoad(e);
        }

        private void Item_List_Load(object sender, EventArgs e)
        {
            new Database().insertView<ItemObject>(table,
                "SELECT il.barcode AS バーコード,il.name AS 商品名,il.price AS 値段,sk.name AS お店 FROM "
                + TableList.ITEM + " AS il,"
                + TableList.ITEM_GENRE + " AS ig," + TableList.STORE + " AS sk WHERE il.genre = ig.id AND il.shop = sk.id;");
        }

        private void reg_goods_list_MouseClick(object sender, MouseEventArgs e)
        {
            item = getCurrentItem();

            change_item_name.Text = item.name;
            change_item_price.Text = item.price.ToString();
            change_item_name.Enabled = true;
            change_item_price.Enabled = true;
            updateItem.Enabled = true;
        }

        private ItemObject getCurrentItem()
        {
            return new Database().selectSingle<ItemObject>(string.Format("WHERE barcode = '{0}'",
                mGridView[0, mGridView.CurrentCell.RowIndex].Value.ToString()));
        }

        private void updateItem_Click(object sender, EventArgs e)
        {
            if (change_item_name.Text != "" && change_item_price.Text != "")
            {
                new Database().updateItem(item);
                change_item_name.Text = "";
                change_item_price.Text = "";
                change_item_name.Enabled = false;
                change_item_price.Enabled = false;
                updateItem.Enabled = false;
            }
        }

        private void Item_List_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) this.Close();
        }
    }
}
