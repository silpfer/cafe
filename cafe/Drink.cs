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
    internal class Drink
    {
        DataBase dataBase = new DataBase();
        public Drink(string n_d, string n_t) {

            dataBase.openConnection();
            SqlCommand n = new SqlCommand("select MAX(id_order) from orders", dataBase.getSqlConnection());
            int id_o = Convert.ToInt32(n.ExecuteScalar());

            int id_d, id_t;
            SqlCommand d_id = new SqlCommand("select id_drink from drink where name=@n", dataBase.getSqlConnection());
            
            d_id.Parameters.AddWithValue("@n", n_d);
            id_d = Convert.ToInt32(d_id.ExecuteScalar());
            

            SqlCommand insertOrderDrink = new SqlCommand("InsertOrderDrink", dataBase.getSqlConnection());
            insertOrderDrink.CommandType = CommandType.StoredProcedure;

            insertOrderDrink.Parameters.AddWithValue("@id_order", id_o);
            insertOrderDrink.Parameters.AddWithValue("@id_drink", id_d);

            insertOrderDrink.ExecuteNonQuery();
            dataBase.closeConnection();
            dataBase.openConnection();
            if (!string.IsNullOrWhiteSpace(n_t))
            {
                SqlCommand t_id = new SqlCommand("select id_topping from topping where name=@n", dataBase.getSqlConnection());
                t_id.Parameters.AddWithValue("@n", n_t);
                id_t = Convert.ToInt32(t_id.ExecuteScalar());                
                string query = "select MAX(id) from order_drink";

                SqlCommand command = new SqlCommand(query, dataBase.getSqlConnection());
                int lastInsertedID = Convert.ToInt32(command.ExecuteScalar());

                Topping topping = new Topping(lastInsertedID, id_t);
            }
            dataBase.closeConnection();
        }
    }
}
