using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tarantula_MTSK.Services;

namespace Tarantula_MTSK.Sayfalar
{
    public partial class Form_Donem_Ekle : Form
    {
        private readonly IDonemService _donemService;
        private int _id = 0; // Güncelleme için ID

        public Form_Donem_Ekle(string connectionString)
        {
            InitializeComponent();
            _donemService = new DonemService(connectionString);
        }

        private async void Form_Donem_Ekle_Load(object sender, EventArgs e)
        {
            // Yıl ComboBox
            for (int yil = DateTime.Now.Year - 5; yil <= DateTime.Now.Year + 1; yil++)
                Cmb_Donemyili.Items.Add(yil);

            // Ay ComboBox (isim olarak)
            for (int ay = 1; ay <= 12; ay++)
                Cmb_Donemay.Items.Add(AyToString(ay));

            // Şube ve Grup ComboBox
            var grupKartlari = await _donemService.GetGrupKartlariAsync();
            if (grupKartlari.Rows.Count > 0)
            {
                var subeler = grupKartlari.AsEnumerable()
                                           .Select(r => r.Field<string>("DONEM_SUBESI"))
                                           .Where(s => !string.IsNullOrEmpty(s))
                                           .Distinct()
                                           .ToArray();

                var gruplar = grupKartlari.AsEnumerable()
                                           .Select(r => r.Field<string>("DONEM_GRUBU"))
                                           .Where(g => !string.IsNullOrEmpty(g))
                                           .Distinct()
                                           .ToArray();

                Cmb_Subesi.Items.Clear();
                Cmb_Grubu.Items.Clear();

                Cmb_Subesi.Items.AddRange(subeler);
                Cmb_Grubu.Items.AddRange(gruplar);
            }

            // Varsayılan seçimler
            SetComboBoxValue(Cmb_Donemyili, DateTime.Now.Year.ToString());
            SetComboBoxValue(Cmb_Donemay, AyToString(DateTime.Now.Month));
            if (Cmb_Subesi.Items.Count > 0) Cmb_Subesi.SelectedIndex = 0;
            if (Cmb_Grubu.Items.Count > 0) Cmb_Grubu.SelectedIndex = 0;
        }

        private void SetComboBoxValue(ComboBox cmb, string value)
        {
            if (!cmb.Items.Contains(value))
                cmb.Items.Add(value);
            cmb.SelectedItem = value;
        }

        // Güncelleme için formu doldur
        public void LoadForUpdate(int id, string yil, string ay, string sube, string donemAdi, string grup, DateTime baslangic, DateTime bitis)
        {
            _id = id;
            SetComboBoxValue(Cmb_Donemyili, yil);
            SetComboBoxValue(Cmb_Donemay, AyToString(int.Parse(ay)));
            SetComboBoxValue(Cmb_Subesi, sube);
            SetComboBoxValue(Cmb_Grubu, grup);

            Text_DonemAdi.Text = donemAdi;
            Data_Baslama.Value = baslangic;
            Date_Bitis.Value = bitis;
        }

        private async void Btn_Kaydet_Click(object sender, EventArgs e)
        {
            try
            {
                // Alan kontrolü
                if (string.IsNullOrWhiteSpace(Cmb_Subesi.Text))
                {
                    MessageBox.Show("Şube seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(Cmb_Grubu.Text))
                {
                    MessageBox.Show("Grup seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int yil = int.Parse(Cmb_Donemyili.Text);
                int ay = AyToNumber(Cmb_Donemay.Text); // ComboBox’tan rakam al
                string sube = Cmb_Subesi.Text;
                string grup = Cmb_Grubu.Text;
                string donemAdi = Text_DonemAdi.Text;
                DateTime baslangic = Data_Baslama.Value;
                DateTime bitis = Date_Bitis.Value;

                if (string.IsNullOrWhiteSpace(donemAdi))
                    donemAdi = $"{yil}-{AyToString(ay)}-{grup}-{sube}";

                if (_id == 0)
                {
                    // Yeni kayıt
                    int yeniId = await _donemService.AddGrupAsync(yil, ay, sube, donemAdi, grup, baslangic, bitis);
                    MessageBox.Show("Yeni dönem kaydedildi. ID: " + yeniId, "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Güncelleme
                    await _donemService.UpdateGrupAsync(_id, donemAdi, grup, baslangic, bitis);
                    MessageBox.Show("Dönem güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Ay numarasını string’e çevir
        private string AyToString(int ay)
        {
            switch (ay)
            {
                case 1: return "OCAK";
                case 2: return "ŞUBAT";
                case 3: return "MART";
                case 4: return "NİSAN";
                case 5: return "MAYIS";
                case 6: return "HAZİRAN";
                case 7: return "TEMMUZ";
                case 8: return "AĞUSTOS";
                case 9: return "EYLÜL";
                case 10: return "EKİM";
                case 11: return "KASIM";
                case 12: return "ARALIK";
                default: return ay.ToString();
            }
        }

        // Ay ismini rakama çevir
        private int AyToNumber(string ay)
        {
            switch (ay.ToUpper())
            {
                case "OCAK": return 1;
                case "ŞUBAT": return 2;
                case "MART": return 3;
                case "NİSAN": return 4;
                case "MAYIS": return 5;
                case "HAZİRAN": return 6;
                case "TEMMUZ": return 7;
                case "AĞUSTOS": return 8;
                case "EYLÜL": return 9;
                case "EKİM": return 10;
                case "KASIM": return 11;
                case "ARALIK": return 12;
                default: return 0;
            }
        }
    }
}
