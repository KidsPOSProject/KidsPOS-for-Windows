using System;
using System.Collections.Generic;
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
