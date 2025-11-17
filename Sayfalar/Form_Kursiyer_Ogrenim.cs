using System;
using System.Windows.Forms;
using Tarantula_MTSK.Models;
using System.Drawing;
using System.IO;
using Tarantula_MTSK.Sayfalar;
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

        // 🔹 Constructor: Kursiyer_Model parametreli
        public Form_Kursiyer_Ogrenim(Kursiyer_Model kursiyer, AramaService evrakService)
        {
            InitializeComponent();
            _kursiyerModel = kursiyer;
            _evrakService = evrakService;

            this.Load += Form_Kursiyer_Ogrenim_Load;



        }

        private void Form_Kursiyer_Ogrenim_Load(object sender, EventArgs e)
        {





            if (_kursiyerModel == null)
            {
                MessageBox.Show("Kursiyer bilgisi alınamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            // Ad Soyad
            Lbl_2.Text = $"{_kursiyerModel.ADI} {_kursiyerModel.SOYADI}".ToUpper();

            // Profil resmi
            if (_kursiyerModel.RESIM != null && _kursiyerModel.RESIM.Length > 0)
                using (var ms = new MemoryStream(_kursiyerModel.RESIM))
                    Tnk_RESIM.Image = Image.FromStream(ms);
            else
                Tnk_RESIM.Image = null;

            // 🔥 KURSIYER_EVRAK verilerini çek
            var evrak = _evrakService.GetKursiyerEvrak(_kursiyerModel.ID);

            if (evrak == null)
                return; // hiç evrak kaydı yoksa form boş kalsın

            // ✅ CheckBox'ları doldur

            Chk_1.Checked = evrak.EkskOgrBel;
            Chk_2.Checked = evrak.EkskSaglik;
            Chk_3.Checked = evrak.EkskSavcilik;
            Chk_4.Checked = evrak.EkskSozlesme;
            Chk_6.Checked = evrak.EkskImza;


            _imgOgr = evrak.ImgOgrBel;
            _imgSaglik = evrak.ImgSaglik;
            _imgSavcilik = evrak.ImgSavcilik;
            _imgImza = evrak.ImgImza;
            _imgSozlesme_On = evrak.ImgSozlesme_On;
            _imgSozlesme_Arka = evrak.ImgSozlesme_Arka;


            // ✅ TextBox alanları doldur
            Cmb_OGR_BEL_TURU.Text = evrak.OgrBelgeTuru;
            Txt_OGR_BEL_VEREN_KURUM.Text = evrak.OgrBelgeVerenKurum;
            Tnk_OGRNM_BELGE_TARIHI.Value = evrak.OgrBelgeTarihi ?? DateTime.Today;
            Txt_OGR_BEL_SAYISI.Text = evrak.OgrBelgeSayisi;

            Tnk_SAG_RAPOR_TARIHI.Value = evrak.SaglikBelgeTarihi ?? DateTime.Today;
            Txt_SAG_RAPOR_BELGENO.Text = evrak.SaglikBelgeNo;
            Txt_SAG_RAPOR_VEREN_KURUM.Text = evrak.SaglikBelverenKurum;
            Txt_SAG_RAPOR_REFERANS.Text = evrak.SaglikBelReferans;

            Tnk_SAVCILIK_BEL_TARIHI.Value = evrak.SavcilikBelgeTarihi ?? DateTime.Today;
            Cmb_SAVCILIK_BEL_VEREN_KURUM.Text = evrak.SavcilikBelgeVerenKurum;
            Txt_SAVCILIK_BEL_NO.Text = evrak.SavcilikBelgeNo;


            SetDate(Tnk_OGRNM_BELGE_TARIHI, evrak.OgrBelgeTarihi);
            SetDate(Tnk_SAG_RAPOR_TARIHI, evrak.SaglikBelgeTarihi);
            SetDate(Tnk_SAVCILIK_BEL_TARIHI, evrak.SavcilikBelgeTarihi);


            // ✅ Fatura alanları

            // ✅ Görselleri PictureBox'a yükle


        }
        private void SetDate(DateTimePicker picker, DateTime? value)
        {
            picker.ShowCheckBox = true; // tarih seçilebilir olsun
            if (value.HasValue)
            {
                picker.Value = value.Value;
                picker.Checked = true;
            }
            else
            {
                picker.Checked = false;
            }
        }

        private void SelectAndLoadImage(ref byte[] imgField, PictureBox box)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Resim Dosyası |*.jpg;*.jpeg;*.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    imgField = File.ReadAllBytes(ofd.FileName);
                    box.Image = Image.FromFile(ofd.FileName);
                }
            }
        }








        private void LoadImageToPictureBox(byte[] data, PictureBox box)
        {
            if (data != null && data.Length > 0)
                using (MemoryStream ms = new MemoryStream(data))
                    box.Image = Image.FromStream(ms);   // ← düzeltildi
            else
                box.Image = null;
        }


        private void Grp_1_Enter(object sender, EventArgs e)
        {

        }


        private void Btn_1_Click(object sender, EventArgs e) => LoadImageToPictureBox(_imgOgr, Pic_belge);


        private void Txt_OGR_BEL_VEREN_KURUM_TextChanged(object sender, EventArgs e)
        {

        }

        private void Lbl_3_Click(object sender, EventArgs e)
        {

        }

        private void Grp_7_Enter(object sender, EventArgs e)
        {

        }

        private void Btn_35_Click(object sender, EventArgs e)
        {

        }

        private void Btn_26_Click(object sender, EventArgs e)
        {

        }

        private void Btn_27_Click(object sender, EventArgs e)
        {

        }

        private void Cmb_SAVCILIK_BEL_VEREN_KURUM_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Txt_SAVCILIK_BEL_TARIHI_TextChanged(object sender, EventArgs e)
        {

        }

        private void Txt_SAVCILIK_BEL_NO_TextChanged(object sender, EventArgs e)
        {

        }

        private void Grp_6_Enter(object sender, EventArgs e)
        {

        }

        private void Btn_33_Click(object sender, EventArgs e)
        {

        }

        private void Btn_28_Click(object sender, EventArgs e)
        {

        }

        private void Btn_23_Click(object sender, EventArgs e)
        {

        }

        private void Cmb_OZUR_DURUMU_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Btn_24_Click(object sender, EventArgs e)
        {

        }

        private void Btn_25_Click(object sender, EventArgs e)
        {

        }

        private void Lbl_saglik_Click(object sender, EventArgs e)
        {

        }

        private void Txt_SAG_RAPOR_REFERANS_TextChanged(object sender, EventArgs e)
        {

        }

        private void Txt_SAG_RAPOR_BELGENO_TextChanged(object sender, EventArgs e)
        {

        }

        private void Txy_SAG_RAPOR_VEREN_KURUM_TextChanged(object sender, EventArgs e)
        {

        }

        private void Txt_SAG_RAPOR_TARIHI_TextChanged(object sender, EventArgs e)
        {

        }

        private void Gpr_5_Enter(object sender, EventArgs e)
        {

        }

        private void Btn_21_Click(object sender, EventArgs e)
        {

        }

        private void Btn_22_Click(object sender, EventArgs e)
        {

        }

        private void Btn_29_Click(object sender, EventArgs e)
        {

        }

        private void Btn_20_Click(object sender, EventArgs e)
        {

        }

        private void Cmb_OGR_BEL_TURU_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Txt_OGR_BEL_TARIHI_TextChanged(object sender, EventArgs e)
        {

        }

        private void Txt_OGR_BEL_SAYISI_TextChanged(object sender, EventArgs e)
        {

        }

        private void Grp_evrak_Enter(object sender, EventArgs e)
        {

        }

        private void Btn_17_Click(object sender, EventArgs e)
        {

        }

        private void Btn_16_Click(object sender, EventArgs e)
        {

        }

        private void Tnk_RESIM_Click(object sender, EventArgs e)
        {

        }

        private void Btn_15_Click(object sender, EventArgs e)
        {

        }

        private void Btn_14_Click(object sender, EventArgs e)
        {

        }

        private void Btn_13_Click(object sender, EventArgs e)
        {

        }

        private void Btn_12_Click(object sender, EventArgs e)
        {

        }

        private void Btn_11_Click(object sender, EventArgs e)
        {
            Form_Tarama Taram = new Form_Tarama();
            Taram.Show(); // yeni sayfayı aç
        }

        private void Btn_6_Click(object sender, EventArgs e)
        {

        }
        private void Btn_Soz_On_Click(object sender, EventArgs e) => LoadImageToPictureBox(_imgSozlesme_On, Pic_belge);


        private void Btn_4_Click(object sender, EventArgs e) => LoadImageToPictureBox(_imgImza, Pic_belge);


        private void Btn_3_Click(object sender, EventArgs e) => LoadImageToPictureBox(_imgSavcilik, Pic_belge);

        private void Btn_2_Click(object sender, EventArgs e) => LoadImageToPictureBox(_imgSaglik, Pic_belge);


        private void Lbl_2_Click(object sender, EventArgs e)
        {

        }

        private void Pic_belge_Click(object sender, EventArgs e)
        {

        }

        private void Pic_exit_Click(object sender, EventArgs e)
        {

        }
        private byte[] ResimSecVeYukle(PictureBox pic)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pic.Image = Image.FromFile(ofd.FileName);
                    return File.ReadAllBytes(ofd.FileName); // byte[] olarak döner ✅
                }
            }
            return null;
        }


        private void Btn_Kaydet1_Click(object sender, EventArgs e)
        {
            if (_kursiyerModel == null)
            {
                MessageBox.Show("Kursiyer bilgisi gelmedi!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var evrak = new KursiyerEvrak_Model
            {
                ID_Kursiyer = _kursiyerModel.ID,

                EkskOgrBel = Chk_1.Checked,
                EkskSaglik = Chk_2.Checked,
                EkskSavcilik = Chk_3.Checked,
                EkskSozlesme = Chk_4.Checked,
                EkskImza = Chk_6.Checked,

                OgrBelgeTuru = Cmb_OGR_BEL_TURU.Text,
                OgrBelgeVerenKurum = Txt_OGR_BEL_VEREN_KURUM.Text,
                OgrBelgeSayisi = Txt_OGR_BEL_SAYISI.Text,
                OgrBelgeTarihi = Tnk_OGRNM_BELGE_TARIHI.Value,

                SaglikBelgeNo = Txt_SAG_RAPOR_BELGENO.Text,
                SaglikBelgeTarihi = Tnk_SAG_RAPOR_TARIHI.Value,
                SaglikBelverenKurum = Txt_SAG_RAPOR_VEREN_KURUM.Text,
                SaglikBelReferans = Txt_SAG_RAPOR_REFERANS.Text,


                SavcilikBelgeNo = Txt_SAVCILIK_BEL_NO.Text,
                SavcilikBelgeTarihi = Tnk_SAVCILIK_BEL_TARIHI.Value,
                SavcilikBelgeVerenKurum = Cmb_SAVCILIK_BEL_VEREN_KURUM.Text,


                ImgOgrBel = _imgOgr,
                ImgSaglik = _imgSaglik,
                ImgSavcilik = _imgSavcilik,

                ImgImza = _imgImza,
                ImgSozlesme_On = _imgSozlesme_On,
                ImgSozlesme_Arka = _imgSozlesme_Arka
            };

            bool sonuc = _evrakService.SaveKursiyerEvrak(evrak);

            if (sonuc)
                MessageBox.Show("Evrak bilgileri kaydedildi ✅", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Kayıt sırasında bir sorun oluştu!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Btn_Soz_Arka_Click(object sender, EventArgs e) => LoadImageToPictureBox(_imgSozlesme_Arka, Pic_belge);

    }

}