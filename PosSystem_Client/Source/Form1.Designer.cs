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
            this.モード切替ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.practice_mode = new System.Windows.Forms.ToolStripMenuItem();
            this.take_mode = new System.Windows.Forms.ToolStripMenuItem();
            this.Item_Regist = new System.Windows.Forms.ToolStripMenuItem();
            this.ユーザ登録ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.各種リストToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.商品リストToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.商品リストEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.売上リストToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ユーザリストToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ダミーデータ挿入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ユーザーToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.商品ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.システムバーコードToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.印刷ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.確認ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.display_timer = new System.Windows.Forms.Timer(this.components);
            this.reg_goods_list = new System.Windows.Forms.ListView();
            this.disp_scan_goods = new System.Windows.Forms.Panel();
            this.scan_goods_price = new System.Windows.Forms.Label();
            this.scan_goods_name = new System.Windows.Forms.Label();
            this.reg_goods_sum = new System.Windows.Forms.TextBox();
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
            this.モード切替ToolStripMenuItem,
            this.Item_Regist,
            this.ユーザ登録ToolStripMenuItem,
            this.各種リストToolStripMenuItem,
            this.ダミーデータ挿入ToolStripMenuItem,
            this.システムバーコードToolStripMenuItem});
            this.top_menu.Location = new System.Drawing.Point(0, 0);
            this.top_menu.Name = "top_menu";
            this.top_menu.Size = new System.Drawing.Size(897, 26);
            this.top_menu.TabIndex = 1;
            this.top_menu.Text = "menuStrip1";
            this.top_menu.Visible = false;
            // 
            // モード切替ToolStripMenuItem
            // 
            this.モード切替ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.practice_mode,
            this.take_mode});
            this.モード切替ToolStripMenuItem.Name = "モード切替ToolStripMenuItem";
            this.モード切替ToolStripMenuItem.Size = new System.Drawing.Size(80, 22);
            this.モード切替ToolStripMenuItem.Text = "モード切替";

            // 
            // Item_Regist
            // 
            this.Item_Regist.Name = "Item_Regist";
            this.Item_Regist.Size = new System.Drawing.Size(68, 22);
            this.Item_Regist.Text = "商品登録";
            this.Item_Regist.Click += new System.EventHandler(this.Item_Regist_Click);
            // 
            // ユーザ登録ToolStripMenuItem
            // 
            this.ユーザ登録ToolStripMenuItem.Name = "ユーザ登録ToolStripMenuItem";
            this.ユーザ登録ToolStripMenuItem.Size = new System.Drawing.Size(80, 22);
            this.ユーザ登録ToolStripMenuItem.Text = "ユーザ登録";
            this.ユーザ登録ToolStripMenuItem.Click += new System.EventHandler(this.ユーザ登録ToolStripMenuItem_Click);
            // 
            // 各種リストToolStripMenuItem
            // 
            this.各種リストToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.商品リストToolStripMenuItem,
            this.商品リストEditToolStripMenuItem,
            this.売上リストToolStripMenuItem,
            this.ユーザリストToolStripMenuItem1});
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
            // 売上リストToolStripMenuItem
            // 
            this.売上リストToolStripMenuItem.Name = "売上リストToolStripMenuItem";
            this.売上リストToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.売上リストToolStripMenuItem.Text = "売上リスト";
            this.売上リストToolStripMenuItem.Click += new System.EventHandler(this.売上リストToolStripMenuItem_Click);
            // 
            // ユーザリストToolStripMenuItem1
            // 
            this.ユーザリストToolStripMenuItem1.Name = "ユーザリストToolStripMenuItem1";
            this.ユーザリストToolStripMenuItem1.Size = new System.Drawing.Size(168, 22);
            this.ユーザリストToolStripMenuItem1.Text = "ユーザリスト";
            this.ユーザリストToolStripMenuItem1.Click += new System.EventHandler(this.ユーザリストToolStripMenuItem1_Click);
            // 
            // ダミーデータ挿入ToolStripMenuItem
            // 
            this.ダミーデータ挿入ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ユーザーToolStripMenuItem,
            this.商品ToolStripMenuItem});
            this.ダミーデータ挿入ToolStripMenuItem.Name = "ダミーデータ挿入ToolStripMenuItem";
            this.ダミーデータ挿入ToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.ダミーデータ挿入ToolStripMenuItem.Text = "ダミーデータ挿入";
            // 
            // ユーザーToolStripMenuItem
            // 
            this.ユーザーToolStripMenuItem.Name = "ユーザーToolStripMenuItem";
            this.ユーザーToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.ユーザーToolStripMenuItem.Text = "ユーザー";
            this.ユーザーToolStripMenuItem.Click += new System.EventHandler(this.ユーザーToolStripMenuItem_Click);
            // 
            // 商品ToolStripMenuItem
            // 
            this.商品ToolStripMenuItem.Name = "商品ToolStripMenuItem";
            this.商品ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.商品ToolStripMenuItem.Text = "商品";
            this.商品ToolStripMenuItem.Click += new System.EventHandler(this.商品ToolStripMenuItem_Click);
            // 
            // システムバーコードToolStripMenuItem
            // 
            this.システムバーコードToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.印刷ToolStripMenuItem,
            this.確認ToolStripMenuItem});
            this.システムバーコードToolStripMenuItem.Name = "システムバーコードToolStripMenuItem";
            this.システムバーコードToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.システムバーコードToolStripMenuItem.Text = "システムバーコード";
            // 
            // 印刷ToolStripMenuItem
            // 
            this.印刷ToolStripMenuItem.Name = "印刷ToolStripMenuItem";
            this.印刷ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.印刷ToolStripMenuItem.Text = "印刷";
            this.印刷ToolStripMenuItem.Click += new System.EventHandler(this.印刷ToolStripMenuItem_Click);
            // 
            // 確認ToolStripMenuItem
            // 
            this.確認ToolStripMenuItem.Name = "確認ToolStripMenuItem";
            this.確認ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.確認ToolStripMenuItem.Text = "確認";
            // 
            // display_timer
            // 
            this.display_timer.Enabled = true;
            this.display_timer.Tick += new System.EventHandler(this.display_timer_Tick);
            // 
            // reg_goods_list
            // 
            this.reg_goods_list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reg_goods_list.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.reg_goods_list.Location = new System.Drawing.Point(12, 29);
            this.reg_goods_list.MultiSelect = false;
            this.reg_goods_list.Name = "reg_goods_list";
            this.reg_goods_list.Size = new System.Drawing.Size(873, 308);
            this.reg_goods_list.TabIndex = 2;
            this.reg_goods_list.UseCompatibleStateImageBehavior = false;
            this.reg_goods_list.SizeChanged += new System.EventHandler(this.reg_goods_list_SizeChanged);
            this.reg_goods_list.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.reg_goods_list_MouseDoubleClick);
            // 
            // disp_scan_goods
            // 
            this.disp_scan_goods.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.disp_scan_goods.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.disp_scan_goods.Controls.Add(this.scan_goods_price);
            this.disp_scan_goods.Controls.Add(this.scan_goods_name);
            this.disp_scan_goods.Location = new System.Drawing.Point(13, 343);
            this.disp_scan_goods.Name = "disp_scan_goods";
            this.disp_scan_goods.Size = new System.Drawing.Size(872, 92);
            this.disp_scan_goods.TabIndex = 3;
            // 
            // scan_goods_price
            // 
            this.scan_goods_price.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.scan_goods_price.AutoSize = true;
            this.scan_goods_price.Font = new System.Drawing.Font("MS UI Gothic", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.scan_goods_price.Location = new System.Drawing.Point(680, 35);
            this.scan_goods_price.Name = "scan_goods_price";
            this.scan_goods_price.Size = new System.Drawing.Size(0, 37);
            this.scan_goods_price.TabIndex = 1;
            // 
            // scan_goods_name
            // 
            this.scan_goods_name.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.scan_goods_name.AutoSize = true;
            this.scan_goods_name.Font = new System.Drawing.Font("MS UI Gothic", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.scan_goods_name.Location = new System.Drawing.Point(63, 35);
            this.scan_goods_name.Name = "scan_goods_name";
            this.scan_goods_name.Size = new System.Drawing.Size(0, 37);
            this.scan_goods_name.TabIndex = 0;
            // 
            // reg_goods_sum
            // 
            this.reg_goods_sum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reg_goods_sum.Font = new System.Drawing.Font("MS UI Gothic", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.reg_goods_sum.Location = new System.Drawing.Point(280, 438);
            this.reg_goods_sum.Name = "reg_goods_sum";
            this.reg_goods_sum.ReadOnly = true;
            this.reg_goods_sum.Size = new System.Drawing.Size(277, 44);
            this.reg_goods_sum.TabIndex = 5;
            this.reg_goods_sum.Text = "0";
            this.reg_goods_sum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            this.Controls.Add(this.reg_goods_sum);
            this.Controls.Add(this.reg_goods_list);
            this.Controls.Add(this.top_menu);
            this.MainMenuStrip = this.top_menu;
            this.Name = "Form1";
            this.Text = "POS";
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
        private System.Windows.Forms.ToolStripMenuItem Item_Regist;
        private System.Windows.Forms.ToolStripStatusLabel disp_now_time;
        private System.Windows.Forms.ToolStripStatusLabel disp_store_name;
        private System.Windows.Forms.Timer display_timer;
        private System.Windows.Forms.Panel disp_scan_goods;
        private System.Windows.Forms.Label scan_goods_name;
        private System.Windows.Forms.Label scan_goods_price;
        private System.Windows.Forms.ToolStripStatusLabel debug_Test;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button reg_account;
        private System.Windows.Forms.Button reg_clear;
        public System.Windows.Forms.ListView reg_goods_list;
        internal System.Windows.Forms.TextBox reg_goods_sum;
        private System.Windows.Forms.ToolStripMenuItem モード切替ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem practice_mode;
        private System.Windows.Forms.ToolStripMenuItem take_mode;
        private System.Windows.Forms.ToolStripMenuItem ユーザ登録ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem システムバーコードToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 印刷ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 確認ToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label reg_user;
        private System.Windows.Forms.ToolStripMenuItem 各種リストToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 商品リストToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 売上リストToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 商品リストEditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ユーザリストToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ダミーデータ挿入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ユーザーToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 商品ToolStripMenuItem;
    }
}

