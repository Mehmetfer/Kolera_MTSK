using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tarantula_MTSK.Models;
using Tarantula_MTSK.Services;

namespace Tarantula_MTSK.Sayfalar
{
    public partial class Form_Mebbis : Form
    {
        private readonly MebbisService _mebbisService;
        private readonly string _connectionString;
        private Mebbis_Model _seciliKursiyer;

        public Form_Mebbis(string connectionString)
        {
            InitializeComponent();
            _connectionString = connectionString;
            _mebbisService = new MebbisService(connectionString);
            this.Load += Form_Mebbis_Load;
        }

        private async void Form_Mebbis_Load(object sender, EventArgs e)
        {
            ConfigureGrid();
            await LoadDonemlerAsync();
        }

        private void ConfigureGrid()
        {
            Dtg_Donemlerlistele.ReadOnly = true;
            Dtg_Donemlerlistele.AllowUserToAddRows = false;
            Dtg_Donemlerlistele.AllowUserToDeleteRows = false;
            Dtg_Donemlerlistele.RowHeadersVisible = false;
            Dtg_Donemlerlistele.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Dtg_Donemlerlistele.SelectionChanged += Dtg_Donemlerlistele_SelectionChanged;
        }

        private async Task LoadDonemlerAsync()
        {
            DataTable dt = await _mebbisService.GetDonemlerAsync();

            DataRow dr = dt.NewRow();
            dr["ID"] = -1;
            dr["DONEM_ADI"] = "BEKLEYEN DÖNEM";
            dt.Rows.InsertAt(dr, 0);

            Combo_Donemler.DisplayMember = "DONEM_ADI";
            Combo_Donemler.ValueMember = "ID";
            Combo_Donemler.DataSource = dt;

            Combo_Donemler.SelectedIndexChanged += Combo_Donemler_SelectedIndexChanged;
        }

        private async void Combo_Donemler_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Combo_Donemler.SelectedValue == null) return;

            int donemId = Convert.ToInt32(Combo_Donemler.SelectedValue);
            DataTable dt = await _mebbisService.GetKursiyerByDonemIdAsync(donemId);

            foreach (DataRow row in dt.Rows)
            {
                row["EkskOgrBel"] = EvrakDonusum(row["EkskOgrBel"]);
                row["EkskSaglik"] = EvrakDonusum(row["EkskSaglik"]);
                row["EkskSavcilik"] = EvrakDonusum(row["EkskSavcilik"]);
                row["EkskSozlesme"] = EvrakDonusum(row["EkskSozlesme"]);
                row["EkskImza"] = EvrakDonusum(row["EkskImza"]);
            }

            Dtg_Donemlerlistele.DataSource = dt.DefaultView.ToTable(false,
                "ID_KURSIYER", "ADI", "SOYADI", "TC_NO", "SERTIFIKA_SINIFI", "DONEM_ADI",
                "EkskOgrBel", "EkskSaglik", "EkskSavcilik", "EkskSozlesme", "EkskImza"
            );

            if (Dtg_Donemlerlistele.Columns.Contains("ID_KURSIYER"))
                Dtg_Donemlerlistele.Columns["ID_KURSIYER"].Visible = false;
        }

        private string EvrakDonusum(object value)
        {
            if (value == null || value == DBNull.Value) return "Yok";
            string v = value.ToString().Trim().ToUpper();
            return v == "VAR" ? "Var" : "Yok";
        }

        private async void Dtg_Donemlerlistele_SelectionChanged(object sender, EventArgs e)
        {
            if (Dtg_Donemlerlistele.SelectedRows.Count == 0) return;

            DataGridViewRow row = Dtg_Donemlerlistele.SelectedRows[0];
            int idKursiyer = Convert.ToInt32(row.Cells["ID_KURSIYER"].Value);

            _seciliKursiyer = await _mebbisService.GetEvrakByKursiyerIdAsync(idKursiyer);
            if (_seciliKursiyer == null) return;

            Tnk_ADI.Text = _seciliKursiyer.ADI;
            Tnk_SOYADI.Text = _seciliKursiyer.SOYADI;
            Tnk_ADAY_NO.Text = _seciliKursiyer.ADAY_NO;
            Cmb_SINIFI.Text = _seciliKursiyer.SERTIFIKA_SINIFI;

            if (_seciliKursiyer.Foto != null)
            {
                Tnk_RESIM_Kursiyer.Image?.Dispose();
                using (MemoryStream ms = new MemoryStream(_seciliKursiyer.Foto))
                {
                    Tnk_RESIM_Kursiyer.Image = Image.FromStream(ms);
                }
            }
            else
            {
                Tnk_RESIM_Kursiyer.Image = null;
            }
        }

        private void Btn_Aktar_Click(object sender, EventArgs e)
        {
            if (_seciliKursiyer == null)
            {
                MessageBox.Show("Önce bir kursiyer seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ShowMebbisForm();
        }

        private void Btn_itele_Click(object sender, EventArgs e)
        {
            if (_seciliKursiyer == null)
            {
                MessageBox.Show("Önce bir kursiyer seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ShowMebbisForm();
        }

        private void ShowMebbisForm()
        {
            Form_Mebbis_Aktarim frm = new Form_Mebbis_Aktarim(_seciliKursiyer, _connectionString);
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }
    }
}
