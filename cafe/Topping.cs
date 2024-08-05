using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;
using System.Diagnostics;


namespace cafe
{
    internal class Topping
    {
        DataBase dataBase = new DataBase();

        public Topping(int lastInsertedID, int id_t) 
        {
            dataBase.openConnection();
            SqlCommand insertOrderTopping = new SqlCommand("InsertOrderTopping", dataBase.getSqlConnection());
            insertOrderTopping.CommandType = CommandType.StoredProcedure;

            insertOrderTopping.Parameters.AddWithValue("@id_od", lastInsertedID);
            insertOrderTopping.Parameters.AddWithValue("@id_topping ", id_t);
            insertOrderTopping.ExecuteNonQuery();
            dataBase.closeConnection();
        }
    }
}
