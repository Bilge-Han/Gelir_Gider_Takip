using Gelir_Gider_Takip.Cls;
using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace Gelir_Gider_Takip
{
    public partial class Form1 : Form
    {
        private Timer _timer;
        private DateTime _now;
        Gelir_Gider_TakipEntities db = new Gelir_Gider_TakipEntities();
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
        void timer_Baslat()
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

            cmbGiderTipi.DataSource = db.GIDER_TIPLERI.OrderBy(x => x.gdr_Gider_Kod).ToList();
            cmbGiderTipi.DisplayMember = "gdr_Gider_Ad";
            cmbGiderTipi.ValueMember = "gdr_Gider_Kod";

            cmbListeTip.DataSource = db.GIDER_TIPLERI.OrderBy(x => x.gdr_Gider_Kod).ToList();
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
                    using (var db = new Gelir_Gider_TakipEntities())
                    {
                        GELIR_GIDER_KAYITLARI ggk = new GELIR_GIDER_KAYITLARI();
                        ggk.ggr_tarih = Convert.ToDateTime(dtTarih.Value);
                        ggk.ggr_tipi = tip();
                        ggk.ggr_gider_tipi = cmbGiderTipi.SelectedValue.ToString();
                        ggk.ggr_tutar = Convert.ToDouble(calTutar.Text.ToString().Replace(",", "."));
                        ggk.ggr_aciklama = txtAciklama.Text;
                        ggk.ggr_kayit_tarih = DateTime.Now;
                        db.GELIR_GIDER_KAYITLARI.Add(ggk);
                        db.SaveChanges();
                    }
                    #region sql
                    //glb.sql.Command(""
                    //        + "    INSERT INTO[dbo].[GELIR_GIDER_KAYITLARI]     "
                    //        + "           ([ggr_tarih]                          "
                    //        + "           ,[ggr_tipi]                           "
                    //        + "           ,[ggr_gider_tipi]                     "
                    //        + "           ,[ggr_tutar]                          "
                    //        + "           ,[ggr_aciklama]                       "
                    //        + "           ,[ggr_kayit_tarih])                   "
                    //        + "     VALUES                                      "
                    //        + "           ('" + Convert.ToDateTime(dtTarih.Value).ToString("yyyyMMdd") + "'   " //< ggr_tarih, datetime,>              "
                    //        + "           ,'" + tip() + "'   " //< ggr_tipi, nvarchar(50),>           "
                    //        + "           ,'" + cmbGiderTipi.SelectedValue.ToString() + "'   " //< ggr_gider_tipi, nvarchar(50),>     "
                    //        + "           , " + Convert.ToDouble(calTutar.Text).ToString().Replace(",", ".") + "   " //< ggr_tutar, float,>                 "
                    //        + "           ,'" + txtAciklama.Text + "'   " //< ggr_aciklama, nvarchar(max),>      "
                    //        + "           , getdate()  " //< ggr_kayit_tarih, datetime,> "
                    //        + " )       "
                    //        );
                    #endregion
                    dgv_Update();
                }
                else
                {
                    int id = Convert.ToInt32(calID.Text);
                    using (var db = new Gelir_Gider_TakipEntities())
                    {
                        var ggk = db.GELIR_GIDER_KAYITLARI.Where(x => x.ggr_ID == id).FirstOrDefault();
                        ggk.ggr_tarih = Convert.ToDateTime(dtTarih.Value);
                        ggk.ggr_tipi = tip();
                        ggk.ggr_gider_tipi = cmbGiderTipi.SelectedValue.ToString();
                        ggk.ggr_tutar = Convert.ToDouble(calTutar.Text.ToString().Replace(",", "."));
                        ggk.ggr_aciklama = txtAciklama.Text;
                        db.SaveChanges();
                    }
                    #region sql
                    //glb.sql.Command(""
                    //       + "    update [dbo].[GELIR_GIDER_KAYITLARI]   set  "
                    //       + "            [ggr_tarih]        = '" + Convert.ToDateTime(dtTarih.Value).ToString("yyyyMMdd") + "'   " //< ggr_tarih, datetime,>              "                  "
                    //       + "           ,[ggr_tipi]         = '" + tip() + "'   " //< ggr_tipi, nvarchar(50),>           "                  "
                    //       + "           ,[ggr_gider_tipi]   = '" + cmbGiderTipi.SelectedValue.ToString() + "'   " //< ggr_gider_tipi, nvarchar(50),>     "                  "
                    //       + "           ,[ggr_tutar]        = '" + Convert.ToDouble(calTutar.Text).ToString().Replace(",", ".") + "'   " //< ggr_tutar, float,>                 "                  "
                    //       + "           ,[ggr_aciklama]     = '" + txtAciklama.Text + "'   " //< ggr_aciklama, nvarchar(max),>      "                  "
                    //       + "     where     ggr_ID = " + Convert.ToInt32(calID.Text) + "                                  "
                    //       );
                    #endregion
                    dgv_Update();
                }
            }
            else MessageBox.Show("Lütfen İşlem Tipi Seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }
        void dgv_Update()
        {
            DateTime baslangic = DateTime.Parse(dtBaslangic.Value.ToShortDateString());
            DateTime bitis = DateTime.Parse(dtBitis.Value.ToShortDateString());
            string gelir = "Gelir";
            string gider = "Gider";
            using (var db = new Gelir_Gider_TakipEntities())
            {
                if (rdListeHepsi.Checked)
                {
                    db.GELIR_GIDER_KAYITLARI.Where(x => x.ggr_tarih >= baslangic && x.ggr_tarih <= bitis && x.ggr_gider_tipi == cmbListeTip.Text).OrderByDescending(x => x.ggr_tarih).Load();
                    var ggk = db.GELIR_GIDER_KAYITLARI.Local.ToBindingList();
                    guna2DataGridView1.DataSource = ggk;

                    double glr = Convert.ToDouble(ggk.Where(x => x.ggr_tipi == gelir).Sum(x => x.ggr_tutar));
                    double gdr = Convert.ToDouble(ggk.Where(x => x.ggr_tipi == gider).Sum(x => x.ggr_tutar));
                    txGelir.Text =glr.ToString("C2");
                    txGider.Text = gdr.ToString("C2");
                    double dglr = glr-gdr;
                    txHesap.Text = dglr.ToString("C2");
                }
                else if (rdListeGelir.Checked)
                {
                    db.GELIR_GIDER_KAYITLARI.Where(x => x.ggr_tarih >= baslangic && x.ggr_tarih <= bitis && x.ggr_gider_tipi == cmbListeTip.Text && x.ggr_tipi == gelir).OrderByDescending(x => x.ggr_tarih).Load();
                    var ggk = db.GELIR_GIDER_KAYITLARI.Local.ToBindingList();
                    guna2DataGridView1.DataSource = ggk;

                    txGelir.Text = Convert.ToDouble(ggk.Where(x => x.ggr_tipi == gelir).Sum(x => x.ggr_tutar)).ToString("C2");
                    txGider.Text = Convert.ToDouble(ggk.Where(x => x.ggr_tipi == gider).Sum(x => x.ggr_tutar)).ToString("C2");
                    txHesap.Text = Convert.ToDouble(Convert.ToDouble(txGelir.Text) - Convert.ToDouble(txGider.Text)).ToString("C2");
                }
                else if (rdListeGider.Checked)
                {
                    db.GELIR_GIDER_KAYITLARI.Where(x => x.ggr_tarih >= baslangic && x.ggr_tarih <= bitis && x.ggr_gider_tipi == cmbListeTip.Text && x.ggr_tipi == gider).OrderByDescending(x => x.ggr_tarih).Load();
                    var ggk = db.GELIR_GIDER_KAYITLARI.Local.ToBindingList();
                    guna2DataGridView1.DataSource = ggk;

                    txGelir.Text = Convert.ToDouble(ggk.Where(x => x.ggr_tipi == gelir).Sum(x => x.ggr_tutar)).ToString("C2");
                    txGider.Text = Convert.ToDouble(ggk.Where(x => x.ggr_tipi == gider).Sum(x => x.ggr_tutar)).ToString("C2");
                    txHesap.Text = Convert.ToDouble(Convert.ToDouble(txGelir.Text) - Convert.ToDouble(txGider.Text)).ToString("C2");
                }
            }
            #region eski
            //guna2DataGridView1.DataSource = glb.sql.Table("select * from dbo.fn_GelirGiderListe(0) " + where);
            //if (guna2DataGridView1.Rows.Count > 0)
            //{
            //    double gelirr = 0;
            //    double giderr = 0;
            //    for (int i = 0; i < guna2DataGridView1.Rows.Count; i++)
            //    {
            //        if (guna2DataGridView1.Rows[i].Cells["Tipi"].Value.ToString() == "Gelir")
            //        {
            //            gelir += Convert.ToDouble(guna2DataGridView1.Rows[i].Cells["Tutar"].Value.ToString());
            //        }
            //        else if (guna2DataGridView1.Rows[i].Cells["Tipi"].Value.ToString() == "Gider")
            //        {
            //            gider += Convert.ToDouble(guna2DataGridView1.Rows[i].Cells["Tutar"].Value.ToString());
            //        }
            //    }
            //    double hesap = gelirr - giderr;
            //    txGelir.Text = gelirr.ToString("C2");
            //    txGider.Text = giderr.ToString("C2");
            //    txHesap.Text = hesap.ToString("C2");
            //}
            //where = " ";
            #endregion
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
            int id = Convert.ToInt32(calID.Text);
            if (MessageBox.Show("Bu " + tip() + " kaydını silmek istediğinzden emin misiniz?", "Onay Verin!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (db.GELIR_GIDER_KAYITLARI.Any(x => x.ggr_ID == id))
                {
                    var ggk = db.GELIR_GIDER_KAYITLARI.Find(id);
                    db.GELIR_GIDER_KAYITLARI.Remove(ggk);
                    db.SaveChanges();
                    dgv_Update();
                }
                //glb.sql.Command("delete from  [dbo].[GELIR_GIDER_KAYITLARI]    where     ggr_ID = " + Convert.ToInt32(calID.Text) + "     ");
            }
        }
        //string where = " ";

        private void btnTipEkle_Click(object sender, EventArgs e)
        {
            new GiderTipi_Ekle() { }.ShowDialog();
            combo_doldur();
        }
        private void btnAra_Click(object sender, EventArgs e)
        {
            DateTime baslangic = Convert.ToDateTime(dtBaslangic.Value);
            DateTime bitis = Convert.ToDateTime(dtBitis.Value).AddDays(1);
            string gelir = "Gelir";
            string gider = "Gider";
            if (cmbListeTip.Text == "Hepsi")
            {
                if (rdListeGelir.Checked == true)
                {
                    db.GELIR_GIDER_KAYITLARI.Where(x => x.ggr_tarih >= baslangic && x.ggr_tarih <= bitis&&x.ggr_tipi==gelir).OrderByDescending(x => x.ggr_tarih).Load();
                    var ggk = db.GELIR_GIDER_KAYITLARI.Local.ToBindingList();
                    guna2DataGridView1.DataSource = ggk;

                    //where = " where Tipi = 'Gelir' and ( '" + baslangic.ToString("yyyyMMdd") + "' <= [Tarih] and  [Tarih] < '" + bitis.ToString("yyyyMMdd") + "') ";
                }
                if (rdListeGider.Checked == true)
                {
                    db.GELIR_GIDER_KAYITLARI.Where(x => x.ggr_tarih >= baslangic && x.ggr_tarih <= bitis && x.ggr_tipi == gider).OrderByDescending(x => x.ggr_tarih).Load();
                    var ggk = db.GELIR_GIDER_KAYITLARI.Local.ToBindingList();
                    guna2DataGridView1.DataSource = ggk;

                    //where = " where Tipi = 'Gider' and ( '" + baslangic.ToString("yyyy-MM-dd") + "' <= [Tarih] and  [Tarih] < '" + bitis.ToString("yyyy-MM-dd") + "') ";
                }
                if (rdListeHepsi.Checked == true)
                {
                    db.GELIR_GIDER_KAYITLARI.Where(x => x.ggr_tarih >= baslangic && x.ggr_tarih <= bitis).OrderByDescending(x => x.ggr_tarih).Load();
                    var ggk = db.GELIR_GIDER_KAYITLARI.Local.ToBindingList();
                    guna2DataGridView1.DataSource = ggk;

                    //where = " where ( '" + baslangic.ToString("yyyy-MM-dd") + " '<= [Tarih] and  [Tarih] < '" + bitis.ToString("yyyy-MM-dd") + "')";
                }
            }
            else if (cmbListeTip.Text != "")
            {
                if (rdListeGelir.Checked == true)
                {
                    db.GELIR_GIDER_KAYITLARI.Where(x => x.ggr_tarih >= baslangic && x.ggr_tarih <= bitis&&x.ggr_gider_tipi==cmbListeTip.Text && x.ggr_tipi == gelir).OrderByDescending(x => x.ggr_tarih).Load();
                    var ggk = db.GELIR_GIDER_KAYITLARI.Local.ToBindingList();
                    guna2DataGridView1.DataSource = ggk;

                    //where = " where Tipi = 'Gelir' and ( '" + baslangic.ToString("yyyyMMdd") + "' <= [Tarih] and  [Tarih] < '" + bitis.ToString("yyyyMMdd") + "') and ([Gider Tipi] = '" + cmbListeTip.Text + "')";
                }
                if (rdListeGider.Checked == true)
                {
                    db.GELIR_GIDER_KAYITLARI.Where(x => x.ggr_tarih >= baslangic && x.ggr_tarih <= bitis && x.ggr_gider_tipi == cmbListeTip.Text && x.ggr_tipi == gider).OrderByDescending(x => x.ggr_tarih).Load();
                    var ggk = db.GELIR_GIDER_KAYITLARI.Local.ToBindingList();
                    guna2DataGridView1.DataSource = ggk;

                    //where = " where Tipi = 'Gider' and ( '" + baslangic.ToString("yyyy-MM-dd") + "' <= [Tarih] and  [Tarih] < '" + bitis.ToString("yyyy-MM-dd") + "') and ([Gider Tipi] = '" + cmbListeTip.Text + "')";
                }
                if (rdListeHepsi.Checked == true)
                {
                    db.GELIR_GIDER_KAYITLARI.Where(x => x.ggr_tarih >= baslangic && x.ggr_tarih <= bitis && x.ggr_gider_tipi == cmbListeTip.Text).OrderByDescending(x => x.ggr_tarih).Load();
                    var ggk = db.GELIR_GIDER_KAYITLARI.Local.ToBindingList();
                    guna2DataGridView1.DataSource = ggk;

                    //where = " where ( '" + baslangic.ToString("yyyy-MM-dd") + " '<= [Tarih] and  [Tarih] < '" + bitis.ToString("yyyy-MM-dd") + "')and ([Gider Tipi] = '" + cmbListeTip.Text + "')";
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

        private void bRaporAl_Click(object sender, EventArgs e)
        {
            Raporlar.Baslik = "GENEL RAPOR";
            Raporlar.BaslangicTarih = dtBaslangic.Value.ToShortDateString();
            Raporlar.BitisTarih = dtBitis.Value.ToShortDateString();
            Raporlar.ToplamGelir = txGelir.Text;
            Raporlar.ToplamGider = txGider.Text;
            Raporlar.HesapOzet = "DENEME";
            Raporlar.RaporSayfasiRaporu(guna2DataGridView1);
        }
    }
}
