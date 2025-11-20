using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tarantula_MTSK.Models;
using Tarantula_MTSK.Services;

namespace Tarantula_MTSK.Sayfalar
{
    public partial class Form_Personel : Form
    {
        private readonly PersonelService _personelService;
        private readonly ServerAyar _serverAyar;
        private int _seciliID = 0;

        public Form_Personel(ServerAyar serverAyar)
        {
            InitializeComponent();
            _serverAyar = serverAyar ?? throw new ArgumentNullException(nameof(serverAyar));

            // Windows Authentication kontrolü
            if (_serverAyar.BaglantiTuru == "Windows")
            {
                _serverAyar.ConnectionString = $"Server={_serverAyar.Sunucu};Database={_serverAyar.VeritabaniAdi};Trusted_Connection=True;TrustServerCertificate=True;";
            }

            _personelService = new PersonelService(_serverAyar.ConnectionString);

            // Event handler bağlamaları
            Btn_Yeniekle.Click += Btn_Yeniekle_Click;
            Btn_Kaydet.Click += Btn_Kaydet_Click;
            Btn_Sil.Click += Btn_Sil_Click;
            Btn_Resim_Ekle.Click += Btn_Resim_Ekle_Click;
            Dvg_Personel.CellClick += Dvg_Personel_CellClick;
        }

        private async void Form_Personel_Load(object sender, EventArgs e)
        {
            Dvg_Personel.AutoGenerateColumns = true;
            await PersonelleriYukle();
            ComboBoxDoldur();
        }

