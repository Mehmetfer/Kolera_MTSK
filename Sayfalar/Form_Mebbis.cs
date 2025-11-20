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

        public Form_Mebbis(string connectionString)
        {
            InitializeComponent();
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
        }

        private async Task LoadDonemlerAsync()
        {
            DataTable dt = await _mebbisService.GetDonemlerAsync();

            // BEKLEYEN DÖNEM en başta
            DataRow dr = dt.NewRow();
            dr["ID"] = -1;
            dr["DONEM_ADI"] = "BEKLEYEN DÖNEM";
            dt.Rows.InsertAt(dr, 0);

            Combo_Donemler.DisplayMember = "DONEM_ADI";
            Combo_Donemler.ValueMember = "ID";
            Combo_Donemler.DataSource = dt;

            Combo_Donemler.SelectedIndexChanged += Combo_Donemler_SelectedIndexChanged;
            Dtg_Donemlerlistele.SelectionChanged += Dtg_Donemlerlistele_SelectionChanged;
        }

        private string EvrakDonusum(object value)
        {
            if (value == null || value == DBNull.Value) return "Yok";
            string v = value.ToString().Trim().ToUpper();
            return v == "VAR" ? "Var" : "Yok";
        }

        private async void Combo_Donemler_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Combo_Donemler.SelectedValue == null) return;

            int donemId = Convert.ToInt32(Combo_Donemler.SelectedValue);

            DataTable dt = await _mebbisService.GetKursiyerByDonemIdAsync(donemId);

            // Datagrid’de Var/Yok stringlerini güncelle
            foreach (DataRow row in dt.Rows)
            {
                row["EkskOgrBel"] = EvrakDonusum(row["EkskOgrBel"]);
                row["EkskSaglik"] = EvrakDonusum(row["EkskSaglik"]);
                row["EkskSavcilik"] = EvrakDonusum(row["EkskSavcilik"]);
                row["EkskSozlesme"] = EvrakDonusum(row["EkskSozlesme"]);
                row["EkskImza"] = EvrakDonusum(row["EkskImza"]);
            }

            var viewTable = dt.DefaultView.ToTable(false,
                "ID_KURSIYER", "ADI", "SOYADI", "TC_NO", "SERTIFIKA_SINIFI", "DONEM_ADI",
                "EkskOgrBel", "EkskSaglik", "EkskSavcilik", "EkskSozlesme", "EkskImza"
            );

            Dtg_Donemlerlistele.DataSource = viewTable;

            if (Dtg_Donemlerlistele.Columns.Contains("ID_KURSIYER"))
                Dtg_Donemlerlistele.Columns["ID_KURSIYER"].Visible = false;
        }

        private async void Dtg_Donemlerlistele_SelectionChanged(object sender, EventArgs e)
        {
            if (Dtg_Donemlerlistele.SelectedRows.Count == 0) return;

            DataGridViewRow row = Dtg_Donemlerlistele.SelectedRows[0];
            int idKursiyer = Convert.ToInt32(row.Cells["ID_KURSIYER"].Value);

            Mebbis_Model evrak = await _mebbisService.GetEvrakByKursiyerIdAsync(idKursiyer);
            if (evrak == null) return;

            // Checkboxları doldur (boolean)
           

            // TextBoxları doldur
            Tnk_ADI.Text = evrak.ADI;
            Tnk_SOYADI.Text = evrak.SOYADI;
            Tnk_ADAY_NO.Text = evrak.ADAY_NO;
            Cmb_SINIFI.Text = evrak.SERTIFIKA_SINIFI;

            // Kursiyer resmi yükle
            if (evrak.Foto != null)
            {
                Tnk_RESIM_Kursiyer.Image?.Dispose();
                using (MemoryStream ms = new MemoryStream(evrak.Foto))
                {
                    Tnk_RESIM_Kursiyer.Image = Image.FromStream(ms);
                }
            }
            else
            {
                Tnk_RESIM_Kursiyer.Image = null;
            }
        }

       
          
           private async void Btn_Resim_Kayit_Click(object sender, EventArgs e)
        {
            // Veritabanından kullanıcı adı ve şifreyi çek
            var (kullaniciAdi, sifre) = await _mebbisService.GetMebbisLoginAsync();

            // MEBBİS giriş sayfasını aç
            Web_Mebbis.Navigate("https://mebbis.meb.gov.tr");

            Web_Mebbis.DocumentCompleted += (s, ev) =>
            {
                if (Web_Mebbis.Document != null)
                {
                    var doc = Web_Mebbis.Document;

                    // Kullanıcı adı inputu
                    var txtKullanici = doc.GetElementById("txtKullaniciAd");
                    if (txtKullanici != null)
                        txtKullanici.SetAttribute("value", kullaniciAdi);

                    // Şifre inputu
                    var txtSifre = doc.GetElementById("txtSifre");
                    if (txtSifre != null)
                        txtSifre.SetAttribute("value", sifre);

                    // İsteğe bağlı: Giriş butonuna otomatik tıklamak
                    // var btnGiris = doc.GetElementById("btnGiris");
                    // btnGiris?.InvokeMember("click");
                }
            };
        }
    }
    }
    

