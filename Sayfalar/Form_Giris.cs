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
            _serverAyar = serverAyar ?? throw new ArgumentNullException(nameof(serverAyar));

            // Artık Program.cs'de oluşturulan ConnectionString'i kullanıyoruz
            _kullaniciService = new KullaniciService(_serverAyar.ConnectionString);
        }

        private async void Form_Giris_Load(object sender, EventArgs e)
        {
            await LoadKullaniciAdlari();
        }

        private async Task LoadKullaniciAdlari()
        {
            try
            {
                var kullaniciAdlari = await _kullaniciService.GetKullaniciAdlariAsync();
                ComboBox_KullaniciAdi.Items.Clear();
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

        // Giriş butonu
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
                    // Ana Menü sayfasına yönlendir
                    Ana_Menu anaMenu = new Ana_Menu(_serverAyar);
                    anaMenu.Show();
                    this.Hide();
                }
                else
                {
                    Lbl_kaz.Text = "Kullanıcı adı veya şifre hatalı!";
                    Lbl_kaz.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}");
            }
        }

        // Server ayarına yönlendirme
        private void PictureBox1_Click(object sender, EventArgs e)
        {
            Server form2 = new Server();
            form2.Show();
            this.Hide();
        }

        private void PictureBox5_Click(object sender, EventArgs e)
        {
            Server formServer = new Server();
            formServer.Show();
            this.Hide();
            MessageBox.Show("Server ayarlarına yönlendiriliyorsunuz!");
        }

        private void Kapat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            Server form2 = new Server();
            form2.Show();
            this.Hide();
        }

        private void PictureBox4_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
