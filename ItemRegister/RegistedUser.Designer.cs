using System.ComponentModel;
using System.Windows.Forms;

namespace DBRegister
{
    partial class RegistedUser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.mGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.mGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.mGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mGridView.Location = new System.Drawing.Point(0, 0);
            this.mGridView.MultiSelect = false;
            this.mGridView.Name = "dataGridView1";
            this.mGridView.ReadOnly = true;
            this.mGridView.RowTemplate.Height = 21;
            this.mGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mGridView.Size = new System.Drawing.Size(284, 262);
            this.mGridView.TabIndex = 0;
            // 
            // RegistedUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.mGridView);
            this.Name = "RegistedUser";
            this.Text = "RegistedUser";
            this.Load += new System.EventHandler(this.RegistedUser_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView mGridView;
    }
}