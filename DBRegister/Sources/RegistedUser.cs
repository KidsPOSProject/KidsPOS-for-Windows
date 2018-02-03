﻿using System;
using System.Data;
using System.Windows.Forms;
using KidsPos.Sources.Database;
using KidsPos.Sources.Util;

namespace DBRegister.Sources
{
    public partial class RegistedUser : Form
    {
        private readonly DataTable _table = new DataTable();

        public RegistedUser()
        {
            InitializeComponent();
            mGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            MinimizeBox = false;
            MaximizeBox = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            mGridView.DataSource = _table;
            base.OnLoad(e);
        }

        private void RegistedUser_Load(object sender, EventArgs e)
        {
            new Database().InsertView<StaffObject>(_table);
        }
    }
}