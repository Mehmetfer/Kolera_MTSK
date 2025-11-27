using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Tarantula_MTSK.Models;
using Tarantula_MTSK.Services;

namespace Tarantula_MTSK.Sayfalar
{
    public partial class Form_Kursiyer_Ogrenim : Form
    {
        private readonly Kursiyer_Model _kursiyerModel;
        private readonly AramaService _evrakService;

        private byte[] _imgOgr = null;
        private byte[] _imgSaglik = null;
        private byte[] _imgSavcilik = null;
        private byte[] _imgImza = null;
        private byte[] _imgSozlesme_On = null;
        private byte[] _imgSozlesme_Arka = null;

        public Form_Kursiyer_Ogrenim(Kursiyer_Model kursiyer, AramaService evrakService)
        {
            InitializeComponent();

            _kursiyerModel = kursiyer;
            _evrakService = evrakService;

            this.Load += Form_Kursiyer_Ogrenim_Load;

            // 🔹 Pic_Belgem butonları
            Btn_1.Click += Btn_1_Click;
            Btn_2.Click += Btn_2_Click;
            Btn_3.Click += Btn_3_Click;
            Btn_4.Click += Btn_4_Click;
            Btn_Soz_On.Click += Btn_Soz_On_Click;
            Btn_Soz_Arka.Click += Btn_Soz_Arka_Click;

            // 🔹 Tarama butonları
            Btn_Tara_Ogrenim.Click += Btn_Tara_Ogrenim_Click;
            Btn_Tara_Saglik.Click += Btn_Tara_Saglik_Click;
            Btn_Tara_Savcilik.Click += Btn_Tara_Savcilik_Click;
            Btn_Tara_Imza.Click += Btn_Tara_Imza_Click;
            Btn_Tara_SozlesmeOn.Click += Btn_Tara_SozlesmeOn_Click;
            Btn_Tara_SozlesmeArka.Click += Btn_Tara_SozlesmeArka_Click;
        }

