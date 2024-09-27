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

        public int GetSteviloIzdaniRacunovZaMesec(DateTime prviDan, DateTime zadnjiDan)
        {
            int steviloRacunov = 0;

            using (var connection = dbConnection.GetConnection())
            {
                connection.Open();
                string query = @"SELECT COUNT(*) FROM RacunGlava WHERE Datum BETWEEN @PrviDanMeseca AND @ZadnjiDanMeseca";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PrviDanMeseca", prviDan);
                    command.Parameters.AddWithValue("@ZadnjiDanMeseca", zadnjiDan);

                    steviloRacunov = Convert.ToInt32(command.ExecuteScalar());
                }
            }

            return steviloRacunov;
        }

        public decimal GetVsotaMaterialovZaMesec(DateTime prviDan, DateTime zadnjiDan)
        {
            decimal vsotaMaterialov = 0m;

            using (var connection = dbConnection.GetConnection())
            {
                connection.Open();
                string query = @"SELECT SUM(rp.CenaPostavke) 
                        FROM RacunPostavka rp
                        JOIN RacunGlava rg ON rp.RacunGlavaID = rg.RacunGlavaID
                        WHERE rp.Storitev = 'material' 
                        AND rg.Datum BETWEEN @PrviDanMeseca AND @ZadnjiDanMeseca";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PrviDanMeseca", prviDan);
                    command.Parameters.AddWithValue("@ZadnjiDanMeseca", zadnjiDan);

                    var result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        vsotaMaterialov = Convert.ToDecimal(result);
                    }
                }
            }

            return vsotaMaterialov;
        }

        public decimal GetVsotaDelaZaMesec(DateTime prviDan, DateTime zadnjiDan)
        {
            decimal vsotaDela = 0m;

            using (var connection = dbConnection.GetConnection())
            {
                connection.Open();
                string query = @"SELECT SUM(rp.CenaPostavke) 
                         FROM RacunPostavka rp
                         JOIN RacunGlava rg ON rp.RacunGlavaID = rg.RacunGlavaID
                         WHERE rp.Storitev != 'material'
                         AND rg.Datum BETWEEN @PrviDanMeseca AND @ZadnjiDanMeseca";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PrviDanMeseca", prviDan);
                    command.Parameters.AddWithValue("@ZadnjiDanMeseca", zadnjiDan);

                    var result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        vsotaDela = Convert.ToDecimal(result);
                    }
                }
            }

            return vsotaDela;
        }

        public decimal GetSkupniPrilivZaMesec(DateTime prviDanMeseca, DateTime zadnjiDanMeseca)
        {
            decimal skupniPriliv = 0;

            using (var connection = dbConnection.GetConnection())
            {
                connection.Open();
                string query = @"SELECT SUM(rp.CenaPostavke) AS SkupniPriliv
                         FROM RacunGlava rg
                         JOIN RacunPostavka rp ON rg.RacunGlavaID = rp.RacunGlavaID
                         WHERE rg.Datum BETWEEN @PrviDanMeseca AND @ZadnjiDanMeseca";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PrviDanMeseca", prviDanMeseca);
                    command.Parameters.AddWithValue("@ZadnjiDanMeseca", zadnjiDanMeseca);

                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        skupniPriliv = Convert.ToDecimal(result);
                    }
                }
            }

            return skupniPriliv;
        }

        public string GetStevilkaZadnjegaRacuna()
        {
            string stevilkaRacuna = null;

            using (var connection = dbConnection.GetConnection())
            {
                connection.Open();
                string query = @"SELECT StevilkaRacuna 
                         FROM RacunGlava 
                         ORDER BY Datum DESC 
                         LIMIT 1";

                using (var command = new MySqlCommand(query, connection))
                {
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        stevilkaRacuna = result.ToString();
                    }
                }
            }

            return stevilkaRacuna;
        }

        public string GetStrankaZadnjegaRacuna()
        {
            string stranka = null;

            using (var connection = dbConnection.GetConnection())
            {
                connection.Open();
                string query = @"
                        SELECT COALESCE(s.NazivPodjetja, s.ImeInPriimek) AS Stranka
                        FROM RacunGlava rg
                        JOIN Stranka s ON rg.StrankaID = s.StrankaID
                        ORDER BY rg.Datum DESC
                        LIMIT 1";

                using (var command = new MySqlCommand(query, connection))
                {
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        stranka = result.ToString();
                    }
                }
            }

            return stranka;
        }

        public DateTime? GetDatumZadnjegaRacuna()
        {
            DateTime? datumZadnjegaRacuna = null;

            using (var connection = dbConnection.GetConnection())
            {
                connection.Open();
                string query = @"
                    SELECT Datum
                    FROM RacunGlava
                    ORDER BY Datum DESC
                    LIMIT 1";

                using (var command = new MySqlCommand(query, connection))
                {
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        datumZadnjegaRacuna = Convert.ToDateTime(result);
                    }
                }
            }

            return datumZadnjegaRacuna;
        }

        public decimal GetSkupnaCenaZadnjegaRacuna()
        {
            decimal skupnaCena = 0.0m;

            using (var connection = dbConnection.GetConnection())
            {
                connection.Open();

                // Pridobitev ID zadnjega računa
                string queryRacunID = @"
            SELECT RacunGlavaID 
            FROM RacunGlava 
            ORDER BY Datum DESC 
            LIMIT 1";

                int? racunGlavaID = null;
                using (var command = new MySqlCommand(queryRacunID, connection))
                {
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        racunGlavaID = Convert.ToInt32(result);
                    }
                }

                if (racunGlavaID.HasValue)
                {
                    // Pridobitev skupne cene postavk za ta račun
                    string queryCena = @"
                SELECT SUM(CenaPostavke) 
                FROM RacunPostavka 
                WHERE RacunGlavaID = @RacunGlavaID";

                    using (var command = new MySqlCommand(queryCena, connection))
                    {
                        command.Parameters.AddWithValue("@RacunGlavaID", racunGlavaID.Value);

                        object resultCena = command.ExecuteScalar();
                        if (resultCena != DBNull.Value)
                        {
                            skupnaCena = Convert.ToDecimal(resultCena);
                        }
                    }
                }
            }

            return skupnaCena;
        }


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

        public Dictionary<int, string> GetRacun()
        {
            Dictionary<int, string> racuni = new Dictionary<int, string>();
            using (var connection = dbConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT RacunGlavaID, StevilkaRacuna, Datum FROM RacunGlava ORDER BY Datum DESC";
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        racuni.Add(reader.GetInt32("RacunGlavaID"), reader.GetString("StevilkaRacuna"));
                    }
                }
            }

            return racuni;
        }

        public Dictionary<int, string> GetAllStranke()
        {
            Dictionary<int, string> stranke = new Dictionary<int, string>();

            using (var connection = dbConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT StrankaID, COALESCE(NazivPodjetja, ImeInPriimek) AS Stranka FROM Stranka ORDER BY Stranka ASC";
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

        public List<RacunGlava> GetRacuniPoRacunu(int racunID)
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
                         WHERE rg.RacunGlavaID = @RacunGlavaID
                         GROUP BY rg.RacunGlavaID, rg.StevilkaRacuna, rg.Kraj, rg.Datum, rg.DatumOpravljeno, rg.DatumZapade, 
                                  rg.StrankaID, s.NazivPodjetja, s.ImeInPriimek
                         ORDER BY rg.Datum DESC";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RacunGlavaID", racunID);
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

        public List<RacunGlava> GetRacuniPoSkupniceni(int cenaOd, int cenaDo)
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
                   GROUP BY rg.RacunGlavaID, rg.StevilkaRacuna, rg.Kraj, rg.Datum, rg.DatumOpravljeno, rg.DatumZapade, 
                            rg.StrankaID, s.NazivPodjetja, s.ImeInPriimek
                   HAVING SkupnaCena >= @CenaOd AND SkupnaCena <= @CenaDo
                   ORDER BY rg.Datum DESC";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CenaOd", cenaOd);
                    command.Parameters.AddWithValue("@CenaDo", cenaDo);

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

        public List<Stranka> PridobiVseStranke()
        {
            List<Stranka> stranke = new List<Stranka>();

            using (var connection = dbConnection.GetConnection())
            {
                connection.Open();
                string query = @"SELECT StrankaID, COALESCE(NazivPodjetja, ImeInPriimek) AS Stranka, 
                UlicaInHisnaStevilka, PostaInKraj, DavcnaStevilka, SedezPodjetja, Email
                FROM Stranka
                ORDER BY Stranka ASC";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Stranka stranka = RacunMapper.MapToRacunStranka(reader);
                            stranke.Add(stranka);
                        }
                    }
                }
            }

            return stranke;
        }

        public List<Stranka> PridobiStrankePoNazivu(string iskalniPojem)
        {
            List<Stranka> stranke = new List<Stranka>();

            using (var connection = dbConnection.GetConnection())
            {
                connection.Open();
                string query = @"SELECT StrankaID, COALESCE(NazivPodjetja, ImeInPriimek) AS Stranka, 
                        UlicaInHisnaStevilka, PostaInKraj, DavcnaStevilka, SedezPodjetja, Email
                        FROM Stranka
                        WHERE NazivPodjetja LIKE CONCAT('%', @IskalniPojem, '%') 
                        OR ImeInPriimek LIKE CONCAT('%', @IskalniPojem, '%')";
                using (var command = new MySqlCommand(query, connection))
                {
                    // Define and add the parameter
                    command.Parameters.AddWithValue("@IskalniPojem", iskalniPojem.Trim());

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Stranka stranka = RacunMapper.MapToRacunStranka(reader);
                            stranke.Add(stranka);
                        }
                    }
                }
            }

            return stranke;
        }

        public bool PreveriEmailObstaja(string email)
        {
            using (var connection = dbConnection.GetConnection())
            {
                string query = "SELECT COUNT(1) FROM Stranka WHERE Email = @Email";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0; // Returns true if email exists, false otherwise
                }
            }
        }

        public void DodajFizicnoOsebo(string imeInPriimek, string ulicaInHisnaStevilka, string postaInKraj, string email)
        {
            using (var connection = dbConnection.GetConnection())
            {
                string query = @"INSERT INTO Stranka (ImeInPriimek, UlicaInHisnaStevilka, PostaInKraj, NazivPodjetja, DavcnaStevilka, SedezPodjetja, Email)
                         VALUES (@ImeInPriimek, @UlicaInHisnaStevilka, @PostaInKraj, NULL, NULL, NULL, @Email)";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ImeInPriimek", imeInPriimek);
                    command.Parameters.AddWithValue("@UlicaInHisnaStevilka", ulicaInHisnaStevilka);
                    command.Parameters.AddWithValue("@PostaInKraj", postaInKraj);
                    command.Parameters.AddWithValue("@Email", email);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DodajPravnoOsebo(string nazivPodjetja, string davcnaStevilka, string sedezPodjetja, string email)
        {
            using (var connection = dbConnection.GetConnection())
            {
                string query = @"INSERT INTO Stranka (NazivPodjetja, DavcnaStevilka, PostaInKraj, SedezPodjetja, UlicaInHisnaStevilka, ImeInPriimek, Email)
                         VALUES (@NazivPodjetja, @DavcnaStevilka, NULL, @SedezPodjetja, NULL, NULL, @Email)";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NazivPodjetja", nazivPodjetja);
                    command.Parameters.AddWithValue("@DavcnaStevilka", davcnaStevilka);
                    command.Parameters.AddWithValue("@SedezPodjetja", sedezPodjetja);
                    command.Parameters.AddWithValue("@Email", email);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Stranka> IsciStranke(string iskalniPogoj)
        {
            List<Stranka> predlogiStrank = new List<Stranka>();

            using (var connection = dbConnection.GetConnection())
            {
                connection.Open();
                string query = @"SELECT StrankaID, COALESCE(NazivPodjetja, ImeInPriimek) AS Stranka, Email
                         FROM Stranka
                         WHERE NazivPodjetja LIKE @IskalniPogoj OR ImeInPriimek LIKE @IskalniPogoj";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IskalniPogoj", "%" + iskalniPogoj + "%");

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Stranka stranka = new Stranka
                            {
                                StrankaID = reader.GetInt32("StrankaID"),
                                ImeInPriimek = reader.IsDBNull(reader.GetOrdinal("Stranka"))
                                               ? null
                                               : reader.GetString("Stranka"),
                                Email = reader.GetString("Email")
                            };
                            predlogiStrank.Add(stranka);
                        }
                    }
                }
            }
            return predlogiStrank;
        }



        #endregion methods
    }
}