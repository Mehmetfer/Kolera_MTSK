using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tarantula_MTSK.Models;
using Tarantula_MTSK.Services;
using static Tarantula_MTSK.Models.Kursiyer_EkleModel;

namespace Tarantula_MTSK.Sayfalar
{
    public partial class Form_Kursiyer_ekle : Form
    {
        private readonly KursiyerEkleService _kursiyerServis;
        private Kursiyer_EkleModel _aktifModel = null;

        public Form_Kursiyer_ekle(string connectionString)
        {
            InitializeComponent();
            _kursiyerServis = new KursiyerEkleService(connectionString);

            Tnk_DOGUM_TARIHI.ShowCheckBox = true;
            Tnk_KAYIT_TARIHI.ShowCheckBox = true;

            this.Load += Form_Kursiyer_ekle_Load;
        }

        private async void Form_Kursiyer_ekle_Load(object sender, EventArgs e)
        {
            try
            {
                // DÖNEMLER
                var donemler = await _kursiyerServis.GetDonemlerAsync();
                Cmb_DONEM.Items.Clear();
                Cmb_DONEM.DisplayMember = "DonemAdi";
                Cmb_DONEM.ValueMember = "Id";
                Cmb_DONEM.DataSource = donemler;

                // SINIFLAR (string)
                var siniflar = await _kursiyerServis.GetSertifikaSiniflariAsync();
                Cmb_SINIFI.Items.Clear();
                Cmb_SINIFI.Items.AddRange(siniflar.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show("ComboBox verileri yüklenirken hata oluştu:\n" + ex.Message);
            }
        }

        private void Btn_ResimYukle_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                    Tnk_RESIM_Kursiyer.Image = Image.FromFile(ofd.FileName);
            }
        }

        private byte[] ResimToByte(Image img)
        {
            if (img == null) return null;
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        private async void PictureBox4_Click(object sender, EventArgs e)
        {
            try
            {
                // BASİT KONTROLLER
                if (string.IsNullOrWhiteSpace(Tnk_ADI.Text))
                {
                    MessageBox.Show("Ad alanı boş bırakılamaz!");
                    return;
                }

                if (Cmb_DONEM.SelectedItem == null)
                {
                    MessageBox.Show("Lütfen geçerli bir dönem seçiniz!");
                    return;
                }

                if (Cmb_SINIFI.SelectedItem == null)
                {
                    MessageBox.Show("Lütfen geçerli bir sertifika sınıfı seçiniz!");
                    return;
                }

                var secilenDonem = Cmb_DONEM.SelectedItem as Donem;
                var secilenSinif = Cmb_SINIFI.SelectedItem?.ToString();

                byte[] resimBytes = ResimToByte(Tnk_RESIM_Kursiyer.Image);

                DateTime? dogumTarihi = Tnk_DOGUM_TARIHI.Checked
                    ? Tnk_DOGUM_TARIHI.Value
                    : (DateTime?)null;

                DateTime? kayitTarihi = Tnk_KAYIT_TARIHI.Checked
                    ? Tnk_KAYIT_TARIHI.Value
                    : DateTime.Now;

                int adayNo = 0;
                int.TryParse(Tnk_ADAY_NO.Text, out adayNo);

                var model = new Kursiyer_EkleModel
                {
                    ADI = Tnk_ADI.Text.Trim(),
                    SOYADI = Tnk_SOYADI.Text.Trim(),
                    ID_GRUP_KARTI = secilenDonem.Id,
                    SERTIFIKA_SINIFI = secilenSinif,
                    ONCE_SERT_SINIFI = null,
                    TC_NO = Tnk_TC_NO.Text.Trim(),
                    DOGUM_TARIHI = dogumTarihi,
                    KAYIT_TARIHI = kayitTarihi,
                    RESIM = resimBytes,
                    KURSIYER_DURUMU = 1,
                    ADAY_NO = adayNo
                };

                int yeniId = await _kursiyerServis.AddKursiyerAsync(model);
                _aktifModel = model;
                _aktifModel.ID = yeniId;

                MessageBox.Show("Yeni kursiyer başarıyla eklendi!", "Bilgi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kayıt sırasında hata oluştu:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_EkleResim_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Title = "Resim Seçiniz";
                    ofd.Filter = "Resim Dosyaları (*.jpg; *.jpeg; *.png)|*.jpg;*.jpeg;*.png";

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        Tnk_RESIM_Kursiyer.Image = Image.FromFile(ofd.FileName);
                        Tnk_RESIM_Kursiyer.Tag = ofd.FileName;   // gerekirse dosya yolu

                        MessageBox.Show("Resim başarıyla yüklendi!", "Bilgi",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Resim yüklenirken hata oluştu:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_Tarama_Click(object sender, EventArgs e)
        {
            var Taram = new Form_Tarama
            {
                Owner = this,                          // Ana formu owner yap
                StartPosition = FormStartPosition.CenterParent // Ana form ortasında aç
            };
            Taram.Show();
        }

        private void Btn_Evrak_Click(object sender, EventArgs e)
        {

        }
    }
}
