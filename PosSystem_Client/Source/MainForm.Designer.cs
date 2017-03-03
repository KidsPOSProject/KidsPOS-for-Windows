using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace PosSystem.Source
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private IContainer components = null;

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
            this.components = new Container();
            this.debug_display = new StatusStrip();
            this.disp_now_time = new ToolStripStatusLabel();
            this.toolMenuDebug = new MenuStrip();
            this.ダミーToolStripMenuItem = new ToolStripMenuItem();
            this.商品ToolStripMenuItem1 = new ToolStripMenuItem();
            this.スタッフToolStripMenuItem = new ToolStripMenuItem();
            this.未登録スタッフToolStripMenuItem = new ToolStripMenuItem();
            this.システムバーコード印刷ToolStripMenuItem = new ToolStripMenuItem();
            this.ダミーユーザ印刷ToolStripMenuItem = new ToolStripMenuItem();
            this.印刷ToolStripMenuItem = new ToolStripMenuItem();
            this.確認ToolStripMenuItem = new ToolStripMenuItem();
            this.ユーザーToolStripMenuItem = new ToolStripMenuItem();
            this.商品ToolStripMenuItem = new ToolStripMenuItem();
            this.display_timer = new Timer(this.components);
            this.tItemList = new ListView();
            this.disp_scan_goods = new Panel();
            this.lScanItemPrice = new Label();
            this.lScanItemName = new Label();
            this.tSumItemPrice = new TextBox();
            this.label1 = new Label();
            this.reg_account = new Button();
            this.reg_clear = new Button();
            this.label3 = new Label();
            this.reg_user = new Label();
            this.toolMenuClient = new MenuStrip();
            this.接続先ToolStripMenuItem = new ToolStripMenuItem();
            this.リストToolStripMenuItem = new ToolStripMenuItem();
            this.商リストToolStripMenuItem = new ToolStripMenuItem();
            this.売上リストToolStripMenuItem1 = new ToolStripMenuItem();
            this.toolMenuServer = new MenuStrip();
            this.サーバーを建てるToolStripMenuItem = new ToolStripMenuItem();
            this.printDocument1 = new PrintDocument();
            this.debug_display.SuspendLayout();
            this.toolMenuDebug.SuspendLayout();
            this.disp_scan_goods.SuspendLayout();
            this.toolMenuClient.SuspendLayout();
            this.toolMenuServer.SuspendLayout();
            this.SuspendLayout();
            // 
            // debug_display
            // 
            this.debug_display.Items.AddRange(new ToolStripItem[] {
            this.disp_now_time});
            this.debug_display.Location = new Point(0, 541);
            this.debug_display.Name = "debug_display";
            this.debug_display.Size = new Size(897, 23);
            this.debug_display.TabIndex = 0;
            this.debug_display.Text = "statusStrip1";
            // 
            // disp_now_time
            // 
            this.disp_now_time.Name = "disp_now_time";
            this.disp_now_time.Size = new Size(12, 18);
            this.disp_now_time.Text = " ";
            // 
            // toolMenuDebug
            // 
            this.toolMenuDebug.Items.AddRange(new ToolStripItem[] {
            this.ダミーToolStripMenuItem,
            this.システムバーコード印刷ToolStripMenuItem,
            this.ダミーユーザ印刷ToolStripMenuItem});
            this.toolMenuDebug.Location = new Point(0, 0);
            this.toolMenuDebug.Name = "toolMenuDebug";
            this.toolMenuDebug.Size = new Size(897, 26);
            this.toolMenuDebug.TabIndex = 1;
            this.toolMenuDebug.Text = "menuStrip1";
            this.toolMenuDebug.Visible = false;
            // 
            // ダミーToolStripMenuItem
            // 
            this.ダミーToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
            this.商品ToolStripMenuItem1,
            this.スタッフToolStripMenuItem,
            this.未登録スタッフToolStripMenuItem});
            this.ダミーToolStripMenuItem.Name = "ダミーToolStripMenuItem";
            this.ダミーToolStripMenuItem.Size = new Size(116, 22);
            this.ダミーToolStripMenuItem.Text = "ダミーデータ挿入";
            // 
            // 商品ToolStripMenuItem1
            // 
            this.商品ToolStripMenuItem1.Name = "商品ToolStripMenuItem1";
            this.商品ToolStripMenuItem1.Size = new Size(160, 22);
            this.商品ToolStripMenuItem1.Text = "商品";
            this.商品ToolStripMenuItem1.Click += new EventHandler(this.商品ToolStripMenuItem1_Click);
            // 
            // スタッフToolStripMenuItem
            // 
            this.スタッフToolStripMenuItem.Name = "スタッフToolStripMenuItem";
            this.スタッフToolStripMenuItem.Size = new Size(160, 22);
            this.スタッフToolStripMenuItem.Text = "スタッフ";
            this.スタッフToolStripMenuItem.Click += new EventHandler(this.スタッフToolStripMenuItem_Click);
            // 
            // 未登録スタッフToolStripMenuItem
            // 
            this.未登録スタッフToolStripMenuItem.Name = "未登録スタッフToolStripMenuItem";
            this.未登録スタッフToolStripMenuItem.Size = new Size(160, 22);
            this.未登録スタッフToolStripMenuItem.Text = "未登録スタッフ";
            this.未登録スタッフToolStripMenuItem.Click += new EventHandler(this.未登録スタッフToolStripMenuItem_Click);
            // 
            // システムバーコード印刷ToolStripMenuItem
            // 
            this.システムバーコード印刷ToolStripMenuItem.Name = "システムバーコード印刷ToolStripMenuItem";
            this.システムバーコード印刷ToolStripMenuItem.Size = new Size(152, 22);
            this.システムバーコード印刷ToolStripMenuItem.Text = "システムバーコード印刷";
            this.システムバーコード印刷ToolStripMenuItem.Click += new EventHandler(this.システムバーコード印刷ToolStripMenuItem_Click);
            // 
            // ダミーユーザ印刷ToolStripMenuItem
            // 
            this.ダミーユーザ印刷ToolStripMenuItem.Name = "ダミーユーザ印刷ToolStripMenuItem";
            this.ダミーユーザ印刷ToolStripMenuItem.Size = new Size(116, 22);
            this.ダミーユーザ印刷ToolStripMenuItem.Text = "ダミーユーザ印刷";
            this.ダミーユーザ印刷ToolStripMenuItem.Click += new EventHandler(this.ダミーユーザ印刷ToolStripMenuItem_Click);
            // 
            // 印刷ToolStripMenuItem
            // 
            this.印刷ToolStripMenuItem.Name = "印刷ToolStripMenuItem";
            this.印刷ToolStripMenuItem.Size = new Size(32, 19);
            // 
            // 確認ToolStripMenuItem
            // 
            this.確認ToolStripMenuItem.Name = "確認ToolStripMenuItem";
            this.確認ToolStripMenuItem.Size = new Size(32, 19);
            // 
            // ユーザーToolStripMenuItem
            // 
            this.ユーザーToolStripMenuItem.Name = "ユーザーToolStripMenuItem";
            this.ユーザーToolStripMenuItem.Size = new Size(32, 19);
            // 
            // 商品ToolStripMenuItem
            // 
            this.商品ToolStripMenuItem.Name = "商品ToolStripMenuItem";
            this.商品ToolStripMenuItem.Size = new Size(32, 19);
            // 
            // display_timer
            // 
            this.display_timer.Enabled = true;
            this.display_timer.Tick += new EventHandler(this.display_timer_Tick);
            // 
            // tItemList
            // 
            this.tItemList.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
                        | AnchorStyles.Left)
                        | AnchorStyles.Right)));
            this.tItemList.Font = new Font("MS UI Gothic", 36F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(128)));
            this.tItemList.Location = new Point(12, 29);
            this.tItemList.MultiSelect = false;
            this.tItemList.Name = "tItemList";
            this.tItemList.Size = new Size(873, 308);
            this.tItemList.TabIndex = 2;
            this.tItemList.UseCompatibleStateImageBehavior = false;
            this.tItemList.SizeChanged += new EventHandler(this.OnSizeChangedReadItemList);
            // 
            // disp_scan_goods
            // 
            this.disp_scan_goods.Anchor = ((AnchorStyles)(((AnchorStyles.Bottom | AnchorStyles.Left)
                        | AnchorStyles.Right)));
            this.disp_scan_goods.BackColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.disp_scan_goods.Controls.Add(this.lScanItemPrice);
            this.disp_scan_goods.Controls.Add(this.lScanItemName);
            this.disp_scan_goods.Location = new Point(13, 343);
            this.disp_scan_goods.Name = "disp_scan_goods";
            this.disp_scan_goods.Size = new Size(872, 92);
            this.disp_scan_goods.TabIndex = 3;
            // 
            // lScanItemPrice
            // 
            this.lScanItemPrice.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Bottom)));
            this.lScanItemPrice.AutoSize = true;
            this.lScanItemPrice.Font = new Font("MS UI Gothic", 27.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(128)));
            this.lScanItemPrice.Location = new Point(680, 35);
            this.lScanItemPrice.Name = "lScanItemPrice";
            this.lScanItemPrice.Size = new Size(0, 37);
            this.lScanItemPrice.TabIndex = 1;
            // 
            // lScanItemName
            // 
            this.lScanItemName.Anchor = AnchorStyles.Top;
            this.lScanItemName.AutoSize = true;
            this.lScanItemName.Font = new Font("MS UI Gothic", 27.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(128)));
            this.lScanItemName.Location = new Point(63, 35);
            this.lScanItemName.Name = "lScanItemName";
            this.lScanItemName.Size = new Size(0, 37);
            this.lScanItemName.TabIndex = 0;
            // 
            // tSumItemPrice
            // 
            this.tSumItemPrice.Anchor = ((AnchorStyles)(((AnchorStyles.Bottom | AnchorStyles.Left)
                        | AnchorStyles.Right)));
            this.tSumItemPrice.Font = new Font("MS UI Gothic", 27.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(128)));
            this.tSumItemPrice.Location = new Point(280, 438);
            this.tSumItemPrice.Name = "tSumItemPrice";
            this.tSumItemPrice.ReadOnly = true;
            this.tSumItemPrice.Size = new Size(277, 44);
            this.tSumItemPrice.TabIndex = 5;
            this.tSumItemPrice.Text = "0";
            this.tSumItemPrice.TextAlign = HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.Anchor = ((AnchorStyles)(((AnchorStyles.Bottom | AnchorStyles.Left)
                        | AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new Font("MS UI Gothic", 27.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new Point(130, 441);
            this.label1.Name = "label1";
            this.label1.Size = new Size(144, 37);
            this.label1.TabIndex = 4;
            this.label1.Text = "きんがく：";
            // 
            // reg_account
            // 
            this.reg_account.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.reg_account.Font = new Font("MS UI Gothic", 24F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(128)));
            this.reg_account.Location = new Point(578, 441);
            this.reg_account.Name = "reg_account";
            this.reg_account.Size = new Size(198, 72);
            this.reg_account.TabIndex = 6;
            this.reg_account.Text = "かいけい";
            this.reg_account.UseVisualStyleBackColor = true;
            this.reg_account.MouseClick += new MouseEventHandler(this.reg_account_MouseClick);
            // 
            // reg_clear
            // 
            this.reg_clear.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.reg_clear.Font = new Font("MS UI Gothic", 24F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(128)));
            this.reg_clear.Location = new Point(796, 441);
            this.reg_clear.Name = "reg_clear";
            this.reg_clear.Size = new Size(89, 72);
            this.reg_clear.TabIndex = 7;
            this.reg_clear.Text = "クリア";
            this.reg_clear.UseVisualStyleBackColor = true;
            this.reg_clear.Click += new EventHandler(this.reg_clear_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new Font("MS UI Gothic", 26.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new Point(19, 489);
            this.label3.Name = "label3";
            this.label3.Size = new Size(255, 35);
            this.label3.TabIndex = 8;
            this.label3.Text = "レジ打ち担当者：";
            // 
            // reg_user
            // 
            this.reg_user.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            this.reg_user.AutoSize = true;
            this.reg_user.Font = new Font("MS UI Gothic", 26.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(128)));
            this.reg_user.Location = new Point(302, 489);
            this.reg_user.Name = "reg_user";
            this.reg_user.Size = new Size(26, 35);
            this.reg_user.TabIndex = 9;
            this.reg_user.Text = " ";
            // 
            // toolMenuClient
            // 
            this.toolMenuClient.Items.AddRange(new ToolStripItem[] {
            this.接続先ToolStripMenuItem,
            this.リストToolStripMenuItem});
            this.toolMenuClient.Location = new Point(0, 0);
            this.toolMenuClient.Name = "toolMenuClient";
            this.toolMenuClient.Size = new Size(897, 26);
            this.toolMenuClient.TabIndex = 10;
            this.toolMenuClient.Text = "menuStrip1";
            this.toolMenuClient.Visible = false;
            // 
            // 接続先ToolStripMenuItem
            // 
            this.接続先ToolStripMenuItem.Name = "接続先ToolStripMenuItem";
            this.接続先ToolStripMenuItem.Size = new Size(72, 22);
            this.接続先ToolStripMenuItem.Text = "接続先 ▼";
            // 
            // リストToolStripMenuItem
            // 
            this.リストToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
            this.商リストToolStripMenuItem,
            this.売上リストToolStripMenuItem1});
            this.リストToolStripMenuItem.Name = "リストToolStripMenuItem";
            this.リストToolStripMenuItem.Size = new Size(56, 22);
            this.リストToolStripMenuItem.Text = "リスト";
            // 
            // 商リストToolStripMenuItem
            // 
            this.商リストToolStripMenuItem.Name = "商リストToolStripMenuItem";
            this.商リストToolStripMenuItem.Size = new Size(136, 22);
            this.商リストToolStripMenuItem.Text = "商品リスト";
            this.商リストToolStripMenuItem.Click += new EventHandler(this.商リストToolStripMenuItem_Click);
            // 
            // 売上リストToolStripMenuItem1
            // 
            this.売上リストToolStripMenuItem1.Name = "売上リストToolStripMenuItem1";
            this.売上リストToolStripMenuItem1.Size = new Size(136, 22);
            this.売上リストToolStripMenuItem1.Text = "売上リスト";
            this.売上リストToolStripMenuItem1.Click += new EventHandler(this.売上リストToolStripMenuItem1_Click);
            // 
            // toolMenuServer
            // 
            this.toolMenuServer.Items.AddRange(new ToolStripItem[] {
            this.サーバーを建てるToolStripMenuItem});
            this.toolMenuServer.Location = new Point(0, 0);
            this.toolMenuServer.Name = "toolMenuServer";
            this.toolMenuServer.Size = new Size(897, 26);
            this.toolMenuServer.TabIndex = 11;
            this.toolMenuServer.Text = "menuStrip1";
            this.toolMenuServer.Visible = false;
            // 
            // サーバーを建てるToolStripMenuItem
            // 
            this.サーバーを建てるToolStripMenuItem.Name = "サーバーを建てるToolStripMenuItem";
            this.サーバーを建てるToolStripMenuItem.Size = new Size(116, 22);
            this.サーバーを建てるToolStripMenuItem.Text = "サーバーを建てる";
            this.サーバーを建てるToolStripMenuItem.Click += new EventHandler(this.サーバーを建てるToolStripMenuItem_Click);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new SizeF(6F, 12F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(897, 564);
            this.Controls.Add(this.toolMenuServer);
            this.Controls.Add(this.reg_user);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.reg_clear);
            this.Controls.Add(this.reg_account);
            this.Controls.Add(this.debug_display);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.disp_scan_goods);
            this.Controls.Add(this.tSumItemPrice);
            this.Controls.Add(this.tItemList);
            this.Controls.Add(this.toolMenuClient);
            this.Controls.Add(this.toolMenuDebug);
            this.MainMenuStrip = this.toolMenuDebug;
            this.Name = "Form1";
            this.Text = "POS";
            this.FormClosing += new FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new EventHandler(this.Form1_Load);
            this.KeyDown += new KeyEventHandler(this.Form1_KeyDown);
            this.debug_display.ResumeLayout(false);
            this.debug_display.PerformLayout();
            this.toolMenuDebug.ResumeLayout(false);
            this.toolMenuDebug.PerformLayout();
            this.disp_scan_goods.ResumeLayout(false);
            this.disp_scan_goods.PerformLayout();
            this.toolMenuClient.ResumeLayout(false);
            this.toolMenuClient.PerformLayout();
            this.toolMenuServer.ResumeLayout(false);
            this.toolMenuServer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private StatusStrip debug_display;
        private MenuStrip toolMenuDebug;
        private ToolStripStatusLabel disp_now_time;
        private Timer display_timer;
        private Panel disp_scan_goods;
        private Label lScanItemName;
        private Label lScanItemPrice;
        private Label label1;
        private Button reg_account;
        private Button reg_clear;
        public ListView tItemList;
        internal TextBox tSumItemPrice;
        private ToolStripMenuItem 印刷ToolStripMenuItem;
        private ToolStripMenuItem 確認ToolStripMenuItem;
        private Label label3;
        public Label reg_user;
        private ToolStripMenuItem ユーザーToolStripMenuItem;
        private ToolStripMenuItem 商品ToolStripMenuItem;
        private MenuStrip toolMenuClient;
        private ToolStripMenuItem 接続先ToolStripMenuItem;
        private ToolStripMenuItem ダミーToolStripMenuItem;
        private ToolStripMenuItem 商品ToolStripMenuItem1;
        private ToolStripMenuItem スタッフToolStripMenuItem;
        private ToolStripMenuItem 未登録スタッフToolStripMenuItem;
        private MenuStrip toolMenuServer;
        private ToolStripMenuItem サーバーを建てるToolStripMenuItem;
        private ToolStripMenuItem リストToolStripMenuItem;
        private ToolStripMenuItem 商リストToolStripMenuItem;
        private ToolStripMenuItem 売上リストToolStripMenuItem1;
        private ToolStripMenuItem システムバーコード印刷ToolStripMenuItem;
        private PrintDocument printDocument1;
        private ToolStripMenuItem ダミーユーザ印刷ToolStripMenuItem;
    }
}

