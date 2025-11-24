using System;

namespace Tarantula_MTSK.Models
{
    public class Kurs_Ayar_Model
    {
        // PARAM_KURSBILGILERI
        public string KURS_ADI { get; set; }
        public string KURS_ADI_KISA { get; set; }
        public string IL_KODU { get; set; }
        public string ILCE_KODU { get; set; }
        public string TELEFON { get; set; }
        public string ADRES { get; set; }
        public string KURUCU_ADI { get; set; }
        public string MUDUR_ADI { get; set; }
        public string MUDUR_YRD_ADI { get; set; }
        public DateTime? KURS_IZIN_TARIHI { get; set; }

        // PARAM_GENEL_PARAMETRELER
        public string MEBBIS_KUL_ADI_1 { get; set; }
        public string MEBBIS_KUL_SIF_1 { get; set; }
        public string MEBBIS_KUL_ADI_2 { get; set; }
        public string MEBBIS_KUL_SIF_2 { get; set; }
        public string MEBBIS_KUL_ADI_3 { get; set; }
        public string MEBBIS_KUL_SIF_3 { get; set; }
        public string MEBBIS_KUL_ADI_4 { get; set; }
        public string MEBBIS_KUL_SIF_4 { get; set; }
        public string MEBBIS_KUL_ADI_5 { get; set; }
        public string MEBBIS_KUL_SIF_5 { get; set; }

        // LINKLER
        public string[] LINKLER { get; set; }
    }
}
