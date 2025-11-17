
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            await LoadDonemlerAsync();

           
        }

        private async Task LoadDonemlerAsync()
        {
            try
            {
                DataTable dtDonemler = await _mebbisService.GetDonemlerAsync();
                Combo_Donemler.DisplayMember = "DONEM_ADI";
                Combo_Donemler.ValueMember = "DONEM_ADI";
                Combo_Donemler.DataSource = dtDonemler;

                Combo_Donemler.SelectedIndexChanged += Combo_donemler_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dönemler yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private async void Combo_donemler_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Combo_Donemler.SelectedValue == null) return;

            string secilenDonem = Combo_Donemler.SelectedValue.ToString();

            try
            {
                int? grupId = await _mebbisService.GetGrupIdByDonemAsync(secilenDonem);
                if (grupId.HasValue)
                {
                    Dtg_Donemlerlistele.DataSource = await _mebbisService.GetKursiyerByGrupIdAsync(grupId.Value);
                }
                else
                {
                    Dtg_Donemlerlistele.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kursiyerler yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void Alt_Resim_Kayit_Click(object sender, EventArgs e)
        {
            
        }
    }
}