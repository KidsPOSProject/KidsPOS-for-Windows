namespace PosSystem
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
            this.Item_Regist = new System.Windows.Forms.ToolStripMenuItem();
            this.Items_List = new System.Windows.Forms.ToolStripMenuItem();
            this.Sales_List = new System.Windows.Forms.ToolStripMenuItem();
            this.display_timer = new System.Windows.Forms.Timer(this.components);
            this.reg_goods_list = new System.Windows.Forms.ListView();
            this.disp_scan_goods = new System.Windows.Forms.Panel();
            this.scan_goods_price = new System.Windows.Forms.Label();
            this.scan_goods_name = new System.Windows.Forms.Label();
            this.reg_goods_sum = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.reg_account = new System.Windows.Forms.Button();
            this.reg_clear = new System.Windows.Forms.Button();
            this.モード切替ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.practice_mode = new System.Windows.Forms.ToolStripMenuItem();
            this.take_mode = new System.Windows.Forms.ToolStripMenuItem();
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
            this.debug_display.Location = new System.Drawing.Point(0, 542);
            this.debug_display.Name = "debug_display";
            this.debug_display.Size = new System.Drawing.Size(897, 22);
            this.debug_display.TabIndex = 0;
            this.debug_display.Text = "statusStrip1";
            // 
            // disp_now_time
            // 
            this.disp_now_time.Name = "disp_now_time";
            this.disp_now_time.Size = new System.Drawing.Size(9, 17);
            this.disp_now_time.Text = " ";
            // 
            // disp_store_name
            // 
            this.disp_store_name.Name = "disp_store_name";
            this.disp_store_name.Size = new System.Drawing.Size(9, 17);
            this.disp_store_name.Text = " ";
            // 
            // debug_Test
            // 
            this.debug_Test.Name = "debug_Test";
            this.debug_Test.Size = new System.Drawing.Size(9, 17);
            this.debug_Test.Text = " ";
            // 
            // top_menu
            // 
            this.top_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Item_Regist,
            this.Items_List,
            this.Sales_List,
            this.モード切替ToolStripMenuItem});
            this.top_menu.Location = new System.Drawing.Point(0, 0);
            this.top_menu.Name = "top_menu";
            this.top_menu.Size = new System.Drawing.Size(897, 24);
            this.top_menu.TabIndex = 1;
            this.top_menu.Text = "menuStrip1";
            // 
            // Item_Regist
            // 
            this.Item_Regist.Name = "Item_Regist";
            this.Item_Regist.Size = new System.Drawing.Size(65, 20);
            this.Item_Regist.Text = "商品登録";
            this.Item_Regist.Click += new System.EventHandler(this.Item_Regist_Click);
            // 
            // Items_List
            // 
            this.Items_List.Name = "Items_List";
            this.Items_List.Size = new System.Drawing.Size(65, 20);
            this.Items_List.Text = "商品リスト";
            this.Items_List.Click += new System.EventHandler(this.Items_List_Click);
            // 
            // Sales_List
            // 
            this.Sales_List.Name = "Sales_List";
            this.Sales_List.Size = new System.Drawing.Size(65, 20);
            this.Sales_List.Text = "売上リスト";
            this.Sales_List.Click += new System.EventHandler(this.Sales_List_Click);
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
            this.disp_scan_goods.Size = new System.Drawing.Size(872, 104);
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
            this.reg_goods_sum.Font = new System.Drawing.Font("MS UI Gothic", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.reg_goods_sum.Location = new System.Drawing.Point(280, 453);
            this.reg_goods_sum.Name = "reg_goods_sum";
            this.reg_goods_sum.ReadOnly = true;
            this.reg_goods_sum.Size = new System.Drawing.Size(277, 71);
            this.reg_goods_sum.TabIndex = 5;
            this.reg_goods_sum.Text = "0";
            this.reg_goods_sum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(29, 456);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 64);
            this.label1.TabIndex = 4;
            this.label1.Text = "きんがく：";
            // 
            // reg_account
            // 
            this.reg_account.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.reg_account.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.reg_account.Location = new System.Drawing.Point(570, 452);
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
            this.reg_clear.Location = new System.Drawing.Point(796, 453);
            this.reg_clear.Name = "reg_clear";
            this.reg_clear.Size = new System.Drawing.Size(89, 72);
            this.reg_clear.TabIndex = 7;
            this.reg_clear.Text = "クリア";
            this.reg_clear.UseVisualStyleBackColor = true;
            this.reg_clear.Click += new System.EventHandler(this.reg_clear_Click);
            // 
            // モード切替ToolStripMenuItem
            // 
            this.モード切替ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.practice_mode,
            this.take_mode});
            this.モード切替ToolStripMenuItem.Name = "モード切替ToolStripMenuItem";
            this.モード切替ToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.モード切替ToolStripMenuItem.Text = "モード切替";
            // 
            // practice_mode
            // 
            this.practice_mode.Name = "practice_mode";
            this.practice_mode.Size = new System.Drawing.Size(152, 22);
            this.practice_mode.Text = "練習モード";
            this.practice_mode.Click += new System.EventHandler(this.practice_mode_Click);
            // 
            // take_mode
            // 
            this.take_mode.Name = "take_mode";
            this.take_mode.Size = new System.Drawing.Size(152, 22);
            this.take_mode.Text = "本番モード";
            this.take_mode.Click += new System.EventHandler(this.take_mode_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 564);
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
        private System.Windows.Forms.ToolStripMenuItem Items_List;
        private System.Windows.Forms.ToolStripMenuItem Sales_List;
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
    }
}

