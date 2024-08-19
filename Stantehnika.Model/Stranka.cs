using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stantehnika.Model
{
    public class Stranka
    {
        public int StrankaID { get; set; }
        public string ImeInPriimek { get; set; }
        public string UlicaInHisnaStevilka { get; set; }
        public string PostaInKraj { get; set; }
        public string NazivPodjetja { get; set; }
        public string DavcnaStevilka { get; set; }
        public string SedezPodjetja { get; set; }
        public string Email { get; set; }
    }
}
