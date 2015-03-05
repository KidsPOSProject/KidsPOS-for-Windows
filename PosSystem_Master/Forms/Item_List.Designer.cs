namespace PosSystem_Master
{
    partial class ItemList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.change_item_name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.change_item_price = new System.Windows.Forms.TextBox();
            this.updateItem = new System.Windows.Forms.Button();
            this.edit_panel = new System.Windows.Forms.Panel();
            this.mGridView = new System.Windows.Forms.DataGridView();
            this.edit_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 37);
            this.label1.TabIndex = 4;
            this.label1.Text = "しょうひんリスト";
            // 
            // change_item_name
            // 
            this.change_item_name.Enabled = false;
            this.change_item_name.Font = new System.Drawing.Font("MS UI Gothic", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.change_item_name.Location = new System.Drawing.Point(260, 16);
            this.change_item_name.Name = "change_item_name";
            this.change_item_name.Size = new System.Drawing.Size(283, 44);
            this.change_item_name.TabIndex = 5;
            this.change_item_name.Text = " ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(27, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(227, 37);
            this.label2.TabIndex = 6;
            this.label2.Text = "しょうひんめい：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(110, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 37);
            this.label3.TabIndex = 8;
            this.label3.Text = "きんがく：";
            // 
            // change_item_price
            // 
            this.change_item_price.Enabled = false;
            this.change_item_price.Font = new System.Drawing.Font("MS UI Gothic", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.change_item_price.Location = new System.Drawing.Point(260, 66);
            this.change_item_price.Name = "change_item_price";
            this.change_item_price.Size = new System.Drawing.Size(283, 44);
            this.change_item_price.TabIndex = 7;
            this.change_item_price.Text = " ";
            // 
            // updateItem
            // 
            this.updateItem.Enabled = false;
            this.updateItem.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.updateItem.Location = new System.Drawing.Point(582, 19);
            this.updateItem.Name = "updateItem";
            this.updateItem.Size = new System.Drawing.Size(168, 91);
            this.updateItem.TabIndex = 9;
            this.updateItem.Text = "こうしん";
            this.updateItem.UseVisualStyleBackColor = true;
            this.updateItem.Click += new System.EventHandler(this.updateItem_Click);
            // 
            // edit_panel
            // 
            this.edit_panel.Controls.Add(this.label2);
            this.edit_panel.Controls.Add(this.change_item_name);
            this.edit_panel.Controls.Add(this.updateItem);
            this.edit_panel.Controls.Add(this.change_item_price);
            this.edit_panel.Controls.Add(this.label3);
            this.edit_panel.Location = new System.Drawing.Point(12, 285);
            this.edit_panel.Name = "edit_panel";
            this.edit_panel.Size = new System.Drawing.Size(768, 126);
            this.edit_panel.TabIndex = 11;
            this.edit_panel.Visible = false;
            // 
            // mGridView
            // 
            this.mGridView.AllowUserToAddRows = false;
            this.mGridView.AllowUserToDeleteRows = false;
            this.mGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.mGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.mGridView.Location = new System.Drawing.Point(12, 53);
            this.mGridView.MultiSelect = false;
            this.mGridView.Name = "mGridView";
            this.mGridView.ReadOnly = true;
            this.mGridView.RowTemplate.Height = 21;
            this.mGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mGridView.Size = new System.Drawing.Size(768, 226);
            this.mGridView.TabIndex = 22;
            // 
            // ItemList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 421);
            this.Controls.Add(this.mGridView);
            this.Controls.Add(this.edit_panel);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.Name = "ItemList";
            this.Text = "Item_List";
            this.Load += new System.EventHandler(this.Item_List_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Item_List_KeyDown);
            this.edit_panel.ResumeLayout(false);
            this.edit_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox change_item_name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox change_item_price;
        private System.Windows.Forms.Button updateItem;
        private System.Windows.Forms.Panel edit_panel;
        private System.Windows.Forms.DataGridView mGridView;
    }
}