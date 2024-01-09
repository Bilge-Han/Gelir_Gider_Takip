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
    public partial class Form1 : Form
    {
        private Timer _timer;
        private DateTime _now;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            dgv_Update();
            combo_doldur();
            timer_Baslat();
        }
        void timer_Baslat ()
        {
            _timer = new Timer();
            _timer.Interval = 1000; // 1000 milisaniye (1 saniye) interval
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            _now = DateTime.Now;
            lblTrh.Text = _now.ToString();
        }
        void combo_doldur()
        {
            cmbGiderTipi.DataSource = glb.sql.Table("select gdr_Gider_Kod,gdr_Gider_Ad  from GIDER_TIPLERI");
            cmbGiderTipi.DisplayMember = "gdr_Gider_Ad";
            cmbGiderTipi.ValueMember = "gdr_Gider_Kod";

            cmbListeTip.DataSource = glb.sql.Table("select gdr_Gider_Kod,gdr_Gider_Ad  from GIDER_TIPLERI");
            cmbListeTip.DisplayMember = "gdr_Gider_Ad";
            cmbListeTip.ValueMember = "gdr_Gider_Kod";

        }
        string tip()
        {
            string tip = "";
            if (rdGelir.Checked == true)
            {
                tip = "Gelir";
            }
            if (rdGider.Checked == true)
            {
                tip = "Gider";
            }
            return tip;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (tip() != "")
            {
                if (Convert.ToInt32(calID.Text) == 0)
                {
                    glb.sql.Command(""
                            + "    INSERT INTO[dbo].[GELIR_GIDER_KAYITLARI]     "
                            + "           ([ggr_tarih]                          "
                            + "           ,[ggr_tipi]                           "
                            + "           ,[ggr_gider_tipi]                     "
                            + "           ,[ggr_tutar]                          "
                            + "           ,[ggr_aciklama]                       "
                            + "           ,[ggr_kayit_tarih])                   "
                            + "     VALUES                                      "
                            + "           ('" + Convert.ToDateTime(dtTarih.Value).ToString("yyyyMMdd") + "'   " //< ggr_tarih, datetime,>              "
                            + "           ,'" + tip() + "'   " //< ggr_tipi, nvarchar(50),>           "
                            + "           ,'" + cmbGiderTipi.SelectedValue.ToString() + "'   " //< ggr_gider_tipi, nvarchar(50),>     "
                            + "           , " + Convert.ToDouble(calTutar.Text).ToString().Replace(",", ".") + "   " //< ggr_tutar, float,>                 "
                            + "           ,'" + txtAciklama.Text + "'   " //< ggr_aciklama, nvarchar(max),>      "
                            + "           , getdate()  " //< ggr_kayit_tarih, datetime,> "
                            + " )       "
                            );
                    dgv_Update();
                }
                else
                {
                    glb.sql.Command(""
                           + "    update [dbo].[GELIR_GIDER_KAYITLARI]   set  "
                           + "            [ggr_tarih]        = '" + Convert.ToDateTime(dtTarih.Value).ToString("yyyyMMdd") + "'   " //< ggr_tarih, datetime,>              "                  "
                           + "           ,[ggr_tipi]         = '" + tip() + "'   " //< ggr_tipi, nvarchar(50),>           "                  "
                           + "           ,[ggr_gider_tipi]   = '" + cmbGiderTipi.SelectedValue.ToString() + "'   " //< ggr_gider_tipi, nvarchar(50),>     "                  "
                           + "           ,[ggr_tutar]        = '" + Convert.ToDouble(calTutar.Text).ToString().Replace(",", ".") + "'   " //< ggr_tutar, float,>                 "                  "
                           + "           ,[ggr_aciklama]     = '" + txtAciklama.Text + "'   " //< ggr_aciklama, nvarchar(max),>      "                  "
                           + "     where     ggr_ID = " + Convert.ToInt32(calID.Text) + "                                  "
                           );
                    dgv_Update();
                }
            }
            else MessageBox.Show("Lütfen İşlem Tipi Seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }
        void dgv_Update()
        {
            guna2DataGridView1.DataSource = glb.sql.Table("select * from dbo.fn_GelirGiderListe(0) "+where);
            if (guna2DataGridView1.Rows.Count > 0)
            {
                double gelir = 0;
                double gider = 0;
                for (int i = 0; i < guna2DataGridView1.Rows.Count; i++)
                {
                    if (guna2DataGridView1.Rows[i].Cells["Tipi"].Value.ToString() == "Gelir")
                    {
                        gelir += Convert.ToDouble(guna2DataGridView1.Rows[i].Cells["Tutar"].Value.ToString());
                    }
                    else if (guna2DataGridView1.Rows[i].Cells["Tipi"].Value.ToString() == "Gider")
                    {
                        gider += Convert.ToDouble(guna2DataGridView1.Rows[i].Cells["Tutar"].Value.ToString());
                    }
                }
                double hesap = gelir - gider;
                //txGelir.Text = gelir.ToString("C2");
                //txGider.Text = gider.ToString("C2");
                //txHesap.Text = hesap.ToString("C2");
            }
            where = " ";
        }

        private void btnPrgKapat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.guna2DataGridView1.Rows[e.RowIndex];
                calID.Text = row.Cells["KAYITNO"].Value.ToString();
                calTutar.Text = row.Cells["Tutar"].Value.ToString();
                txtAciklama.Text = row.Cells["Açıklama"].Value.ToString();
                string gelir_gider = row.Cells["Tipi"].Value.ToString();

                switch (gelir_gider)
                {
                    case "Gelir":
                        rdGelir.Checked = true;
                        break;
                    case "Gider":
                        rdGider.Checked = true;
                        break;
                    default:
                        rdGider.Checked = false;
                        rdGelir.Checked = false;
                        break;

                }
                dtTarih.Text = row.Cells["Tarih"].Value.ToString();
                cmbGiderTipi.SelectedValue = row.Cells["Gider Tip Kod"].Value.ToString();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bu " + tip() + " kaydını silmek istediğinzden emin misiniz?", "Onay Verin!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                glb.sql.Command("delete from  [dbo].[GELIR_GIDER_KAYITLARI]    where     ggr_ID = " + Convert.ToInt32(calID.Text) + "     ");
                dgv_Update();
            }
        }
        string where = " ";
        private void rdListeHepsi_CheckedChanged(object sender, EventArgs e)
        {
            //if (where!=" ")
            //{
            //    if (rdListeGelir.Checked == true)
            //    {
            //        where = " and Tipi = 'Gelir'  ";
            //    }
            //    if (rdListeGider.Checked == true)
            //    {
            //        where = " and Tipi = 'Gider'  ";
            //    }
            //    if (rdListeHepsi.Checked == true)
            //    {
            //        where = " ";
            //    }
            //}
            //else
            //{
            //    if (rdListeGelir.Checked == true)
            //    {
            //        where = " where Tipi = 'Gelir'  ";
            //    }
            //    if (rdListeGider.Checked == true)
            //    {
            //        where = " where Tipi = 'Gider'  ";
            //    }
            //    if (rdListeHepsi.Checked == true)
            //    {
            //        where = " ";
            //    }
            //}
            
            //dgv_Update();
        }

        private void btnTipEkle_Click(object sender, EventArgs e)
        {
            new GiderTipi_Ekle() { }.ShowDialog();
            combo_doldur();
        }

        private void dtBitis_ValueChanged(object sender, EventArgs e)
        {
            //if (where!=" ")
            //{
            //    where += " and ( [Tarih] >= " + dtBaslangic.Value.ToString("yyyy-MM-dd") + " and [Tarih] <= " + dtBitis.Value.ToString("yyyy-MM-dd") + ")";
            //}
            //else
            //{
            //    where = "where ( [Tarih] >= " + dtBaslangic.Value.ToString("yyyy-MM-dd") + " and [Tarih] <= " + dtBitis.Value.ToString("yyyy-MM-dd") + " )";
            //}
            //dgv_Update();
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            DateTime baslangic = Convert.ToDateTime(dtBaslangic.Value);
            DateTime bitis = Convert.ToDateTime(dtBitis.Value).AddDays(1);
            if (cmbListeTip.Text=="Hepsi")
            {
                if (rdListeGelir.Checked == true)
                {
                    where = " where Tipi = 'Gelir' and ( '" + baslangic.ToString("yyyyMMdd") + "' <= [Tarih] and  [Tarih] < '" + bitis.ToString("yyyyMMdd") + "') ";
                }
                if (rdListeGider.Checked == true)
                {
                    where = " where Tipi = 'Gider' and ( '" + baslangic.ToString("yyyy-MM-dd") + "' <= [Tarih] and  [Tarih] < '" + bitis.ToString("yyyy-MM-dd") + "') ";
                }
                if (rdListeHepsi.Checked == true)
                {
                    where = " where ( '" + baslangic.ToString("yyyy-MM-dd") + " '<= [Tarih] and  [Tarih] < '" + bitis.ToString("yyyy-MM-dd") + "')";
                }
            }
            else if (cmbListeTip.Text!="")
            {
                if (rdListeGelir.Checked == true)
                {
                    where = " where Tipi = 'Gelir' and ( '" + baslangic.ToString("yyyyMMdd") + "' <= [Tarih] and  [Tarih] < '" + bitis.ToString("yyyyMMdd") + "') and ([Gider Tipi] = '" + cmbListeTip.Text + "')";
                }
                if (rdListeGider.Checked == true)
                {
                    where = " where Tipi = 'Gider' and ( '" + baslangic.ToString("yyyy-MM-dd") + "' <= [Tarih] and  [Tarih] < '" + bitis.ToString("yyyy-MM-dd") + "') and ([Gider Tipi] = '" + cmbListeTip.Text + "')";
                }
                if (rdListeHepsi.Checked == true)
                {
                    where = " where ( '" + baslangic.ToString("yyyy-MM-dd") + " '<= [Tarih] and  [Tarih] < '" + bitis.ToString("yyyy-MM-dd") + "')and ([Gider Tipi] = '" + cmbListeTip.Text + "')";
                }
            }
            dgv_Update();
        }
        private void BelliRapor()
        {

        }

        private void bGunlukRaporAl_Click(object sender, EventArgs e)
        {
            DateTime gunluk = _now.AddDays(-1);
            
        }

        private void bAylikRaporAl_Click(object sender, EventArgs e)
        {
            DateTime aylik = _now.AddMonths(-1);

        }

        private void bYillikRaporAl_Click(object sender, EventArgs e)
        {
            DateTime yillik = _now.AddYears(-1);

        }
    }
}
