using ScannerDemo;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WIA;

namespace Tarantula_MTSK.Sayfalar
{
    public partial class Form_Tarama : Form
    {
        private byte[] mevcutResim; // Önceden varsa gönderilecek
        public event Action<byte[]> TaramaTamamlandi;

        public Form_Tarama(byte[] mevcutResim = null)
        {
            InitializeComponent();
            this.mevcutResim = mevcutResim;
            this.Load += Form_Tarama_Load;

            Btn_Tara.Click += Btn_Tara_Click;
            Btn_Dosya.Click += Btn_Dosya_Click;
        }

        private void Form_Tarama_Load(object sender, EventArgs e)
        {
            if (mevcutResim != null && mevcutResim.Length > 0)
            {
                using (var ms = new MemoryStream(mevcutResim))
                    RESIM_TARAMA.Image = Image.FromStream(ms);
            }
        }

        // 🔹 Public property ekliyoruz
        public PictureBox ResimBox
        {
            get { return RESIM_TARAMA; }
        }

        private void Btn_Tara_Click(object sender, EventArgs e)
        {
            Scanner device = ListBox1.SelectedItem as Scanner;
            if (device == null)
            {
                MessageBox.Show("Lütfen bir tarayıcı seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                ImageFile image = device.ScanImage(WIA.FormatID.wiaFormatJPEG);
                var tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".jpg");
                image.SaveFile(tempFile);

                FileInfo fi = new FileInfo(tempFile);
                if (fi.Length < 10 * 1024 || fi.Length > 100 * 1024)
                {
                    MessageBox.Show("Dosya boyutu 10KB - 100KB arasında olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                byte[] imgBytes = File.ReadAllBytes(tempFile);
                RESIM_TARAMA.Image = Image.FromFile(tempFile);

                // Ana forma gönder
                TaramaTamamlandi?.Invoke(imgBytes);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tarama sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_Dosya_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "JPEG Dosyaları|*.jpg;*.jpeg"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FileInfo fi = new FileInfo(ofd.FileName);
                if (fi.Length < 10 * 1024 || fi.Length > 100 * 1024)
                {
                    MessageBox.Show("Dosya boyutu 10KB - 100KB arasında olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                byte[] imgBytes = File.ReadAllBytes(ofd.FileName);
                RESIM_TARAMA.Image = Image.FromFile(ofd.FileName);

                // Ana forma gönder
                TaramaTamamlandi?.Invoke(imgBytes);
                this.Close();
            }
        }
    }
}
