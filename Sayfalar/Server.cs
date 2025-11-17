using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using Tarantula_MTSK.Models; // ServerAyar'ın bulunduğu namespace'i buraya eklemelisiniz.

namespace Tarantula_MTSK.Sayfalar
{
    public partial class Server : Form
    {
        // XML dosyasının kısa yolu
        private readonly string xmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Baglanti.xml");

        public Server()
        {
            InitializeComponent();
            Btn_save.Click += Btn_save_Click;
        }

        private void Server_Load(object sender, EventArgs e)
        {
            try
            {
                // XML dosyası varsa oku
                if (File.Exists(xmlPath))
                {
                    XDocument xmlDoc = XDocument.Load(xmlPath);

                    Txtserver1.Text = xmlDoc.Root.Element("Sunucu")?.Value ?? "";
                    Txt_baglanti1.Text = xmlDoc.Root.Element("BaglantiTuru")?.Value ?? "";
                    Txt_kul1.Text = xmlDoc.Root.Element("KullaniciAdi")?.Value ?? "";
                    Txt_Parola.Text = xmlDoc.Root.Element("Parola")?.Value ?? "";
                    Txt_datam1.Text = xmlDoc.Root.Element("VeritabaniAdi")?.Value ?? "";
                }
                else
                {
                    // Dosya yoksa otomatik boş oluştur
                    CreateDefaultXml();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bağlantı dosyası okunamadı:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateDefaultXml()
        {
            try
            {
                // Ana klasörde dosyayı oluşturuyoruz
                XDocument xmlDoc = new XDocument(
                    new XElement("ServerAyar",
                        new XElement("Sunucu", ""),
                        new XElement("BaglantiTuru", ""),
                        new XElement("KullaniciAdi", ""),
                        new XElement("Parola", ""),
                        new XElement("VeritabaniAdi", "")
                    )
                );
                xmlDoc.Save(xmlPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Varsayılan bağlantı dosyası oluşturulurken hata oluştu:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                // Kullanıcıdan gelen verileri XML'e kaydediyoruz
                string server = Txtserver1.Text;
                string baglantiTuru = Txt_baglanti1.Text;
                string kullaniciAdi = Txt_kul1.Text;
                string parola = Txt_Parola.Text;
                string veritabaniAdi = Txt_datam1.Text;

                // Veritabanı bilgilerini geçerli olup olmadığını kontrol et
                if (string.IsNullOrWhiteSpace(server) || string.IsNullOrWhiteSpace(baglantiTuru) ||
                    string.IsNullOrWhiteSpace(kullaniciAdi) || string.IsNullOrWhiteSpace(parola) ||
                    string.IsNullOrWhiteSpace(veritabaniAdi))
                {
                    MessageBox.Show("Lütfen tüm alanları doldurduğunuzdan emin olun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // XML dosyasını kaydet
                XDocument xmlDoc = new XDocument(
                    new XElement("ServerAyar",
                        new XElement("Sunucu", server),
                        new XElement("BaglantiTuru", baglantiTuru),
                        new XElement("KullaniciAdi", kullaniciAdi),
                        new XElement("Parola", parola),
                        new XElement("VeritabaniAdi", veritabaniAdi)
                    )
                );
                xmlDoc.Save(xmlPath);

                MessageBox.Show("Bağlantı bilgileri başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Form_Giris formunu aç
                Form_Giris formGiris = new Form_Giris(new ServerAyar
                {
                    Sunucu = server,
                    BaglantiTuru = baglantiTuru,
                    KullaniciAdi = kullaniciAdi,
                    Parola = parola,
                    VeritabaniAdi = veritabaniAdi
                });
                formGiris.Show();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kaydetme hatası:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Grp_ServerInfo_Enter(object sender, EventArgs e)
        {

        }

        private void Kapat_Click(object sender, EventArgs e)
        {
           
        }

        private void Btn_save_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Kullanıcıdan gelen verileri XML'e kaydediyoruz
                string server = Txtserver1.Text;
                string baglantiTuru = Txt_baglanti1.Text;
                string kullaniciAdi = Txt_kul1.Text;
                string parola = Txt_Parola.Text;
                string veritabaniAdi = Txt_datam1.Text;

                // Veritabanı bilgilerini geçerli olup olmadığını kontrol et
                if (string.IsNullOrWhiteSpace(server) || string.IsNullOrWhiteSpace(baglantiTuru) ||
                    string.IsNullOrWhiteSpace(kullaniciAdi) || string.IsNullOrWhiteSpace(parola) ||
                    string.IsNullOrWhiteSpace(veritabaniAdi))
                {
                    MessageBox.Show("Lütfen tüm alanları doldurduğunuzdan emin olun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // XML dosyasını kaydet
                XDocument xmlDoc = new XDocument(
                    new XElement("ServerAyar",
                        new XElement("Sunucu", server),
                        new XElement("BaglantiTuru", baglantiTuru),
                        new XElement("KullaniciAdi", kullaniciAdi),
                        new XElement("Parola", parola),
                        new XElement("VeritabaniAdi", veritabaniAdi)
                    )
                );
                xmlDoc.Save(xmlPath);

                MessageBox.Show("Bağlantı bilgileri başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Form_Giris formunu aç
                Form_Giris formGiris = new Form_Giris(new ServerAyar
                {
                    Sunucu = server,
                    BaglantiTuru = baglantiTuru,
                    KullaniciAdi = kullaniciAdi,
                    Parola = parola,
                    VeritabaniAdi = veritabaniAdi
                });
                formGiris.Show();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kaydetme hatası:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
