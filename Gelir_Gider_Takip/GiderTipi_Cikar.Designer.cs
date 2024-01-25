
namespace Gelir_Gider_Takip
{
    partial class GiderTipi_Cikar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GiderTipi_Cikar));
            this.btnTipCikar = new Guna.UI2.WinForms.Guna2Button();
            this.cmbGiderTipi = new Guna.UI2.WinForms.Guna2ComboBox();
            this.guna2HtmlLabel7 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.SuspendLayout();
            // 
            // btnTipCikar
            // 
            this.btnTipCikar.AutoRoundedCorners = true;
            this.btnTipCikar.BorderRadius = 23;
            this.btnTipCikar.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnTipCikar.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnTipCikar.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnTipCikar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnTipCikar.FillColor = System.Drawing.Color.DarkRed;
            this.btnTipCikar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnTipCikar.ForeColor = System.Drawing.Color.White;
            this.btnTipCikar.Location = new System.Drawing.Point(54, 132);
            this.btnTipCikar.Name = "btnTipCikar";
            this.btnTipCikar.Size = new System.Drawing.Size(88, 48);
            this.btnTipCikar.TabIndex = 24;
            this.btnTipCikar.Text = "Çıkar";
            this.btnTipCikar.Click += new System.EventHandler(this.btnTipCikar_Click);
            // 
            // cmbGiderTipi
            // 
            this.cmbGiderTipi.BackColor = System.Drawing.Color.Transparent;
            this.cmbGiderTipi.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbGiderTipi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGiderTipi.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbGiderTipi.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbGiderTipi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbGiderTipi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbGiderTipi.ItemHeight = 30;
            this.cmbGiderTipi.Items.AddRange(new object[] {
            "Hepsi"});
            this.cmbGiderTipi.Location = new System.Drawing.Point(12, 75);
            this.cmbGiderTipi.Name = "cmbGiderTipi";
            this.cmbGiderTipi.Size = new System.Drawing.Size(170, 36);
            this.cmbGiderTipi.TabIndex = 23;
            // 
            // guna2HtmlLabel7
            // 
            this.guna2HtmlLabel7.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.guna2HtmlLabel7.ForeColor = System.Drawing.Color.Black;
            this.guna2HtmlLabel7.Location = new System.Drawing.Point(33, 36);
            this.guna2HtmlLabel7.Name = "guna2HtmlLabel7";
            this.guna2HtmlLabel7.Size = new System.Drawing.Size(120, 23);
            this.guna2HtmlLabel7.TabIndex = 22;
            this.guna2HtmlLabel7.Text = "Gelir/Gider Tipi";
            // 
            // GiderTipi_Cikar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(210, 205);
            this.Controls.Add(this.btnTipCikar);
            this.Controls.Add(this.cmbGiderTipi);
            this.Controls.Add(this.guna2HtmlLabel7);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GiderTipi_Cikar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tip Silme";
            this.Load += new System.EventHandler(this.GiderTipi_Cikar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button btnTipCikar;
        private Guna.UI2.WinForms.Guna2ComboBox cmbGiderTipi;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel7;
    }
}