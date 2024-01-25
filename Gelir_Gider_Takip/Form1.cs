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
            timer_Baslat();
            rdGelir.Checked = true;
            rdListeHepsi.Checked = true;
            dtBaslangic.Value = DateTime.Now.AddDays(-7);
            dtBitis.Value = DateTime.Now;
            dtTarih.Value = DateTime.Now;
            txGelir.Text = 0.ToString("C2");
            txGider.Text = 0.ToString("C2");
            txHesap.Text = 0.ToString("C2");
            combo_doldur();
            Islemler.GridDuzenle(guna2DataGridView1);
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
            cmbGiderTipi.SelectedValue = "Hepsi";

            cmbListeTip.DataSource = db.GIDER_TIPLERI.OrderBy(x => x.gdr_Gider_Kod).ToList();
            cmbListeTip.DisplayMember = "gdr_Gider_Ad";
            cmbListeTip.ValueMember = "gdr_Gider_Kod";
            cmbListeTip.SelectedValue = "Hepsi";
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
                int id = Convert.ToInt32(calID.Text);
                if (!db.GELIR_GIDER_KAYITLARI.Any(x => x.ggr_ID == id))
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
                }
                else
                {
                        var ggk = db.GELIR_GIDER_KAYITLARI.Where(x => x.ggr_ID == id).FirstOrDefault();
                        ggk.ggr_tarih = DateTime.Now;
                        ggk.ggr_tipi = tip();
                        ggk.ggr_gider_tipi = cmbGiderTipi.SelectedValue.ToString();
                        ggk.ggr_tutar = Convert.ToDouble(calTutar.Text.ToString().Replace(",", "."));
                        ggk.ggr_aciklama = txtAciklama.Text;
                        db.SaveChanges(); 
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
                }
            }
            else MessageBox.Show("Lütfen İşlem Tipi Seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }
        void dgv_Update()
        {
            //guna2DataGridView1.DataSource = null;
            //DateTime baslangic = DateTime.Parse(dtBaslangic.Value.ToShortDateString());
            //DateTime bitis = DateTime.Parse(dtBitis.Value.ToShortDateString());
            //string gelir = "Gelir";
            //string gider = "Gider";
            //if (rdListeHepsi.Checked)
            //{
            //    db.GELIR_GIDER_KAYITLARI.Where(x => x.ggr_tarih >= baslangic && x.ggr_tarih <= bitis&& x.ggr_gider_tipi == cmbListeTip.Text).OrderByDescending(x => x.ggr_tarih).Load();
            //    var ggk = db.GELIR_GIDER_KAYITLARI.Local.ToBindingList();
            //    guna2DataGridView1.DataSource = ggk;

            //    txGelir.Text = ggk.Where(x => x.ggr_tipi == gelir).Sum(x => x.ggr_tutar).ToString();
            //    txGider.Text = ggk.Where(x => x.ggr_tipi == gider).Sum(x => x.ggr_tutar).ToString();
            //}
            //else if (rdListeGelir.Checked)
            //{
            //    db.GELIR_GIDER_KAYITLARI.Where(x => x.ggr_tarih >= baslangic && x.ggr_tarih <= bitis  && x.ggr_gider_tipi == cmbListeTip.Text && x.ggr_tipi == gelir).OrderByDescending(x => x.ggr_tarih).Load();
            //    var ggk = db.GELIR_GIDER_KAYITLARI.Local.ToBindingList();
            //    guna2DataGridView1.DataSource = ggk;

            //    txGelir.Text = ggk.Where(x => x.ggr_tipi == gelir).Sum(x => x.ggr_tutar).ToString();
            //    txGider.Text = ggk.Where(x => x.ggr_tipi == gider).Sum(x => x.ggr_tutar).ToString();
            //}
            //else if (rdListeGider.Checked)
            //{
            //    db.GELIR_GIDER_KAYITLARI.Where(x => x.ggr_tarih >= baslangic && x.ggr_tarih <= bitis
            //    && x.ggr_gider_tipi == cmbListeTip.Text && x.ggr_tipi == gider).OrderByDescending(x => x.ggr_tarih).Load();
            //    var ggk = db.GELIR_GIDER_KAYITLARI.Local.ToBindingList();
            //    guna2DataGridView1.DataSource = ggk;

            //    txGelir.Text = ggk.Where(x => x.ggr_tipi == gelir).Sum(x => x.ggr_tutar).ToString();
            //    txGider.Text = ggk.Where(x => x.ggr_tipi == gider).Sum(x => x.ggr_tutar).ToString();
            //}
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
                calID.Text = row.Cells[0].Value.ToString();
                calTutar.Text = row.Cells[4].Value.ToString();
                txtAciklama.Text = row.Cells[5].Value.ToString();
                string gelir_gider = row.Cells[2].Value.ToString();

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
                dtTarih.Text = row.Cells[1].Value.ToString();
                cmbGiderTipi.SelectedValue = row.Cells[3].Value.ToString();
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

        private void btnTipCikar_Click(object sender, EventArgs e)
        {
            new GiderTipi_Cikar() { }.ShowDialog();
            combo_doldur();
        }
        private void btnAra_Click(object sender, EventArgs e)
        {
            guna2DataGridView1.DataSource = null;
            DateTime baslangic = dtBaslangic.Value.AddDays(-1);
            DateTime bitis = dtBitis.Value;
            using (var data = new Gelir_Gider_TakipEntities())
            {
                if (cmbListeTip.Text=="Hepsi")
                {
                    if (rdListeGelir.Checked)
                    {
                        var liste = db.GELIR_GIDER_KAYITLARI
                          .Where(k => k.ggr_tarih >= baslangic && k.ggr_tarih <= bitis
                                  && k.ggr_tipi == "Gelir")
                          .ToList();
                        guna2DataGridView1.DataSource = liste;

                        var gelirler = liste.Where(k => k.ggr_tipi == "Gelir").ToList();
                        var giderler = liste.Where(k => k.ggr_tipi == "Gider").ToList();
                        double toplamGelir = (double)gelirler.Sum(k => k.ggr_tutar);
                        double toplamGider = (double)giderler.Sum(k => k.ggr_tutar);
                        double hesap = toplamGelir - toplamGider;
                        txGelir.Text = toplamGelir.ToString("C2");
                        txGider.Text = toplamGider.ToString("C2");
                        txHesap.Text = hesap.ToString("C2");
                    }
                    else if (rdListeGider.Checked)
                    {
                        var liste = db.GELIR_GIDER_KAYITLARI
                          .Where(k => k.ggr_tarih >= baslangic && k.ggr_tarih <= bitis
                                  && k.ggr_tipi == "Gider")
                          .ToList();
                        guna2DataGridView1.DataSource = liste;

                        var gelirler = liste.Where(k => k.ggr_tipi == "Gelir").ToList();
                        var giderler = liste.Where(k => k.ggr_tipi == "Gider").ToList();
                        double toplamGelir = (double)gelirler.Sum(k => k.ggr_tutar);
                        double toplamGider = (double)giderler.Sum(k => k.ggr_tutar);
                        double hesap = toplamGelir - toplamGider;
                        txGelir.Text = toplamGelir.ToString("C2");
                        txGider.Text = toplamGider.ToString("C2");
                        txHesap.Text = hesap.ToString("C2");
                    }
                    else if (rdListeHepsi.Checked)
                    {
                        var liste = db.GELIR_GIDER_KAYITLARI
                          .Where(k => k.ggr_tarih >= baslangic && k.ggr_tarih <= bitis)
                          .ToList();
                        guna2DataGridView1.DataSource = liste;

                        var gelirler = liste.Where(k => k.ggr_tipi == "Gelir").ToList();
                        var giderler = liste.Where(k => k.ggr_tipi == "Gider").ToList();
                        double toplamGelir = (double)gelirler.Sum(k => k.ggr_tutar);
                        double toplamGider = (double)giderler.Sum(k => k.ggr_tutar);
                        double hesap = toplamGelir - toplamGider;
                        txGelir.Text = toplamGelir.ToString("C2");
                        txGider.Text = toplamGider.ToString("C2");
                        txHesap.Text = hesap.ToString("C2");
                    }
                }
                else if (cmbListeTip!=null)
                {
                    if (rdListeGelir.Checked)
                    {
                        var liste = db.GELIR_GIDER_KAYITLARI
                          .Where(k => k.ggr_tarih >= baslangic && k.ggr_tarih <= bitis
                                  && k.ggr_tipi == "Gelir"&&k.ggr_gider_tipi==cmbListeTip.SelectedValue.ToString())
                          .ToList();
                        guna2DataGridView1.DataSource = liste;

                        var gelirler = liste.Where(k => k.ggr_tipi == "Gelir").ToList();
                        var giderler = liste.Where(k => k.ggr_tipi == "Gider").ToList();
                        double toplamGelir = (double)gelirler.Sum(k => k.ggr_tutar);
                        double toplamGider = (double)giderler.Sum(k => k.ggr_tutar);
                        double hesap = toplamGelir - toplamGider;
                        txGelir.Text = toplamGelir.ToString("C2");
                        txGider.Text = toplamGider.ToString("C2");
                        txHesap.Text = hesap.ToString("C2");
                    }
                    else if (rdListeGider.Checked)
                    {
                        var liste = db.GELIR_GIDER_KAYITLARI
                          .Where(k => k.ggr_tarih >= baslangic && k.ggr_tarih <= bitis
                                  && k.ggr_tipi == "Gider" && k.ggr_gider_tipi == cmbListeTip.SelectedValue.ToString())
                          .ToList();
                        guna2DataGridView1.DataSource = liste;

                        var gelirler = liste.Where(k => k.ggr_tipi == "Gelir").ToList();
                        var giderler = liste.Where(k => k.ggr_tipi == "Gider").ToList();
                        double toplamGelir = (double)gelirler.Sum(k => k.ggr_tutar);
                        double toplamGider = (double)giderler.Sum(k => k.ggr_tutar);
                        double hesap = toplamGelir - toplamGider;
                        txGelir.Text = toplamGelir.ToString("C2");
                        txGider.Text = toplamGider.ToString("C2");
                        txHesap.Text = hesap.ToString("C2");
                    }
                    else if (rdListeHepsi.Checked)
                    {
                        var liste = db.GELIR_GIDER_KAYITLARI
                          .Where(k => k.ggr_tarih >= baslangic && k.ggr_tarih <= bitis && k.ggr_gider_tipi == cmbListeTip.SelectedValue.ToString())
                          .ToList();
                        guna2DataGridView1.DataSource = liste;

                        var gelirler = liste.Where(k => k.ggr_tipi == "Gelir").ToList();
                        var giderler = liste.Where(k => k.ggr_tipi == "Gider").ToList();
                        double toplamGelir = (double)gelirler.Sum(k => k.ggr_tutar);
                        double toplamGider = (double)giderler.Sum(k => k.ggr_tutar);
                        double hesap = toplamGelir - toplamGider;
                        txGelir.Text = toplamGelir.ToString("C2");
                        txGider.Text = toplamGider.ToString("C2");
                        txHesap.Text = hesap.ToString("C2");
                    }
                }
                Islemler.GridDuzenle(guna2DataGridView1);
            }
        }
        private void bGunlukRaporAl_Click(object sender, EventArgs e)
        {
            DateTime baslangic = _now.AddDays(-1);
            DateTime bitis = _now;
            var liste = db.GELIR_GIDER_KAYITLARI
                          .Where(k => k.ggr_tarih >= baslangic && k.ggr_tarih <= bitis)
                          .ToList();
            guna2DataGridView1.DataSource = liste;
            var gelirler = liste.Where(k => k.ggr_tipi == "Gelir").ToList();
            var giderler = liste.Where(k => k.ggr_tipi == "Gider").ToList();
            double toplamGelir = (double)gelirler.Sum(k => k.ggr_tutar);
            double toplamGider = (double)giderler.Sum(k => k.ggr_tutar);
            double hesap = toplamGelir - toplamGider;
            Raporlar.Baslik = "GUNLUK RAPOR";
            Raporlar.BaslangicTarih = baslangic.ToShortDateString();
            Raporlar.BitisTarih = bitis.ToShortDateString();
            Raporlar.ToplamGelir = toplamGelir.ToString("C2");
            Raporlar.ToplamGider = toplamGider.ToString("C2");
            Raporlar.HesapOzet = hesap.ToString("C2");
            Raporlar.RaporSayfasiRaporu(guna2DataGridView1);
        }
        private void bAylikRaporAl_Click(object sender, EventArgs e)
        {
            DateTime baslangic = _now.AddMonths(-1);
            DateTime bitis = _now;
            var liste = db.GELIR_GIDER_KAYITLARI
                          .Where(k => k.ggr_tarih >= baslangic && k.ggr_tarih <= bitis)
                          .ToList();
            guna2DataGridView1.DataSource = liste;
            var gelirler = liste.Where(k => k.ggr_tipi == "Gelir").ToList();
            var giderler = liste.Where(k => k.ggr_tipi == "Gider").ToList();
            double toplamGelir = (double)gelirler.Sum(k => k.ggr_tutar);
            double toplamGider = (double)giderler.Sum(k => k.ggr_tutar);
            double hesap = toplamGelir - toplamGider;
            Raporlar.Baslik = "AYLIK RAPOR";
            Raporlar.BaslangicTarih = baslangic.ToShortDateString();
            Raporlar.BitisTarih = bitis.ToShortDateString();
            Raporlar.ToplamGelir = toplamGelir.ToString("C2");
            Raporlar.ToplamGider = toplamGider.ToString("C2");
            Raporlar.HesapOzet = hesap.ToString("C2");
            Raporlar.RaporSayfasiRaporu(guna2DataGridView1);
        }
        private void bYillikRaporAl_Click(object sender, EventArgs e)
        {

            DateTime baslangic = _now.AddYears(-1);
            DateTime bitis = _now;
            var liste = db.GELIR_GIDER_KAYITLARI
                          .Where(k => k.ggr_tarih >= baslangic && k.ggr_tarih <= bitis)
                          .ToList();
            guna2DataGridView1.DataSource = liste;
            var gelirler = liste.Where(k => k.ggr_tipi == "Gelir").ToList();
            var giderler = liste.Where(k => k.ggr_tipi == "Gider").ToList();
            double toplamGelir = (double)gelirler.Sum(k => k.ggr_tutar);
            double toplamGider = (double)giderler.Sum(k => k.ggr_tutar);
            double hesap = toplamGelir - toplamGider;
            Raporlar.Baslik = "YILLIK RAPOR";
            Raporlar.BaslangicTarih = baslangic.ToShortDateString();
            Raporlar.BitisTarih = bitis.ToShortDateString();
            Raporlar.ToplamGelir = toplamGelir.ToString("C2");
            Raporlar.ToplamGider = toplamGider.ToString("C2");
            Raporlar.HesapOzet = hesap.ToString("C2");
            Raporlar.RaporSayfasiRaporu(guna2DataGridView1);
        }
        private void bRaporAl_Click(object sender, EventArgs e)
        {
            Raporlar.Baslik = "GENEL RAPOR";
            Raporlar.BaslangicTarih = dtBaslangic.Value.ToShortDateString();
            Raporlar.BitisTarih = dtBitis.Value.ToShortDateString();
            Raporlar.ToplamGelir = Islemler.DoubleYap(txGelir.Text).ToString("C2");
            Raporlar.ToplamGider = Islemler.DoubleYap(txGider.Text).ToString("C2");
            Raporlar.HesapOzet = Islemler.DoubleYap(txHesap.Text).ToString("C2");
            Raporlar.RaporSayfasiRaporu(guna2DataGridView1);
        }

    }
}