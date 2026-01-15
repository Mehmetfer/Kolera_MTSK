using System;

public class Lisans
{
    public string LicenseKey { get; set; }   // LSN_LISANS_NO
    public DateTime ValidUntil { get; set; } // LSN_BITIS_TARIHI
    public string Owner { get; set; }        // LSN_KURUM_KODU
    public string Product { get; set; }
    public string Version { get; set; }
    public string Status { get; set; }
}