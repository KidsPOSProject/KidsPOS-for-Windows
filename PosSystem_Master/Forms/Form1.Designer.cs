namespace PosSystem_Master
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
            this.商品リストEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.売上リストToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.サーバーを建てるToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.接続者確認ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.display_timer = new System.Windows.Forms.Timer(this.components);
            this.readItemList = new System.Windows.Forms.ListView();
            this.disp_scan_goods = new System.Windows.Forms.Panel();
            this.lScanItemPrice = new System.Windows.Forms.Label();
            this.lScanItemName = new System.Windows.Forms.Label();
            this.tSumItemPrice = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.reg_account = new System.Windows.Forms.Button();
            this.reg_clear = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.reg_user = new System.Windows.Forms.Label();
            this.debug_display.SuspendLayout();
            this.top_menu.SuspendLayout();
            this.disp_scan_goods.SuspendLayout();
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
            this.サーバーを建てるToolStripMenuItem,
            this.接続者確認ToolStripMenuItem});
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
            this.商品リストEditToolStripMenuItem,
            this.売上リストToolStripMenuItem});
            this.各種リストToolStripMenuItem.Name = "各種リストToolStripMenuItem";
            this.各種リストToolStripMenuItem.Size = new System.Drawing.Size(68, 22);
            this.各種リストToolStripMenuItem.Text = "各リスト";
            // 
            // 商品リストToolStripMenuItem
            // 
            this.商品リストToolStripMenuItem.Name = "商品リストToolStripMenuItem";
            this.商品リストToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.商品リストToolStripMenuItem.Text = "商品リスト";
            this.商品リストToolStripMenuItem.Click += new System.EventHandler(this.商品リストToolStripMenuItem_Click);
            // 
            // 商品リストEditToolStripMenuItem
            // 
            this.商品リストEditToolStripMenuItem.Name = "商品リストEditToolStripMenuItem";
            this.商品リストEditToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.商品リストEditToolStripMenuItem.Text = "商品リスト(Edit)";
            this.商品リストEditToolStripMenuItem.Click += new System.EventHandler(this.商品リストEditToolStripMenuItem_Click);
            // 
            // 売上リストToolStripMenuItem
            // 
            this.売上リストToolStripMenuItem.Name = "売上リストToolStripMenuItem";
            this.売上リストToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.売上リストToolStripMenuItem.Text = "売上リスト";
            this.売上リストToolStripMenuItem.Click += new System.EventHandler(this.売上リストToolStripMenuItem_Click);
            // 
            // サーバーを建てるToolStripMenuItem
            // 
            this.サーバーを建てるToolStripMenuItem.Name = "サーバーを建てるToolStripMenuItem";
            this.サーバーを建てるToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.サーバーを建てるToolStripMenuItem.Text = "サーバーを建てる";
            this.サーバーを建てるToolStripMenuItem.Click += new System.EventHandler(this.サーバーを建てるToolStripMenuItem_Click);
            // 
            // 接続者確認ToolStripMenuItem
            // 
            this.接続者確認ToolStripMenuItem.Enabled = false;
            this.接続者確認ToolStripMenuItem.Name = "接続者確認ToolStripMenuItem";
            this.接続者確認ToolStripMenuItem.Size = new System.Drawing.Size(80, 22);
            this.接続者確認ToolStripMenuItem.Text = "接続者確認";
            this.接続者確認ToolStripMenuItem.Click += new System.EventHandler(this.接続者確認ToolStripMenuItem_Click);
            // 
            // display_timer
            // 
            this.display_timer.Enabled = true;
            this.display_timer.Tick += new System.EventHandler(this.display_timer_Tick);
            // 
            // readItemList
            // 
            this.readItemList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.readItemList.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.readItemList.Location = new System.Drawing.Point(12, 29);
            this.readItemList.MultiSelect = false;
            this.readItemList.Name = "readItemList";
            this.readItemList.Size = new System.Drawing.Size(873, 308);
            this.readItemList.TabIndex = 2;
            this.readItemList.UseCompatibleStateImageBehavior = false;
            this.readItemList.SizeChanged += new System.EventHandler(this.reg_goods_list_SizeChanged);
            this.readItemList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.reg_goods_list_MouseDoubleClick);
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
            // tSumItemPrice
            // 
            this.tSumItemPrice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tSumItemPrice.Font = new System.Drawing.Font("MS UI Gothic", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tSumItemPrice.Location = new System.Drawing.Point(280, 438);
            this.tSumItemPrice.Name = "tSumItemPrice";
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
            this.reg_account.Click += new System.EventHandler(this.bAccount_Click);
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
            this.Controls.Add(this.readItemList);
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
        public System.Windows.Forms.ListView readItemList;
        internal System.Windows.Forms.TextBox tSumItemPrice;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label reg_user;
        private System.Windows.Forms.ToolStripMenuItem 各種リストToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 商品リストToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 売上リストToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 商品リストEditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem サーバーを建てるToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 接続者確認ToolStripMenuItem;
    }
}

