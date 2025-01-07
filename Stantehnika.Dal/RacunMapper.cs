using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stantehnika.Model;
using MySql.Data.MySqlClient;

namespace Stantehnika.Dal
{
    public class RacunMapper
    {
        public static RacunGlava MapToRacunGlava(MySqlDataReader reader)
        {
            string kraj = reader.IsDBNull(reader.GetOrdinal("Kraj")) ? string.Empty : reader.GetString("Kraj");
            decimal skupnaCena = reader.IsDBNull(reader.GetOrdinal("SkupnaCena")) ? 0.00m : reader.GetDecimal("SkupnaCena");
            decimal cenaMaterial = reader.IsDBNull(reader.GetOrdinal("CenaMaterial")) ? 0.00m : reader.GetDecimal("CenaMaterial");

            return new RacunGlava
            {
                RacunGlavaID = reader.GetInt32("RacunGlavaID"),
                StevilkaRacuna = reader.GetString("StevilkaRacuna"),
                Kraj = kraj,
                Datum = reader.IsDBNull(reader.GetOrdinal("Datum"))
                        ? string.Empty
                        : reader.GetDateTime(reader.GetOrdinal("Datum")).ToString("dd.MM.yyyy"),
                DatumOpravljeno = reader.IsDBNull(reader.GetOrdinal("DatumOpravljeno"))
                                  ? string.Empty
                                  : reader.GetDateTime(reader.GetOrdinal("DatumOpravljeno")).ToString("dd.MM.yyyy"),
                Datumzapade = reader.IsDBNull(reader.GetOrdinal("Datumzapade"))
                              ? string.Empty
                              : reader.GetDateTime(reader.GetOrdinal("Datumzapade")).ToString("dd.MM.yyyy"),
                StrankaID = reader.GetInt32("StrankaID"),
                NazivPodjetja = reader.IsDBNull(reader.GetOrdinal("Stranka"))
                                ? string.Empty
                                : reader.GetString("Stranka"),
                SkupnaCena = skupnaCena.ToString("N2") + " €",
                CenaMaterial = cenaMaterial.ToString("N2") + " €",
                CenaDelo = (skupnaCena - cenaMaterial).ToString("N2") + " €"
            };
        }


        public static Stranka MapToRacunStranka(MySqlDataReader reader)
        {
            var stranka = new Stranka
            {
                StrankaID = reader.GetInt32("StrankaID"),
                ImeInPriimek = reader.IsDBNull(reader.GetOrdinal("Stranka"))
                              ? null
                              : reader.GetString("Stranka"),
                UlicaInHisnaStevilka = reader.IsDBNull(reader.GetOrdinal("UlicaInHisnaStevilka"))
                                        ? null
                                        : reader.GetString("UlicaInHisnaStevilka"),
                PostaInKraj = reader.IsDBNull(reader.GetOrdinal("PostaInKraj"))
                              ? null
                              : reader.GetString("PostaInKraj"),
                NazivPodjetja = reader.IsDBNull(reader.GetOrdinal("Stranka"))
                                ? null
                                : reader.GetString("Stranka"),
                SedezPodjetja = reader.IsDBNull(reader.GetOrdinal("SedezPodjetja"))
                                 ? null
                                 : reader.GetString("SedezPodjetja"),
                Email = reader.IsDBNull(reader.GetOrdinal("Email"))
                         ? null
                         : reader.GetString("Email"),
                DavcnaStevilka = reader.IsDBNull(reader.GetOrdinal("DavcnaStevilka"))
                                  ? null
                                  : reader.GetString("DavcnaStevilka")
            };

            if (stranka.SedezPodjetja != null)
            {
                stranka.Naslov = stranka.SedezPodjetja;
            }
            else if (stranka.UlicaInHisnaStevilka != null && stranka.PostaInKraj != null)
            {
                stranka.Naslov = $"{stranka.UlicaInHisnaStevilka}, {stranka.PostaInKraj}";
            }
            else
            {
                stranka.Naslov = null;
            }

            return stranka;
        }
    }
}
