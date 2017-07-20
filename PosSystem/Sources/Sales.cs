using System;
using System.Windows.Forms;
using KidsPos.Object.Database;
using KidsPos.Util;

namespace PosSystem.Source
{
    public partial class Sales : Form
    {
        private readonly SaleObject _sale;
        public Sales(SaleObject sale)
        {
            InitializeComponent();
            InitializeListView(sales_list);
            MaximizeBox = !MaximizeBox;
            MinimizeBox = !MinimizeBox;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            _sale = sale;
        }

        private void Sales_Load(object sender, EventArgs e)
        {
            buy_time.Text = _sale.CreatedAt;
            scan_goods(_sale.Items.Split(new[]{","}, StringSplitOptions.RemoveEmptyEntries));
            StaffObject staff = new Database().SelectSingle<StaffObject>(string.Format("WHERE barcode = '{0}'", _sale.StaffId));
            if(staff != null) sale_staff_name.Text = staff.Name;
        }
        public static void InitializeListView(ListView listview)
        {
            // ListViewコントロールのプロパティを設定
            listview.FullRowSelect = true;
            listview.GridLines = true;
            listview.Sorting = SortOrder.Ascending;
            listview.View = View.Details;

            // 列（コラム）ヘッダの作成
            var goodsId = new ColumnHeader();
            var goodsOrder = new ColumnHeader();
            var goodsItem = new ColumnHeader();
            var goodsPrice = new ColumnHeader();

            goodsId.Text = "ID";
            goodsId.Width = 100;
            goodsId.Tag = 1;
            goodsId.TextAlign = HorizontalAlignment.Center;

            goodsOrder.Text = "商品名";
            goodsOrder.Width = 430;
            goodsOrder.Tag = 4;
            goodsOrder.TextAlign = HorizontalAlignment.Center;

            goodsItem.Text = "個数";
            goodsItem.Width = 150;
            goodsItem.Tag = 1;
            goodsItem.TextAlign = HorizontalAlignment.Center;

            goodsPrice.Text = "金額";
            goodsPrice.Width = 150;
            goodsPrice.Tag = 2;
            goodsPrice.TextAlign = HorizontalAlignment.Right;

            ColumnHeader[] colHeaderRegValue = { goodsId, goodsOrder, goodsItem, goodsPrice };
            listview.Columns.AddRange(colHeaderRegValue);
        }

        public void scan_goods(string[] itemNum)
        {
            Database db = new Database();
            for (var i = 0; i < itemNum.Length; i++)
            {
                ItemObject item = db.SelectSingle<ItemObject>($"where id = '{itemNum[i]}'");
                if (item != null)
                {
                    sales_list.Items.Add(new ListViewItem(new[] {item.Id.ToString(), item.Name, "1", item.Price.ToString(), "×"}));
                }
                else
                { 
                     MessageBox.Show(@"現在は登録されていない商品がありました
 [" + itemNum[i] + "]", @"読み込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }
    }
}
