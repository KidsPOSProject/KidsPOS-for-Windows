using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace PosSystem
{
    public partial class Item_Regist : Form
    {
        public Item_Regist()
        {
            InitializeComponent();
            this.MaximizeBox = !this.MaximizeBox;
            this.MinimizeBox = !this.MinimizeBox;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            practice_status.Text = (Form1.isPractice) ? "練習モードなので登録は出来ません。" : "商品の登録が出来ます";

        }

        private void Item_Regist_Load(object sender, EventArgs e)
        {
            ireg_genre.SelectedIndex = 0;
            ireg_store.Text = Form1.store_name;
            ireg_kind.Text = Form1.store_kind;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ireg_name.Text != "" && ireg_price.Text != "")
            {
                Item_Regist_OK f = new Item_Regist_OK(ireg_genre.Text, ireg_name.Text, ireg_price.Text);
                f.ShowDialog(this);

                ireg_name.Text = "";
                ireg_price.Text = "";

                f.Dispose();
                
                this.Close();
            }
            else
            {
                debug_text.Text = "正しく入力をしてください！！";
            }
        }

        private void ireg_price_TextChanged(object sender, EventArgs e)
        {
            if (!Validation.IsNumeric(ireg_price.Text)) ireg_price.Text = "0";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void load_csv_file_Click(object sender, EventArgs e)
        {
            do_load(atsumi_pos.file_load());
        }
        public bool do_load(ArrayList csv)
        {
            int item_position = -1;
            for (int i = 0; i < csv.Count; i++)
            {
                if (csv[i].ToString().StartsWith("##商品リスト"))
                {
                    item_position = i;
                    break;
                }
            }
            if (item_position == -1)
            {
                MessageBox.Show(
                    "ファイルがおかしいよ"+Environment.NewLine+
                    "・CSV形式であるか"+Environment.NewLine+
                    "・しっかりフォーマットに沿っているか"+Environment.NewLine+
                    "を確認してください","読み込みエラー",MessageBoxButtons.OK,MessageBoxIcon.Question
                    );
                return false;
            }

            int column_count = 0;
            for (int i = item_position + 1; i < csv.Count; i++)
            {
                if (!csv[i].ToString().StartsWith("#")) continue;
                column_count++;
            }

            if (column_count == 0)
            {
                MessageBox.Show(
                    "ファイルがおかしいよ" + Environment.NewLine +
                    "・しっかりフォーマットに沿っているか" + Environment.NewLine +
                    "を確認してください", "読み込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Question
                    );
                return false;
            }


            for (int i = item_position + column_count + 1; i < csv.Count; i += column_count)
            {
                Barcode bar = new Barcode(
                    BarCode_Prefix.ITEM,
                    Form1.store_num, atsumi_pos.read_count_num(Form1.db_file, "item_list").ToString("D5"));

                if (atsumi_pos.Insert(new atsumi_pos.ItemTable(bar.show(), csv[i].ToString(), csv[i + 2].ToString(), Form1.store_num.ToString())))
                {
                    MessageBox.Show("アイテムの登録が出来田っぽい");
                }
                else
                {

                    MessageBox.Show("アイテムの登録は出来て内っぽい");
                }
            }

            return true;
       } 
    }

    public sealed class Validation
    {

        #region　IsNumeric メソッド (+1)

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     文字列が数値であるかどうかを返します。</summary>
        /// <param name="stTarget">
        ///     検査対象となる文字列。<param>
        /// <returns>
        ///     指定した文字列が数値であれば true。それ以外は false。</returns>
        /// -----------------------------------------------------------------------------
        public static bool IsNumeric(string stTarget)
        {
            double dNullable;

            return double.TryParse(
                stTarget,
                System.Globalization.NumberStyles.Any,
                null,
                out dNullable
            );
        }


        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     オブジェクトが数値であるかどうかを返します。</summary>
        /// <param name="oTarget">
        ///     検査対象となるオブジェクト。<param>
        /// <returns>
        ///     指定したオブジェクトが数値であれば true。それ以外は false。</returns>
        /// -----------------------------------------------------------------------------
        public static bool IsNumeric(object oTarget)
        {
            return IsNumeric(oTarget.ToString());
        }

        #endregion

    }
}
