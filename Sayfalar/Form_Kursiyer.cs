using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tarantula_MTSK.Models;
using Tarantula_MTSK.Services;

namespace Tarantula_MTSK.Sayfalar
{
    public partial class Form_Kursiyer : Form
    {
        private Kursiyer_Model _aktifModel;
        private readonly KursiyerService _kursiyerServis;
        private readonly AramaService _aramaServis;

        public Form_Kursiyer(string connectionString, AramaService aramaServis)
        {
            InitializeComponent();

            _kursiyerServis = new KursiyerService(connectionString);
            _aramaServis = aramaServis ?? throw new ArgumentNullException(nameof(aramaServis));

            this.TopLevel = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;

            if (Tnk_RESIM_Kursiyer != null)
                Tnk_RESIM_Kursiyer.SizeMode = PictureBoxSizeMode.Zoom;

            if (Btn_Kaydet != null) Btn_Kaydet.Click += async (s, e) => await KaydetAsync();
            if (Btn_Sil != null) Btn_Sil.Click += async (s, e) => await SilAsync();
            if (Btn_EkleResim != null) Btn_EkleResim.Click += (s, e) => ResimSec();

            this.Load += async (s, e) =>
            {
                await SinifComboboxDoldurAsync();
                CheckboxlariSaltOkunurYap();
            };
        }

        private void CheckboxlariSaltOkunurYap()
        {

        }
        public void YeniKursiyer()
        {
            _aktifModel = null;
            AlanlariTemizle();
        }

        public async void SetKursiyer(Kursiyer_Model model)
        {
            if (model == null) return;
            _aktifModel = model;

            var sinifBilgileri = await _aramaServis.GetKursiyerSinifBilgileriAsync(model.ID);
            await KursiyerGosterAsync(model, sinifBilgileri);
        }

