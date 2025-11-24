using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using Microsoft.Win32;  // ⭐ EKLENDİ
using Tarantula_MTSK.Models;
using Tarantula_MTSK.Sayfalar;

namespace Tarantula_MTSK
{
    static class Program
    {
        private readonly static Mutex mutex = new Mutex(true, "TarantulaMTSKAppMutex");

        [STAThread]
        static void Main()
        {
            // Zaten açıksa engelle
            if (!mutex.WaitOne(TimeSpan.Zero, true))
            {
                var result = MessageBox.Show(
                    "Program zaten çalışıyor. İkinci kez açmaya çalışıyorsunuz. Görev Yöneticisini açmak ister misiniz?",
                    "Uyarı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try { Process.Start("taskmgr.exe"); }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Görev Yöneticisi açılamadı: " + ex.Message);
                    }
                }

                return;
            }

            // ⭐⭐⭐ IE11 MODU ZORLA ⭐⭐⭐
            UygulamaIcinIE11Ayari();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string xmlPath = Path.Combine(Application.StartupPath, "Baglanti.xml");
            var serverAyar = DeserializeServerAyar(xmlPath);

            if (serverAyar == null)
            {
                MessageBox.Show("Server ayarları yüklenemedi.");
                return;
            }

            // ConnectionString oluştur
            if (serverAyar.BaglantiTuru == "Windows")
            {
                serverAyar.ConnectionString =
                    $"Server={serverAyar.Sunucu};Database={serverAyar.VeritabaniAdi};Trusted_Connection=True;TrustServerCertificate=True;";
            }
            else
            {
                serverAyar.ConnectionString =
                    $"Server={serverAyar.Sunucu};Database={serverAyar.VeritabaniAdi};User Id={serverAyar.KullaniciAdi};Password={serverAyar.Parola};TrustServerCertificate=True;";
            }

            Application.Run(new Form_Giris(serverAyar));
        }

        // IE11 FONKSİYONU
        static void UygulamaIcinIE11Ayari()
        {
            try
            {
                string exeAdi = Application.ProductName + ".exe";

                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(
                    @"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION"))
                {
                    key.SetValue(exeAdi, 11001, RegistryValueKind.DWord);
                }
            }
            catch { }
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
