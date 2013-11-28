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
            this.商品登録ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.商品リストToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.売上リストToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.display_timer = new System.Windows.Forms.Timer(this.components);
            this.reg_goods_list = new System.Windows.Forms.ListView();
            this.disp_scan_goods = new System.Windows.Forms.Panel();
            this.scan_goods_price = new System.Windows.Forms.Label();
            this.scan_goods_name = new System.Windows.Forms.Label();
            this.reg_goods_sum = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.reg_account = new System.Windows.Forms.Button();
            this.reg_clear = new System.Windows.Forms.Button();
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
            this.商品登録ToolStripMenuItem,
            this.商品リストToolStripMenuItem,
            this.売上リストToolStripMenuItem});
            this.top_menu.Location = new System.Drawing.Point(0, 0);
            this.top_menu.Name = "top_menu";
            this.top_menu.Size = new System.Drawing.Size(897, 26);
            this.top_menu.TabIndex = 1;
            this.top_menu.Text = "menuStrip1";
            // 
            // 商品登録ToolStripMenuItem
            // 
            this.商品登録ToolStripMenuItem.Name = "商品登録ToolStripMenuItem";
            this.商品登録ToolStripMenuItem.Size = new System.Drawing.Size(68, 22);
            this.商品登録ToolStripMenuItem.Text = "商品登録";
            this.商品登録ToolStripMenuItem.Click += new System.EventHandler(this.商品登録ToolStripMenuItem_Click);
            // 
            // 商品リストToolStripMenuItem
            // 
            this.商品リストToolStripMenuItem.Name = "商品リストToolStripMenuItem";
            this.商品リストToolStripMenuItem.Size = new System.Drawing.Size(80, 22);
            this.商品リストToolStripMenuItem.Text = "商品リスト";
            this.商品リストToolStripMenuItem.Click += new System.EventHandler(this.商品リストToolStripMenuItem_Click);
            // 
            // 売上リストToolStripMenuItem
            // 
            this.売上リストToolStripMenuItem.Name = "売上リストToolStripMenuItem";
            this.売上リストToolStripMenuItem.Size = new System.Drawing.Size(80, 22);
            this.売上リストToolStripMenuItem.Text = "売上リスト";
            this.売上リストToolStripMenuItem.Click += new System.EventHandler(this.売上リストToolStripMenuItem_Click);
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
            this.reg_goods_list.SelectedIndexChanged += new System.EventHandler(this.reg_goods_list_SelectedIndexChanged);
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
            this.reg_goods_sum.TextChanged += new System.EventHandler(this.reg_goods_sum_TextChanged);
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
        private System.Windows.Forms.ToolStripMenuItem 商品登録ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 商品リストToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 売上リストToolStripMenuItem;
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
    }
}

