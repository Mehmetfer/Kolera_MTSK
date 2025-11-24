using Microsoft.Web.WebView2.WinForms;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tarantula_MTSK.Models;
using Tarantula_MTSK.Services;

namespace Tarantula_MTSK.Sayfalar
{
    public partial class Form_Mebbis_Aktarim : Form
    {
        private readonly MebbisService _mebbisService;
        private readonly Mebbis_Model _kursiyer;

        const int OFFSET = 10;
        const int MENU_COUNT = 7;

        private readonly Button[] menuButtons;
        private readonly Panel[] menuPanels;

        public Form_Mebbis_Aktarim(Mebbis_Model kursiyer, string connectionString)
        {
            InitializeComponent();

            _kursiyer = kursiyer ?? throw new ArgumentNullException(nameof(kursiyer));
            _mebbisService = new MebbisService(connectionString ?? throw new ArgumentNullException(nameof(connectionString)));

            // Kullanıcı bilgilerini ayarla
            Lbl_ADI.Text = _kursiyer.ADI;
            Lbl_SOYADI.Text = _kursiyer.SOYADI;
            if (_kursiyer.Foto != null)
            {
                using (MemoryStream ms = new MemoryStream(_kursiyer.Foto))
                {
                    Tnk_RESIM_Kursiyer.Image = Image.FromStream(ms);
                }
            }

            Grp_Mebbis.Visible = true;

            // Menü button ve panel dizileri
            menuButtons = new Button[] { B1, B2, B3, B4, B5, B6, B7 };
            menuPanels = new Panel[] { P1, P2, P3, P4, P5, P6, P7 };
            InitializeExpandMenu();

            for (int i = 0; i < MENU_COUNT; i++)
            {
                int index = i;
                menuButtons[i].Click += (s, e) => ExpandMenu(index);
            }

            // Menü label clickleri
            Lbl_Menu1.Click += Lbl_Menu1_Click;
            Lbl_Menu2.Click += (s, e) => MenuClicked(2);
            Lbl_Menu3.Click += (s, e) => MenuClicked(3);
            Lbl_Menu4.Click += (s, e) => MenuClicked(4);
            Lbl_Menu5.Click += (s, e) => MenuClicked(5);
            //Lbl_Menu6.Click += (s, e) => MenuClicked(6);
            Lbl_Menu7.Click += (s, e) => MenuClicked(7);

            InitializeWebViewAsync();
        }

        private void ExpandMenu(int index)
        {
            bool isClosed = (int)menuButtons[index].Tag == 0;

            if (isClosed)
            {
                menuButtons[index].Tag = 1;
                menuButtons[index].Text = "▼ " + menuButtons[index].Text.Substring(2);
                menuPanels[index].Visible = true;
            }
            else
            {
                menuButtons[index].Tag = 0;
                menuButtons[index].Text = "▶ " + menuButtons[index].Text.Substring(2);
                menuPanels[index].Visible = false;
            }

            RepositionMenu();
        }

        private void RepositionMenu()
        {
            for (int i = 0; i < MENU_COUNT; i++)
            {
                if (i == 0)
                    menuButtons[i].Top = OFFSET;
                else
                    menuButtons[i].Top = GetAfterPosition(i - 1);

                menuPanels[i].Top = menuButtons[i].Bottom;
            }
        }

        private int GetAfterPosition(int index)
        {
            int pos = menuButtons[index].Bottom;
            if ((int)menuButtons[index].Tag == 1)
                pos = menuPanels[index].Bottom;
            return pos + OFFSET;
        }

        private void InitializeExpandMenu()
        {
            for (int i = 0; i < MENU_COUNT; i++)
            {
                menuButtons[i].Tag = 0;
                menuButtons[i].Text = "▶ " + menuButtons[i].Text;
                if (i == 0)
                    menuButtons[i].Top = OFFSET;
                else
                    menuButtons[i].Top = menuButtons[i - 1].Bottom + OFFSET;

                menuPanels[i].Visible = false;
                menuPanels[i].Top = menuButtons[i].Bottom;
                menuPanels[i].Width = menuButtons[i].Width;
            }
        }

