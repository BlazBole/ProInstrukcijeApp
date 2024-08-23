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
            decimal skupnaCena = reader.GetDecimal("SkupnaCena");
            decimal cenaMaterial = reader.IsDBNull(reader.GetOrdinal("CenaMaterial"))
                                   ? 0.00m
                                   : reader.GetDecimal("CenaMaterial");

            return new RacunGlava
            {
                RacunGlavaID = reader.GetInt32("RacunGlavaID"),
                StevilkaRacuna = reader.GetString("StevilkaRacuna"),
                Kraj = reader.GetString("Kraj"),
                Datum = reader.GetDateTime(reader.GetOrdinal("Datum")).ToString("dd.MM.yyyy"),
                DatumOpravljeno = reader.GetDateTime(reader.GetOrdinal("DatumOpravljeno")).ToString("dd.MM.yyyy"),
                Datumzapade = reader.GetDateTime(reader.GetOrdinal("Datumzapade")).ToString("dd.MM.yyyy"),
                StrankaID = reader.GetInt32("StrankaID"),
                NazivPodjetja = reader.GetString("Stranka"),
                SkupnaCena = skupnaCena.ToString("N2") + " €",
                CenaMaterial = cenaMaterial.ToString("N2") + " €",
                CenaDelo = (skupnaCena - cenaMaterial).ToString("N2") + " €"
            };
        }
    }
}
