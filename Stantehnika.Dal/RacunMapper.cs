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
                Datum = reader.GetDateTime("Datum"),
                DatumOpravljeno = reader.GetDateTime("DatumOpravljeno"),
                Datumzapade = reader.GetDateTime("Datumzapade"),
                StrankaID = reader.GetInt32("StrankaID")
            };
        }
    }
}
