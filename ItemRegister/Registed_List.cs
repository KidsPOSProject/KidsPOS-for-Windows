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

        int count = 0;
        int print_page_num;

        string[,] selected_barcode;

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


        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }


        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (print_page_num == count -1)
            {
                print_template.print_temple(
                    selected_barcode[print_page_num, 0],
                    selected_barcode[print_page_num, 1],
                    selected_barcode[print_page_num, 2],
                    print_grid,
                    e);

                // 印刷終了を指定
                e.HasMorePages = false;

            }else{

                print_template.print_temple(
                    selected_barcode[print_page_num, 0],
                    selected_barcode[print_page_num, 1],
                    selected_barcode[print_page_num, 2],
                    print_grid,
                    e);
                // 印刷継続を指定
                e.HasMorePages = true;

            }
            print_page_num++;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            count = 0;
            print_page_num = 0;

            selected_barcode = new string[dataGridView2.SelectedCells.Count, 3];

            //重複して印刷しないようにする。
            List<int> li = new List<int>();

            foreach (DataGridViewCell r in dataGridView2.SelectedCells)
            {
                if(0 > li.BinarySearch(r.RowIndex)){
                    int index = r.RowIndex;

                    li.Add(r.RowIndex);
                    //バイナリサーチ用にソート
                    li.Sort();

                    string[,] store_data = atsumi_pos.find_store(Form1.db_file_pos, int.Parse(dataGridView2.Rows[index].Cells[4].Value.ToString()));

                    selected_barcode[count, 0] = dataGridView2.Rows[index].Cells[1].Value.ToString();
                    selected_barcode[count, 1] = dataGridView2.Rows[index].Cells[2].Value.ToString();
                    selected_barcode[count, 2] = store_data[0,1];

                    count++;
                }
            }
            if (count > 0)
            {

                //PrintDocumentオブジェクトの作成
                System.Drawing.Printing.PrintDocument pd =
                    new System.Drawing.Printing.PrintDocument();
                //PrintPageイベントハンドラの追加
                pd.PrintPage +=
                    new System.Drawing.Printing.PrintPageEventHandler(printDocument1_PrintPage);

                //PrintDialogクラスの作成
                PrintDialog pdlg = new PrintDialog();
                //PrintDocumentを指定
                pdlg.Document = pd;
                //印刷の選択ダイアログを表示する
                if (pdlg.ShowDialog() == DialogResult.OK)
                {
                    //OKがクリックされた時は印刷する
                    pd.Print();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            count = 0;
            print_page_num = 0;

           
                selected_barcode = new string[dataGridView2.Rows.Count, 3];

                foreach (DataGridViewRow r in dataGridView2.Rows)
                {
                    if (r.Cells[1].Value == null) break;
                    string[,] store_data = atsumi_pos.find_store(Form1.db_file_pos, int.Parse(r.Cells[4].Value.ToString()));

                    selected_barcode[count, 0] = r.Cells[1].Value.ToString();
                    selected_barcode[count, 1] = r.Cells[2].Value.ToString();
                    selected_barcode[count, 2] = store_data[0, 1];

                    count++;
                }
            if (count > 0)
            {

                //PrintDocumentオブジェクトの作成
                System.Drawing.Printing.PrintDocument pd =
                    new System.Drawing.Printing.PrintDocument();
                //PrintPageイベントハンドラの追加
                pd.PrintPage +=
                    new System.Drawing.Printing.PrintPageEventHandler(printDocument1_PrintPage);

                //PrintDialogクラスの作成
                PrintDialog pdlg = new PrintDialog();
                //PrintDocumentを指定
                pdlg.Document = pd;
                //印刷の選択ダイアログを表示する
                if (pdlg.ShowDialog() == DialogResult.OK)
                {
                    //OKがクリックされた時は印刷する
                    pd.Print();
                }
            }
        }


    }
}
