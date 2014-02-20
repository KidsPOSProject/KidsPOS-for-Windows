namespace ItemRegister
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
            this.debug_test.SuspendLayout();
            this.SuspendLayout();
            // 
            // load_csv_file
            // 
            this.load_csv_file.Font = new System.Drawing.Font("MS UI Gothic", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.load_csv_file.Location = new System.Drawing.Point(20, 97);
            this.load_csv_file.Name = "load_csv_file";
            this.load_csv_file.Size = new System.Drawing.Size(309, 46);
            this.load_csv_file.TabIndex = 34;
            this.load_csv_file.Text = "CSVから読み込み";
            this.load_csv_file.UseVisualStyleBackColor = true;
            this.load_csv_file.Click += new System.EventHandler(this.load_csv_file_Click);
            // 
            // practice_status
            // 
            this.practice_status.AutoSize = true;
            this.practice_status.Font = new System.Drawing.Font("MS UI Gothic", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.practice_status.Location = new System.Drawing.Point(14, 16);
            this.practice_status.Name = "practice_status";
            this.practice_status.Size = new System.Drawing.Size(339, 35);
            this.practice_status.TabIndex = 31;
            this.practice_status.Text = "商品の登録が出来ます";
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
            this.debug_test.Size = new System.Drawing.Size(360, 23);
            this.debug_test.TabIndex = 29;
            this.debug_test.Text = "statusStrip1";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("MS UI Gothic", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button1.Location = new System.Drawing.Point(20, 235);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(309, 83);
            this.button1.TabIndex = 35;
            this.button1.Text = "データベースの確認";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 414);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.load_csv_file);
            this.Controls.Add(this.practice_status);
            this.Controls.Add(this.debug_test);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.debug_test.ResumeLayout(false);
            this.debug_test.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button load_csv_file;
        private System.Windows.Forms.Label practice_status;
        private System.Windows.Forms.ToolStripStatusLabel debug_text;
        private System.Windows.Forms.StatusStrip debug_test;
        private System.Windows.Forms.Button button1;

    }
}

