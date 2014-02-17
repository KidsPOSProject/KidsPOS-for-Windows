namespace PosSystem
{
    partial class Item_Regist_OK
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Item_Regist_OK));
            this.regist_status_text = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.print = new System.Windows.Forms.Button();
            this.ireg_ok_name = new System.Windows.Forms.Label();
            this.ireg_ok_price = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // regist_status_text
            // 
            this.regist_status_text.AutoSize = true;
            this.regist_status_text.Font = new System.Drawing.Font("MS UI Gothic", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.regist_status_text.Location = new System.Drawing.Point(180, 21);
            this.regist_status_text.Name = "regist_status_text";
            this.regist_status_text.Size = new System.Drawing.Size(445, 37);
            this.regist_status_text.TabIndex = 2;
            this.regist_status_text.Text = " 商品の登録に失敗しました。";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(83, 152);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 48);
            this.label2.TabIndex = 4;
            this.label2.Text = "商品名";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(131, 218);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 48);
            this.label3.TabIndex = 5;
            this.label3.Text = "値段";
            // 
            // print
            // 
            this.print.Font = new System.Drawing.Font("MS UI Gothic", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.print.Location = new System.Drawing.Point(569, 272);
            this.print.Name = "print";
            this.print.Size = new System.Drawing.Size(230, 71);
            this.print.TabIndex = 8;
            this.print.Text = "印刷";
            this.print.UseVisualStyleBackColor = true;
            this.print.Click += new System.EventHandler(this.print_Click);
            // 
            // ireg_ok_name
            // 
            this.ireg_ok_name.AutoSize = true;
            this.ireg_ok_name.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ireg_ok_name.Location = new System.Drawing.Point(326, 152);
            this.ireg_ok_name.Name = "ireg_ok_name";
            this.ireg_ok_name.Size = new System.Drawing.Size(35, 48);
            this.ireg_ok_name.TabIndex = 9;
            this.ireg_ok_name.Text = " ";
            // 
            // ireg_ok_price
            // 
            this.ireg_ok_price.AutoSize = true;
            this.ireg_ok_price.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ireg_ok_price.Location = new System.Drawing.Point(326, 218);
            this.ireg_ok_price.Name = "ireg_ok_price";
            this.ireg_ok_price.Size = new System.Drawing.Size(35, 48);
            this.ireg_ok_price.TabIndex = 10;
            this.ireg_ok_price.Text = " ";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("MS UI Gothic", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button2.Location = new System.Drawing.Point(23, 375);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(776, 93);
            this.button2.TabIndex = 11;
            this.button2.Text = "レジモードへ戻る";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // printDialog1
            // 
            this.printDialog1.Document = this.printDocument1;
            this.printDialog1.UseEXDialog = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(133, 302);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(352, 33);
            this.label5.TabIndex = 13;
            this.label5.Text = "1回で 15枚 印刷されます";
            // 
            // Item_Regist_OK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 480);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.ireg_ok_price);
            this.Controls.Add(this.ireg_ok_name);
            this.Controls.Add(this.print);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.regist_status_text);
            this.Name = "Item_Regist_OK";
            this.Text = "Item_Regist_OK";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Item_Regist_OK_FormClosed);
            this.Load += new System.EventHandler(this.Item_Regist_OK_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label regist_status_text;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button print;
        private System.Windows.Forms.Label ireg_ok_name;
        private System.Windows.Forms.Label ireg_ok_price;
        private System.Windows.Forms.Button button2;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.Label label5;
    }
}