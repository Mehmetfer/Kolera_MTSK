using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarantula_MTSK.Models
{

    public class Lisans_Model
    {
        public string LisansNo { get; set; }
        public string BitisTarihi { get; set; }
        public int MakineSayisi { get; set; }
        public string GuidSfr { get; set; } // LisansService ile uyumlu
    
   
       
        public string GuidSifre { get; set; }
    }
}
