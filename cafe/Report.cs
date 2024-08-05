using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace cafe
{
    public partial class Report : Form
    {
        DataBase database = new DataBase();

        public Report()
        {
            InitializeComponent();
            work.Titles.Add("Заробіток працівників");
        }

        private void fill_form()
        {
            database.openConnection();

            SqlCommand command = new SqlCommand("GetWorkerResult", database.getSqlConnection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@startDate", dateTimePicker1.Value);
            command.Parameters.AddWithValue("@endDate", dateTimePicker2.Value);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            workers.DataSource = dataTable;
            workers.Columns[0].Visible = false;
            workers.Columns[1].HeaderText = "Ім'я";
            workers.Columns[2].HeaderText = "Виторг";
            workers.Columns[2].Width = 80;

            string query1 = "select count(id_order) from orders where done=1 AND date BETWEEN @startDate AND @endDate";
            SqlCommand command1 = new SqlCommand(query1, database.getSqlConnection());
            command1.Parameters.AddWithValue("@startDate", dateTimePicker1.Value);
            command1.Parameters.AddWithValue("@endDate", dateTimePicker2.Value);
            string numb = command1.ExecuteScalar().ToString();
            label1.Text = "Загальна кількість замовлень: " + numb;

            string query2 = "select Sum(price) from orders where done=1 AND date BETWEEN @startDate AND @endDate";
            SqlCommand command2 = new SqlCommand(query2, database.getSqlConnection());
            command2.Parameters.AddWithValue("@startDate", dateTimePicker1.Value);
            command2.Parameters.AddWithValue("@endDate", dateTimePicker2.Value);
            string summ = command2.ExecuteScalar().ToString();
            label2.Text = "Загальна  сума: " + summ;

            database.closeConnection();
        }

        private void workers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var activeCell = workers.CurrentCell;
            string value = "";
            if (activeCell != null)
            {
                value = workers.Rows[activeCell.RowIndex].Cells[activeCell.ColumnIndex-1].Value.ToString();
            }
            fill_order(Convert.ToInt32(value));
        }

        void fill_order(int work)
        {
            database.openConnection();
            string query = "SELECT id_order, price FROM orders WHERE id_worker = @n AND date BETWEEN @startDate AND @endDate";
            SqlCommand command = new SqlCommand(query, database.getSqlConnection());
            command.Parameters.AddWithValue("@n", work);
            command.Parameters.AddWithValue("@startDate", dateTimePicker1.Value);
            command.Parameters.AddWithValue("@endDate", dateTimePicker2.Value);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            orders.DataSource = dataTable;
            orders.Columns[0].HeaderText = "Замовлення";
            orders.Columns[0].Width = 80;
            orders.Columns[1].HeaderText = "Ціна";
            orders.Columns[1].Width = 70;
            database.closeConnection();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void orders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var activeCell = orders.CurrentCell;
            int value = 1;
            if (activeCell != null)
            {
                value = Convert.ToInt32(orders.Rows[activeCell.RowIndex].Cells[activeCell.ColumnIndex].Value);
            }
            fill(value);
        }

        void fill(int id_order)
        {
            database.openConnection();

            DataTable dataTable1 = new DataTable();

            SqlCommand get_dess = new SqlCommand("GetOrderDess", database.getSqlConnection());
            get_dess.CommandType = CommandType.StoredProcedure;

            get_dess.Parameters.AddWithValue("@n", id_order);
            SqlDataAdapter adapter1 = new SqlDataAdapter(get_dess);
            adapter1.Fill(dataTable1);

            dessert.DataSource = dataTable1;
            dessert.Columns[0].Visible = false;
            dessert.Columns[1].Width = 150;
            dessert.Columns[2].Width = 75;
            dessert.Columns[3].Width = 75;
            dessert.Columns[1].HeaderText = "Десерт";
            dessert.Columns[2].HeaderText = "Кількість";

            DataTable dataTable2 = new DataTable();

            SqlCommand get_drink = new SqlCommand("GetOrderDrink", database.getSqlConnection());
            get_drink.CommandType = CommandType.StoredProcedure;

            get_drink.Parameters.AddWithValue("@n", id_order);
            SqlDataAdapter adapter2 = new SqlDataAdapter(get_drink);
            adapter2.Fill(dataTable2);

            drink.DataSource = dataTable2;
            drink.Columns[0].Visible = false;
            drink.Columns[1].Width = 100;
            drink.Columns[2].Width = 125;
            drink.Columns[3].Width = 75;
            drink.Columns[1].HeaderText = "Напій";
            drink.Columns[2].HeaderText = "Топінг";
        }

        private void inf_Click(object sender, EventArgs e)
        {
            fill_form();
            createDiagram();
            label1.Visible = true;
            label2.Visible = true;
            work.Visible = true;
        }

        private void createDiagram()
        {
            work.Series["S1"].Points.Clear();            

            foreach(DataGridViewRow row in workers.Rows)
            {
                work.Series["S1"].Points.AddXY(row.Cells[1].Value?.ToString(), row.Cells[2].Value);
            }
        }

    }
}
