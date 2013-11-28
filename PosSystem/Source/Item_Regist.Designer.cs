namespace PosSystem
{
    partial class Item_Regist
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ireg_store = new System.Windows.Forms.Label();
            this.ireg_kind = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ireg_genre = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ireg_name = new System.Windows.Forms.TextBox();
            this.ireg_price = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ireg_regist = new System.Windows.Forms.Button();
            this.debug_test = new System.Windows.Forms.StatusStrip();
            this.debug_text = new System.Windows.Forms.ToolStripStatusLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.debug_test.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 35);
            this.label1.TabIndex = 3;
            this.label1.Text = "店";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(12, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 35);
            this.label2.TabIndex = 4;
            this.label2.Text = "店種類";
            // 
            // ireg_store
            // 
            this.ireg_store.AutoSize = true;
            this.ireg_store.Font = new System.Drawing.Font("MS UI Gothic", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ireg_store.Location = new System.Drawing.Point(296, 9);
            this.ireg_store.Name = "ireg_store";
            this.ireg_store.Size = new System.Drawing.Size(26, 35);
            this.ireg_store.TabIndex = 5;
            this.ireg_store.Text = " ";
            // 
            // ireg_kind
            // 
            this.ireg_kind.AutoSize = true;
            this.ireg_kind.Font = new System.Drawing.Font("MS UI Gothic", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ireg_kind.Location = new System.Drawing.Point(296, 74);
            this.ireg_kind.Name = "ireg_kind";
            this.ireg_kind.Size = new System.Drawing.Size(26, 35);
            this.ireg_kind.TabIndex = 6;
            this.ireg_kind.Text = " ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(12, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(185, 35);
            this.label3.TabIndex = 7;
            this.label3.Text = "商品カテゴリ";
            // 
            // ireg_genre
            // 
            this.ireg_genre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ireg_genre.Font = new System.Drawing.Font("MS UI Gothic", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ireg_genre.FormattingEnabled = true;
            this.ireg_genre.Items.AddRange(new object[] {
            "食品",
            "汎用"});
            this.ireg_genre.Location = new System.Drawing.Point(304, 132);
            this.ireg_genre.Name = "ireg_genre";
            this.ireg_genre.Size = new System.Drawing.Size(205, 43);
            this.ireg_genre.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(12, 211);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 35);
            this.label4.TabIndex = 9;
            this.label4.Text = "商品名";
            // 
            // ireg_name
            // 
            this.ireg_name.Font = new System.Drawing.Font("MS UI Gothic", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ireg_name.Location = new System.Drawing.Point(304, 211);
            this.ireg_name.Name = "ireg_name";
            this.ireg_name.Size = new System.Drawing.Size(419, 42);
            this.ireg_name.TabIndex = 10;
            // 
            // ireg_price
            // 
            this.ireg_price.Font = new System.Drawing.Font("MS UI Gothic", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ireg_price.Location = new System.Drawing.Point(304, 300);
            this.ireg_price.Name = "ireg_price";
            this.ireg_price.Size = new System.Drawing.Size(218, 42);
            this.ireg_price.TabIndex = 12;
            this.ireg_price.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ireg_price.TextChanged += new System.EventHandler(this.ireg_price_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(12, 303);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 35);
            this.label5.TabIndex = 11;
            this.label5.Text = "値段";
            // 
            // ireg_regist
            // 
            this.ireg_regist.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ireg_regist.Location = new System.Drawing.Point(535, 397);
            this.ireg_regist.Name = "ireg_regist";
            this.ireg_regist.Size = new System.Drawing.Size(188, 72);
            this.ireg_regist.TabIndex = 13;
            this.ireg_regist.Text = "登録";
            this.ireg_regist.UseVisualStyleBackColor = true;
            this.ireg_regist.Click += new System.EventHandler(this.button1_Click);
            // 
            // debug_test
            // 
            this.debug_test.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.debug_text});
            this.debug_test.Location = new System.Drawing.Point(0, 481);
            this.debug_test.Name = "debug_test";
            this.debug_test.Size = new System.Drawing.Size(735, 23);
            this.debug_test.TabIndex = 14;
            this.debug_test.Text = "statusStrip1";
            // 
            // debug_text
            // 
            this.debug_text.Name = "debug_text";
            this.debug_text.Size = new System.Drawing.Size(12, 18);
            this.debug_text.Text = " ";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button1.Location = new System.Drawing.Point(20, 397);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(311, 72);
            this.button1.TabIndex = 15;
            this.button1.Text = "レジに戻る";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Item_Regist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 504);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.debug_test);
            this.Controls.Add(this.ireg_regist);
            this.Controls.Add(this.ireg_price);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ireg_name);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ireg_genre);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ireg_kind);
            this.Controls.Add(this.ireg_store);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Item_Regist";
            this.Text = "Item_Regist";
            this.Load += new System.EventHandler(this.Item_Regist_Load);
            this.debug_test.ResumeLayout(false);
            this.debug_test.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label ireg_store;
        private System.Windows.Forms.Label ireg_kind;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button ireg_regist;
        private System.Windows.Forms.StatusStrip debug_test;
        private System.Windows.Forms.ToolStripStatusLabel debug_text;
        public System.Windows.Forms.TextBox ireg_name;
        public System.Windows.Forms.TextBox ireg_price;
        protected internal System.Windows.Forms.ComboBox ireg_genre;
        private System.Windows.Forms.Button button1;
    }
}