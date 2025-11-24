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
        public byte[] ImgSozlesme_On { get; set; }
        public byte[] ImgSozlesme_Arka { get; set; }
        public byte[] ImgFatura { get; set; }

        // ---------------------------
        // Evrak detay bilgileri
        // ---------------------------
        public string OgrBelTuru { get; set; }
        public string OgrBelgeVerenKurum { get; set; }
        public DateTime? OgrBelgeTarihi { get; set; }
        public string OgrBelgeNo { get; set; }
        public string OgrBelgeSayisi { get; set; }

        public string SaglikRaporVerenKurum { get; set; }
        public DateTime? SaglikRaporTarihi { get; set; }
        public string SaglikRaporNo { get; set; }
        public string SaglikRaporReferans { get; set; }

        public string SavcilikBelgeVerenKurum { get; set; }  // eski "SavcilikBelVerVerenKurum"
        public DateTime? SavcilikBelgeTarihi { get; set; }   // eski "SavcilikBelTarihi"
        public string SavcilikBelgeNo { get; set; }          // eski "SavcilikBelNo"

        public string OzurDurumu { get; set; }
        public string YabanciDil { get; set; }

        public string FaturaNo { get; set; }
        public DateTime? FaturaTarihi { get; set; }
        public decimal? FaturaTutari { get; set; }

        // ---------------------------
        // Ekstra resimler / belgeler
        // ---------------------------
        public byte[] ImgMTSKSertifika { get; set; }
        public byte[] ImgMuracaat { get; set; }
        public byte[] ImgDiger1 { get; set; }
        public byte[] ImgDiger2 { get; set; }
        public byte[] ImgDiger3 { get; set; }
        public byte[] ImgSozlesmeOn { get; set; }
        public byte[] ImgSozlesmeArka { get; set; }
        // Web linki
        // ---------------------------
        public string URL { get; set; }
        

        // ---------------------------
        // Zaman bilgisi
        // ---------------------------
        public DateTime Tarih { get; set; } = DateTime.MinValue;
    }
}
 
 
    
