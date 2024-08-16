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
            connectionString = "Server=212.44.101.98;Database=stante58_StantehnikaAPP;User=stante58_blaz;Password=Gregor.bole12345;";
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
