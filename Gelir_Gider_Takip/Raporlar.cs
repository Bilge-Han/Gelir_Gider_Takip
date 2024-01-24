using Microsoft.Reporting.WinForms;
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
            fRaporGoster f = new fRaporGoster();
            f.reportViewer1.LocalReport.DataSources.Clear();
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
