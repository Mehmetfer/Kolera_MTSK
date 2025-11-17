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
    public partial class Form_Arama : Form
    {
        private readonly AramaService _service;
        private readonly string _connectionString;
        private readonly Timer _searchTimer;

        public Form_Arama(string connectionString)
        {
            InitializeComponent();
            _connectionString = connectionString;
            _service = new AramaService(connectionString);

            this.TopLevel = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;

            // DataGridView ayarları
            Dvg_Kursiyerler.ReadOnly = true;
            Dvg_Kursiyerler.AllowUserToAddRows = false;
            Dvg_Kursiyerler.AllowUserToDeleteRows = false;
            Dvg_Kursiyerler.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Dvg_Kursiyerler.MultiSelect = false;
            Dvg_Kursiyerler.DoubleBuffered(true);
            Dvg_Kursiyerler.AutoGenerateColumns = false;

            // Eventler
            this.Load += Form_Arama_Load;
            Dvg_Kursiyerler.CellClick += DvgKursiyerler_CellClick;
            Dvg_Kursiyerler.CellDoubleClick += DvgKursiyerler_CellDoubleClick;

            // Arama gecikme (400ms)
            _searchTimer = new Timer { Interval = 400 };
            _searchTimer.Tick += async (s, e) =>
            {
                _searchTimer.Stop();
                await AraAsync();
            };

            Txt_Ara.TextChanged += (s, e) =>
            {
                _searchTimer.Stop();
                _searchTimer.Start();
            };
        }

        private async void Form_Arama_Load(object sender, EventArgs e)
        {
            StilAyarla();
            await ListeleAsync();
        }

        private void StilAyarla()
        {
            Dvg_Kursiyerler.EnableHeadersVisualStyles = false;
            Dvg_Kursiyerler.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            Dvg_Kursiyerler.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            Dvg_Kursiyerler.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            Dvg_Kursiyerler.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            Dvg_Kursiyerler.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 255);
        }

        private async Task ListeleAsync()
        {
            try
            {
                PrgBar.Visible = true;
                var dt = await _service.GetAllKursiyerAsync();

                Dvg_Kursiyerler.Columns.Clear();
                AddColumn("ID", "ID", "No", 60, false);
                AddColumn("TC_NO", "TC_NO", "TC Kimlik No", 130);
                AddColumn("ADI", "ADI", "Adı", 130);
                AddColumn("SOYADI", "SOYADI", "Soyadı", 130);
                AddColumn("GSM_1", "GSM_1", "Telefon", 110);
                AddColumn("SERTIFIKA_SINIFI", "SERTIFIKA_SINIFI", "Ehliyet Sınıfı", 50);
                AddColumn("KAYIT_TARIHI", "KAYIT_TARIHI", "Kayıt Tarihi", 120);
                AddColumn("KIM_BABA_ADI", "KIM_BABA_ADI", "Baba Adı", 130);
                AddColumn("KIM_ANA_ADI", "KIM_ANA_ADI", "Ana Adı", 130);
                AddColumn("GSM_2", "GSM_2", "Cep Telefonu", 200);
                Dvg_Kursiyerler.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri yükleme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                PrgBar.Visible = false;
            }
        }

        private async Task AraAsync()
        {
            try
            {
                PrgBar.Visible = true;
                string keyword = Txt_Ara.Text.Trim();

                var dt = string.IsNullOrWhiteSpace(keyword)
                    ? await _service.GetAllKursiyerAsync()
                    : await _service.SearchKursiyerAsync(keyword); // SQL tarafında case-insensitive ve Ad+Soyad birleşik

                Dvg_Kursiyerler.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Arama hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                PrgBar.Visible = false;
            }
        }

        private async void DvgKursiyerler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (!(Dvg_Kursiyerler.Rows[e.RowIndex].DataBoundItem is DataRowView rv)) return;

            var row = rv.Row;
            int id = Convert.ToInt32(row["ID"]);
            string adi = row["ADI"].ToString();
            string soyadi = row["SOYADI"].ToString();
            Tnk_Adim.Text = $"{adi} {soyadi}".Trim();

            try
            {
                var bytes = await _service.GetKursiyerResimByIdAsync(id);
                if (bytes != null)
                {
                    using (var ms = new MemoryStream(bytes))
                    using (var img = Image.FromStream(ms))
                    {
                        Tnk_RESIM.SizeMode = PictureBoxSizeMode.Zoom;
                        Tnk_RESIM.Image = new Bitmap(img);
                    }
                }
                else
                {
                    Tnk_RESIM.Image = null;
                }
            }
            catch
            {
                Tnk_RESIM.Image = null;
            }
        }

        private async void DvgKursiyerler_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (!(Dvg_Kursiyerler.Rows[e.RowIndex].DataBoundItem is DataRowView rv)) return;

            var row = rv.Row;
            int id = Convert.ToInt32(row["ID"]);

            var model = new Kursiyer_Model
            {
                ID = id,
                ADI = row["ADI"].ToString(),
                SOYADI = row["SOYADI"].ToString(),
                TC_NO = row["TC_NO"].ToString(),
                GSM_1 = row["GSM_1"].ToString(),
                SERTIFIKA_SINIFI = row["SERTIFIKA_SINIFI"].ToString(),
                KIM_ANA_ADI = row["KIM_ANA_ADI"].ToString(),
                KIM_BABA_ADI = row["KIM_BABA_ADI"].ToString(),
                SARI_NOTLAR = row["SARI_NOTLAR"]?.ToString(),
                DOGUM_TARIHI = row["DOGUM_TARIHI"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["DOGUM_TARIHI"]) : null,
                KAYIT_TARIHI = row["KAYIT_TARIHI"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["KAYIT_TARIHI"]) : null,
                ADAY_NO = row["ADAY_NO"]?.ToString(),
                GSM_2 = row["GSM_2"]?.ToString(),
                EV_ADRESI = row["EV_ADRESI"]?.ToString(),
                EV_TELEFON = row["EV_TELEFON"]?.ToString(),
                RESIM = await _service.GetKursiyerResimByIdAsync(id)
            };

            if (this.ParentForm is Ana_Menu ana)
            {
                var frm = new Form_Kursiyer(_connectionString, _service);
                frm.SetKursiyer(model);
                ana.OpenChildForm(frm, "Form_Kursiyer");
                this.Close();
            }
        }

        private void AddColumn(string name, string dataProperty, string headerText, int width, bool visible = true)
        {
            var col = new DataGridViewTextBoxColumn
            {
                Name = name,
                DataPropertyName = dataProperty,
                HeaderText = headerText,
                Width = width,
                ReadOnly = true,
                Visible = visible,
                SortMode = DataGridViewColumnSortMode.Automatic
            };
            Dvg_Kursiyerler.Columns.Add(col);
        }

        private void Grparama_Enter(object sender, EventArgs e)
        {

        }
    }

    public static class DataGridViewExtensions
    {
        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.SetProperty,
                null, dgv, new object[] { setting });
        }
    }
}
