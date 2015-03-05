﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Collections;
using PosSystem.Util;
using PosSystem.Object.Database;

namespace PosSystem_Client
{
    public partial class Sales : Form
    {
        string buycode = "";
        public Sales(string _buycode)
        {
            InitializeComponent();
            InitializeListView(sales_list);
            this.MaximizeBox = !this.MaximizeBox;
            this.MinimizeBox = !this.MinimizeBox;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.buycode = _buycode;
        }

        private void Sales_Load(object sender, EventArgs e)
        {
            //id, barcode,created_at,points,price,items,store
            SaleObject sale = new Database().selectSingle<SaleObject>(string.Format("where barcode = '{0}'", buycode));
            if (sale == null)
            {
                MessageBox.Show("無効な売り上げIDです。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Question);
                this.Close();
            }
            buy_time.Text = UnixTime.FromUnixTime(long.Parse(sale.createdAt)).ToLocalTime().ToString();
            scan_goods(sale.items.Split(','));
        }
        public static void InitializeListView(ListView listview)
        {
            // ListViewコントロールのプロパティを設定
            listview.FullRowSelect = true;
            listview.GridLines = true;
            listview.Sorting = SortOrder.Ascending;
            listview.View = View.Details;

            // 列（コラム）ヘッダの作成
            ColumnHeader goods_id = new ColumnHeader();
            ColumnHeader goods_order = new ColumnHeader();
            ColumnHeader goods_item = new ColumnHeader();
            ColumnHeader goods_price = new ColumnHeader();

            goods_id.Text = "ID";
            goods_id.Width = 100;
            goods_id.Tag = 1;
            goods_id.TextAlign = HorizontalAlignment.Center;

            goods_order.Text = "商品名";
            goods_order.Width = 430;
            goods_order.Tag = 4;
            goods_order.TextAlign = HorizontalAlignment.Center;

            goods_item.Text = "個数";
            goods_item.Width = 150;
            goods_item.Tag = 1;
            goods_item.TextAlign = HorizontalAlignment.Center;

            goods_price.Text = "金額";
            goods_price.Width = 150;
            goods_price.Tag = 2;
            goods_price.TextAlign = HorizontalAlignment.Right;

            ColumnHeader[] colHeaderRegValue = { goods_id, goods_order, goods_item, goods_price };
            listview.Columns.AddRange(colHeaderRegValue);
        }

        public void scan_goods(string[] item_num)
        {
            int bad_item = 0;
            Database db = new Database();
            for (int i = 0; i < item_num.Length; i++)
            {
                ItemObject item = db.selectSingle<ItemObject>(string.Format("where id = '{0}'", item_num[i]));
                if (item != null)
                {
                    sales_list.Items.Add(new ListViewItem(new string[] {item.id.ToString(), item.name, "1", item.price.ToString(), "×"}));
                }
                else
                {
                    bad_item++;
                }
            }
            if (bad_item > 0) MessageBox.Show("現在は登録されていない商品がありました", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Sales_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) this.Close();
        }
    }
}
