using System;

namespace Tarantula_MTSK.Models
{
    public class Arac_Model
    {
       
            public int ID { get; set; }
            public string ARAC_TIPI { get; set; }
            public string ARAC_PLAKA { get; set; }
            public string DURUMU { get; set; }
            public string RENGI { get; set; }
            public string VITES_TURU { get; set; }
            public string MODEL { get; set; }
            public DateTime? MUHAYENE_TAR { get; set; }
            public DateTime? SIGORTA_BAS_TAR { get; set; }
            public int? AKT { get; set; } // <- nullable
        }
    }
