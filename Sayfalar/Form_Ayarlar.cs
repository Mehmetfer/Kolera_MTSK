using System;
using System.Windows.Forms;
using Tarantula_MTSK.Models;
using Tarantula_MTSK.Services;

namespace Tarantula_MTSK.Sayfalar
{
    public partial class Form_Ayarlar : Form
    {
        private readonly Kurs_Ayar_Service _kursAyarService;
        private Kurs_Ayar_Model _model;

        public Form_Ayarlar(string connectionString)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            _kursAyarService = new Kurs_Ayar_Service(connectionString);

            this.Load += Form_Ayarlar_Load;
            Btn_Kaydet.Click += Btn_Kaydet_Click; // Kaydet butonunu bağla
        }

        private void Form_Ayarlar_Load(object sender, EventArgs e)
        {
            try
            {
                _model = _kursAyarService.GetKursAyar();
                Doldur();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veriler yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void Doldur()
        {
            if (_model == null) return;

            // PARAM_KURSBILGILERI
            Tnk_KURS_ADI.Text = _model.KURS_ADI;
            Tnk_KURS_ADI_KISA.Text = _model.KURS_ADI_KISA;
            Tnk_IL_KODU.Text = _model.IL_KODU;
            Tnk_ILCE_KODU.Text = _model.ILCE_KODU;
            Tnk_TELEFON.Text = _model.TELEFON;
            Tnk_ADRES.Text = _model.ADRES;
            Tnk_KURUCU_ADI.Text = _model.KURUCU_ADI;
            Tnk_MUDUR_ADI.Text = _model.MUDUR_ADI;
            Tnk_MUDUR_YRD_ADI.Text = _model.MUDUR_YRD_ADI;
            if (_model.KURS_IZIN_TARIHI.HasValue)
                Tnk_KURS_IZIN_TARIHI.Value = _model.KURS_IZIN_TARIHI.Value;

            // PARAM_GENEL_PARAMETRELER
            Txt_Mebbis_Kullaniciadi.Text = _model.MEBBIS_KUL_ADI_1;
            Txt_Mebbis_Kullanicisifre.Text = _model.MEBBIS_KUL_SIF_1;

            // LINKLER
            Tnk_Link.Lines = _model.LINKLER ?? Array.Empty<string>();
        }

        private void Btn_Kaydet_Click(object sender, EventArgs e)
        {
            try
            {
                // Formdan verileri modele aktar
                _model.KURS_ADI = Tnk_KURS_ADI.Text;
                _model.KURS_ADI_KISA = Tnk_KURS_ADI_KISA.Text;
                _model.IL_KODU = Tnk_IL_KODU.Text;
                _model.ILCE_KODU = Tnk_ILCE_KODU.Text;
                _model.TELEFON = Tnk_TELEFON.Text;
                _model.ADRES = Tnk_ADRES.Text;
                _model.KURUCU_ADI = Tnk_KURUCU_ADI.Text;
                _model.MUDUR_ADI = Tnk_MUDUR_ADI.Text;
                _model.MUDUR_YRD_ADI = Tnk_MUDUR_YRD_ADI.Text;
                _model.KURS_IZIN_TARIHI = Tnk_KURS_IZIN_TARIHI.Value;

                _model.MEBBIS_KUL_ADI_1 = Txt_Mebbis_Kullaniciadi.Text;
                _model.MEBBIS_KUL_SIF_1 = Txt_Mebbis_Kullanicisifre.Text;

                _model.LINKLER = Tnk_Link.Lines;

                // Kaydet
                _kursAyarService.SaveKursAyar(_model);

                MessageBox.Show("Kayıt başarılı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kayıt sırasında hata oluştu: " + ex.Message);
            }
        }

        private void Tnk_Link_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
