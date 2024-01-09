using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Guna.UI2.WinForms;
namespace Gelir_Gider_Takip
{
    class Nesnelerim
    {
     
    }
    class tGuna : Guna2TextBox
    {
        public tGuna()
        {
            this.AutoRoundedCorners = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(117)))), ((int)(((byte)(117)))));
            this.BorderRadius = 14;
            this.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.DefaultText = "";
            this.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Font = new System.Drawing.Font("Segoe UI Historic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Location = new System.Drawing.Point(138, 139);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "guna2TextBox1";
            this.PasswordChar = '\0';
            this.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(117)))), ((int)(((byte)(117)))));
            this.PlaceholderText = "Kullanıcı Adı";
            this.SelectedText = "";
            this.ShadowDecoration.BorderRadius = 20;
            this.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(117)))), ((int)(((byte)(117)))));
            this.ShadowDecoration.Enabled = true;
            this.Size = new System.Drawing.Size(200, 30);
        }
    }
}
