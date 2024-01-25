using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Gelir_Gider_Takip
{
    class Raporlar
    {
        public static string Baslik { get; set; }
        public static string BaslangicTarih { get; set; }
        public static string BitisTarih { get; set; }
        public static string ToplamGelir { get; set; }
        public static string ToplamGider { get; set; }
        public static string HesapOzet { get; set; }
        public static void RaporSayfasiRaporu(DataGridView dgv)
        {
            List<GELIR_GIDER_KAYITLARI> list = new List<GELIR_GIDER_KAYITLARI>();
            list.Clear();
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                list.Add(new GELIR_GIDER_KAYITLARI
                {
                    ggr_ID = Convert.ToInt32(dgv.Rows[i].Cells["ggr_ID"].Value.ToString()),
                    ggr_tarih = Convert.ToDateTime(dgv.Rows[i].Cells["ggr_tarih"].Value.ToString()),
                    ggr_tipi = dgv.Rows[i].Cells["ggr_tipi"].Value.ToString(),
                    ggr_gider_tipi = dgv.Rows[i].Cells["ggr_gider_tipi"].Value.ToString(),
                    ggr_tutar = Convert.ToDouble(dgv.Rows[i].Cells["ggr_tutar"].Value.ToString()),
                    ggr_aciklama = dgv.Rows[i].Cells["ggr_aciklama"].Value.ToString(),
                    ggr_kayit_tarih = Convert.ToDateTime(dgv.Rows[i].Cells["ggr_kayit_tarih"].Value.ToString()),
                });
            }
            ReportDataSource rs = new ReportDataSource();
            rs.Name = "dsGenelRapor";
            rs.Value = list;
            fRaporGoster f = new fRaporGoster();
            f.reportViewer1.LocalReport.DataSources.Clear();
            f.reportViewer1.LocalReport.DataSources.Add(rs);
            f.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\rpGenelRapor.rdlc";

            ReportParameter[] rPrm = new ReportParameter[6];
            rPrm[0] = new ReportParameter("Baslik", Baslik);
            rPrm[1] = new ReportParameter("BaslangicTarih", BaslangicTarih);
            rPrm[2] = new ReportParameter("BitisTarih", BitisTarih);
            rPrm[3] = new ReportParameter("ToplamGelir", ToplamGelir);
            rPrm[4] = new ReportParameter("ToplamGider", ToplamGider);
            rPrm[5] = new ReportParameter("HesapOzet", HesapOzet);
            f.reportViewer1.LocalReport.SetParameters(rPrm);
            f.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            f.reportViewer1.ZoomMode = ZoomMode.FullPage;
            f.ShowDialog();
        }
    }
}
