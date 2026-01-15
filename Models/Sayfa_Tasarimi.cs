using System;
using System.Windows.Forms;
using System.Drawing;

namespace Tarantula_MTSK.Sayfalar
{
    public class Sayfa_Tasarimi : Form
    {
        public Sayfa_Tasarimi()
        {
            // Ortak sayfa tasarımı
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.LightGray; // İstediğin renk
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MinimumSize = new Size(1024, 768);
        }

        // Ortak metot örneği
        public void BilgiMesaji(string mesaj)
        {
            MessageBox.Show(mesaj, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
