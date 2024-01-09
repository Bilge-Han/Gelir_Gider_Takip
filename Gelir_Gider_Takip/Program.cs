using Gelir_Gider_Takip.Cls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Gelir_Gider_Takip
{
    static class Program
    {
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {
            glb.sql.Baglanti();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
