namespace Loader
{
    partial class FrmHelp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHelp));
            this.D2 = new System.Windows.Forms.Label();
            this.D1 = new System.Windows.Forms.Label();
            this.PageSwitcher = new System.Windows.Forms.Button();
            this.D3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // D2
            // 
            this.D2.AutoSize = true;
            this.D2.Location = new System.Drawing.Point(12, 9);
            this.D2.Name = "D2";
            this.D2.Size = new System.Drawing.Size(657, 403);
            this.D2.TabIndex = 5;
            this.D2.Text = resources.GetString("D2.Text");
            this.D2.Visible = false;
            // 
            // D1
            // 
            this.D1.AutoSize = true;
            this.D1.Location = new System.Drawing.Point(12, 9);
            this.D1.Name = "D1";
            this.D1.Size = new System.Drawing.Size(614, 403);
            this.D1.TabIndex = 6;
            this.D1.Text = resources.GetString("D1.Text");
            // 
            // PageSwitcher
            // 
            this.PageSwitcher.Location = new System.Drawing.Point(541, 427);
            this.PageSwitcher.Name = "PageSwitcher";
            this.PageSwitcher.Size = new System.Drawing.Size(149, 23);
            this.PageSwitcher.TabIndex = 7;
            this.PageSwitcher.Text = "Следующая страница";
            this.PageSwitcher.UseVisualStyleBackColor = true;
            this.PageSwitcher.Click += new System.EventHandler(this.button1_Click);
            // 
            // D3
            // 
            this.D3.AutoSize = true;
            this.D3.Location = new System.Drawing.Point(12, 9);
            this.D3.Name = "D3";
            this.D3.Size = new System.Drawing.Size(655, 364);
            this.D3.TabIndex = 8;
            this.D3.Text = resources.GetString("D3.Text");
            this.D3.Visible = false;
            // 
            // FrmHelp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 459);
            this.Controls.Add(this.D3);
            this.Controls.Add(this.PageSwitcher);
            this.Controls.Add(this.D1);
            this.Controls.Add(this.D2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmHelp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Краткая справка";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmHelp_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label D2;
        private System.Windows.Forms.Label D1;
        private System.Windows.Forms.Button PageSwitcher;
        private System.Windows.Forms.Label D3;
    }
}