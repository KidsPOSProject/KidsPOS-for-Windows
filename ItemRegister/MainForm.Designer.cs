namespace DBRegister
{
    partial class MainForm
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
            this.bLoadItem = new System.Windows.Forms.Button();
            this.practice_status = new System.Windows.Forms.Label();
            this.bShowItem = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.bLoadUser = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bShowUser = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lKidsDate = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bLoadItem
            // 
            this.bLoadItem.Font = new System.Drawing.Font("MS UI Gothic", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bLoadItem.Location = new System.Drawing.Point(14, 42);
            this.bLoadItem.Name = "bLoadItem";
            this.bLoadItem.Size = new System.Drawing.Size(117, 46);
            this.bLoadItem.TabIndex = 34;
            this.bLoadItem.Text = "商品";
            this.bLoadItem.UseVisualStyleBackColor = true;
            this.bLoadItem.Click += new System.EventHandler(this.load_csv_file_Click);
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
            // bShowItem
            // 
            this.bShowItem.Font = new System.Drawing.Font("MS UI Gothic", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bShowItem.Location = new System.Drawing.Point(14, 38);
            this.bShowItem.Name = "bShowItem";
            this.bShowItem.Size = new System.Drawing.Size(117, 46);
            this.bShowItem.TabIndex = 35;
            this.bShowItem.Text = "商品";
            this.bShowItem.UseVisualStyleBackColor = true;
            this.bShowItem.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.bLoadUser);
            this.panel1.Controls.Add(this.bLoadItem);
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
            // bLoadUser
            // 
            this.bLoadUser.Font = new System.Drawing.Font("MS UI Gothic", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bLoadUser.Location = new System.Drawing.Point(174, 42);
            this.bLoadUser.Name = "bLoadUser";
            this.bLoadUser.Size = new System.Drawing.Size(117, 46);
            this.bLoadUser.TabIndex = 35;
            this.bLoadUser.Text = "ユーザ";
            this.bLoadUser.UseVisualStyleBackColor = true;
            this.bLoadUser.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.bShowUser);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.bShowItem);
            this.panel2.Location = new System.Drawing.Point(20, 252);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(309, 100);
            this.panel2.TabIndex = 37;
            // 
            // bShowUser
            // 
            this.bShowUser.Font = new System.Drawing.Font("MS UI Gothic", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bShowUser.Location = new System.Drawing.Point(174, 38);
            this.bShowUser.Name = "bShowUser";
            this.bShowUser.Size = new System.Drawing.Size(117, 46);
            this.bShowUser.TabIndex = 38;
            this.bShowUser.Text = "ユーザ";
            this.bShowUser.UseVisualStyleBackColor = true;
            this.bShowUser.Click += new System.EventHandler(this.button3_Click);
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
            this.ClientSize = new System.Drawing.Size(355, 367);
            this.Controls.Add(this.lKidsDate);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.practice_status);
            this.Name = "Form1";
            this.Text = "DBRegister";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bLoadItem;
        private System.Windows.Forms.Label practice_status;
        private System.Windows.Forms.Button bShowItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bLoadUser;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button bShowUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lKidsDate;

    }
}

