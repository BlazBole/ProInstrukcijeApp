using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stantehnika.Model
{
    public class RacunMaterial
    {
        public int RacunMaterialID { get; set; }
        public string NazivMateriala { get; set; }
        public int RacunGlavaID { get; set; } // Tuji kljuc

        public RacunGlava RacunGlava { get; set; } // Navigacijska lastnost
    }
}
