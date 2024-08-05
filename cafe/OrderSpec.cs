using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;

namespace cafe
{
    public partial class OrderSpec : Form
    {
        int id_order;
        DataBase dataBase = new DataBase();
        public OrderSpec(int id)
        {
            InitializeComponent();

            id_order = id;

            fill();
        }

        private void fill()
        {
            dataBase.openConnection();
            string query = "Select * from orders where id_order=@n";
            SqlCommand command = new SqlCommand(query, dataBase.getSqlConnection());
            command.Parameters.AddWithValue("@n", id_order);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
            {
                int id_worker = (int)dataTable.Rows[0][3];
                query = "select name from worker where id_worker=@n";
                SqlCommand search = new SqlCommand(query, dataBase.getSqlConnection());
                search.Parameters.AddWithValue("@n", id_worker);
                object result = search.ExecuteScalar();
                label4.Text += "\n" + dataTable.Rows[0][0].ToString();
                label2.Text += "\n" + result.ToString();
                label3.Text += "\n" + dataTable.Rows[0][1].ToString();
                if ((bool)dataTable.Rows[0][2]) label1.Text = "З собою";
                else label1.Text = "В кафе";
                if ((bool)dataTable.Rows[0][4]) label5.Text = "Готівка";
                else label5.Text = "Карта";
                Price.Text += dataTable.Rows[0][5].ToString();
            }

            DataTable dataTable1 = new DataTable();

            SqlCommand get_dess = new SqlCommand("GetOrderDetailsDess", dataBase.getSqlConnection());
            get_dess.CommandType = CommandType.StoredProcedure;

            get_dess.Parameters.AddWithValue("@n", id_order);
            SqlDataAdapter adapter1 = new SqlDataAdapter(get_dess);
            adapter1.Fill(dataTable1);

            dessert.DataSource = dataTable1;
            dessert.Columns[0].Visible = false;
            dessert.Columns[1].Width = 250;
            dessert.Columns[2].Width = 100;
            dessert.Columns[1].HeaderText = "Десерт";
            dessert.Columns[2].HeaderText = "Кількість";

            DataTable dataTable2 = new DataTable();

            SqlCommand get_drink = new SqlCommand("GetOrderDetailsDrink", dataBase.getSqlConnection());
            get_drink.CommandType = CommandType.StoredProcedure;

            get_drink.Parameters.AddWithValue("@n", id_order);
            SqlDataAdapter adapter2 = new SqlDataAdapter(get_drink);
            adapter2.Fill(dataTable2);

            drink.DataSource = dataTable2;
            drink.Columns[0].Visible = false;
            drink.Columns[1].Width = 150;
            drink.Columns[2].Width = 150;
            drink.Columns[1].HeaderText = "Напій";
            drink.Columns[2].HeaderText = "Топінг";
        }

        public void printFile()
        {
            string name = @"Замовлення"+id_order.ToString();
            string directoryPath = @"D:\doc\ПППІ\cafe\Друк";

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string filePath = Path.Combine(directoryPath, name + ".txt");

            using (StreamWriter writer = File.CreateText(filePath))
            {
                writer.WriteLine(label4.Text);
                writer.WriteLine(label2.Text);
                writer.WriteLine(label3.Text);
                writer.WriteLine(label1.Text);
                writer.WriteLine(label5.Text);

                if (dessert.Rows.Count > 0) 
                {
                    writer.WriteLine("\nДесерти:");

                    foreach (DataGridViewRow row in dessert.Rows)
                    {
                        writer.WriteLine($"Назва десерту: {row.Cells[1].Value?.ToString()} Кількість: {row.Cells[2].Value?.ToString()}");
                    }
                }

                if (drink.Rows.Count > 0)
                {
                    writer.WriteLine("\nНапої:");

                    foreach (DataGridViewRow row in drink.Rows)
                    {
                        if (!string.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                        {
                            writer.WriteLine($"Назва напою: {row.Cells[1].Value?.ToString()} Топінг: {row.Cells[2].Value?.ToString()}");
                        }
                        else writer.WriteLine($"Назва напою: {row.Cells[1].Value?.ToString()}");
                    }
                }
                
                writer.WriteLine($"\n{Price.Text}");
            }

            if (File.Exists(filePath))
                MessageBox.Show("Чек записано у файл: " + name);

        }

        private void print_Click(object sender, EventArgs e)
        {
            printFile();
        }
    }
}
