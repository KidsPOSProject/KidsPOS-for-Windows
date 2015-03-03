using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace DBRegister
{
    public partial class RegistedUser : Form
    {
        public RegistedUser()
        {
            InitializeComponent();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private DataTable dataTable = new DataTable();

        protected override void OnLoad(EventArgs e)
        {
            dataGridView1.DataSource = dataTable;
            base.OnLoad(e);
        }
        private void RegistedUser_Load(object sender, EventArgs e)
        {
            DataBase db = DataBase.getInstance();
            insert_view(dataTable, DataBase.DBPath.STAFF, DataBase.TableList.STAFF);
        }
        public void insert_view(DataTable dt, string _source, string _table, string _query = "")
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
