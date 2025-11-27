using System;
using System.Drawing;
using System.Windows.Forms;
using Tarantula_MTSK.Models;
using Tarantula_MTSK.Services;
using Tarantula_MTSK.Helpers;

namespace Tarantula_MTSK.Sayfalar
{
    public partial class Ana_Menu : Form
    {
        private Form currentChildForm = null;
        private readonly ServerAyar _serverAyar;
        private readonly AramaService _aramaServis;

        public Ana_Menu(ServerAyar serverAyar)
        {
            _serverAyar = serverAyar ?? throw new ArgumentNullException(nameof(serverAyar));

            InitializeComponent();
            this.IsMdiContainer = true;
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;

            // Lisans bilgisini label'a yaz
            FormLisansHelper.SetLisansLabel(Lbl_Lisans);

            // Arama butonunu lisans durumuna göre kilitle
            if (!LicenseManager.IsLicensed)
            {
                Mebbis_Button.Enabled = false; // ToolStripMenuItem için Enabled yeterli
                Arama_Button.ToolTipText = "Bu özellik lisans gerektiriyor.";
            }

            // AramaService başlat
            _aramaServis = new AramaService(_serverAyar.ConnectionString);

            // Menü item eventleri
            Anasayfa.Click += Anasayfa_Click;
            Kursiyer_Ekle.Click += Kursiyer_Ekle_Click;
            Arama_Button.Click += Arama_Button_Click;
            
        }

        public void OpenChildForm(Form childForm, string formName = null)
        {
            if (!string.IsNullOrEmpty(formName) && Application.OpenForms[formName] is Form mevcut)
            {
                mevcut.BringToFront();
                return;
            }

            currentChildForm?.Close();
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            this.Controls.Add(childForm);
            childForm.BringToFront();
            childForm.Show();
        }

        private void Anasayfa_Click(object sender, EventArgs e)
        {
            currentChildForm?.Close();
            currentChildForm = null;
        }

        private void Kursiyer_Ekle_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["Form_Kursiyer_ekle"] is Form mevcut)
            {
                mevcut.BringToFront();
                return;
            }

            var frmYeniKursiyer = new Form_Kursiyer_ekle(_serverAyar.ConnectionString);
            OpenChildForm(frmYeniKursiyer, "Form_Kursiyer_ekle");
        }

        private void Arama_Button_Click(object sender, EventArgs e)
        {
            if (!LicenseManager.IsLicensed) return;

            var frmArama = new Form_Arama(_serverAyar.ConnectionString);
            OpenChildForm(frmArama, "Form_Arama");
        }

        private void Pic_exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Programdan çıkmak istediğinizden emin misiniz?", "Çıkış Onayı",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void Mebbis_Button_Click(object sender, EventArgs e)
        {
            var frmMebbis = new Form_Mebbis(_serverAyar.ConnectionString);
            OpenChildForm(frmMebbis, "Form_Mebbis");
        }

        private void Donem_Grup_Click(object sender, EventArgs e)
        {
            var frmDonemler = new Form_Donemler(_serverAyar.ConnectionString);
            OpenChildForm(frmDonemler, "Form_Donemler");
        }

        private void Peronsel_Button_Click(object sender, EventArgs e)
        {
            var frmpersonel = new Form_Personel(_serverAyar);
            OpenChildForm(frmpersonel, "Form_Personel");
        }

        private void Ayarlar_Button_Click(object sender, EventArgs e)
        {
            var frmayar = new Form_Ayarlar(_serverAyar.ConnectionString);
            OpenChildForm(frmayar, "Form_Ayarlar");
        }

        private void Raporlar_Button_Click(object sender, EventArgs e)
        {
            var frmrapor = new Form_Raporlar(_serverAyar.ConnectionString);
            OpenChildForm(frmrapor, "Form_Raporlar");
        }

        private void Yedek_Button_Click(object sender, EventArgs e)
        {
            var frmyedek = new Form_Yedek(_serverAyar.ConnectionString);
            OpenChildForm(frmyedek, "Form_Yedek");
        }

        private void Yardim_Button_Click(object sender, EventArgs e)
        {
            var frmYardim = new Form_Yardim(_serverAyar.ConnectionString);
            OpenChildForm(frmYardim, "Form_Yardim");
        }

        private void Araclar_Button_Click(object sender, EventArgs e)
        {
            var frmAraclar = new Form_Araclar(_serverAyar);
            OpenChildForm(frmAraclar, "Form_Araclar");
        }

        private void Pic_exit_1_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
        "Programdan çıkmak istediğinizden emin misiniz?",
        "Çıkış Onayı",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question
    );

            if (result == DialogResult.Yes)
            {
                Application.Exit(); // Uygulamayı kapatır
            }
        }
    }
}
