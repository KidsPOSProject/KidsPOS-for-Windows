﻿using System.ComponentModel;
using System.Windows.Forms;

namespace PosSystem.Source
{
    partial class Sales
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sales_time = new System.Windows.Forms.Label();
            this.buy_time = new System.Windows.Forms.Label();
            this.sale_staff_name = new System.Windows.Forms.Label();
            this.sale_staff = new System.Windows.Forms.Label();
            this.sales_list = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // sales_time
            // 
            this.sales_time.AutoSize = true;
            this.sales_time.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.sales_time.Location = new System.Drawing.Point(12, 25);
            this.sales_time.Name = "sales_time";
            this.sales_time.Size = new System.Drawing.Size(105, 21);
            this.sales_time.TabIndex = 1;
            this.sales_time.Text = "購入日付：";
            // 
            // buy_time
            // 
            this.buy_time.AutoSize = true;
            this.buy_time.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buy_time.Location = new System.Drawing.Point(138, 25);
            this.buy_time.Name = "buy_time";
            this.buy_time.Size = new System.Drawing.Size(16, 21);
            this.buy_time.TabIndex = 2;
            this.buy_time.Text = " ";
            // 
            // sale_staff_name
            // 
            this.sale_staff_name.AutoSize = true;
            this.sale_staff_name.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.sale_staff_name.Location = new System.Drawing.Point(138, 67);
            this.sale_staff_name.Name = "sale_staff_name";
            this.sale_staff_name.Size = new System.Drawing.Size(16, 21);
            this.sale_staff_name.TabIndex = 4;
            this.sale_staff_name.Text = " ";
            // 
            // sale_staff
            // 
            this.sale_staff.AutoSize = true;
            this.sale_staff.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.sale_staff.Location = new System.Drawing.Point(12, 67);
            this.sale_staff.Name = "sale_staff";
            this.sale_staff.Size = new System.Drawing.Size(120, 21);
            this.sale_staff.TabIndex = 3;
            this.sale_staff.Text = "担当スタッフ：";
            // 
            // sales_list
            // 
            this.sales_list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sales_list.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.sales_list.Location = new System.Drawing.Point(12, 128);
            this.sales_list.MultiSelect = false;
            this.sales_list.Name = "sales_list";
            this.sales_list.Size = new System.Drawing.Size(873, 424);
            this.sales_list.TabIndex = 5;
            this.sales_list.UseCompatibleStateImageBehavior = false;
            // 
            // Sales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 564);
            this.Controls.Add(this.sales_list);
            this.Controls.Add(this.sale_staff_name);
            this.Controls.Add(this.sale_staff);
            this.Controls.Add(this.buy_time);
            this.Controls.Add(this.sales_time);
            this.KeyPreview = true;
            this.Name = "Sales";
            this.Text = "Sales";
            this.Load += new System.EventHandler(this.Sales_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label sales_time;
        private Label buy_time;
        private Label sale_staff_name;
        private Label sale_staff;
        public ListView sales_list;

    }
}