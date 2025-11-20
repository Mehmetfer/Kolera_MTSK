using System.Xml.Serialization;

namespace Tarantula_MTSK.Models
{
    public class ServerAyar
    {
        public string Sunucu { get; set; }
        public string BaglantiTuru { get; set; }   // "Windows" veya "SQL"
        public string KullaniciAdi { get; set; }
        public string Parola { get; set; }
        public string VeritabaniAdi { get; set; }

        [XmlIgnore]
        public string ConnectionString { get; set; }
    }
}
