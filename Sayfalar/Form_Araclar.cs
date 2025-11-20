using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tarantula_MTSK.Models;
using Tarantula_MTSK.Services;

namespace Tarantula_MTSK.Sayfalar
{
    public partial class Form_Araclar : Form
    {
        private readonly AracService _aracService;
        private readonly ServerAyar _serverAyar;
        private int _seciliID = 0;

        public Form_Araclar(ServerAyar serverAyar)
        {
            InitializeComponent();
            _serverAyar = serverAyar ?? throw new ArgumentNullException(nameof(serverAyar));

            // Windows Authentication kontrolü
            if (_serverAyar.BaglantiTuru == "Windows")
            {
                _serverAyar.ConnectionString = $"Server={_serverAyar.Sunucu};Database={_serverAyar.VeritabaniAdi};Trusted_Connection=True;TrustServerCertificate=True;";
            }

            _aracService = new AracService(_serverAyar.ConnectionString);
        }

        private async void Form_Araclar_Load(object sender, EventArgs e)
        {
            
            Dvg_Araclar.AutoGenerateColumns = true; // Kolonları otomatik oluştur
            ComboBoxlariDoldur(); // <- Burada dolduruyoruz
            await AraclariYukle();
        }

        private async Task AraclariYukle()
        {
            try
            {
                var liste = await _aracService.GetAraclarAsync();
                Dvg_Araclar.DataSource = liste;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Araçlar yüklenirken bir hata oluştu: " + ex.Message);
            }
        }

        private void Dvg_Araclar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = Dvg_Araclar.Rows[e.RowIndex];
            _seciliID = row.Cells["ID"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["ID"].Value) : 0;

            Cmb_Aracdurum.Text = row.Cells["DURUMU"].Value?.ToString() ?? "";
            Tnk_Plaka.Text = row.Cells["ARAC_PLAKA"].Value?.ToString() ?? "";
            Cmb_Turu.Text = row.Cells["ARAC_TIPI"].Value?.ToString() ?? "";
            Tnk_Renk.Text = row.Cells["RENGI"].Value?.ToString() ?? "";
            Cmb_Vites.Text = row.Cells["VITES_TURU"].Value?.ToString() ?? "";
            Txt_Model.Text = row.Cells["MODEL"].Value?.ToString() ?? "";

            Tnk_Muayene.Text = row.Cells["MUHAYENE_TAR"].Value != DBNull.Value
                ? Convert.ToDateTime(row.Cells["MUHAYENE_TAR"].Value).ToString("yyyy-MM-dd")
                : "";

            Tnk_Sigorta.Text = row.Cells["SIGORTA_BAS_TAR"].Value != DBNull.Value
                ? Convert.ToDateTime(row.Cells["SIGORTA_BAS_TAR"].Value).ToString("yyyy-MM-dd")
                : "";
        }

        private void Temizle()
        {
            _seciliID = 0;
            Cmb_Aracdurum.Text = "";
            Tnk_Plaka.Text = "";
            Cmb_Turu.Text = "";
            Tnk_Renk.Text = "";
            Cmb_Vites.Text = "";
            Txt_Model.Text = "";
            Tnk_Muayene.Text = "";
            Tnk_Sigorta.Text = "";
        }

        private void Btn_Yeni_Ekle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

       

        private async void Btn_Sil_Click(object sender, EventArgs e)
        {
            if (_seciliID == 0)
            {
                MessageBox.Show("Lütfen silmek için bir araç seçin.");
                return;
            }

            if (MessageBox.Show("Bu aracı silmek istiyor musunuz?", "Onay", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    await _aracService.DeleteAracAsync(_seciliID);
                    MessageBox.Show("Araç silindi.");
                    await AraclariYukle();
                    Temizle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Araç silinirken hata oluştu: " + ex.Message);
                }
            }
        }

        private void Btn_Yeniekle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private async void Btn_Save_Click_1(object sender, EventArgs e)
        {
            var model = new Arac_Model
            {
                ID = _seciliID,
                DURUMU = Cmb_Aracdurum.Text,
                ARAC_PLAKA = Tnk_Plaka.Text,
                ARAC_TIPI = Cmb_Turu.Text,
                RENGI = Tnk_Renk.Text,
                VITES_TURU = Cmb_Vites.Text,
                MODEL = Txt_Model.Text,
                AKT = 1, // AKT default 1, null sorununa karşı
                MUHAYENE_TAR = string.IsNullOrWhiteSpace(Tnk_Muayene.Text)
                    ? null
                    : (DateTime?)Convert.ToDateTime(Tnk_Muayene.Text),
                SIGORTA_BAS_TAR = string.IsNullOrWhiteSpace(Tnk_Sigorta.Text)
                    ? null
                    : (DateTime?)Convert.ToDateTime(Tnk_Sigorta.Text)
            };

            try
            {
                if (_seciliID == 0)
                {
                    await _aracService.AddAracAsync(model);
                    MessageBox.Show("Araç başarıyla eklendi.");
                }
                else
                {
                    await _aracService.UpdateAracAsync(model);
                    MessageBox.Show("Araç güncellendi.");
                }

                await AraclariYukle();
                Temizle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Araç kaydedilirken hata oluştu: " + ex.Message);
            }
        }
        private void ComboBoxlariDoldur()
        {
            // Araç Durumu
            Cmb_Aracdurum.Items.Clear();
            Cmb_Aracdurum.Items.AddRange(new string[] { "Aktif", "Pasif", "Bakımda" });

            // Araç Tipi
            Cmb_Turu.Items.Clear();
            Cmb_Turu.Items.AddRange(new string[] { "Otomobil", "Kamyon", "Minibüs", "Motosiklet" });

            // Vites Türü
            Cmb_Vites.Items.Clear();
            Cmb_Vites.Items.AddRange(new string[] { "Manuel", "Otomatik", "Yarı Otomatik" });
        }

       

       

        private void Btn_Kaydet_Click(object sender, EventArgs e)
        {

        }
    }
}
