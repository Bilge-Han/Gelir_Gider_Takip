using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gelir_Gider_Takip
{
    public partial class GiderTipi_Cikar : Form
    {
        public GiderTipi_Cikar()
        {
            InitializeComponent();
        }
        Gelir_Gider_TakipEntities db = new Gelir_Gider_TakipEntities();
        private void GiderTipi_Cikar_Load(object sender, EventArgs e)
        {
            ComboDoldur();
        }
        void ComboDoldur()
        {
            cmbGiderTipi.DataSource = db.GIDER_TIPLERI.OrderBy(x => x.gdr_ID).ToList();
            cmbGiderTipi.DisplayMember = "gdr_Gider_Ad";
            cmbGiderTipi.ValueMember = "gdr_Gider_Kod";
        }
        private void btnTipCikar_Click(object sender, EventArgs e)
        {
            string tipAd = cmbGiderTipi.SelectedValue.ToString();
            if (cmbGiderTipi.SelectedValue.ToString()!="Hepsi")
            {
                if (db.GIDER_TIPLERI.Any(x => x.gdr_Gider_Kod == tipAd))
                {
                    var gt = db.GIDER_TIPLERI.Where(x=>x.gdr_Gider_Kod==tipAd).FirstOrDefault();
                    db.GIDER_TIPLERI.Remove(gt);
                    db.SaveChanges();
                    ComboDoldur();
                    MessageBox.Show(tipAd+" adlı tip silinmiştir.");
                }
            }
            else MessageBox.Show("Bu tipi silemezsiniz.");

        }


    }
}
