using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace ItemRegister
{
    public partial class Registed_List : Form
    {
        bool isFirst_item = true;
        bool isFirst_store = true;
        bool isFirst_genre = true;
        public Registed_List()
        {
            InitializeComponent();
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
            insert_view(dataTable, Form1.db_file_pos, "item_list", 
                "SELECT il.id,il.barcode AS バーコード,sk.name AS お店, il.name AS 商品名,ig.name AS ジャンル,il.price AS 値段 FROM item_list AS il,item_genre AS ig,store_kind AS sk WHERE il.genre = ig.id AND il.shop = sk.id;");
            insert_view(dataTable2, Form1.db_file_pos, "item_list");
            insert_view(dataTable3, Form1.db_file_pos, "item_genre");
            insert_view(dataTable4, Form1.db_file_pos, "store_kind");
        }
        public void insert_view(DataTable dt,string _source,string _table,string _query ="")
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=" + _source))

                if (_query == "")
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM " + _table + " " + _query, con))
                    {
                        adapter.Fill(dt);
                    }
                }
                else
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(_query, con))
                    {
                        adapter.Fill(dt);
                    }
                }
        }
    }
}
