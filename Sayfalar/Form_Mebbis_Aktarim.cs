using System;
using System.Windows.Forms;

namespace Tarantula_MTSK.Sayfalar
{
    public partial class Form_Mebbis_Aktarim : Form
    {
        // Alanlar constructor ile gelecek
        private readonly string _resim;
        private readonly string _adayNo;
        private readonly string _sinif;
        // Diğer alanlar buraya eklenebilir

        public Form_Mebbis_Aktarim(string resim, string adayNo, string sinif /* diğer parametreler */)
        {
            InitializeComponent();

            _resim = resim;
            _adayNo = adayNo;
            _sinif = sinif;
            // Diğer parametreleri al

            this.Load += Form_Mebbis_Aktarim_Load;
        }

        private void Form_Mebbis_Aktarim_Load(object sender, EventArgs e)
        {
            // Kontrolleri doldur
            Tnk_RESIM_Kursiyer.Text = _resim;
            Tnk_ADAY_NO.Text = _adayNo;
            Cmb_SINIFI.Text = _sinif;
            // Diğer kontrolleri doldur
        }
    }
}
