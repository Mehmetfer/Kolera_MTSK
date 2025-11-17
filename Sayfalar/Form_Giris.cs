using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tarantula_MTSK.Models;
using Tarantula_MTSK.Services;

namespace Tarantula_MTSK.Sayfalar
{
    public partial class Form_Giris : Form
    {
        private readonly KullaniciService _kullaniciService;
        private readonly ServerAyar _serverAyar;

        // Yapıcı
        public Form_Giris(ServerAyar serverAyar)
        {
            InitializeComponent();
            _serverAyar = serverAyar;  // ServerAyar nesnesini alıyoruz
            _kullaniciService = new KullaniciService($"Server={serverAyar.Sunucu};Database={serverAyar.VeritabaniAdi};User Id={serverAyar.KullaniciAdi};Password={serverAyar.Parola};");
        }

        private async void Form_Giris_Load(object sender, EventArgs e)
        {
            // Kullanıcı adlarını ComboBox'a yükle
            await LoadKullaniciAdlari();
        }

        private async Task LoadKullaniciAdlari()
        {
            try
            {
                var kullaniciAdlari = await _kullaniciService.GetKullaniciAdlariAsync();
                foreach (var kullaniciAdi in kullaniciAdlari)
                {
                    ComboBox_KullaniciAdi.Items.Add(kullaniciAdi);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veritabanına bağlanırken bir hata oluştu: {ex.Message}");
            }
        }

        // PictureBox5 tıklama işlemi
        private void PictureBox5_Click_1(object sender, EventArgs e)
        {
            // Server formunu açıyoruz
            Server server = new Server();
            server.Show();
            this.Hide();

            // Bu metod PictureBox5 tıklandığında çağrılacak
            MessageBox.Show("Server ayarlarına yönlendiriliyorsunuz!");
        }

        // PictureBox5 tıklama işlemi - Server formuna yönlendirme
        private void PictureBox5_Click(object sender, EventArgs e)
        {
            Server Formserver = new Server();
            Formserver.Show();
            this.Hide();
        }

        // Giriş işlemi
        private async void Btngiris_Click_1(object sender, EventArgs e)
        {
            string kullaniciAdi = ComboBox_KullaniciAdi.SelectedItem?.ToString();
            string parola = Txt_Parola.Text;

            if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(parola))
            {
                Lbl_kaz.Text = "Kullanıcı adı veya şifre boş olamaz!";
                Lbl_kaz.Visible = true;
                return;
            }

            try
            {
                bool isValid = await _kullaniciService.IsValidKullanici(kullaniciAdi, parola);

                if (isValid)
                {
                    
                  
                    // Kullanıcı başarılı giriş yaptıysa Ana Menü sayfasına yönlendir
                    Ana_Menu anaMenu = new Ana_Menu(_serverAyar);
                    anaMenu.Show();
                    this.Hide();
                }
                else
                {
                    // Kullanıcı adı veya şifre hatalıysa uyarı mesajı göster
                    Lbl_kaz.Text = "Kullanıcı adı veya şifre hatalı!";
                    Lbl_kaz.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}");
            }
        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ComboBox_KullaniciAdi_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            Server form2 = new Server(); // Form2'yi oluştur
            form2.Show();              // Form2'yi göster
            this.Hide();               // İstersen Form1'i gizle
        }

        private void Kapat_Click(object sender, EventArgs e)
        {
            Close();    
        }

        private void PictureBox4_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
