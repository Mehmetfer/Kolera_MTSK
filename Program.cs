using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using Tarantula_MTSK.Models;
using Tarantula_MTSK.Sayfalar;

namespace Tarantula_MTSK
{
    static class Program
    {
        // Mutex ismi eşsiz olmalı
        private readonly static Mutex mutex = new Mutex(true, "TarantulaMTSKAppMutex");

        [STAThread]
        static void Main()
        {
            // Program zaten çalışıyorsa
            if (!mutex.WaitOne(TimeSpan.Zero, true))
            {
                var result = MessageBox.Show(
                    "Program zaten çalışıyor. İkinci kez açmaya çalışıyorsunuz. Görev Yöneticisini açmak ister misiniz?",
                    "Uyarı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        Process.Start("taskmgr.exe");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Görev Yöneticisi açılamadı: " + ex.Message);
                    }
                }

                return; // Program ikinci kez açılmasın
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string xmlPath = Path.Combine(Application.StartupPath, "Baglanti.xml");

            var serverAyar = DeserializeServerAyar(xmlPath);

            if (serverAyar == null)
            {
                MessageBox.Show("Server ayarları yüklenemedi.");
                return;
            }

            serverAyar.ConnectionString =
                $"Server={serverAyar.Sunucu};Database={serverAyar.VeritabaniAdi};User Id={serverAyar.KullaniciAdi};Password={serverAyar.Parola};TrustServerCertificate=True;";

            Application.Run(new Form_Giris(serverAyar));
        }

        static ServerAyar DeserializeServerAyar(string xmlFilePath)
        {
            if (!File.Exists(xmlFilePath))
            {
                MessageBox.Show("XML dosyası bulunamadı.");
                return null;
            }

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ServerAyar));
                using (FileStream fs = new FileStream(xmlFilePath, FileMode.Open))
                {
                    return (ServerAyar)serializer.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Deseralizasyon hatası: {ex.Message}");
                return null;
            }
        }
    }
}
