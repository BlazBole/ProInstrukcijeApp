using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;



namespace Stantehnika.Dal
{
    public class DatabaseConnection
    {
        #region private members
        private string connectionString;
        #endregion

        #region constructor
        public DatabaseConnection()
        {
            // Prilagodi povezavo na osnovi tvojih podatkov (host, baza, uporabnik, geslo)
            connectionString = "Server=152.89.235.90;Database=proinstr_ProInstrukcijeAPP;User=proinstr_blazb;Password=Blaz.bole123;charset=utf8mb4;Connection Timeout=60";
        }
        #endregion constructor

        #region methods
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
        #endregion methods
    }
}
