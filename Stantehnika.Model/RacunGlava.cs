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
        public DateTime Datum { get; set; }
        public DateTime DatumOpravljeno { get; set; }
        public DateTime Datumzapade { get; set; }
        public int StrankaID { get; set; } // Tuji kljuc

        public Stranka Stranka { get; set; } // Navigacijska lastnost
        public string NazivPodjetja { get; set; }
        public string ImeInPriimek { get; set; }
    }
}
