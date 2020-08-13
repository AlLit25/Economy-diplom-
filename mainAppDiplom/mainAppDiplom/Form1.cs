using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace mainAppDiplom
{
    public partial class MainWin : Form
    {
        public MainWin()
        {
            InitializeComponent();
            print_data();
            print_data_all();

            create_Series_Chart2();

            clearLabel();


        }
        #region operation with dataDB

        static int colRow;

        int[] data_year;
        double[] data_own;
        double[] data_state;
        double[] data_foregn;
        double[] data_others;
        double[] data_all;
        double[] data_vvp;
        public void print_data()
        {
            colRow = col_rowDB();
            data_year = new int[colRow];
            data_own = new double[colRow];
            data_state = new double[colRow];
            data_foregn = new double[colRow];
            data_others = new double[colRow];
            data_all = new double[colRow];
            data_vvp = new double[colRow];

            label10.Text = "";
            label11.Text = "";
            label12.Text = "";
            label13.Text = "";
            label14.Text = "";
            label16.Text = "";


            int i = 0;
            DB db = new DB();
            SQLiteCommand comm = new SQLiteCommand("SELECT Year, Data_own, Data_State, Data_foregn, Data_others, Data_vvp FROM 'statisticsData'", db.getConn());
            db.openConn();

            SQLiteDataReader reader = comm.ExecuteReader();
            while(reader.Read())
            {
                data_year[i] = Convert.ToInt32(reader[0].ToString());
                data_own[i] = Convert.ToDouble(reader[1].ToString());
                data_state[i] = Convert.ToDouble(reader[2].ToString());
                data_foregn[i] = Convert.ToDouble(reader[3].ToString());
                data_others[i] = Convert.ToDouble(reader[4].ToString());
                data_vvp[i] = Convert.ToDouble(reader[5].ToString());

                label10.Text += data_year[i] + Environment.NewLine + Environment.NewLine;
                label11.Text += data_own[i] + Environment.NewLine + Environment.NewLine;
                label12.Text += data_state[i] + Environment.NewLine + Environment.NewLine;
                label13.Text += data_foregn[i] + Environment.NewLine + Environment.NewLine;
                label14.Text += data_others[i] + Environment.NewLine + Environment.NewLine;
                label16.Text += data_vvp[i] + Environment.NewLine + Environment.NewLine;
                comboBox1.Items.Add(data_year[i]);
                comboBox3.Items.Add(data_year[i]);
                comboBox4.Items.Add(data_year[i]);
                i++;
            }
            db.closeConn();
        }
        public void print_data_all()
        {
            label15.Text = "";
            for (int i = 0; i < colRow; i++)
            {
                data_all[i] = data_own[i] + data_state[i] + data_foregn[i] + data_others[i];
                if (data_all[i] == 0) label15.Text += "";
                else label15.Text += data_all[i];
                
                label15.Text += Environment.NewLine + Environment.NewLine;
                
            }
        }
        #endregion

        #region chart
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(chart1.Titles.Count == 0) chart1.Titles.Add("Дані відображені у відсотках");
            chart1.Series["S_Year"].Points.Clear();
            int count;
            count = Convert.ToInt32(comboBox1.SelectedIndex.ToString());
            //label17.Text = data_own[index].ToString();

            chart1.Series["S_Year"].IsValueShownAsLabel = true;
            
            double ownpercent = Math.Round(data_own[count] / (data_all[count] / 100),1);
            double statepercent = Math.Round(data_state[count] / (data_all[count] / 100), 1);
            double foregnpercent = Math.Round(data_foregn[count] / (data_all[count] / 100), 1);
            double otherspercent = Math.Round(data_others[count] / (data_all[count] / 100), 1);

            chart1.Series["S_Year"].Points.AddXY("Власні", ownpercent);
            chart1.Series["S_Year"].Points.AddXY("Державний бюджет", statepercent);
            chart1.Series["S_Year"].Points.AddXY("Іноземних інвесторів", foregnpercent);
            chart1.Series["S_Year"].Points.AddXY("Інщі джерела", otherspercent);
        }

        public void create_Series_Chart2()
        {
            chart2.Series.Add(new Series("Власні")
            {
                ChartType = SeriesChartType.Spline,
                Color = Color.Red
            });

            chart2.Series.Add(new Series("Державний бюджет")
            {
                ChartType = SeriesChartType.Spline,
                Color = Color.Green
            });

            chart2.Series.Add(new Series("Іноземних інвесторів")
            {
                ChartType = SeriesChartType.Spline,
                Color = Color.Blue
            });

            chart2.Series.Add(new Series("Інщі джерела")
            {
                ChartType = SeriesChartType.Spline,
                Color = Color.Orange
            });

            chart2.Series.Add(new Series("Залежність ВВП/Інвестицій")
            {
                ChartType = SeriesChartType.Point,
                Color = Color.BlueViolet
            });
            chart2.Series.Add(new Series("Лінійна регресія")
            {
                ChartType = SeriesChartType.Spline,
                Color = Color.Blue
            });
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            chart2.Series["Власні"].Points.Clear();
            chart2.Series["Державний бюджет"].Points.Clear();
            chart2.Series["Іноземних інвесторів"].Points.Clear();
            chart2.Series["Інщі джерела"].Points.Clear();
            chart2.Series["Залежність ВВП/Інвестицій"].Points.Clear();
            chart2.Series["Лінійна регресія"].Points.Clear();

            

            string chart_name = comboBox2.SelectedItem.ToString();

            label18.Text = "Вісь X - РІК";
            label19.Text = "Вісь Y - млн грн";
            labelY.Visible = false;
            labelRR.Visible = false;

            if (chart_name == "Загальний")
            {
                for (int i = 0; i < data_year.Length; i++)
                {
                    chart2.Series["Власні"].Points.AddXY(data_year[i], data_own[i]);
                    chart2.Series["Державний бюджет"].Points.AddXY(data_year[i], data_state[i]);
                    chart2.Series["Іноземних інвесторів"].Points.AddXY(data_year[i], data_foregn[i]);
                    chart2.Series["Інщі джерела"].Points.AddXY(data_year[i], data_others[i]);
                }
            }

            if(chart_name == "Власні")
            {
                for (int i = 0; i < data_year.Length; i++)
                {
                    chart2.Series["Власні"].Points.AddXY(data_year[i], data_own[i]);
                }
            }
            if(chart_name == "Державний бюджет")
            {
                for (int i = 0; i < data_year.Length; i++)
                {
                    chart2.Series["Державний бюджет"].Points.AddXY(data_year[i], data_state[i]);
                }
            }

            if (chart_name == "Іноземних інвесторів")
            {
                for (int i = 0; i < data_year.Length; i++)
                {
                    chart2.Series["Іноземних інвесторів"].Points.AddXY(data_year[i], data_foregn[i]);
                }
            }
            if (chart_name == "Інщі джерела")
            {
                for (int i = 0; i < data_year.Length; i++)
                {
                    chart2.Series["Інщі джерела"].Points.AddXY(data_year[i], data_others[i]);
                }
            }
            if (chart_name == "Залежність ВВП/Інвестицій")
            {
                label18.Text = "Вісь X - Загальні інвестицій";
                label19.Text = "Вісь Y - ВВП";
                labelY.Visible = true;
                labelRR.Visible = true;
                line_Reg();
                for (int i = 0; i < data_all.Length; i++)
                {
                    chart2.Series["Залежність ВВП/Інвестицій"].Points.AddXY(data_all[i], data_vvp[i]);
                    chart2.Series["Лінійна регресія"].Points.AddXY(data_all[i], y + x * data_all[i]);
                }
                labelY.Text = "y = "+x+" + "+ y+"x";
                labelRR.Text = "RR = 0," + sumXX;
            }
        }
        #endregion

        #region regresion line
        double x, y;
        int sumX, sumY;
        long sumXX, sumXY;
        public void line_Reg()
        {
            sumX = 0;
            sumY = 0;
            sumXX = 0;
            sumXY = 0;

            for (int i = 0; i < data_all.Length; i++)
            {
                sumX += Convert.ToInt32(Math.Round(data_all[i],0));
                sumY += Convert.ToInt32(Math.Round(data_vvp[i],0));
                sumXX += Convert.ToInt64(Math.Round(Math.Pow(data_all[i],2), 0));
                sumXY += Convert.ToInt64(Math.Round(data_all[i]*data_vvp[i], 0));
            }
            equation_reg(sumX,sumY,sumXX, sumXY);
        }

        public void equation_reg(int sumX, int sumY, long sumXX, long sumXY)
        {
            double delta, deltaX, deltaY;
            double tbX1Y1 = Convert.ToDouble(data_all.Length),
                tbX2Y1 = Convert.ToDouble(sumX),
                tbX1Y2 = Convert.ToDouble(sumX),
                tbX2Y2 = Convert.ToDouble(sumXX),
                tbResult1 = Convert.ToDouble(sumY),
                tbResult2 = Convert.ToDouble(sumXY);

            //  |x1y1 | x2y1 | result1 |
            //  |-----|------|---------|
            //  |x1y2 | x2y2 | result2 |

            delta = (tbX1Y1 * tbX2Y2) - (tbX1Y2 * tbX2Y1);
            deltaX = (tbResult1 * tbX2Y2) - (tbResult2 * tbX2Y1);
            deltaY = (tbX1Y1 * tbResult2) - (tbX2Y1 * tbResult1);

            y = Math.Round(deltaX / delta,0);
            x = Math.Round(deltaY / delta, 3);

            //label17.Text = y + "x + " + x + " | " + tbX1Y1 + " | " + tbX1Y2 + " | " + tbX2Y1 + " | " + tbX2Y2 + " | " + tbResult1 + " | " + tbResult2;
        }

        #endregion

        #region percent growth
        public void clearLabel()
        {
            label21.Text = "";
            label22.Text = "";
            label23.Text = "";
            label24.Text = "";
            label25.Text = "";
            label26.Text = "";
        }

        public void calculation()
        {
            double ownS=0, ownE=0, stateS=0, stateE=0, foregnS=0, foregnE=0, othersS=0, othersE=0, vvpS=0, vvpE=0;
            if (comboBox3.SelectedItem != null && comboBox4.SelectedItem != null)
            {
                if (comboBox3.SelectedIndex < comboBox4.SelectedIndex)
                {
                    DB db = new DB();
                    SQLiteCommand startYear = new SQLiteCommand("SELECT Data_own, Data_State, Data_foregn, Data_others, Data_vvp FROM 'statisticsData' WHERE Year =" + comboBox3.SelectedItem.ToString(), db.getConn());
                    SQLiteCommand endYear = new SQLiteCommand("SELECT Data_own, Data_State, Data_foregn, Data_others, Data_vvp FROM 'statisticsData' WHERE Year =" + comboBox4.SelectedItem.ToString(), db.getConn());
                    db.openConn();
                    SQLiteDataReader readerSY = startYear.ExecuteReader();
                    while (readerSY.Read())
                    {
                        ownS = Convert.ToDouble(readerSY[0]);
                        stateS = Convert.ToDouble(readerSY[1]);
                        foregnS = Convert.ToDouble(readerSY[2]);
                        othersS = Convert.ToDouble(readerSY[3]);
                        vvpS = Convert.ToDouble(readerSY[4]);
                    }
                    SQLiteDataReader readerEY = endYear.ExecuteReader();
                    while (readerEY.Read())
                    {
                        ownE = Convert.ToDouble(readerEY[0]);
                        stateE = Convert.ToDouble(readerEY[1]);
                        foregnE = Convert.ToDouble(readerEY[2]);
                        othersE = Convert.ToDouble(readerEY[3]);
                        vvpE = Convert.ToDouble(readerEY[4]);
                    }
                    db.closeConn();

                    label21.Text = Convert.ToString(Math.Round(100 - (ownS / (ownE / 100)), 1));
                    label22.Text = Convert.ToString(Math.Round(100 - (stateS / (stateE / 100)), 1));
                    label23.Text = Convert.ToString(Math.Round(100 - (foregnS / (foregnE / 100)), 1));
                    label24.Text = Convert.ToString(Math.Round(100 - (othersS / (othersE / 100)), 1));
                    label25.Text = Convert.ToString(Math.Round(Convert.ToDouble(label21.Text) +
                        Convert.ToDouble(label22.Text) +
                        Convert.ToDouble(label23.Text) +
                        Convert.ToDouble(label24.Text), 1)) + "%";
                    label26.Text = Convert.ToString(Math.Round(100 - (vvpS / (vvpE / 100)), 1)) + "%";
                    label21.Text += "%";
                    label22.Text += "%";
                    label23.Text += "%";
                    label24.Text += "%";
                }
                else MessageBox.Show("Перевірте коректність послідовності обраних років!"+ Environment.NewLine + 
                    "(Необхідна послідовність від меншого до більшого)");
            }
        }
        #endregion
        private void button3_Click(object sender, EventArgs e)
        {
            label10.Text = "";
            label11.Text = "";
            label12.Text = "";
            label13.Text = "";
            label14.Text = "";
            label15.Text = "";
            label16.Text = "";
            comboBox1.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();

            col_rowDB();
            print_data();
            print_data_all();
            label17.Text = col_rowDB().ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            help h = new help();
            h.ShowDialog();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            calculation();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            calculation();
        }

        public static int col_rowDB()
        {
            DB db = new DB();
            //DataTable table = new DataTable();
            //SQLiteDataAdapter adapter = new SQLiteDataAdapter();
            SQLiteCommand comm = new SQLiteCommand("SELECT * FROM 'statisticsData'", db.getConn());

            //adapter.SelectCommand = comm;
            //adapter.Fill(table);
            db.openConn();
            int i = 0;
            SQLiteDataReader reader = comm.ExecuteReader();
            while (reader.Read()) i++;
            db.closeConn();
                return i;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            edit editForm = new edit();
            editForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
