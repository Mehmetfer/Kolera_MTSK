using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using Microsoft.Win32;
using Tarantula_MTSK.Models;
using Tarantula_MTSK.Sayfalar;

namespace Tarantula_MTSK
{
    static class Program
    {
        private static readonly Mutex mutex =
            new Mutex(true, "Tarantula_MTSK_SINGLE_INSTANCE");

        [STAThread]
        static void Main()
        {
            // 🔒 Tekil çalıştırma
            if (!mutex.WaitOne(TimeSpan.Zero, true))
            {
                MessageBox.Show(
                    "Program zaten çalışıyor.",
                    "Uyarı",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            IE11Zorla();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string xmlPath = Path.Combine(
                Application.StartupPath,
                "Baglanti.xml");

            ServerAyar serverAyar = XmlOku(xmlPath);
            if (serverAyar == null)
                return;

            // 🔑 ConnectionString oluştur
            try
            {
                if (serverAyar.BaglantiTuru == "AttachDbFilename")
                {
                    serverAyar.ConnectionString =
                        "Data Source=(LocalDB)\\MSSQLLocalDB;" +
                        $"AttachDbFilename={serverAyar.VeritabaniAdi};" +
                        "Integrated Security=True;" +
                        "TrustServerCertificate=True;";
                }
                else // Windows Auth
                {
                    serverAyar.ConnectionString =
                        $"Server={serverAyar.Sunucu};" +
                        $"Database={serverAyar.VeritabaniAdi};" +
                        "Trusted_Connection=True;" +
                        "TrustServerCertificate=True;";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "ConnectionString oluşturulamadı:\n" + ex.Message,
                    "HATA",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            Application.Run(new Form_Giris(serverAyar));
        }

        // 🌐 IE11 WebBrowser modu
        private static void IE11Zorla()
        {
            try
            {
                string exe = Application.ProductName + ".exe";
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(
                    @"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION"))
                {
                    key.SetValue(exe, 11001, RegistryValueKind.DWord);
                }
            }
            catch { }
        }

        // 📄 XML Oku
        private static ServerAyar XmlOku(string path)
        {
            if (!File.Exists(path))
            {
                MessageBox.Show("Baglanti.xml bulunamadı!");
                return null;
            }

            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(ServerAyar));
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    return (ServerAyar)ser.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "XML okunamadı:\n" + ex.Message,
                    "HATA",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
