using System;
using System.Windows.Forms;
using Tarantula_MTSK.Models;

namespace Tarantula_MTSK.Sayfalar
{
    public partial class Form_RaporGoruntule : Form
    {
        private readonly Kursiyer_Model _kursiyer;

        public Form_RaporGoruntule(Kursiyer_Model kursiyer)
        {
            InitializeComponent();
            _kursiyer = kursiyer ?? throw new ArgumentNullException(nameof(kursiyer));
            this.Shown += Form_RaporGoruntule_Shown;
        }

        private void Form_RaporGoruntule_Shown(object sender, EventArgs e)
        {
            try
            {
                // Resmi base64 olarak ekle
                string resimHtml = "Yok";
                if (_kursiyer.RESIM != null && _kursiyer.RESIM.Length > 0)
                {
                    string resimBase64 = Convert.ToBase64String(_kursiyer.RESIM);
                    resimHtml = $"<img src='data:image/jpeg;base64,{resimBase64}' style='width:83px;height:108px;' />";
                }

                string html = $@"
<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html>
<head>
<meta http-equiv='Content-Type' content='text/html; charset=utf-8'>
<style type='text/css'>
/* CSS sınıfları buraya yapıştırılabilir */
.cs531D72F0 {{ font-family:Arial; font-size:13px; }}
.cs19EE5173 {{ font-family:Arial; font-size:13px; }}
.csD87980A5 {{ font-family:Arial; font-size:13px; text-align:center; }}
</style>
</head>
<body leftMargin=10 topMargin=10 rightMargin=10 bottomMargin=10 style='background-color:#FFFFFF'>
<table cellpadding='0' cellspacing='0' border='0' style='width:741px;'>

<tr>
    <td class='cs531D72F0' colspan='5'>Adı Soyadı</td>
    <td class='cs19EE5173' colspan='4'>: {_kursiyer.ADI} {_kursiyer.SOYADI}</td>
</tr>
<tr>
    <td class='cs531D72F0' colspan='5'>T.C. Kimlik No</td>
    <td class='cs19EE5173' colspan='9'>: {_kursiyer.TC_NO}</td>
</tr>
<tr>
    <td class='cs531D72F0' colspan='5'>Baba Adı</td>
    <td class='cs19EE5173' colspan='4'>: {_kursiyer.KIM_BABA_ADI}</td>
</tr>
<tr>
    <td class='cs531D72F0' colspan='5'>Ana Adı</td>
    <td class='cs19EE5173' colspan='4'>: {_kursiyer.KIM_ANA_ADI}</td>
</tr>
<tr>
    <td class='cs531D72F0' colspan='5'>Doğum Tarihi</td>
    <td class='cs19EE5173' colspan='4'>: {_kursiyer.DOGUM_TARIHI:dd.MM.yyyy}</td>
</tr>
<tr>
    <td class='cs531D72F0' colspan='5'>Adres</td>
    <td class='cs19EE5173' colspan='25'>: {_kursiyer.EV_ADRESI}</td>
</tr>
<tr>
    <td class='cs531D72F0' colspan='5'>Telefon / GSM</td>
    <td class='cs19EE5173' colspan='25'>: {_kursiyer.GSM_1}</td>
</tr>
<tr>
    <td class='cs531D72F0' colspan='5'>Resim</td>
    <td class='cs19EE5173' colspan='25'>{resimHtml}</td>
</tr>

<tr>
    <td class='csD87980A5' colspan='30' style='height:50px; text-align:center;'>
        Bu bölüm kursiyer beyanı ve yetkili onayı içindir.
    </td>
</tr>

</table>
</body>
</html>";

                // WebBrowser'da göster
                Web_Rapor_Goster.DocumentText = html;
                Web_Rapor_Goster.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Rapor görüntülenirken hata oluştu:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
