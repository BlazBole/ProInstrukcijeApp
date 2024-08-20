using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stantehnika.Model
{
    public class RacunGlava
    {
        public int RacunGlavaID { get; set; }
        public string StevilkaRacuna { get; set; }
        public string Kraj { get; set; }
        public string Datum { get; set; }
        public string DatumOpravljeno { get; set; }
        public string Datumzapade { get; set; }
        public int StrankaID { get; set; } // Tuji kljuc

        public Stranka Stranka { get; set; } // Navigacijska lastnost
        public string NazivPodjetja { get; set; }
        public string ImeInPriimek { get; set; }
        public string SkupnaCena { get; set; }
    }
}
