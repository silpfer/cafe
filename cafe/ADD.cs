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
    internal class ADD
    {
        DataBase dataBase = new DataBase();

        public ADD(string name, int numb)
        {
            dataBase.openConnection();
            SqlCommand s_id = new SqlCommand("Select id_dessert from dessert where name=@n", dataBase.getSqlConnection());

            s_id.Parameters.AddWithValue("@n", name);
            int id = Convert.ToInt32(s_id.ExecuteScalar());

            SqlCommand n = new SqlCommand("select MAX(id_order) from orders", dataBase.getSqlConnection());
            int id_n = Convert.ToInt32(n.ExecuteScalar());

            SqlCommand insert_order = new SqlCommand("InsertOrderDessert", dataBase.getSqlConnection());
            insert_order.CommandType = CommandType.StoredProcedure;

            insert_order.Parameters.AddWithValue("@id_order", id_n);
            insert_order.Parameters.AddWithValue("@id_dessert", id);
            insert_order.Parameters.AddWithValue("@number", numb);

            insert_order.ExecuteNonQuery();

            dataBase.closeConnection();
            
        }
    }
}