        private void Form_Kursiyer_Ogrenim_Load(object sender, EventArgs e)
        {
            if (_kursiyerModel == null)
            {
                MessageBox.Show("Kursiyer bilgisi alınamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            // 🔹 Ad-Soyad
            Lbl_Adsoyad.Text = $"{_kursiyerModel.ADI} {_kursiyerModel.SOYADI}".ToUpper();

            // 🔹 Profil resmi
            if (_kursiyerModel.RESIM != null && _kursiyerModel.RESIM.Length > 0)
            {
                using (var ms = new MemoryStream(_kursiyerModel.RESIM))
                    Tnk_RESIM_Kursiyer.Image = Image.FromStream(ms);
            }

            // 🔹 Kursiyer evraklarını yükle
            var evrak = _evrakService.GetKursiyerEvrak(_kursiyerModel.ID);
            if (evrak == null) return;

            // 🔹 CheckBox'ları doldur
            Chk_1.Checked = evrak.EkskOgrBel;
            Chk_2.Checked = evrak.EkskSaglik;
            Chk_3.Checked = evrak.EkskSavcilik;
            Chk_4.Checked = evrak.EkskSozlesme;
            Chk_6.Checked = evrak.EkskImza;

            // 🔹 TextBox ve ComboBox alanlarını doldur
            Cmb_OGR_BEL_TURU.Text = evrak.OgrBelgeTuru ?? string.Empty;
            Txt_OGR_BEL_VEREN_KURUM.Text = evrak.OgrBelgeVerenKurum ?? string.Empty;
            Txt_OGR_BEL_SAYISI.Text = evrak.OgrBelgeSayisi ?? string.Empty;

            Txt_SAG_RAPOR_BELGENO.Text = evrak.SaglikBelgeNo ?? string.Empty;
            Txt_SAG_RAPOR_VEREN_KURUM.Text = evrak.SaglikBelverenKurum ?? string.Empty;
            Txt_SAG_RAPOR_REFERANS.Text = evrak.SaglikBelReferans ?? string.Empty;

            Txt_SAVCILIK_BEL_NO.Text = evrak.SavcilikBelgeNo ?? string.Empty;
            Cmb_SAVCILIK_BEL_VEREN_KURUM.Text = evrak.SavcilikBelgeVerenKurum ?? string.Empty;

            // 🔹 Görselleri ata
            _imgOgr = evrak.ImgOgrBel;
            _imgSaglik = evrak.ImgSaglik;
            _imgSavcilik = evrak.ImgSavcilik;
            _imgImza = evrak.ImgImza;
            _imgSozlesme_On = evrak.ImgSozlesme_On;
            _imgSozlesme_Arka = evrak.ImgSozlesme_Arka;

            // 🔹 Tarihleri güvenli şekilde ata
            SetDate(Tnk_OGRNM_BELGE_TARIHI, evrak.OgrBelgeTarihi);
            SetDate(Tnk_SAG_RAPOR_TARIHI, evrak.SaglikBelgeTarihi);
            SetDate(Tnk_SAVCILIK_BEL_TARIHI, evrak.SavcilikBelgeTarihi);
        }
        

        private void SetDate(DateTimePicker picker, DateTime? value)
        {
            picker.ShowCheckBox = true;
            if (value.HasValue && value.Value >= picker.MinDate && value.Value <= picker.MaxDate)
            {
                picker.Value = value.Value;
                picker.Checked = true;
            }
            else
            {
                picker.Checked = false;
                picker.Value = DateTime.Today;
            }
        }

        // 🔹 Pic_Belgem butonları
        private void Btn_1_Click(object sender, EventArgs e) => LoadImageToPictureBox(_imgOgr, Pic_Belgem);
        private void Btn_2_Click(object sender, EventArgs e) => LoadImageToPictureBox(_imgSaglik, Pic_Belgem);
        private void Btn_3_Click(object sender, EventArgs e) => LoadImageToPictureBox(_imgSavcilik, Pic_Belgem);
        private void Btn_4_Click(object sender, EventArgs e) => LoadImageToPictureBox(_imgImza, Pic_Belgem);
        private void Btn_Soz_On_Click(object sender, EventArgs e) => LoadImageToPictureBox(_imgSozlesme_On, Pic_Belgem);
        private void Btn_Soz_Arka_Click(object sender, EventArgs e) => LoadImageToPictureBox(_imgSozlesme_Arka, Pic_Belgem);

        // 🔹 Tarama butonları
        private void Btn_Tara_Ogrenim_Click(object sender, EventArgs e) =>
            OpenTaramaForm(_imgOgr, data => _imgOgr = data);

        private void Btn_Tara_Saglik_Click(object sender, EventArgs e) =>
            OpenTaramaForm(_imgSaglik, data => _imgSaglik = data);

        private void Btn_Tara_Savcilik_Click(object sender, EventArgs e) =>
            OpenTaramaForm(_imgSavcilik, data => _imgSavcilik = data);

        private void Btn_Tara_Imza_Click(object sender, EventArgs e) =>
            OpenTaramaForm(_imgImza, data => _imgImza = data);

        private void Btn_Tara_SozlesmeOn_Click(object sender, EventArgs e) =>
            OpenTaramaForm(_imgSozlesme_On, data => _imgSozlesme_On = data);

        private void Btn_Tara_SozlesmeArka_Click(object sender, EventArgs e) =>
            OpenTaramaForm(_imgSozlesme_Arka, data => _imgSozlesme_Arka = data);

        // 🔹 Tarama formunu aç, resmi Pic_Belgem'e ve Form_Tarama.RESIM_TARAMA'ya gönder
        private void OpenTaramaForm(byte[] mevcutResim, Action<byte[]> kayitAction)
        {
            using (var taramaForm = new Form_Tarama(mevcutResim))
            {
                // Tarama tamamlandığında
                taramaForm.TaramaTamamlandi += TaramaTamamlandiHandler;

                void TaramaTamamlandiHandler(byte[] data)
                {
                    // Pic_Belgem'de göster ve kaydet
                    LoadImageToPictureBox(data, Pic_Belgem);
                    kayitAction(data);
                }

                taramaForm.ShowDialog();
                taramaForm.TaramaTamamlandi -= TaramaTamamlandiHandler; // temizlik
            }
        }

        

        // 🔹 Pic_Belgem’e yüklemek için ortak method
        private void LoadImageToPictureBox(byte[] data, PictureBox box)
        {
            if (data != null && data.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream(data))
                    box.Image = Image.FromStream(ms);
            }
            else
            {
                box.Image = null;
            }
        }

        private void Btn_Kaydet1_Click(object sender, EventArgs e)
        {
            if (_kursiyerModel == null) return;

            var evrak = new KursiyerEvrak_Model
            {
                ID_Kursiyer = _kursiyerModel.ID,

                // 🔹 Eksik/var kontrolleri
                EkskOgrBel = Chk_1.Checked,
                EkskSaglik = Chk_2.Checked,
                EkskSavcilik = Chk_3.Checked,
                EkskSozlesme = Chk_4.Checked,
                EkskImza = Chk_6.Checked,

                // 🔹 Text alanlar
                OgrBelgeTuru = string.IsNullOrWhiteSpace(Cmb_OGR_BEL_TURU.Text) ? null : Cmb_OGR_BEL_TURU.Text,
                OgrBelgeVerenKurum = string.IsNullOrWhiteSpace(Txt_OGR_BEL_VEREN_KURUM.Text) ? null : Txt_OGR_BEL_VEREN_KURUM.Text,
                OgrBelgeSayisi = string.IsNullOrWhiteSpace(Txt_OGR_BEL_SAYISI.Text) ? null : Txt_OGR_BEL_SAYISI.Text,

                SaglikBelgeNo = string.IsNullOrWhiteSpace(Txt_SAG_RAPOR_BELGENO.Text) ? null : Txt_SAG_RAPOR_BELGENO.Text,
                SaglikBelverenKurum = string.IsNullOrWhiteSpace(Txt_SAG_RAPOR_VEREN_KURUM.Text) ? null : Txt_SAG_RAPOR_VEREN_KURUM.Text,
                SaglikBelReferans = string.IsNullOrWhiteSpace(Txt_SAG_RAPOR_REFERANS.Text) ? null : Txt_SAG_RAPOR_REFERANS.Text,

                SavcilikBelgeNo = string.IsNullOrWhiteSpace(Txt_SAVCILIK_BEL_NO.Text) ? null : Txt_SAVCILIK_BEL_NO.Text,
                SavcilikBelgeVerenKurum = string.IsNullOrWhiteSpace(Cmb_SAVCILIK_BEL_VEREN_KURUM.Text) ? null : Cmb_SAVCILIK_BEL_VEREN_KURUM.Text,

                // 🔹 Tarihler
                OgrBelgeTarihi = Tnk_OGRNM_BELGE_TARIHI.Checked ? (DateTime?)Tnk_OGRNM_BELGE_TARIHI.Value : null,
                SaglikBelgeTarihi = Tnk_SAG_RAPOR_TARIHI.Checked ? (DateTime?)Tnk_SAG_RAPOR_TARIHI.Value : null,
                SavcilikBelgeTarihi = Tnk_SAVCILIK_BEL_TARIHI.Checked ? (DateTime?)Tnk_SAVCILIK_BEL_TARIHI.Value : null,

                // 🔹 Resimler
                ImgOgrBel = _imgOgr,
                ImgSaglik = _imgSaglik,
                ImgSavcilik = _imgSavcilik,
                ImgImza = _imgImza,
                ImgSozlesme_On = _imgSozlesme_On,
                ImgSozlesme_Arka = _imgSozlesme_Arka
            };

            bool kayitBasarili = _evrakService.SaveKursiyerEvrak(evrak);

            MessageBox.Show(
                kayitBasarili ? "Evrak bilgileri kaydedildi ✅" : "Kayıt sırasında bir sorun oluştu!",
                "Bilgi",
                MessageBoxButtons.OK,
                kayitBasarili ? MessageBoxIcon.Information : MessageBoxIcon.Error
            );
        }
    }
    }

