using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Tarantula_MTSK.Helpers
{
    public static class FormLisansHelper
    {
        public static void SetLisansLabel(Label label)
        {
            try
            {
                if (!LicenseManager.IsLicensed)
                {
                    label.Text = "DEMO SÜRÜM – Lisans süresi dolmuş";
                    label.ForeColor = Color.Red;
                    return;
                }

                label.Text =
                    $"Lisans No: {LicenseManager.LicenseInfo?.LisansNo} — Bitiş: {LicenseManager.ExpireDate:dd.MM.yyyy}";
                label.ForeColor = Color.LimeGreen;
            }
            catch
            {
                label.Text = "Lisans bilgisi okunamadı!";
                label.ForeColor = Color.Red;
            }
        }
    }
}
