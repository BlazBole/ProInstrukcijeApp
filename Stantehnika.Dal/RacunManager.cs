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
                string query = "SELECT * FROM RacunGlava";

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
