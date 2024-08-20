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
            return new RacunGlava
            {
                RacunGlavaID = reader.GetInt32("RacunGlavaID"),
                StevilkaRacuna = reader.GetString("StevilkaRacuna"),
                Kraj = reader.GetString("Kraj"),
                Datum = reader.GetDateTime(reader.GetOrdinal("Datum")).ToString("dd.MM.yyyy"),
                DatumOpravljeno = reader.GetDateTime(reader.GetOrdinal("DatumOpravljeno")).ToString("dd.MM.yyyy"),
                Datumzapade = reader.GetDateTime(reader.GetOrdinal("Datumzapade")).ToString("dd.MM.yyyy"),
                StrankaID = reader.GetInt32("StrankaID"),
                NazivPodjetja = reader.IsDBNull(reader.GetOrdinal("NazivPodjetja")) ?
                                    reader.GetString("ImeInPriimek") :
                                    reader.GetString("NazivPodjetja"),
                SkupnaCena = reader.GetDecimal("SkupnaCena").ToString("N2") + " €",
            };
        }
    }
}
