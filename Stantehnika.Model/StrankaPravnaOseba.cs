using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stantehnika.Model
{
    public class StrankaPravnaOseba : Stranka
    {
        public string NazivPodjetja { get; set; }
        public string DavcnaStevilka { get; set; }
        public string SedezPodjetja { get; set; }
    }
}
