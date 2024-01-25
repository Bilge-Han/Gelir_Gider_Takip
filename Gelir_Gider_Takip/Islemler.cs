using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gelir_Gider_Takip
{
    class Islemler
    {
        public static void GridDuzenle(DataGridView dgv)
        {
            if (dgv.Columns.Count > 0)
            {
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    switch (dgv.Columns[i].HeaderText)
                    {
                        case "ggr_ID":
                            dgv.Columns[i].HeaderText = "Kayıt NO"; break;
                        case "ggr_tarih":
                            dgv.Columns[i].HeaderText = "Tarih"; break;
                        case "ggr_tipi":
                            dgv.Columns[i].HeaderText = "Tipi"; break;
                        case "ggr_gider_tipi":
                            dgv.Columns[i].HeaderText = "Gelir/Gider Tipi"; break;
                        case "ggr_tutar":
                            dgv.Columns[i].HeaderText = "Tutar"; dgv.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dgv.Columns[i].DefaultCellStyle.Format = "C2"; break;
                        case "ggr_aciklama":
                            dgv.Columns[i].HeaderText = "Açıklama"; break;
                        case "ggr_kayit_tarih":
                            dgv.Columns[i].HeaderText = "Kayıt Tarihi"; break;
                    }
                }
            }
        }
    }
}
