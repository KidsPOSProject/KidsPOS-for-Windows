namespace PosSystem
{
    partial class Account
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.reg_goods_list = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.reg_goods_sum = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.received_money = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // reg_goods_list
            // 
            this.reg_goods_list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reg_goods_list.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.reg_goods_list.Location = new System.Drawing.Point(0, 12);
            this.reg_goods_list.MultiSelect = false;
            this.reg_goods_list.Name = "reg_goods_list";
            this.reg_goods_list.Size = new System.Drawing.Size(792, 275);
            this.reg_goods_list.TabIndex = 4;
            this.reg_goods_list.UseCompatibleStateImageBehavior = false;
            this.reg_goods_list.SizeChanged += new System.EventHandler(this.reg_goods_list_SizeChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(12, 296);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 64);
            this.label1.TabIndex = 6;
            this.label1.Text = "きんがく：";
            // 
            // reg_goods_sum
            // 
            this.reg_goods_sum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reg_goods_sum.Font = new System.Drawing.Font("MS UI Gothic", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.reg_goods_sum.Location = new System.Drawing.Point(245, 293);
            this.reg_goods_sum.Name = "reg_goods_sum";
            this.reg_goods_sum.ReadOnly = true;
            this.reg_goods_sum.Size = new System.Drawing.Size(331, 71);
            this.reg_goods_sum.TabIndex = 7;
            this.reg_goods_sum.Text = "0";
            this.reg_goods_sum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(-1, 393);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(258, 64);
            this.label2.TabIndex = 8;
            this.label2.Text = "あずかり：";
            // 
            // received_money
            // 
            this.received_money.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.received_money.Font = new System.Drawing.Font("MS UI Gothic", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.received_money.Location = new System.Drawing.Point(245, 390);
            this.received_money.Name = "received_money";
            this.received_money.Size = new System.Drawing.Size(331, 71);
            this.received_money.TabIndex = 9;
            this.received_money.Text = "0";
            this.received_money.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("MS UI Gothic", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button1.Location = new System.Drawing.Point(582, 293);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(210, 168);
            this.button1.TabIndex = 10;
            this.button1.Text = "かいけい";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Account
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 473);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.received_money);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.reg_goods_sum);
            this.Controls.Add(this.reg_goods_list);
            this.Name = "Account";
            this.Text = "Account";
            this.Load += new System.EventHandler(this.Account_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ListView reg_goods_list;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox reg_goods_sum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox received_money;
        private System.Windows.Forms.Button button1;
    }
}