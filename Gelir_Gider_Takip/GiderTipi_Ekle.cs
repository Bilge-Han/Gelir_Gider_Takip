using Gelir_Gider_Takip.Cls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gelir_Gider_Takip
{
    public partial class GiderTipi_Ekle : Form
    {
        public GiderTipi_Ekle()
        {
            InitializeComponent();
        }

        private void GiderTipi_Ekle_Load(object sender, EventArgs e)
        {

        }

        private void btnPrgKapat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            int kontrol = Convert.ToInt16(glb.sql.Command("select count(*) from GIDER_TIPLERI where gdr_Gider_Kod = '" + txtGiderTipKod + "'"));
            if (kontrol == 0)
            {
                glb.sql.Command(""
                         + "       INSERT INTO[dbo].[GIDER_TIPLERI] "
                         + "              ([gdr_Gider_Kod] "
                         + "              ,[gdr_Gider_Ad]) "
                         + "        VALUES "
                         + "              ( '" + txtGiderTipKod.Text + "' "//< gdr_Gider_Kod, nvarchar(50),> "
                         + "              , '" + txtGiderTipAd.Text + "' "//< gdr_Gider_Kod, nvarchar(50),> "
                         + ") ");
                if (glb.sql.exception == null)
                {
                    MessageBox.Show("Kayıt Başarılı");
                }
            }
            else
            {
                glb.sql.Command(""
                    + "    update [dbo].[GIDER_TIPLERI] set "
                    + "              "
                    + "               [gdr_Gider_Ad]  = '" + txtGiderTipKod.Text + "' "//< gdr_Gider_Kod, nvarchar(50),> "
                    + "        where  gdr_Gider_Kod = '" + txtGiderTipKod.Text + "'"
                    + " ");
                if (glb.sql.exception == null)
                {
                    MessageBox.Show("Güncelleme Başarılı");
                }
            }
        }
    }
}
