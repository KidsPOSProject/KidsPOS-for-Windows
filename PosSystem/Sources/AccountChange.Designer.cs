using System.ComponentModel;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace PosSystem.Source
{
    partial class AccountChange
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountChange));
            this.label2 = new System.Windows.Forms.Label();
            this.received_money = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.reg_goods_sum = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.change = new System.Windows.Forms.TextBox();
            this.practice_status = new System.Windows.Forms.Label();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(48, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 37);
            this.label2.TabIndex = 12;
            this.label2.Text = "あずかり：";
            // 
            // received_money
            // 
            this.received_money.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.received_money.Font = new System.Drawing.Font("MS UI Gothic", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.received_money.Location = new System.Drawing.Point(249, 109);
            this.received_money.Name = "received_money";
            this.received_money.ReadOnly = true;
            this.received_money.Size = new System.Drawing.Size(271, 44);
            this.received_money.TabIndex = 13;
            this.received_money.Text = "0";
            this.received_money.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(54, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 37);
            this.label1.TabIndex = 10;
            this.label1.Text = "きんがく：";
            // 
            // reg_goods_sum
            // 
            this.reg_goods_sum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.reg_goods_sum.Font = new System.Drawing.Font("MS UI Gothic", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.reg_goods_sum.Location = new System.Drawing.Point(249, 59);
            this.reg_goods_sum.Name = "reg_goods_sum";
            this.reg_goods_sum.ReadOnly = true;
            this.reg_goods_sum.Size = new System.Drawing.Size(271, 44);
            this.reg_goods_sum.TabIndex = 11;
            this.reg_goods_sum.Text = "0";
            this.reg_goods_sum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(80, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 37);
            this.label3.TabIndex = 15;
            this.label3.Text = "おつり：";
            // 
            // change
            // 
            this.change.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.change.Enabled = false;
            this.change.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.change.Location = new System.Drawing.Point(204, 160);
            this.change.Name = "change";
            this.change.ReadOnly = true;
            this.change.Size = new System.Drawing.Size(271, 55);
            this.change.TabIndex = 16;
            this.change.Text = "0";
            this.change.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // practice_status
            // 
            this.practice_status.AutoSize = true;
            this.practice_status.Font = new System.Drawing.Font("MS UI Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.practice_status.Location = new System.Drawing.Point(12, 9);
            this.practice_status.Name = "practice_status";
            this.practice_status.Size = new System.Drawing.Size(0, 27);
            this.practice_status.TabIndex = 18;
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
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(55, 251);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(404, 35);
            this.label4.TabIndex = 19;
            this.label4.Text = "エンターキーを押すと戻ります";
            // 
            // Account_change
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 295);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.practice_status);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.change);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.received_money);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.reg_goods_sum);
            this.KeyPreview = true;
            this.Name = "AccountChange";
            this.Text = "Account_change";
            this.Load += new System.EventHandler(this.Account_change_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Account_change_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label2;
        private TextBox received_money;
        private Label label1;
        private TextBox reg_goods_sum;
        private Label label3;
        private TextBox change;
        private Label practice_status;
        private PrintPreviewDialog printPreviewDialog1;
        private PrintDocument printDocument1;
        private Label label4;
    }
}