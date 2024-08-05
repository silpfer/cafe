using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace cafe
{
    internal class DataBase
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-T3VP5V1;Initial Catalog=cafe;Integrated Security=True");

        public void openConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed) { sqlConnection.Open(); }
        }

        public void closeConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open) { sqlConnection.Close(); }
        }

        public SqlConnection getSqlConnection() { return sqlConnection; }
    }
}
