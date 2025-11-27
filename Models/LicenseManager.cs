using System;
using Tarantula_MTSK.Models;

public static class LicenseManager
{
    public static bool IsLicensed { get; private set; }
    public static DateTime ExpireDate { get; private set; }
    public static Lisans_Model LicenseInfo { get; private set; }

    public static void CheckLicense(Lisans_Model model)
    {
        LicenseInfo = model;

        if (model == null || string.IsNullOrEmpty(model.LisansNo))
        {
            IsLicensed = false;
            return;
        }

        if (!DateTime.TryParse(model.BitisTarihi, out DateTime bitis))
        {
            IsLicensed = false;
            return;
        }

        ExpireDate = bitis;

        IsLicensed = bitis >= DateTime.Now;
    }
}
