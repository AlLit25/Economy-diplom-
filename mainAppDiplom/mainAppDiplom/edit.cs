using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mainAppDiplom
{
    public partial class edit : Form
    {
        public edit()
        {
            InitializeComponent();
            textBox1.Text = "0";
            textBox2.Text = "0";
            textBox3.Text = "0";
            textBox4.Text = "0";
            textBox5.Text = "0";
            textBox7.Text = "0";
            label9.Text = "";
            print_year();
        }

        //add new entry/edit entry
        private void button1_Click(object sender, EventArgs e)
        {
            string insert = "INSERT INTO statisticsData (Year, Data_own, Data_State, Data_foregn, Data_others, Data_vvp) VALUES ('"+textBox1.Text+"', '"+textBox2.Text+"', '"+ textBox3.Text + "', '"+ textBox4.Text + "', '"+ textBox5.Text + "', '"+ textBox7.Text + "')";
            string update = "UPDATE statisticsData SET Data_own = "+ textBox2.Text + ", Data_State = "+ textBox3.Text + ", Data_foregn = "+ textBox4.Text + ", Data_others = "+ textBox5.Text + ", Data_vvp = "+textBox7.Text + " WHERE Year = " + textBox1.Text;
            string save = "SAVEPOINT \"RESTOREPOINT\"";
            DB db = new DB();
            DataTable table = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter();
            SQLiteCommand comm = new SQLiteCommand("SELECT * FROM 'statisticsData' WHERE Year = "+textBox1.Text, db.getConn());

            adapter.SelectCommand = comm;
            adapter.Fill(table);
            if (table.Rows.Count == 0)
            {
                
                SQLiteCommand insertQuery = new SQLiteCommand(insert, db.getConn());
                
                db.openConn();
                if (insertQuery.ExecuteNonQuery() == 1) MessageBox.Show("Додано новий запис");
                else MessageBox.Show("Виникли помилки при роботі з базою даних");
                db.closeConn();
            }
            else
            {
                SQLiteCommand updateQuery = new SQLiteCommand(update, db.getConn());
                SQLiteCommand saveQuery = new SQLiteCommand(save, db.getConn());
                db.openConn();
                //updateQuery.ExecuteNonQuery();
                if (updateQuery.ExecuteNonQuery() == 1)
                {
                    saveQuery.ExecuteNonQuery();
                    MessageBox.Show("Данні змінено");
                    
                }
                else MessageBox.Show("Виникли помилки при роботі з базою даних");

                db.closeConn();
            }
            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int year;
            year = Convert.ToInt32(comboBox1.SelectedItem.ToString());
            textBox1.Text = year.ToString();

            DB db = new DB();
            SQLiteCommand comm = new SQLiteCommand("SELECT Data_own, Data_State, Data_foregn, Data_others, Data_vvp FROM 'statisticsData' WHERE Year = "+year, db.getConn());
            db.openConn();
            SQLiteDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                textBox2.Text = reader[0].ToString();
                textBox3.Text = reader[1].ToString();
                textBox4.Text = reader[2].ToString();
                textBox5.Text = reader[3].ToString();
                label9.Text = Convert.ToString(Convert.ToInt32(reader[0]) + Convert.ToInt32(reader[1]) + Convert.ToInt32(reader[2]) + Convert.ToInt32(reader[3]));
                textBox7.Text = reader[4].ToString();
            }
            db.closeConn();
        }

        static int colRow;
        
        int[] data_year;
        public void print_year()
        {
            colRow = col_rowDB();
            data_year = new int[colRow];
            comboBox1.Items.Clear();
            DB db = new DB();
            SQLiteCommand comm = new SQLiteCommand("SELECT Year FROM statisticsData", db.getConn());
            db.openConn();
            SQLiteDataReader reader = comm.ExecuteReader();
            for (int i = 0; reader.Read() == true; i++)
            {
                data_year[i] = Convert.ToInt32(reader[0].ToString());
                comboBox1.Items.Add(data_year[i]);
            }
            db.closeConn();
        }

        public static int col_rowDB()
        {
            DB db = new DB();
            DataTable table = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter();
            SQLiteCommand comm = new SQLiteCommand("SELECT * FROM statisticsData", db.getConn());

            adapter.SelectCommand = comm;
            adapter.Fill(table);
            return table.Rows.Count;
        }

        //delete entry
        private void button2_Click(object sender, EventArgs e)
        {
            string delete = "DELETE FROM statisticsData WHERE Year = " + textBox1.Text;
            DB db = new DB();
            SQLiteCommand deleteQuery = new SQLiteCommand(delete, db.getConn());

            db.openConn();
            if (deleteQuery.ExecuteNonQuery() == 1) MessageBox.Show("Запис видалено з бази даних");
            else MessageBox.Show("Запис не видалено! Помилка роботи з БД");

            textBox1.Text = "0";
            textBox2.Text = "0";
            textBox3.Text = "0";
            textBox4.Text = "0";
            textBox5.Text = "0";
            label9.Text = "";
            textBox7.Text = "0";

            print_year();
        }

        public void All_sum()
        {
            label9.Text = Convert.ToString(Convert.ToInt64(textBox2.Text) + Convert.ToInt64(textBox3.Text) + Convert.ToInt64(textBox4.Text) + Convert.ToInt64(textBox5.Text));
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44)
            {
                e.Handled = true;
            }
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44)
            {
                e.Handled = true;
            }
        }
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44)
            {
                e.Handled = true;
            }
        }
        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44)
            {
                e.Handled = true;
            }
        }
        private void textBox2_Leave(object sender, EventArgs e)
        {
            All_sum();
        }
        private void textBox3_Leave(object sender, EventArgs e)
        {
            All_sum();
        }
        private void textBox4_Leave(object sender, EventArgs e)
        {
            All_sum();
        }
        private void textBox5_Leave(object sender, EventArgs e)
        {
            All_sum();
        }

        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44)
            {
                e.Handled = true;
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44)
            {
                e.Handled = true;
            }
        }
    }
}
