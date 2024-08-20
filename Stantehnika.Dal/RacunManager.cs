using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Stantehnika.Model;

namespace Stantehnika.Dal
{
    public class RacunManager
    {
        #region private members
        private DatabaseConnection dbConnection;
        #endregion

        #region constructor
        public RacunManager()
        {
            dbConnection = new DatabaseConnection();
        }
        #endregion constructor

        #region methods
        public List<RacunGlava> GetAllRacuni()
        {
            List<RacunGlava> racuni = new List<RacunGlava>();

            using (var connection = dbConnection.GetConnection())
            {
                connection.Open();
                string query = @"SELECT rg.RacunGlavaID, rg.StevilkaRacuna, rg.Kraj, rg.Datum, rg.DatumOpravljeno, rg.DatumZapade, 
                                rg.StrankaID, s.NazivPodjetja, 
                                SUM(rp.CenaPostavke) AS SkupnaCena
                         FROM RacunGlava rg 
                         JOIN Stranka s ON rg.StrankaID = s.StrankaID
                         LEFT JOIN RacunPostavka rp ON rg.RacunGlavaID = rp.RacunGlavaID
                         GROUP BY rg.RacunGlavaID, rg.StevilkaRacuna, rg.Kraj, rg.Datum, rg.DatumOpravljeno, rg.DatumZapade, 
                                  rg.StrankaID, s.NazivPodjetja
                         ORDER BY rg.Datum DESC";

                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RacunGlava racun = RacunMapper.MapToRacunGlava(reader);
                        racuni.Add(racun);
                    }
                }
            }

            return racuni;
        }

        #endregion methods
    }
}
