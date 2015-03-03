namespace DBRegister
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.load_csv_file = new System.Windows.Forms.Button();
            this.practice_status = new System.Windows.Forms.Label();
            this.debug_text = new System.Windows.Forms.ToolStripStatusLabel();
            this.debug_test = new System.Windows.Forms.StatusStrip();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lKidsDate = new System.Windows.Forms.Label();
            this.debug_test.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // load_csv_file
            // 
            this.load_csv_file.Font = new System.Drawing.Font("MS UI Gothic", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.load_csv_file.Location = new System.Drawing.Point(14, 42);
            this.load_csv_file.Name = "load_csv_file";
            this.load_csv_file.Size = new System.Drawing.Size(117, 46);
            this.load_csv_file.TabIndex = 34;
            this.load_csv_file.Text = "商品";
            this.load_csv_file.UseVisualStyleBackColor = true;
            this.load_csv_file.Click += new System.EventHandler(this.load_csv_file_Click);
            // 
            // practice_status
            // 
            this.practice_status.AutoSize = true;
            this.practice_status.Font = new System.Drawing.Font("MS UI Gothic", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.practice_status.Location = new System.Drawing.Point(16, 60);
            this.practice_status.Name = "practice_status";
            this.practice_status.Size = new System.Drawing.Size(319, 35);
            this.practice_status.TabIndex = 31;
            this.practice_status.Text = "各データの登録・確認";
            // 
            // debug_text
            // 
            this.debug_text.Name = "debug_text";
            this.debug_text.Size = new System.Drawing.Size(12, 18);
            this.debug_text.Text = " ";
            // 
            // debug_test
            // 
            this.debug_test.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.debug_text});
            this.debug_test.Location = new System.Drawing.Point(0, 391);
            this.debug_test.Name = "debug_test";
            this.debug_test.Size = new System.Drawing.Size(355, 23);
            this.debug_test.TabIndex = 29;
            this.debug_test.Text = "statusStrip1";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("MS UI Gothic", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button1.Location = new System.Drawing.Point(14, 38);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 46);
            this.button1.TabIndex = 35;
            this.button1.Text = "商品";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.load_csv_file);
            this.panel1.Location = new System.Drawing.Point(20, 110);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(309, 100);
            this.panel1.TabIndex = 36;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 12);
            this.label1.TabIndex = 37;
            this.label1.Text = "CSVから読み込み";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("MS UI Gothic", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button2.Location = new System.Drawing.Point(174, 42);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(117, 46);
            this.button2.TabIndex = 35;
            this.button2.Text = "ユーザ";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Location = new System.Drawing.Point(20, 252);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(309, 100);
            this.panel2.TabIndex = 37;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("MS UI Gothic", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button3.Location = new System.Drawing.Point(174, 38);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(117, 46);
            this.button3.TabIndex = 38;
            this.button3.Text = "ユーザ";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 12);
            this.label2.TabIndex = 38;
            this.label2.Text = "データ確認";
            // 
            // lKidsDate
            // 
            this.lKidsDate.AutoSize = true;
            this.lKidsDate.Location = new System.Drawing.Point(20, 13);
            this.lKidsDate.Name = "lKidsDate";
            this.lKidsDate.Size = new System.Drawing.Size(0, 12);
            this.lKidsDate.TabIndex = 38;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 414);
            this.Controls.Add(this.lKidsDate);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.practice_status);
            this.Controls.Add(this.debug_test);
            this.Name = "Form1";
            this.Text = "DBRegister";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.debug_test.ResumeLayout(false);
            this.debug_test.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button load_csv_file;
        private System.Windows.Forms.Label practice_status;
        private System.Windows.Forms.ToolStripStatusLabel debug_text;
        private System.Windows.Forms.StatusStrip debug_test;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lKidsDate;

    }
}

