using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stantehnika.Model
{
    public class StrankaFizicnaOseba : Stranka
    {
        public string Ime { get; set; }
        public string Priimek { get; set; }
        public string UlicaInHisnaStevilka { get; set; }
        public string PostaInKraj { get; set; }
    }
}
