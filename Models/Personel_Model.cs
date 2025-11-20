using System;

namespace Tarantula_MTSK.Models
{
    public class Personel_Model
    {
        public int ID { get; set; }
        public string PERSONEL_DURUMU { get; set; }
        public string TC_NO { get; set; }
        public string ADI { get; set; }
        public string GSM_1 { get; set; }
        public string SOYADI { get; set; }
        public string EHLIYET_SINIFI { get; set; }
        public string EHLIYET_IKINCI { get; set; }
        public string CINSIYET { get; set; }
        public string MEDENI_DUR { get; set; }
        public DateTime? DOGUM_TARIHI { get; set; }
        public string YONETICI_GOREVI { get; set; }
        public string VERDIGI_DERS_1 { get; set; }
        public DateTime? SOZ_BASLAMA_TAR { get; set; }
        public byte[] RESIM { get; set; }
    }
}