        private async void InitializeWebViewAsync()
        {
            try
            {
                await Web_Mebbis.EnsureCoreWebView2Async();

                string mebbisUrl = "https://mebbis.meb.gov.tr/";
                try
                {
                    var linkModel = await _mebbisService.GetLinkByIdAsync(23);
                    if (linkModel != null && !string.IsNullOrWhiteSpace(linkModel.URL))
                        if (Uri.IsWellFormedUriString(linkModel.URL.Trim(), UriKind.Absolute))
                            mebbisUrl = linkModel.URL.Trim();
                }
                catch { }

                Txt_Link.Text = mebbisUrl;
                Web_Mebbis.CoreWebView2.Navigate(mebbisUrl);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Web tarayıcı başlatılamadı: " + ex.Message, "HATA",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void Btn_Giris_Click(object sender, EventArgs e)
        {
            if (Web_Mebbis?.CoreWebView2 == null)
            {
                MessageBox.Show("Web tarayıcı henüz hazır değil.", "HATA",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var login = await _mebbisService.GetMebbisLoginAsync();
                string kulAdi = login.Item1;
                string sifre = login.Item2;

                if (string.IsNullOrWhiteSpace(kulAdi) || string.IsNullOrWhiteSpace(sifre))
                {
                    MessageBox.Show("Kullanıcı adı veya şifre bulunamadı!", "HATA",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string script = $@"
                    if(document.getElementById('txtKullaniciAd')) {{
                        document.getElementById('txtKullaniciAd').value = '{kulAdi}';
                    }}
                    if(document.getElementById('txtSifre')) {{
                        document.getElementById('txtSifre').value = '{sifre}';
                    }}
                    if(document.getElementById('btnGiris')) {{
                        document.getElementById('btnGiris').click();
                    }}
                ";

                await Web_Mebbis.ExecuteScriptAsync(script);
                Grp_Mebbis.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Giriş hatası: " + ex.Message, "HATA",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void Lbl_Menu1_Click(object sender, EventArgs e)
        {
            if (Web_Mebbis?.CoreWebView2 == null)
            {
                MessageBox.Show("Web tarayıcı henüz hazır değil!", "HATA",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Özel MTSK Modülü butonunu tıklamak için retry mekanizması
                string mtskScript = @"
            function clickButtonByText(text) {
                let btn = Array.from(document.querySelectorAll('button, a')).find(x => x.innerText.includes(text));
                if(btn){ btn.click(); return true; }
                return false;
            }
            clickButtonByText('Özel MTSK Modülü');
        ";

                bool clicked = false;
                int retries = 0;
                while (!clicked && retries < 10)
                {
                    var result = await Web_Mebbis.ExecuteScriptAsync(mtskScript);
                    // ExecuteScriptAsync true/false döndürür string olarak
                    clicked = result != null && result.Contains("true");
                    if (!clicked)
                    {
                        await Task.Delay(500); // yarım saniye bekle
                        retries++;
                    }
                }

                if (!clicked)
                {
                    MessageBox.Show("Özel MTSK Modülü butonu bulunamadı veya tıklanamadı!", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Sayfanın yüklenmesini bekle
                await Task.Delay(1000);

                // Aday Kayıt İşlemleri butonuna tıklama
                string adayScript = @"
            function clickButtonByText(text) {
                let btn = Array.from(document.querySelectorAll('button, a')).find(x => x.innerText.includes(text));
                if(btn){ btn.click(); return true; }
                return false;
            }
            clickButtonByText('Aday Kayıt İşlemleri');
        ";

                clicked = false;
                retries = 0;
                while (!clicked && retries < 10)
                {
                    var result2 = await Web_Mebbis.ExecuteScriptAsync(adayScript);
                    clicked = result2 != null && result2.Contains("true");
                    if (!clicked)
                    {
                        await Task.Delay(500);
                        retries++;
                    }
                }

                if (!clicked)
                {
                    MessageBox.Show("Aday Kayıt İşlemleri butonu bulunamadı veya tıklanamadı!", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Menü tıklama hatası: " + ex.Message, "HATA",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void MenuClicked(int menuId)
        {
            if (Web_Mebbis?.CoreWebView2 == null)
            {
                MessageBox.Show("Web tarayıcı henüz hazır değil!", "HATA",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                await Web_Mebbis.ExecuteScriptAsync($"alert('Menu {menuId} tıklandı');");
            }
            catch { }
        }
    }
}