        private async Task KursiyerGosterAsync(Kursiyer_Model model, (string SertifikaSinifi, string OncekiSertSinifi) sinifBilgileri)
        {
            try
            {
                // TextBox alanları
                Tnk_ADI.Text = model.ADI ?? string.Empty;
                Tnk_SOYADI.Text = model.SOYADI ?? string.Empty;
                Tnk_TC_NO.Text = model.TC_NO ?? string.Empty;
                Tnk_GSM_1.Text = model.GSM_1 ?? string.Empty;
                Tnk_GSM_2.Text = model.GSM_2 ?? string.Empty;
                Tnk_KIM_BABA_ADI.Text = model.KIM_BABA_ADI ?? string.Empty;
                Tnk_KIM_ANA_ADI.Text = model.KIM_ANA_ADI ?? string.Empty;
                Tnk_KIM_DOGUM_YERI.Text = model.KIM_DOGUM_YERI ?? string.Empty;
                Tnk_Adres.Text = model.EV_ADRESI ?? string.Empty;
                Tnk_ADAY_NO.Text = model.ADAY_NO ?? string.Empty;
                Tnk_Referans.Text = model.SARI_NOTLAR ?? string.Empty;

                Tnk_DOGUM_TARIHI.Value = model.DOGUM_TARIHI ?? DateTime.Now;
                Tnk_KAYIT_TARIHI.Value = model.KAYIT_TARIHI ?? DateTime.Now;

                // Resim yükle
                if (model.RESIM != null && model.RESIM.Length > 0)
                {
                    using (var ms = new MemoryStream(model.RESIM))
                    using (var tempImage = Image.FromStream(ms))
                    {
                        Tnk_RESIM_Kursiyer.Image?.Dispose();
                        Tnk_RESIM_Kursiyer.Image = new Bitmap(tempImage);
                    }
                }
                else
                {
                    Tnk_RESIM_Kursiyer.Image?.Dispose();
                    Tnk_RESIM_Kursiyer.Image = null;
                }

                // Combobox doldur
                await SinifComboboxDoldurAsync();

                // Sertifika sınıfları
                if (!string.IsNullOrWhiteSpace(sinifBilgileri.SertifikaSinifi))
                    Cmb_SINIFI.SelectedItem = Cmb_SINIFI.Items.Cast<object>()
                        .FirstOrDefault(i => i.ToString().Equals(sinifBilgileri.SertifikaSinifi, StringComparison.OrdinalIgnoreCase));

                if (!string.IsNullOrWhiteSpace(sinifBilgileri.OncekiSertSinifi))
                    Cmb_ONCEKI_SINIFI.SelectedItem = Cmb_ONCEKI_SINIFI.Items.Cast<object>()
                        .FirstOrDefault(i => i.ToString().Equals(sinifBilgileri.OncekiSertSinifi, StringComparison.OrdinalIgnoreCase));

                // Dönem bilgileri
                var donemBilgileri = await _aramaServis.GetDonemlerVeKursiyerDonemiAsync(model.ID);
                List<string> donemler = donemBilgileri.Donemler ?? new List<string>();
                string kursiyerDonemi = donemBilgileri.KursiyerDonemi;

                Cmb_DONEM.Items.Clear();
                foreach (var d in donemler.OrderBy(x => x))
                    Cmb_DONEM.Items.Add(d);

                if (!string.IsNullOrWhiteSpace(kursiyerDonemi))
                    Cmb_DONEM.SelectedItem = kursiyerDonemi;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Kursiyer bilgileri yüklenemedi:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task SinifComboboxDoldurAsync()
        {
            try
            {
                var sinifListeleri = await _aramaServis.GetParamSertifikaSiniflariAsync();

                Cmb_SINIFI.Items.Clear();
                if (sinifListeleri.YeniSiniflar != null)
                    foreach (var s in sinifListeleri.YeniSiniflar.OrderBy(x => x))
                        Cmb_SINIFI.Items.Add(s);

                Cmb_ONCEKI_SINIFI.Items.Clear();
                if (sinifListeleri.MevcutSiniflar != null)
                    foreach (var s in sinifListeleri.MevcutSiniflar.OrderBy(x => x))
                        Cmb_ONCEKI_SINIFI.Items.Add(s);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sertifika sınıfı yüklenirken hata oluştu:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResimSec()
        {
            try
            {
                using (var ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        using (var temp = Image.FromFile(ofd.FileName))
                        {
                            Tnk_RESIM_Kursiyer.Image?.Dispose();
                            Tnk_RESIM_Kursiyer.Image = new Bitmap(temp);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Resim yüklenirken hata oluştu:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AlanlariTemizle()
        {
            foreach (var ctrl in this.Controls.OfType<TextBox>())
                ctrl.Clear();

            Cmb_SINIFI.SelectedIndex = -1;
            Cmb_ONCEKI_SINIFI.SelectedIndex = -1;
            Cmb_DONEM.SelectedIndex = -1;

            Tnk_RESIM_Kursiyer.Image?.Dispose();
            Tnk_RESIM_Kursiyer.Image = null;

            _aktifModel = null;
        }

        private async Task KaydetAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Tnk_ADI.Text))
                {
                    MessageBox.Show("Ad alanı boş bırakılamaz!");
                    return;
                }

                int? grupId = null;
                if (Cmb_DONEM.SelectedItem != null)
                    grupId = await _aramaServis.GetGrupIdByDonemAsync(Cmb_DONEM.SelectedItem.ToString());

                if (grupId == null)
                {
                    MessageBox.Show("Lütfen geçerli bir dönem seçiniz!");
                    return;
                }

                byte[] resimBytes = null;
                if (Tnk_RESIM_Kursiyer.Image != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        Tnk_RESIM_Kursiyer.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        resimBytes = ms.ToArray();
                    }
                }

                var model = new Kursiyer_Model
                {
                    ID = _aktifModel?.ID ?? 0,
                    ADI = Tnk_ADI.Text.Trim(),
                    SOYADI = Tnk_SOYADI.Text.Trim(),
                    TC_NO = Tnk_TC_NO.Text.Trim(),
                    GSM_1 = Tnk_GSM_1.Text.Trim(),
                    GSM_2 = Tnk_GSM_2.Text.Trim(),
                    KIM_ANA_ADI = Tnk_KIM_ANA_ADI.Text.Trim(),
                    KIM_BABA_ADI = Tnk_KIM_BABA_ADI.Text.Trim(),
                    KIM_DOGUM_YERI = Tnk_KIM_DOGUM_YERI.Text.Trim(),
                    EV_ADRESI = Tnk_Adres.Text.Trim(),
                    ADAY_NO = Tnk_ADAY_NO.Text.Trim(),
                    SARI_NOTLAR = Tnk_Referans.Text.Trim(),
                    DOGUM_TARIHI = Tnk_DOGUM_TARIHI.Value,
                    KAYIT_TARIHI = Tnk_KAYIT_TARIHI.Value,
                    RESIM = resimBytes,
                    ID_GRUP_KARTI = grupId.Value,
                    SERTIFIKA_SINIFI = Cmb_SINIFI.SelectedItem as string,
                    ONCE_SERT_SINIFI = Cmb_ONCEKI_SINIFI.SelectedItem as string
                };

                if (_aktifModel == null || _aktifModel.ID == 0)
                {
                    int yeniId = await _kursiyerServis.AddKursiyerAsync(model);
                    _aktifModel = model;
                    _aktifModel.ID = yeniId;
                    MessageBox.Show("Yeni kursiyer başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await _kursiyerServis.UpdateKursiyerAsync(model);
                    MessageBox.Show("Kursiyer bilgileri güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kayıt işlemi sırasında hata oluştu:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task SilAsync()
        {
            try
            {
                if (_aktifModel == null || _aktifModel.ID == 0)
                {
                    MessageBox.Show("Silinecek kursiyer seçili değil!");
                    return;
                }

                var onay = MessageBox.Show("Bu kursiyeri silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (onay == DialogResult.Yes)
                {
                    await _kursiyerServis.DeleteKursiyerAsync(_aktifModel.ID);
                    MessageBox.Show("Kursiyer başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AlanlariTemizle();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Silme işlemi sırasında hata oluştu:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_Evrak_Click(object sender, EventArgs e)
        {
            if (_aktifModel == null)
            {
                MessageBox.Show("Önce bir kursiyer seçiniz.");
                return;
            }

            var frm = new Form_Kursiyer_Ogrenim(_aktifModel, _aramaServis);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_EkleResim_Click(object sender, EventArgs e)
        {

        }
    }
}
