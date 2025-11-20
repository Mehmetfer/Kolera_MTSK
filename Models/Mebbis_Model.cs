using System;

namespace Tarantula_MTSK.Models
{
    public class Mebbis_Model
    {
        // ---------------------------
        // Evrak kontrol bilgileri
        // ---------------------------
        public bool EkskOgrBel { get; set; }
        public bool EkskSaglik { get; set; }
        public bool EkskSavcilik { get; set; }
        public bool EkskSozlesme { get; set; }
        public bool EkskImza { get; set; }

        // ---------------------------
        // Kursiyer temel bilgileri
        // ---------------------------
        public int Id_Kursiyer { get; set; }
        public string ADI { get; set; }
        public string SOYADI { get; set; }
        public string TC_NO { get; set; }
        public string ADAY_NO { get; set; }
        public string SERTIFIKA_SINIFI { get; set; }
        public string DONEMI { get; set; }
        public byte[] RESIM { get; set; }
        public byte[] Foto { get; set; }
        // ---------------------------
        // Evrak resimleri
        // ---------------------------
        public byte[] ImgOgrBel { get; set; }
        public byte[] ImgSaglik { get; set; }
        public byte[] ImgSavcilik { get; set; }
        public byte[] ImgSozlesme { get; set; }
        public byte[] ImgImza { get; set; }

        // ---------------------------
        // Evrak detay bilgileri
        // ---------------------------
        public DateTime? OgrBelgeTarihi { get; set; }
        public string OgrBelgeNo { get; set; }
        public string OgrBelgeTuru { get; set; }
        public string OgrBelgeVerenKurum { get; set; }
        public string OgrBelgeSayisi { get; set; }

        public string SaglikBelverenKurum { get; set; }
        public string SaglikBelgeNo { get; set; }
        public string SaglikBelgeSayisi { get; set; }
        public DateTime? SaglikBelgeTarihi { get; set; }
        public string SaglikBelgeVerenKurum { get; set; }
        public string SaglikBelReferans { get; set; }

        public string SavcilikBelgeVerenKurum { get; set; }
        public string SavcilikBelgeNo { get; set; }
        public DateTime? SavcilikBelgeTarihi { get; set; }

        public DateTime? SozlesmeTarihi { get; set; }

        // ---------------------------
        // Ekstra resimler / belgeler
        // ---------------------------
        public byte[] ImgMTSKSertifika { get; set; }
        public byte[] ImgMuracaat { get; set; }
        public byte[] ImgSozlesme_On { get; set; }
        public byte[] ImgSozlesme_Arka { get; set; }
        public byte[] ImgDiger1 { get; set; }
        public byte[] ImgDiger2 { get; set; }
        public byte[] ImgDiger3 { get; set; }
        public byte[] ImgFatura { get; set; }

        // ---------------------------
        // Zaman bilgisi
        // ---------------------------
        public DateTime Tarih { get; set; } = DateTime.MinValue;
    }
}