        private async Task PersonelleriYukle()
        {
            try
            {
                var liste = await _personelService.GetPersonellerAsync();
                Dvg_Personel.DataSource = liste;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Personeller yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void ComboBoxDoldur()
        {
            Cmb_durumu.Items.Clear();
            Cmb_durumu.Items.AddRange(new string[] { "Aktif", "Pasif" });

            Cmb_SINIFI.Items.Clear();
            Cmb_SINIFI.Items.AddRange(new string[] { "B", "C", "D", "E" });

            Cmd_ikinci.Items.Clear();
            Cmd_ikinci.Items.AddRange(new string[] { "B", "C", "D", "E" });

            Cmb_Cinsiyet.Items.Clear();
            Cmb_Cinsiyet.Items.AddRange(new string[] { "Erkek", "Kadın" });

            Cmb_Medeni.Items.Clear();
            Cmb_Medeni.Items.AddRange(new string[] { "Bekar", "Evli" });

            Cmb_Yonetici.Items.Clear();
            Cmb_Yonetici.Items.AddRange(new string[] { "Yönetici", "Personel" });

            Cmb_Gorev1.Items.Clear();
            Cmb_Gorev1.Items.AddRange(new string[] { "Sürücü", "Öğretmen", "İdari" });
        }

        private void Dvg_Personel_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = Dvg_Personel.Rows[e.RowIndex];
            _seciliID = row.Cells["ID"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["ID"].Value) : 0;

            Tnk_TC_NO.Text = row.Cells["TC_NO"].Value?.ToString() ?? "";
            Tnk_ADI.Text = row.Cells["ADI"].Value?.ToString() ?? "";
            Tnk_SOYADI.Text = row.Cells["SOYADI"].Value?.ToString() ?? "";
            Tnk_GSM_1.Text = row.Cells["GSM_1"].Value?.ToString() ?? "";
            Cmb_durumu.Text = row.Cells["PERSONEL_DURUMU"].Value?.ToString() ?? "";
            Cmb_SINIFI.Text = row.Cells["EHLIYET_SINIFI"].Value?.ToString() ?? "";
            Cmd_ikinci.Text = row.Cells["EHLIYET_IKINCI"].Value?.ToString() ?? "";
            Cmb_Cinsiyet.Text = row.Cells["CINSIYET"].Value?.ToString() ?? "";
            Cmb_Medeni.Text = row.Cells["MEDENI_DUR"].Value?.ToString() ?? "";
            Cmb_Yonetici.Text = row.Cells["YONETICI_GOREVI"].Value?.ToString() ?? "";
            Cmb_Gorev1.Text = row.Cells["VERDIGI_DERS_1"].Value?.ToString() ?? "";

            Tnk_DOGUM_TARIHI.Text = row.Cells["DOGUM_TARIHI"].Value != DBNull.Value
                ? Convert.ToDateTime(row.Cells["DOGUM_TARIHI"].Value).ToString("yyyy-MM-dd")
                : "";

            Date_Sozlesme.Text = row.Cells["SOZ_BASLAMA_TAR"].Value != DBNull.Value
                ? Convert.ToDateTime(row.Cells["SOZ_BASLAMA_TAR"].Value).ToString("yyyy-MM-dd")
                : "";

            // Resim yükleme
            if (row.Cells["RESIM"].Value != DBNull.Value)
            {
                byte[] resimData = (byte[])row.Cells["RESIM"].Value;
                using (var ms = new System.IO.MemoryStream(resimData))
                {
                    Tnk_RESIM_Personel.Image = Image.FromStream(ms);
                }
            }
            else
            {
                Tnk_RESIM_Personel.Image = null;
            }
        }

        private void Temizle()
        {
            _seciliID = 0;
            Tnk_TC_NO.Text = "";
            Tnk_ADI.Text = "";
            Tnk_SOYADI.Text = "";
            Tnk_GSM_1.Text = "";
            Cmb_durumu.Text = "";
            Cmb_SINIFI.Text = "";
            Cmd_ikinci.Text = "";
            Cmb_Cinsiyet.Text = "";
            Cmb_Medeni.Text = "";
            Cmb_Yonetici.Text = "";
            Cmb_Gorev1.Text = "";
            Tnk_DOGUM_TARIHI.Text = "";
            Date_Sozlesme.Text = "";
            Tnk_RESIM_Personel.Image = null;
        }

        private void Btn_Yeniekle_Click(object sender, EventArgs e) => Temizle();

        private async void Btn_Kaydet_Click(object sender, EventArgs e)
        {
            var model = new Personel_Model
            {
                ID = _seciliID,
                PERSONEL_DURUMU = Cmb_durumu.Text,
                TC_NO = Tnk_TC_NO.Text,
                ADI = Tnk_ADI.Text,
                SOYADI = Tnk_SOYADI.Text,
                GSM_1 = Tnk_GSM_1.Text,
                EHLIYET_SINIFI = Cmb_SINIFI.Text,
                EHLIYET_IKINCI = Cmd_ikinci.Text,
                CINSIYET = Cmb_Cinsiyet.Text,
                MEDENI_DUR = Cmb_Medeni.Text,
                DOGUM_TARIHI = string.IsNullOrWhiteSpace(Tnk_DOGUM_TARIHI.Text) ? null : (DateTime?)Convert.ToDateTime(Tnk_DOGUM_TARIHI.Text),
                YONETICI_GOREVI = Cmb_Yonetici.Text,
                VERDIGI_DERS_1 = Cmb_Gorev1.Text,
                SOZ_BASLAMA_TAR = string.IsNullOrWhiteSpace(Date_Sozlesme.Text) ? null : (DateTime?)Convert.ToDateTime(Date_Sozlesme.Text),
                RESIM = Tnk_RESIM_Personel.Image != null ? ImageToByte(Tnk_RESIM_Personel.Image) : null
            };

            try
            {
                if (_seciliID == 0)
                {
                    await _personelService.AddPersonelAsync(model);
                    MessageBox.Show("Personel başarıyla eklendi.");
                }
                else
                {
                    await _personelService.UpdatePersonelAsync(model);
                    MessageBox.Show("Personel güncellendi.");
                }
                await PersonelleriYukle();
                Temizle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kaydetme işleminde hata: " + ex.Message);
            }
        }

        private async void Btn_Sil_Click(object sender, EventArgs e)
        {
            if (_seciliID == 0)
            {
                MessageBox.Show("Lütfen silmek için bir personel seçin.");
                return;
            }

            if (MessageBox.Show("Bu personeli silmek istiyor musunuz?", "Onay", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    await _personelService.DeletePersonelAsync(_seciliID);
                    MessageBox.Show("Personel silindi.");
                    await PersonelleriYukle();
                    Temizle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Silme işleminde hata: " + ex.Message);
                }
            }
        }

        private void Btn_Resim_Ekle_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Tnk_RESIM_Personel.Image = Image.FromFile(dlg.FileName);
                }
            }
        }

        private byte[] ImageToByte(Image img)
        {
            using (var ms = new System.IO.MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}
