using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stantehnika.Model
{
    public class RacunPostavka
    {
        public int RacunPostavkaID { get; set; }
        public int StevilkaPostavke { get; set; }
        public string Storitev { get; set; }
        public decimal Kolicina { get; set; }
        public string EnotaMerjenja { get; set; }
        public decimal CenaEneKolicine { get; set; }
        public decimal CenaPostavke { get; set; }
        public int RacunGlavaID { get; set; } // Tuji kljuc

        public RacunGlava RacunGlava { get; set; } // Navigacijska lastnost
    }
}
