using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TextBox = System.Windows.Forms.TextBox;
using System.Globalization;
using System.Diagnostics;
using System.Collections;

namespace cafe
{
    public partial class change : Form
    {
        Form1 form1 = (Form1)Application.OpenForms["Form1"];
        DataBase dataBase = new DataBase();
        //поля 
        Label[] label = new Label[6];
        TextBox[] text = new TextBox[6];

        //назва
        Label name1 = new Label();
        TextBox text_name = new TextBox();

        int ch, tab;
        string value="";
        public change(int version, int table, string valu)
        {
            InitializeComponent();

            ch = version;
            tab = table;
            if (valu != null) { value = valu; }
            

            switch (ch) {
                case 1: { name.Text = "Додати"; this.Text = "Додати"; incert_create(); } break;
                case 2: { name.Text = "Змінити"; this.Text = "Змінити"; update_create(); full(); } break;
                case 3: {name.Text = "Видалити"; this.Text = "Видалити"; delete_create(); } break;
            }

            switch (tab)
            {
                case 1: name.Text += " десерт"; break;
                case 2: name.Text += " напій"; break;
                case 3: name.Text += " топінг"; break;
                case 4: name.Text += " робітника"; break;
            }

        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //створення полів
        private void incert_create()
        {
            int x = 10, y = 50, y2 = 75;
            Size size = new Size(150, 100);
            Font font = new Font("Verdana", 13);            
            Color textColor = Color.FromArgb(109, 86, 60);

            int n = 1;
            switch(tab)
            {
                case 1: n = 6;
                    break;
                case 2: n = 4;  
                    break;
                case 3: n = 2;
                    break;
                case 4: n = 1;
                    break;

            }

            for (int i = 0; i < n; i++)
            {
                label[i] = new Label();
                label[i].Location = new Point(x, y);
                label[i].ForeColor = textColor;
                this.Controls.Add(label[i]);

                text[i] = new TextBox();
                text[i].Location = new Point(x, y2);
                text[i].Size = size;
                text[i].ForeColor = textColor;
                text[i].BackColor = Color.FromArgb(254, 250, 224);
                this.Controls.Add(text[i]);

                x += 170;
            }

            if(tab == 1) {
                label[0].Text = "Назва";
                label[1].Text = "Склад";
                label[2].Text = "Дата виготовлення";
                label[3].Text = "Термін придатності, дні";
                label[4].Text = "Вага, г";
                label[5].Text = "Ціна, грн";
            }
            if (tab == 2)
            {
                label[0].Text = "Назва";
                label[1].Text = "Склад";
                label[2].Text = "Об'єм, мл";
                label[3].Text = "Ціна, грн";
            }
            if (tab == 3)
            {
                label[0].Text = "Назва";
                label[1].Text = "Ціна, грн";
            }
            if (tab == 4)
            {
                label[0].Text = "Прізвище ініціали";
            }
        }
        private void update_create()
        {  
            int x = 10, y = 75, y2 = 100;
            Size size = new Size(150, 100);
            Font font = new Font("Verdana", 13);
            Color textColor = Color.FromArgb(109, 86, 60);

            name1 = new Label();
            name1.Location = new Point(10, 45);
            name1.ForeColor = textColor;
            name1.Text = "Початкова назва";
            switch (tab)
            {
                case 1: name1.Text += " десерту"; break;
                case 2: name1.Text += " напою"; break;
                case 3: name1.Text += " топінгу"; break;
                case 4: name1.Text = "Початкове ім'я"; break;
            }
            this.Controls.Add(name1);

            text_name = new TextBox();
            text_name.Location = new Point(110, 45);
            text_name.Size = size;
            text_name.ForeColor = textColor;
            text_name.BackColor = Color.FromArgb(254, 250, 224);
            this.Controls.Add(text_name);

            int n = 1;

            switch (tab)
            {
                case 1:
                    n = 6;
                    break;
                case 2:
                    n = 4;
                    break;
                case 3:
                    n = 2;
                    break;
                case 4:
                    n = 1;
                    break;

            }

            for (int i = 0; i < n; i++)
            {
                label[i] = new Label();
                label[i].Location = new Point(x, y);
                label[i].ForeColor = textColor;
                this.Controls.Add(label[i]);

                text[i] = new TextBox();
                text[i].Location = new Point(x, y2);
                text[i].Size = size;
                text[i].ForeColor = textColor;
                text[i].BackColor = Color.FromArgb(254, 250, 224);
                this.Controls.Add(text[i]);

                x += 170;
            }

            if (tab == 1)
            {
                label[0].Text = "Назва";
                label[1].Text = "Склад";
                label[2].Text = "Дата виготовлення";
                text[2].Text = "YYYY-MM-DD 00:00:00";
                label[3].Text = "Термін придатності, дні";
                label[4].Text = "Вага, г";
                label[5].Text = "Ціна, грн";
            }
            if (tab == 2)
            {
                label[0].Text = "Назва";
                label[1].Text = "Склад";
                label[2].Text = "Об'єм, мл";
                label[3].Text = "Ціна, грн";
            }
            if (tab == 3)
            {
                label[0].Text = "Назва";
                label[1].Text = "Ціна, грн";
            }
            if (tab == 4)
            {
                label[0].Text = "Прізвище ініціали";
            }

        }
        private void delete_create()
        {
            Size size = new Size(150, 100);
            Font font = new Font("Verdana", 13);
            Color textColor = Color.FromArgb(109, 86, 60);

            name1 = new Label();
            name1.Location = new Point(10, 60);
            name1.ForeColor = textColor;
            name1.Text = "Назва";
            switch (tab)
            {
                case 1: name1.Text += " десерту"; break;
                case 2: name1.Text += " напою"; break;
                case 3: name1.Text += " топінгу"; break;
                case 4: name1.Text = "Прізвище ініціали робітника"; break;
            }
            this.Controls.Add(name1);

            text_name = new TextBox();
            text_name.Location = new Point(10, 90);
            text_name.Size = size;
            text_name.ForeColor = textColor;
            text_name.BackColor = Color.FromArgb(254, 250, 224);
            this.Controls.Add(text_name);
            if (value != "")
            {
                dataBase.openConnection();
                string query = "SELECT * FROM dessert WHERE name = @name";

                switch (tab)
                {
                    case 1:
                        {
                            query = "SELECT * FROM dessert WHERE name = @name";
                        }; break;
                    case 2:
                        {
                            query = "SELECT * FROM drink WHERE name = @name";
                        }; break;
                    case 3:
                        {
                            query = "SELECT * FROM topping WHERE name = @name";
                        }; break;
                    case 4:
                        {
                            query = "SELECT * FROM worker WHERE name = @name";
                        }; break;
                }

                SqlCommand search = new SqlCommand(query, dataBase.getSqlConnection());

                search.Parameters.Add("@name", SqlDbType.VarChar).Value = value;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = search;
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                    text_name.Text = dataTable.Rows[0][1].ToString();
                dataBase.closeConnection();
            }

        }

        private void save_Click(object sender, EventArgs e)
        {
            switch (ch)
            {
                case 1: incert(); break;
                case 2: update(); break;
                case 3: delete(); break;
            }            
        }

        //визначення таблиці
        private void update()
        {
            dataBase.openConnection();

            SqlDataAdapter adapter = new SqlDataAdapter();

            switch (tab) {
                case 1: update_dessert(adapter); break;
                case 2: update_drink(adapter); break;
                case 3: update_topping(adapter); break;
                case 4: update_worker(adapter); break;
            }

        }

        private void incert()
        {
            dataBase.openConnection();

            SqlDataAdapter adapter = new SqlDataAdapter();

            switch (tab)
            {
                case 1: incert_dessert(adapter); break;
                case 2: incert_drink(adapter); break;
                case 3: incert_topping(adapter); break;
                case 4: incert_worker(adapter); break;
            }

        }

        private void delete()
        {
            dataBase.openConnection();

            SqlDataAdapter adapter = new SqlDataAdapter();

            switch (tab)
            {
                case 1: delete_dessert(adapter); break;
                case 2: delete_drink(adapter); break;
                case 3: delete_topping(adapter); break;
                case 4: delete_worker(adapter); break;
            }

        }
        
        //зміна
        private void update_dessert(SqlDataAdapter adapter)
        {
            SqlCommand up_d = new SqlCommand("UpdateDessert");
            up_d.CommandType = CommandType.StoredProcedure;
            int id = 0;

            SqlCommand search = new SqlCommand("select id_dessert from dessert where name=@n", dataBase.getSqlConnection());
            search.Parameters.Add("@n", SqlDbType.VarChar).Value = text_name.Text;
            adapter.SelectCommand = search;
            DataTable dataTable1 = new DataTable();
            adapter.Fill(dataTable1);

            string inputDate = text[2].Text;
            string inputFormat = "dd.MM.yyyy HH:mm:ss";
            string desiredFormat = "yyyy-MM-dd HH:mm:ss";

            DateTime date = DateTime.ParseExact(inputDate, inputFormat, System.Globalization.CultureInfo.InvariantCulture);
            string formattedDate = date.ToString(desiredFormat);

            if (dataTable1.Rows.Count > 0)
            {
                id = (int)dataTable1.Rows[0][0];

                up_d.Parameters.Add(new SqlParameter("@id_dessert", SqlDbType.Int)).Value = id;
                up_d.Parameters.Add(new SqlParameter("@new_name", SqlDbType.VarChar, 30)).Value = text[0].Text;
                up_d.Parameters.Add(new SqlParameter("@new_composition", SqlDbType.VarChar, 1000)).Value = text[1].Text;
                up_d.Parameters.Add(new SqlParameter("@new_date_manufacture", SqlDbType.SmallDateTime)).Value = DateTime.ParseExact(formattedDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                up_d.Parameters.Add(new SqlParameter("@new_expiration_date", SqlDbType.Int)).Value = text[3].Text;
                up_d.Parameters.Add(new SqlParameter("@new_weight", SqlDbType.Float)).Value = text[4].Text;
                up_d.Parameters.Add(new SqlParameter("@new_price", SqlDbType.Money)).Value = text[5].Text;

                up_d.Connection = dataBase.getSqlConnection();
                up_d.CommandText = "UpdateDessert";
                up_d.CommandType = CommandType.StoredProcedure;
                up_d.ExecuteNonQuery();

                form1.UpdateTableData();
                this.Hide();
            }
            else MessageBox.Show("Назви не знайдено! Спробуйте ще раз");
        }
        private void update_drink(SqlDataAdapter adapter)
        {
            SqlCommand up_dr = new SqlCommand("UpdateDrink");
            up_dr.CommandType = CommandType.StoredProcedure;
            int id = 0;

            SqlCommand search = new SqlCommand("SELECT id_drink FROM drink WHERE name = @n", dataBase.getSqlConnection());
            search.Parameters.Add("@n", SqlDbType.VarChar).Value = text_name.Text;
            adapter.SelectCommand = search;
            DataTable dataTable1 = new DataTable();
            adapter.Fill(dataTable1);

            if (dataTable1.Rows.Count > 0)
            {
                id = (int)dataTable1.Rows[0][0];

                up_dr.Parameters.Add(new SqlParameter("@id_drink", SqlDbType.Int)).Value = id;
                up_dr.Parameters.Add(new SqlParameter("@new_name", SqlDbType.VarChar, 30)).Value = text[0].Text;
                up_dr.Parameters.Add(new SqlParameter("@new_composition", SqlDbType.VarChar, 100)).Value = text[1].Text;
                up_dr.Parameters.Add(new SqlParameter("@new_volume", SqlDbType.Float)).Value = System.Convert.ToSingle(text[2].Text);
                up_dr.Parameters.Add(new SqlParameter("@new_price", SqlDbType.Money)).Value = System.Convert.ToSingle(text[3].Text);

                up_dr.Connection = dataBase.getSqlConnection();
                up_dr.CommandText = "UpdateDrink";
                up_dr.CommandType = CommandType.StoredProcedure;
                up_dr.ExecuteNonQuery();

                form1.UpdateTableData();
                this.Hide();
            }
            else MessageBox.Show("Назви не знайдено! Спробуйте ще раз");
        }
        private void update_topping(SqlDataAdapter adapter)
        {
            SqlCommand up_t = new SqlCommand("UpdateTopping");
            up_t.CommandType = CommandType.StoredProcedure;
            int id = 0;

            SqlCommand search = new SqlCommand("SELECT id_topping FROM topping WHERE name = @n", dataBase.getSqlConnection());
            search.Parameters.Add("@n", SqlDbType.VarChar).Value = text_name.Text;
            adapter.SelectCommand = search;
            DataTable dataTable1 = new DataTable();
            adapter.Fill(dataTable1);

            if (dataTable1.Rows.Count > 0)
            {
                id = (int)dataTable1.Rows[0][0];

                up_t.Parameters.Add(new SqlParameter("@id_topping", SqlDbType.Int)).Value = id;
                up_t.Parameters.Add(new SqlParameter("@new_name", SqlDbType.VarChar, 30)).Value = text[0].Text;
                up_t.Parameters.Add(new SqlParameter("@new_price", SqlDbType.Money)).Value = System.Convert.ToSingle(text[1].Text);

                up_t.Connection = dataBase.getSqlConnection();
                up_t.CommandText = "UpdateTopping";
                up_t.CommandType = CommandType.StoredProcedure;
                up_t.ExecuteNonQuery();

                form1.UpdateTableData();
                this.Hide();
            }
            else MessageBox.Show("Назви не знайдено! Спробуйте ще раз");
        }
        private void update_worker(SqlDataAdapter adapter)
        {
            SqlCommand up_w = new SqlCommand("UpdateWorker");
            up_w.CommandType = CommandType.StoredProcedure;
            int id = 0;

            SqlCommand search = new SqlCommand("SELECT id_worker FROM worker WHERE name = @n", dataBase.getSqlConnection());
            search.Parameters.Add("@n", SqlDbType.VarChar).Value = text_name.Text;
            adapter.SelectCommand = search;
            DataTable dataTable1 = new DataTable();
            adapter.Fill(dataTable1);

            if (dataTable1.Rows.Count > 0)
            {
                id = (int)dataTable1.Rows[0][0];

                up_w.Parameters.Add(new SqlParameter("@id_worker", SqlDbType.Int)).Value = id;
                up_w.Parameters.Add(new SqlParameter("@new_name", SqlDbType.VarChar, 30)).Value = text[0].Text;

                up_w.Connection = dataBase.getSqlConnection();
                up_w.CommandText = "UpdateWorker";
                up_w.CommandType = CommandType.StoredProcedure;
                up_w.ExecuteNonQuery();

                form1.UpdateTableData();
                this.Hide();
            }
            else MessageBox.Show("Назви не знайдено! Спробуйте ще раз");
        }

        //додавання
        private void incert_dessert(SqlDataAdapter adapter)
        {
            SqlCommand c_d = new SqlCommand("dbo.InsertDessert");
            c_d.CommandType = CommandType.StoredProcedure;

            c_d.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar, 30)).Value = text[0].Text;
            c_d.Parameters.Add(new SqlParameter("@composition", SqlDbType.VarChar, 1000)).Value = text[1].Text;
            c_d.Parameters.Add(new SqlParameter("@date_manufacture", SqlDbType.SmallDateTime)).Value = DateTime.ParseExact(text[2].Text, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            c_d.Parameters.Add(new SqlParameter("@expiration_date", SqlDbType.Int)).Value = text[3].Text;
            c_d.Parameters.Add(new SqlParameter("@weight", SqlDbType.Float)).Value = text[4].Text;
            c_d.Parameters.Add(new SqlParameter("@price", SqlDbType.Money)).Value = text[5].Text;

            c_d.Connection = dataBase.getSqlConnection();
            c_d.CommandText = "dbo.InsertDessert";
            c_d.CommandType = CommandType.StoredProcedure;
            c_d.ExecuteNonQuery();

            form1.UpdateTableData();
            this.Hide();
        }
        private void incert_drink(SqlDataAdapter adapter)
        {
            SqlCommand c_d = new SqlCommand("InsertDrink");
            c_d.CommandType = CommandType.StoredProcedure;

            c_d.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar, 30)).Value = text[0].Text;
            c_d.Parameters.Add(new SqlParameter("@composition", SqlDbType.VarChar, 1000)).Value = text[1].Text;
            c_d.Parameters.Add(new SqlParameter("@volume ", SqlDbType.Float)).Value = System.Convert.ToSingle(text[2].Text);
            c_d.Parameters.Add(new SqlParameter("@price ", SqlDbType.Money)).Value = System.Convert.ToSingle(text[3].Text);

