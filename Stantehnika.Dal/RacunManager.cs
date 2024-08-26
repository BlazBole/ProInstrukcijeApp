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

            try
            {
                using (var connection = dbConnection.GetConnection())
                {
                    connection.Open();
                    string query = @"SELECT rg.RacunGlavaID, rg.StevilkaRacuna, rg.Kraj, rg.Datum, rg.DatumOpravljeno, rg.DatumZapade, 
                            rg.StrankaID, 
                            COALESCE(s.NazivPodjetja, s.ImeInPriimek) AS Stranka, 
                            SUM(rp.CenaPostavke) AS SkupnaCena,
                            (SELECT SUM(rp1.CenaPostavke) 
                            FROM RacunPostavka rp1 
                            WHERE rp1.RacunGlavaID = rg.RacunGlavaID 
                            AND rp1.Storitev = 'material') AS CenaMaterial
                                FROM RacunGlava rg
                                JOIN Stranka s ON rg.StrankaID = s.StrankaID
                                LEFT JOIN RacunPostavka rp ON rg.RacunGlavaID = rp.RacunGlavaID
                                GROUP BY rg.RacunGlavaID, rg.StevilkaRacuna, rg.Kraj, rg.Datum, rg.DatumOpravljeno, rg.DatumZapade, 
                                rg.StrankaID, s.NazivPodjetja, s.ImeInPriimek
                                ORDER BY rg.Datum DESC
                                ";

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
            }
            catch (Exception ex)
            {
                Console.WriteLine("Napaka pri pridobivanju računov: " + ex.Message);
            }

            return racuni;
        }


        public Dictionary<int, string> GetAllStranke()
        {
            Dictionary<int, string> stranke = new Dictionary<int, string>();


            using (var connection = dbConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT StrankaID, COALESCE(NazivPodjetja, ImeInPriimek) AS Stranka FROM Stranka";
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        stranke.Add(reader.GetInt32("StrankaID"), reader.GetString("Stranka"));
                    }
                }
            }

            return stranke;
        }


        public List<RacunGlava> GetRacuniPoStranki(int strankaID)
        {
            List<RacunGlava> racuni = new List<RacunGlava>();

            using (var connection = dbConnection.GetConnection())
            {
                connection.Open();
                string query = @"SELECT rg.RacunGlavaID, rg.StevilkaRacuna, rg.Kraj, rg.Datum, rg.DatumOpravljeno, rg.DatumZapade, 
                         rg.StrankaID, 
                         COALESCE(s.NazivPodjetja, s.ImeInPriimek) AS Stranka, 
                         SUM(rp.CenaPostavke) AS SkupnaCena,
                         SUM(CASE WHEN rp.Storitev = 'material' THEN rp.CenaPostavke ELSE 0 END) AS CenaMaterial
                         FROM RacunGlava rg
                         JOIN Stranka s ON rg.StrankaID = s.StrankaID
                         LEFT JOIN RacunPostavka rp ON rg.RacunGlavaID = rp.RacunGlavaID
                         WHERE rg.StrankaID = @StrankaID
                         GROUP BY rg.RacunGlavaID, rg.StevilkaRacuna, rg.Kraj, rg.Datum, rg.DatumOpravljeno, rg.DatumZapade, 
                                  rg.StrankaID, s.NazivPodjetja, s.ImeInPriimek
                         ORDER BY rg.Datum DESC";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StrankaID", strankaID);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RacunGlava racun = RacunMapper.MapToRacunGlava(reader);
                            racuni.Add(racun);
                        }
                    }
                }
            }

            return racuni;
        }


        public List<RacunGlava> GetRacuniPoDatumu(DateTime datumOd, DateTime datumDo)
        {
            List<RacunGlava> racuni = new List<RacunGlava>();

            using (var connection = dbConnection.GetConnection())
            {
                connection.Open();
                string query = @"SELECT rg.RacunGlavaID, rg.StevilkaRacuna, rg.Kraj, rg.Datum, rg.DatumOpravljeno, rg.DatumZapade, 
                         rg.StrankaID, 
                         COALESCE(s.NazivPodjetja, s.ImeInPriimek) AS Stranka, 
                         SUM(rp.CenaPostavke) AS SkupnaCena,
                         SUM(CASE WHEN rp.Storitev = 'material' THEN rp.CenaPostavke ELSE 0 END) AS CenaMaterial
                         FROM RacunGlava rg
                         JOIN Stranka s ON rg.StrankaID = s.StrankaID
                         LEFT JOIN RacunPostavka rp ON rg.RacunGlavaID = rp.RacunGlavaID
                         WHERE rg.Datum >= @DatumOd AND rg.Datum <= @DatumDo
                         GROUP BY rg.RacunGlavaID, rg.StevilkaRacuna, rg.Kraj, rg.Datum, rg.DatumOpravljeno, rg.DatumZapade, 
                                  rg.StrankaID, s.NazivPodjetja, s.ImeInPriimek
                         ORDER BY rg.Datum DESC";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DatumOd", datumOd);
                    command.Parameters.AddWithValue("@DatumDo", datumDo);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RacunGlava racun = RacunMapper.MapToRacunGlava(reader);
                            racuni.Add(racun);
                        }
                    }
                }
            }

            return racuni;
        }

        #endregion methods
    }
}