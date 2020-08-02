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
            print_year();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //INSERT INTO `statisticsdata` (`year`, `own`, `state`, `foregn`, `other`, `sum_all`, `vvp`) VALUES ('2008', '4526', '458', '4523', '6363', '12548', '125566');
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
                textBox6.Text = Convert.ToString(Convert.ToInt32(reader[0]) + Convert.ToInt32(reader[1]) + Convert.ToInt32(reader[2]) + Convert.ToInt32(reader[3]));
                textBox7.Text = reader[4].ToString();
            }
            db.closeConn();
        }

        static int colRow = col_rowDB();

        int[] data_year = new int[colRow];
        public void print_year()
        {
            DB db = new DB();
            SQLiteCommand comm = new SQLiteCommand("SELECT Year FROM 'statisticsData'", db.getConn());
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
            SQLiteCommand comm = new SQLiteCommand("SELECT * FROM 'statisticsData'", db.getConn());

            adapter.SelectCommand = comm;
            adapter.Fill(table);
            return table.Rows.Count;
        }
    }
}