            c_d.Connection = dataBase.getSqlConnection();
            c_d.CommandText = "InsertDrink";
            c_d.CommandType = CommandType.StoredProcedure;
            c_d.ExecuteNonQuery();

            form1.UpdateTableData();
            this.Hide();
        }
        private void incert_topping(SqlDataAdapter adapter)
        {
            SqlCommand c_d = new SqlCommand("InsertTopping");
            c_d.CommandType = CommandType.StoredProcedure;

            c_d.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar, 30)).Value = text[0].Text;
            c_d.Parameters.Add(new SqlParameter("@price ", SqlDbType.Money)).Value = System.Convert.ToSingle(text[1].Text);

            c_d.Connection = dataBase.getSqlConnection();
            c_d.CommandText = "InsertTopping";
            c_d.CommandType = CommandType.StoredProcedure;
            c_d.ExecuteNonQuery();

            form1.UpdateTableData();
            this.Hide();
        }
        private void incert_worker(SqlDataAdapter adapter)
        {
            SqlCommand c_d = new SqlCommand("InsertWorker");
            c_d.CommandType = CommandType.StoredProcedure;

            c_d.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar, 30)).Value = text[0].Text;

            c_d.Connection = dataBase.getSqlConnection();
            c_d.CommandText = "InsertWorker";
            c_d.CommandType = CommandType.StoredProcedure;
            c_d.ExecuteNonQuery();

            form1.UpdateTableData();
            this.Hide();
        }
    
        //видалення
        private void delete_dessert(SqlDataAdapter adapter)
        {
            SqlCommand d_d = new SqlCommand("dbo.DeleteDessert");
            d_d.CommandType = CommandType.StoredProcedure;
            int id = 0;

            SqlCommand search = new SqlCommand("select id_dessert from dessert where name=@n", dataBase.getSqlConnection());
            search.Parameters.Add("@n", SqlDbType.VarChar).Value = text_name.Text;
            adapter.SelectCommand = search;
            DataTable dataTable1 = new DataTable();
            adapter.Fill(dataTable1);

            if (dataTable1.Rows.Count > 0)
            {
                id = (int)dataTable1.Rows[0][0];
                d_d.Parameters.Add(new SqlParameter("@id_dessert", SqlDbType.Int)).Value = id;

                d_d.Connection = dataBase.getSqlConnection();
                d_d.CommandText = "dbo.DeleteDessert";
                d_d.CommandType = CommandType.StoredProcedure;
                d_d.ExecuteNonQuery();

                form1.UpdateTableData();
                this.Hide();
            }
            else MessageBox.Show("Назви не знайдено! Спробуйте ще раз");
        }
        private void delete_drink(SqlDataAdapter adapter)
        {
            SqlCommand d_d = new SqlCommand("DeleteDrink");
            d_d.CommandType = CommandType.StoredProcedure;
            int id = 0;

            SqlCommand search = new SqlCommand("SELECT id_drink FROM drink WHERE name = @n", dataBase.getSqlConnection());
            search.Parameters.Add("@n", SqlDbType.VarChar).Value = text_name.Text;
            adapter.SelectCommand = search;
            DataTable dataTable1 = new DataTable();
            adapter.Fill(dataTable1);

            if (dataTable1.Rows.Count > 0)
            {
                id = (int)dataTable1.Rows[0][0];
                d_d.Parameters.Add(new SqlParameter("@id_drink", SqlDbType.Int)).Value = id;

                d_d.Connection = dataBase.getSqlConnection();
                d_d.CommandText = "DeleteDrink";
                d_d.CommandType = CommandType.StoredProcedure;
                d_d.ExecuteNonQuery();

                form1.UpdateTableData();
                this.Hide();
            }
            else MessageBox.Show("Назви не знайдено! Спробуйте ще раз");
        }
        private void delete_topping(SqlDataAdapter adapter)
        {
            SqlCommand d_d = new SqlCommand("DeleteTopping");
            d_d.CommandType = CommandType.StoredProcedure;
            int id = 0;

            SqlCommand search = new SqlCommand("SELECT id_topping FROM topping WHERE name = @n", dataBase.getSqlConnection());
            search.Parameters.Add("@n", SqlDbType.VarChar).Value = text_name.Text;
            adapter.SelectCommand = search;
            DataTable dataTable1 = new DataTable();
            adapter.Fill(dataTable1);

            if (dataTable1.Rows.Count > 0)
            {
                id = (int)dataTable1.Rows[0][0];
                d_d.Parameters.Add(new SqlParameter("@id_topping", SqlDbType.Int)).Value = id;

                d_d.Connection = dataBase.getSqlConnection();
                d_d.CommandText = "DeleteTopping";
                d_d.CommandType = CommandType.StoredProcedure;
                d_d.ExecuteNonQuery();

                form1.UpdateTableData();
                this.Hide();
            }
            else MessageBox.Show("Назви не знайдено! Спробуйте ще раз");
        }
        private void delete_worker(SqlDataAdapter adapter)
        {
            SqlCommand d_d = new SqlCommand("DeleteWorker");
            d_d.CommandType = CommandType.StoredProcedure;
            int id = 0;

            SqlCommand search = new SqlCommand("SELECT id_worker FROM worker WHERE name = @n", dataBase.getSqlConnection());
            search.Parameters.Add("@n", SqlDbType.VarChar).Value = text_name.Text;
            adapter.SelectCommand = search;
            DataTable dataTable1 = new DataTable();
            adapter.Fill(dataTable1);

            if (dataTable1.Rows.Count > 0)
            {
                id = (int)dataTable1.Rows[0][0];
                d_d.Parameters.Add(new SqlParameter("@id_worker", SqlDbType.Int)).Value = id;

                d_d.Connection = dataBase.getSqlConnection();
                d_d.CommandText = "DeleteWorker";
                d_d.CommandType = CommandType.StoredProcedure;
                d_d.ExecuteNonQuery();

                form1.UpdateTableData();
                this.Hide();
            }
            else MessageBox.Show(text_name.Text);
        }

        //заповнення полів зміни
        private void full()
        {
            if(value != "")
            {
                dataBase.openConnection();
                string query = "SELECT * FROM dessert WHERE name = @name";

                switch (tab)
                {
                    case 1:
                        {
                            query = "SELECT * FROM dessert WHERE name = @name";
                        }; break;
                    case 2:
                        {
                            query = "SELECT * FROM drink WHERE name = @name";
                        }; break;
                    case 3:
                        {
                            query = "SELECT * FROM topping WHERE name = @name";
                        }; break;
                    case 4:
                        {
                            query = "SELECT * FROM worker WHERE name = @name";
                        }; break;
                }

                SqlCommand search = new SqlCommand(query, dataBase.getSqlConnection());

                search.Parameters.Add("@name", SqlDbType.VarChar).Value = value;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = search;
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                //MessageBox.Show(dataTable.Rows.Count.ToString()+", "+ dataTable.Columns.Count.ToString());
                //text[0].Text = dataTable.Rows[0][4].ToString();

                if (dataTable.Rows.Count > 0)
                {
                    text_name.Text = dataTable.Rows[0][1].ToString();
                    for (int i = 1; i < dataTable.Columns.Count && i < dataTable.Columns.Count; i++)
                    {
                        text[i-1].Text = dataTable.Rows[0][i].ToString().Trim();
                    }
                }
            }

        }
    }
}
