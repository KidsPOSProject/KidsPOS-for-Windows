namespace PosSystem_Client
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
            this.components = new System.ComponentModel.Container();
            this.debug_display = new System.Windows.Forms.StatusStrip();
            this.disp_now_time = new System.Windows.Forms.ToolStripStatusLabel();
            this.disp_store_name = new System.Windows.Forms.ToolStripStatusLabel();
            this.debug_Test = new System.Windows.Forms.ToolStripStatusLabel();
            this.top_menu = new System.Windows.Forms.MenuStrip();
            this.各種リストToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.商品リストToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.売上リストToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.システムバーコードToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.印刷ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.確認ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ダミーToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.商品ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ユーザーToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.商品ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.display_timer = new System.Windows.Forms.Timer(this.components);
            this.tItemList = new System.Windows.Forms.ListView();
            this.disp_scan_goods = new System.Windows.Forms.Panel();
            this.lScanItemPrice = new System.Windows.Forms.Label();
            this.lScanItemName = new System.Windows.Forms.Label();
            this.tSumItemPrice = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.reg_account = new System.Windows.Forms.Button();
            this.reg_clear = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.reg_user = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.接続先ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.スタッフToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.未登録スタッフToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debug_display.SuspendLayout();
            this.top_menu.SuspendLayout();
            this.disp_scan_goods.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // debug_display
            // 
            this.debug_display.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.disp_now_time,
            this.disp_store_name,
            this.debug_Test});
            this.debug_display.Location = new System.Drawing.Point(0, 541);
            this.debug_display.Name = "debug_display";
            this.debug_display.Size = new System.Drawing.Size(897, 23);
            this.debug_display.TabIndex = 0;
            this.debug_display.Text = "statusStrip1";
            // 
            // disp_now_time
            // 
            this.disp_now_time.Name = "disp_now_time";
            this.disp_now_time.Size = new System.Drawing.Size(12, 18);
            this.disp_now_time.Text = " ";
            // 
            // disp_store_name
            // 
            this.disp_store_name.Name = "disp_store_name";
            this.disp_store_name.Size = new System.Drawing.Size(12, 18);
            this.disp_store_name.Text = " ";
            // 
            // debug_Test
            // 
            this.debug_Test.Name = "debug_Test";
            this.debug_Test.Size = new System.Drawing.Size(12, 18);
            this.debug_Test.Text = " ";
            // 
            // top_menu
            // 
            this.top_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.各種リストToolStripMenuItem,
            this.システムバーコードToolStripMenuItem,
            this.ダミーToolStripMenuItem});
            this.top_menu.Location = new System.Drawing.Point(0, 0);
            this.top_menu.Name = "top_menu";
            this.top_menu.Size = new System.Drawing.Size(897, 26);
            this.top_menu.TabIndex = 1;
            this.top_menu.Text = "menuStrip1";
            // 
            // 各種リストToolStripMenuItem
            // 
            this.各種リストToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.商品リストToolStripMenuItem,
            this.売上リストToolStripMenuItem});
            this.各種リストToolStripMenuItem.Name = "各種リストToolStripMenuItem";
            this.各種リストToolStripMenuItem.Size = new System.Drawing.Size(68, 22);
            this.各種リストToolStripMenuItem.Text = "各リスト";
            // 
            // 商品リストToolStripMenuItem
            // 
            this.商品リストToolStripMenuItem.Name = "商品リストToolStripMenuItem";
            this.商品リストToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.商品リストToolStripMenuItem.Text = "商品リスト";
            this.商品リストToolStripMenuItem.Click += new System.EventHandler(this.商品リストToolStripMenuItem_Click);
            // 
            // 売上リストToolStripMenuItem
            // 
            this.売上リストToolStripMenuItem.Name = "売上リストToolStripMenuItem";
            this.売上リストToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.売上リストToolStripMenuItem.Text = "売上リスト";
            this.売上リストToolStripMenuItem.Click += new System.EventHandler(this.売上リストToolStripMenuItem_Click);
            // 
            // ダミーToolStripMenuItem
            // 
            this.ダミーToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.商品ToolStripMenuItem1,
            this.スタッフToolStripMenuItem,
            this.未登録スタッフToolStripMenuItem});
            this.ダミーToolStripMenuItem.Name = "ダミーToolStripMenuItem";
            this.ダミーToolStripMenuItem.Size = new System.Drawing.Size(56, 22);
            this.ダミーToolStripMenuItem.Text = "ダミー";
            // 
            // 商品ToolStripMenuItem1
            // 
            this.商品ToolStripMenuItem1.Name = "商品ToolStripMenuItem1";
            this.商品ToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.商品ToolStripMenuItem1.Text = "商品";
            this.商品ToolStripMenuItem1.Click += new System.EventHandler(this.商品ToolStripMenuItem1_Click);
            // 
            // ユーザーToolStripMenuItem
            // 
            this.ユーザーToolStripMenuItem.Name = "ユーザーToolStripMenuItem";
            this.ユーザーToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // 商品ToolStripMenuItem
            // 
            this.商品ToolStripMenuItem.Name = "商品ToolStripMenuItem";
            this.商品ToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // display_timer
            // 
            this.display_timer.Enabled = true;
            this.display_timer.Tick += new System.EventHandler(this.display_timer_Tick);
            // 
            // reg_goods_list
            // 
            this.tItemList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tItemList.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tItemList.Location = new System.Drawing.Point(12, 55);
            this.tItemList.MultiSelect = false;
            this.tItemList.Name = "reg_goods_list";
            this.tItemList.Size = new System.Drawing.Size(873, 282);
            this.tItemList.TabIndex = 2;
            this.tItemList.UseCompatibleStateImageBehavior = false;
            this.tItemList.SizeChanged += new System.EventHandler(this.reg_goods_list_SizeChanged);
            this.tItemList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.reg_goods_list_MouseDoubleClick);
            // 
            // disp_scan_goods
            // 
            this.disp_scan_goods.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.disp_scan_goods.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.disp_scan_goods.Controls.Add(this.lScanItemPrice);
            this.disp_scan_goods.Controls.Add(this.lScanItemName);
            this.disp_scan_goods.Location = new System.Drawing.Point(13, 343);
            this.disp_scan_goods.Name = "disp_scan_goods";
            this.disp_scan_goods.Size = new System.Drawing.Size(872, 92);
            this.disp_scan_goods.TabIndex = 3;
            // 
            // lScanItemPrice
            // 
            this.lScanItemPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lScanItemPrice.AutoSize = true;
            this.lScanItemPrice.Font = new System.Drawing.Font("MS UI Gothic", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lScanItemPrice.Location = new System.Drawing.Point(680, 35);
            this.lScanItemPrice.Name = "lScanItemPrice";
            this.lScanItemPrice.Size = new System.Drawing.Size(0, 37);
            this.lScanItemPrice.TabIndex = 1;
            // 
            // lScanItemName
            // 
            this.lScanItemName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lScanItemName.AutoSize = true;
            this.lScanItemName.Font = new System.Drawing.Font("MS UI Gothic", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lScanItemName.Location = new System.Drawing.Point(63, 35);
            this.lScanItemName.Name = "lScanItemName";
            this.lScanItemName.Size = new System.Drawing.Size(0, 37);
            this.lScanItemName.TabIndex = 0;
            // 
            // reg_goods_sum
            // 
            this.tSumItemPrice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tSumItemPrice.Font = new System.Drawing.Font("MS UI Gothic", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tSumItemPrice.Location = new System.Drawing.Point(280, 438);
            this.tSumItemPrice.Name = "reg_goods_sum";
            this.tSumItemPrice.ReadOnly = true;
            this.tSumItemPrice.Size = new System.Drawing.Size(277, 44);
            this.tSumItemPrice.TabIndex = 5;
            this.tSumItemPrice.Text = "0";
            this.tSumItemPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(130, 441);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 37);
            this.label1.TabIndex = 4;
            this.label1.Text = "きんがく：";
            // 
            // reg_account
            // 
            this.reg_account.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.reg_account.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.reg_account.Location = new System.Drawing.Point(578, 441);
            this.reg_account.Name = "reg_account";
            this.reg_account.Size = new System.Drawing.Size(198, 72);
            this.reg_account.TabIndex = 6;
            this.reg_account.Text = "かいけい";
            this.reg_account.UseVisualStyleBackColor = true;
            this.reg_account.Click += new System.EventHandler(this.reg_account_Click);
            // 
            // reg_clear
            // 
            this.reg_clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.reg_clear.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.reg_clear.Location = new System.Drawing.Point(796, 441);
            this.reg_clear.Name = "reg_clear";
            this.reg_clear.Size = new System.Drawing.Size(89, 72);
            this.reg_clear.TabIndex = 7;
            this.reg_clear.Text = "クリア";
            this.reg_clear.UseVisualStyleBackColor = true;
            this.reg_clear.Click += new System.EventHandler(this.reg_clear_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(19, 489);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(255, 35);
            this.label3.TabIndex = 8;
            this.label3.Text = "レジ打ち担当者：";
            // 
            // reg_user
            // 
            this.reg_user.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.reg_user.AutoSize = true;
            this.reg_user.Font = new System.Drawing.Font("MS UI Gothic", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.reg_user.Location = new System.Drawing.Point(302, 489);
            this.reg_user.Name = "reg_user";
            this.reg_user.Size = new System.Drawing.Size(26, 35);
            this.reg_user.TabIndex = 9;
            this.reg_user.Text = " ";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.接続先ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 26);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(897, 26);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 接続先ToolStripMenuItem
            // 
            this.接続先ToolStripMenuItem.Name = "接続先ToolStripMenuItem";
            this.接続先ToolStripMenuItem.Size = new System.Drawing.Size(72, 22);
            this.接続先ToolStripMenuItem.Text = "接続先 ▼";
            // 
            // スタッフToolStripMenuItem
            // 
            this.スタッフToolStripMenuItem.Name = "スタッフToolStripMenuItem";
            this.スタッフToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.スタッフToolStripMenuItem.Text = "スタッフ";
            this.スタッフToolStripMenuItem.Click += new System.EventHandler(this.スタッフToolStripMenuItem_Click);
            // 
            // 未登録スタッフToolStripMenuItem
            // 
            this.未登録スタッフToolStripMenuItem.Name = "未登録スタッフToolStripMenuItem";
            this.未登録スタッフToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.未登録スタッフToolStripMenuItem.Text = "未登録スタッフ";
            this.未登録スタッフToolStripMenuItem.Click += new System.EventHandler(this.未登録スタッフToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 564);
            this.Controls.Add(this.reg_user);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.reg_clear);
            this.Controls.Add(this.reg_account);
            this.Controls.Add(this.debug_display);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.disp_scan_goods);
            this.Controls.Add(this.tSumItemPrice);
            this.Controls.Add(this.tItemList);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.top_menu);
            this.MainMenuStrip = this.top_menu;
            this.Name = "Form1";
            this.Text = "POS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.debug_display.ResumeLayout(false);
            this.debug_display.PerformLayout();
            this.top_menu.ResumeLayout(false);
            this.top_menu.PerformLayout();
            this.disp_scan_goods.ResumeLayout(false);
            this.disp_scan_goods.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip debug_display;
        private System.Windows.Forms.MenuStrip top_menu;
        private System.Windows.Forms.ToolStripStatusLabel disp_now_time;
        private System.Windows.Forms.ToolStripStatusLabel disp_store_name;
        private System.Windows.Forms.Timer display_timer;
        private System.Windows.Forms.Panel disp_scan_goods;
        private System.Windows.Forms.Label lScanItemName;
        private System.Windows.Forms.Label lScanItemPrice;
        private System.Windows.Forms.ToolStripStatusLabel debug_Test;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button reg_account;
        private System.Windows.Forms.Button reg_clear;
        public System.Windows.Forms.ListView tItemList;
        internal System.Windows.Forms.TextBox tSumItemPrice;
        private System.Windows.Forms.ToolStripMenuItem システムバーコードToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 印刷ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 確認ToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label reg_user;
        private System.Windows.Forms.ToolStripMenuItem 各種リストToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 商品リストToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 売上リストToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ユーザーToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 商品ToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 接続先ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ダミーToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 商品ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem スタッフToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 未登録スタッフToolStripMenuItem;
    }
}

