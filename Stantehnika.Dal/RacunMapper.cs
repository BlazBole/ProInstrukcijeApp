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
                RacunGlavaID = reader.GetInt32(reader.GetOrdinal("RacunGlavaID")),
                StevilkaRacuna = reader.GetString(reader.GetOrdinal("StevilkaRacuna")),
                Kraj = reader.GetString(reader.GetOrdinal("Kraj")),
                Datum = reader.GetDateTime(reader.GetOrdinal("Datum")),
                DatumOpravljeno = reader.GetDateTime(reader.GetOrdinal("DatumOpravljeno")),
                Datumzapade = reader.GetDateTime(reader.GetOrdinal("Datumzapade")),
                StrankaID = reader.GetInt32(reader.GetOrdinal("StrankaID")),
                NazivPodjetja = reader.IsDBNull(reader.GetOrdinal("NazivPodjetja")) ?
                        $"{reader.GetString(reader.GetOrdinal("ImeInPriimek"))}" :
                        reader.GetString(reader.GetOrdinal("NazivPodjetja"))
            };
        }
    }
}
