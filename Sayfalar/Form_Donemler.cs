using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tarantula_MTSK.Services;

namespace Tarantula_MTSK.Sayfalar
{
    public partial class Form_Donemler : Form
    {
        private readonly IDonemService _donemService;
        private readonly string _connectionString;

        public Form_Donemler(string connectionString)
        {
            InitializeComponent();
            _connectionString = connectionString;
            _donemService = new DonemService(connectionString);
        }

        private async void Form_Donemler_Load(object sender, EventArgs e)
        {
            Btn_Guncelle.Enabled = false;
            Btn_Sil.Enabled = false;

            await DonemleriListeleAsync();
        }

        private async Task DonemleriListeleAsync()
        {
            DataTable dt = await _donemService.GetGrupKartlariAsync();

            // Tarihe göre sıralama: önce BAS_TAR, sonra BIT_TAR
            if (dt.Rows.Count > 0)
            {
                dt.DefaultView.Sort = "BAS_TAR DESC, BIT_TAR DESC";
                dt = dt.DefaultView.ToTable();
            }

            Dtg_goster.DataSource = dt;

            // Sütun başlıkları
            if (Dtg_goster.Columns.Contains("ID")) Dtg_goster.Columns["ID"].HeaderText = "ID";
            if (Dtg_goster.Columns.Contains("DONEM_ADI")) Dtg_goster.Columns["DONEM_ADI"].HeaderText = "Dönem Adı";
            if (Dtg_goster.Columns.Contains("DONEM_YILI")) Dtg_goster.Columns["DONEM_YILI"].HeaderText = "Yıl";
            if (Dtg_goster.Columns.Contains("DONEM_AYI")) Dtg_goster.Columns["DONEM_AYI"].HeaderText = "Ay";
            if (Dtg_goster.Columns.Contains("DONEM_SUBESI")) Dtg_goster.Columns["DONEM_SUBESI"].HeaderText = "Şube";
            if (Dtg_goster.Columns.Contains("DONEM_GRUBU")) Dtg_goster.Columns["DONEM_GRUBU"].HeaderText = "Grup";
            if (Dtg_goster.Columns.Contains("BAS_TAR")) Dtg_goster.Columns["BAS_TAR"].HeaderText = "Başlangıç";
            if (Dtg_goster.Columns.Contains("BIT_TAR")) Dtg_goster.Columns["BIT_TAR"].HeaderText = "Bitiş";

            Dtg_goster.ReadOnly = true;
            Dtg_goster.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void Dtg_goster_SelectionChanged(object sender, EventArgs e)
        {
            bool secili = Dtg_goster.SelectedRows.Count > 0;
            Btn_Guncelle.Enabled = secili;
            Btn_Sil.Enabled = secili;
        }

        private void Btn_Donem_Ekle_Click(object sender, EventArgs e)
        {
            Form_Donem_Ekle formEkle = new Form_Donem_Ekle(_connectionString);
            formEkle.FormClosed += async (s, args) => await DonemleriListeleAsync();
            formEkle.Show();
        }

        private void Btn_Guncelle_Click_1(object sender, EventArgs e)
        {
            AcFormuGuncelle();
        }

        private void Dtg_goster_DoubleClick(object sender, EventArgs e)
        {
            AcFormuGuncelle();
        }

        private void AcFormuGuncelle()
        {
            if (Dtg_goster.SelectedRows.Count == 0) return;

            DataGridViewRow row = Dtg_goster.SelectedRows[0];
            int id = Convert.ToInt32(row.Cells["ID"].Value);

            Form_Donem_Ekle formGuncelle = new Form_Donem_Ekle(_connectionString);

            // Ayı string olarak alıyoruz
            string ay = row.Cells["DONEM_AYI"].Value.ToString();

            formGuncelle.LoadForUpdate(
                id,
                row.Cells["DONEM_YILI"].Value.ToString(),
                ay,
                row.Cells["DONEM_SUBESI"].Value.ToString(),
                row.Cells["DONEM_ADI"].Value.ToString(),
                row.Cells["DONEM_GRUBU"].Value.ToString(),
                Convert.ToDateTime(row.Cells["BAS_TAR"].Value),
                Convert.ToDateTime(row.Cells["BIT_TAR"].Value)
            );

            formGuncelle.FormClosed += async (s, args) => await DonemleriListeleAsync();
            formGuncelle.Show();
        }

        private async void Btn_Sil_Click_1(object sender, EventArgs e)
        {
            if (Dtg_goster.SelectedRows.Count == 0) return;

            DataGridViewRow row = Dtg_goster.SelectedRows[0];
            int id = Convert.ToInt32(row.Cells["ID"].Value);

            if (MessageBox.Show("Seçili dönem silinecek. Emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                await _donemService.DeleteGrupAsync(id);
                await DonemleriListeleAsync();
            }
        }

        private void Dtg_goster_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            bool secili = Dtg_goster.SelectedRows.Count > 0;
            Btn_Guncelle.Enabled = secili;
            Btn_Sil.Enabled = secili;
        }

       
    }
}
