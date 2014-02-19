namespace PosSystem_Client
{
    partial class Staff_List
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
            this.label2 = new System.Windows.Forms.Label();
            this.reg_staff_list = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(-228, 205);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(227, 37);
            this.label2.TabIndex = 12;
            this.label2.Text = "しょうひんめい：";
            // 
            // reg_staff_list
            // 
            this.reg_staff_list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reg_staff_list.Font = new System.Drawing.Font("MS UI Gothic", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.reg_staff_list.Location = new System.Drawing.Point(12, 11);
            this.reg_staff_list.MultiSelect = false;
            this.reg_staff_list.Name = "reg_staff_list";
            this.reg_staff_list.Size = new System.Drawing.Size(727, 450);
            this.reg_staff_list.TabIndex = 10;
            this.reg_staff_list.UseCompatibleStateImageBehavior = false;
            this.reg_staff_list.SelectedIndexChanged += new System.EventHandler(this.reg_staff_list_SelectedIndexChanged);
            // 
            // Staff_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 473);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.reg_staff_list);
            this.KeyPreview = true;
            this.Name = "Staff_List";
            this.Text = "Staff_List";
            this.Load += new System.EventHandler(this.Staff_List_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Staff_List_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ListView reg_staff_list;
    }
}