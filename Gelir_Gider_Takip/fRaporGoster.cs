using System;
using System.Windows.Forms;
namespace Gelir_Gider_Takip
{
    public partial class fRaporGoster : Form
    {
        public fRaporGoster()
        {
            InitializeComponent();
        }

        private void fRaporGoster_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }
    }
}
