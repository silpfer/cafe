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
    public partial class Form1 : Form
    {
        DataBase dataBase = new DataBase();
        int n;
        string value;

        public Form1()
        {
            InitializeComponent();
        }

        private void menu_info_Click(object sender, EventArgs e)
        {
            if(panel_info.Visible) {
                panel_info.Visible = false;
            }
            else panel_info.Visible = true;
        }

        //заповнення таблиці
        private void table_filling()
        {
            inf.Visible = false;
            data_info.Columns.Clear();

            panel_table_info.Visible = true;

            dataBase.openConnection();
            string query = "SELECT * FROM dessert";

            switch (n)
            {
                case 1:
                    {
                        query = "SELECT * FROM dessert";
                        name.Text = "Десерти";
                    }; break;
                case 2:
                    {
                        query = "SELECT * FROM drink";
                        name.Text = "Напої";
                    }; break;
                case 3:
                    {
                        query = "SELECT * FROM topping";
                        name.Text = "Топінги";
                    }; break;
                case 4:
                    {
                        query = "SELECT * FROM worker";
                        name.Text = "Працівники";
                    }; break;
            }

            SqlDataAdapter adapter = new SqlDataAdapter(query, dataBase.getSqlConnection());
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            data_info.DataSource = dataTable;

            foreach (DataRow row in dataTable.Rows)
            {
                foreach (DataColumn column in dataTable.Columns)
                {
                    if (row[column] != null && row[column] != DBNull.Value)
                    {
                        // Очищення зайвих пробілів зі значень у стовпці
                        row[column] = row[column].ToString().Trim();
                    }
                }
            }
            //data_info.AutoGenerateColumns = true;
            data_info.RowHeadersVisible = false;
            switch (n)
            {
                case 1:
                    {
                        data_info.Columns[1].Width = 200;
                        data_info.Columns[2].Width = 400;
                        data_info.Columns[3].Width = 150;
                        data_info.Columns[4].Width = 100;
                        data_info.Columns[5].Width = 50;
                        data_info.Columns[6].Width = 100;
                        data_info.Columns[1].HeaderText = "Назва";
                        data_info.Columns[2].HeaderText = "Склад";
                        data_info.Columns[3].HeaderText = "Дата виготовлення";
                        data_info.Columns[4].HeaderText = "Термін придатності";
                        data_info.Columns[5].HeaderText = "Вага, г";
                        data_info.Columns[6].HeaderText = "Ціна, грн";
                        name.Text = "Десерти";
                    }; break;
                case 2:
                    {
                        data_info.Columns[1].Width = 100;
                        data_info.Columns[2].Width = 150;
                        data_info.Columns[3].Width = 100;
                        data_info.Columns[4].Width = 100;
                        data_info.Columns[1].HeaderText = "Назва";
                        data_info.Columns[2].HeaderText = "Склад";
                        data_info.Columns[3].HeaderText = "Об'єм, мл";
                        data_info.Columns[4].HeaderText = "Ціна, грн";
                        name.Text = "Напої";
                    }; break;
                case 3:
                    {
                        data_info.Columns[1].Width = 100;
                        data_info.Columns[2].Width = 100;
                        data_info.Columns[1].HeaderText = "Назва";
                        data_info.Columns[2].HeaderText = "Ціна, грн";
                    }; break;
                case 4:
                    {
                        data_info.Columns[1].Width = 130;
                        data_info.Columns[1].HeaderText = "Ім'я";
                    }; break;
            }
            data_info.Columns[0].Visible = false;
        }

        public void UpdateTableData()
        {
            table_filling();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            panel8.Visible = true;
            panel9.Visible = false;
            panel2.Visible = false;
            n = 1;
            table_filling();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel8.Visible = true;
            panel9.Visible = false;
            panel2.Visible = false;
            n = 2;
            table_filling();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel8.Visible = true;
            panel9.Visible = false;
            panel2.Visible = false;
            n = 3;
            table_filling();
        }

        public void formOpen(int x)
        {
            var activeCell = data_info.CurrentCell;
            if (activeCell != null)
            {
                value = data_info.Rows[activeCell.RowIndex].Cells[activeCell.ColumnIndex].Value.ToString();
            }

            change incert = new change(x, n, value);
            incert.Show();
        }

        private void incert_Click(object sender, EventArgs e)
        {
            formOpen(1);
        }

        private void update_Click(object sender, EventArgs e)
        {
            formOpen(2);
        }

        private void delete_Click(object sender, EventArgs e)
        {
            formOpen(3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel8.Visible = true;
            panel9.Visible = false;
            panel2.Visible = false;
            n = 4;
            table_filling();
        }

        private void but_order_Click(object sender, EventArgs e)
        {
            if (panel_order.Visible)
                panel_order.Visible = false;
            else panel_order.Visible = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            done.Visible = false;
            if (panel_table_info.Visible) panel_table_info.Visible = false;
            panel2.Visible = true;
            fill_order();

            dessert_options.Items.Clear();

            dataBase.openConnection();
            string query = "SELECT * FROM dessert";
            SqlCommand command = new SqlCommand(query, dataBase.getSqlConnection());

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                dessert_options.Items.Add(reader["name"].ToString().Trim());
            }
            reader.Close();
            dataBase.closeConnection();

            button12.Location = new Point(button12.Location.X, dessert_add.Location.Y+150);
        }

        private void fill_order()
        {
            workers.Items.Clear();
            DateTime currentDateTime = DateTime.Now;
            date_time.Text = currentDateTime.ToString();

            dataBase.openConnection();
            string query = "SELECT * FROM worker";
            SqlCommand command = new SqlCommand(query, dataBase.getSqlConnection());

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                workers.Items.Add(reader["name"].ToString().Trim());
            }
            reader.Close();

            string query1 = "SELECT IDENT_CURRENT('orders') AS last_id";

            SqlCommand command1 = new SqlCommand(query1, dataBase.getSqlConnection());
            SqlDataAdapter adapter = new SqlDataAdapter(command1);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                int lastID = Convert.ToInt32(dataTable.Rows[0]["last_id"]) + 1;
                order_numb.Text = lastID.ToString();
            }
            dataBase.closeConnection();
        }

        private void dessert_button_Click(object sender, EventArgs e)
        {
            if (panel6.Visible) panel6.Visible = false;
            panel5.Visible = true;
        }

        private void drinks_button_Click(object sender, EventArgs e)
        {
            panel6.Visible = true;
            if (panel5.Visible) panel5.Visible = false;
            panel6.Location = new Point(panel5.Location.X, panel5.Location.Y);

            drink_options.Items.Clear();

            dataBase.openConnection();
            string query = "SELECT * FROM drink";
            SqlCommand command = new SqlCommand(query, dataBase.getSqlConnection());

            SqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                drink_options.Items.Add(reader["name"].ToString().Trim());
            }

            reader.Close();
            topping_options.Items.Clear();

            dataBase.openConnection();
            string query1 = "SELECT * FROM topping";
            SqlCommand command1 = new SqlCommand(query1, dataBase.getSqlConnection());

            reader = command1.ExecuteReader();
            topping_options.Items.Add("");
            while (reader.Read())
            {
                topping_options.Items.Add(reader["name"].ToString().Trim());
            }

            reader.Close();
        }
    
        private void button5_Click(object sender, EventArgs e)
        {
            string searchValue = dessert_options.Text;

            bool found = false;

            foreach (DataGridViewRow row in dessert_add.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null && cell.Value.ToString() == searchValue)
                    {
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    break;
                }
            }

            if (found)
            {
                MessageBox.Show("Десерт уже обрано!");
            }
            else
            {
                if (!string.IsNullOrEmpty(dessert_options.Text) && numericUpDown1.Value > 0)
                {
                    string[] row = new string[] { dessert_options.Text, Convert.ToString(numericUpDown1.Value) };

                    dessert_add.Rows.Add(row);
                }
                else MessageBox.Show("Поле не обране!");
            }
            
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(drink_options.Text))
            {
                string[] row = new string[] { drink_options.Text, topping_options.Text };

                drink_add.Rows.Add(row);
            }
            else MessageBox.Show("Напій не обрано!");
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var activeCell = dessert_add.CurrentCell;
            if (dessert_add != null && dessert_add.CurrentCell != null)
            {
                int rowIndex = dessert_add.CurrentCell.RowIndex;
                dessert_add.Rows.RemoveAt(rowIndex);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var activeCell = drink_add.CurrentCell;
            if (drink_add != null && drink_add.CurrentCell != null)
            {
                int rowIndex = drink_add.CurrentCell.RowIndex;
                drink_add.Rows.RemoveAt(rowIndex);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            
            int order_nb = Convert.ToInt32(order_numb.Text);
            string worker = workers.Text;
            int work_id = -1;
            
            string query = "select id_worker from worker where name=@n";
            SqlCommand com = new SqlCommand(query, dataBase.getSqlConnection());
            com.Parameters.AddWithValue("@n", worker);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                work_id = Convert.ToInt32(dataTable.Rows[0][0]);
            }

            string inputDate = date_time.Text;
            string inputFormat = "dd.MM.yyyy HH:mm:ss";
            string desiredFormat = "yyyy-MM-dd HH:mm:ss";

            DateTime date = DateTime.ParseExact(inputDate, inputFormat, System.Globalization.CultureInfo.InvariantCulture);
            string formattedDate = date.ToString(desiredFormat);
            bool in_out = true, kesh_kard = true;
            if (radioButton1.Checked)
            {
                // з собою
                in_out = true;
            }
            else if (radioButton2.Checked)
            {
                // в кафе
                in_out = false;
            }
            if (radioButton3.Checked)
            {
                //  готівка
                kesh_kard = true;
            }
            else if (radioButton4.Checked)
            {
                // карта
                kesh_kard = false;
            }

            dataBase.openConnection();
            SqlCommand insert_order = new SqlCommand("InsertOrder", dataBase.getSqlConnection());
            insert_order.CommandType = CommandType.StoredProcedure;

            insert_order.Parameters.Add(new SqlParameter("@date", SqlDbType.SmallDateTime)).Value = DateTime.ParseExact(formattedDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            insert_order.Parameters.AddWithValue("@take_out", in_out);
            insert_order.Parameters.AddWithValue("@id_worker", work_id);
            insert_order.Parameters.AddWithValue("@cash_card", kesh_kard);

            insert_order.ExecuteNonQuery();
            dataBase.closeConnection();

            foreach (DataGridViewRow row in dessert_add.Rows)
            {
                string name = row.Cells[0].Value?.ToString();
                if (int.TryParse(row.Cells[1].Value?.ToString(), out int numb))
                {
                    ADD add = new ADD(name, numb);
                }
            }

            foreach (DataGridViewRow row in drink_add.Rows)
            {
                string n_d = row.Cells[0].Value?.ToString();
                string n_t = row.Cells[1].Value?.ToString();
                Drink drink = new Drink(n_d, n_t);
            }
            OrderSpec spec = new OrderSpec(order_nb);
            spec.Show();
            fill_order();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            data_info.Columns.Clear();
            dataBase.openConnection();
            name.Text = "Виконані замовлення";
            panel_table_info.Visible = true;
            panel2.Visible = false;
            panel8.Visible = false;
            panel9.Visible = true;
            done.Visible = false;
            inf.Visible = true;
            inf.Location = new Point(incert.Location.X, incert.Location.Y);

            string query = "Select id_order from orders where done=1";

            SqlDataAdapter adapter = new SqlDataAdapter(query, dataBase.getSqlConnection());
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
                data_info.DataSource = dataTable;
                data_info.Columns[0].HeaderText = "Номер замовлення";
            }

            dataBase.closeConnection();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            data_info.Columns.Clear();
            dataBase.openConnection();
            name.Text = "Не виконані замовлення";
            panel_table_info.Visible = true;
            panel2.Visible = false;
            panel8.Visible = false;
            panel9.Visible = true;
            inf.Visible = true;
            done.Visible = true;
            inf.Location = new Point(incert.Location.X, incert.Location.Y);

            string query = "Select id_order from orders where done=0";

            SqlDataAdapter adapter = new SqlDataAdapter(query, dataBase.getSqlConnection());
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
                data_info.DataSource = dataTable;
                data_info.Columns[0].HeaderText = "Номер замовлення";
            }
            else
            {
                MessageBox.Show("Всі замовлення виконано!");
            }
            dataBase.closeConnection();
        }

        private void inf_Click(object sender, EventArgs e)
        {
            int id;
            var activeCell = data_info.CurrentCell;
            if (data_info != null && data_info.CurrentCell != null)
            {
                id = (int)data_info.Rows[activeCell.RowIndex].Cells[activeCell.ColumnIndex].Value;
                OrderSpec order = new OrderSpec(id);
                order.Show();
            }
            
        }

        private void button13_Click(object sender, EventArgs e)
        {
            dataBase.openConnection();
            int id;
            var activeCell = data_info.CurrentCell;
            if (data_info != null && data_info.CurrentCell != null)
            {
                id = (int)data_info.Rows[activeCell.RowIndex].Cells[activeCell.ColumnIndex].Value;
                SqlCommand com = new SqlCommand("update orders set done=1 where id_order=@n", dataBase.getSqlConnection());
                com.Parameters.AddWithValue("@n", id);
                com.ExecuteNonQuery();
            }
            dataBase.closeConnection();
            dataBase.openConnection();
            string query = "Select id_order from orders where done=0";

            SqlDataAdapter adapter = new SqlDataAdapter(query, dataBase.getSqlConnection());
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
                data_info.DataSource = dataTable;
                data_info.Columns[0].HeaderText = "Номер замовлення";
            }
            else
            {
                MessageBox.Show("Всі замовлення виконано!");
            }
            dataBase.closeConnection();
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            Report report = new Report();
            report.Show();
        }

        private void dessert_add_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
