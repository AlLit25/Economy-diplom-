using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            print_year();
            print_data_own();
            print_data_state();
            print_data_foregn();
            print_data_others();
            print_data_all();
            print_data_vvp();
            create_Series_Chart2();



        }
        #region functions data

        int[] data_year = new int[11];
        public void print_year()
        {
            label10.Text = "";

            for (int i = 0; i < data_year.Length; i++)
            {
                int index = 2009;
                data_year[i] = index+i;
                label10.Text += data_year[i] + Environment.NewLine + Environment.NewLine;
                comboBox1.Items.Add(data_year[i]);
            }
        }
        double[] data_own = new double[11];
        public void print_data_own()
        {
            label11.Text = "";
            Random rand = new Random();
            for (int i = 0; i < data_own.Length; i++)
            {
                data_own[i] = Math.Round(rand.Next(4700, 21500) + rand.NextDouble(), 1);
                label11.Text += data_own[i];
                label11.Text += Environment.NewLine + Environment.NewLine;
            }
        }
        double[] data_state = new double[11];
        public void print_data_state()
        {
            label12.Text = "";
            Random rand = new Random();
            for (int i = 0; i < data_state.Length; i++)
            {
                data_state[i] = Math.Round(rand.Next(24, 227) + rand.NextDouble(), 1);
                label12.Text += data_state[i];
                label12.Text += Environment.NewLine + Environment.NewLine;
            }
        }
        double[] data_foregn = new double[11];
        public void print_data_foregn()
        {
            label13.Text = "";
            Random rand = new Random();
            for (int i = 0; i < data_foregn.Length; i++)
            {
                data_foregn[i] = Math.Round(rand.Next(23, 24500) + rand.NextDouble(), 1);
                label13.Text += data_foregn[i];
                label13.Text += Environment.NewLine + Environment.NewLine;
            }
        }
        double[] data_others = new double[11];
        public void print_data_others()
        {
            label14.Text = "";
            Random rand = new Random();
            for (int i = 0; i < data_others.Length; i++)
            {
                data_others[i] = Math.Round(rand.Next(272, 6550) + rand.NextDouble(), 1);
                label14.Text += data_others[i];
                label14.Text += Environment.NewLine + Environment.NewLine;
            }
        }
        double[] data_all = new double[11];
        public void print_data_all()
        {
            label15.Text = "";
            //Random rand = new Random();
            for (int i = 0; i <= 10; i++)
            {
                data_all[i] = data_own[i] + data_state[i] + data_foregn[i] + data_others[i];
                label15.Text += data_all[i];
                label15.Text += Environment.NewLine + Environment.NewLine;
            }
        }
        double[] data_vvp = new double[11];
        public void print_data_vvp()
        {
            label16.Text = "";
            Random rand = new Random();
            for (int i = 0; i <= 10; i++)
            {
                data_vvp[i] = Math.Round(rand.Next(92500, 4800000) + rand.NextDouble(), 1);
                label16.Text += data_vvp[i];
                label16.Text += Environment.NewLine + Environment.NewLine;
            }
        }
        #endregion

        #region chart
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            chart1.Titles.Add("Дані відображені у відсотках");
            chart1.Series["S_Year"].Points.Clear();
            int index;
            index = Convert.ToInt32(comboBox1.SelectedIndex.ToString());
            //label17.Text = data_own[index].ToString();
            chart1.Series["S_Year"].IsValueShownAsLabel = true;

            double ownpercent = Math.Round(data_own[index] / (data_all[index] / 100),1);
            double statepercent = Math.Round(data_state[index] / (data_all[index] / 100), 1);
            double foregnpercent = Math.Round(data_foregn[index] / (data_all[index] / 100), 1);
            double otherspercent = Math.Round(data_others[index] / (data_all[index] / 100), 1);

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

            chart2.Series.Add(new Series("Залежність ВВП/Інвестиції")
            {
                ChartType = SeriesChartType.Point,
                Color = Color.BlueViolet
            });
            chart2.Series.Add(new Series("Лінійна регресія")
            {
                ChartType = SeriesChartType.Point,
                Color = Color.BlueViolet
            });
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            chart2.Series["Власні"].Points.Clear();
            chart2.Series["Державний бюджет"].Points.Clear();
            chart2.Series["Іноземних інвесторів"].Points.Clear();
            chart2.Series["Інщі джерела"].Points.Clear();
            chart2.Series["Залежність ВВП/Інвестиції"].Points.Clear();

            string chart_name = comboBox2.SelectedItem.ToString();

            label18.Text = "Вісь X - РІК";
            label19.Text = "Вісь Y - млн грн";
            if(chart_name == "Загальний")
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
            //Залежність ВВП від інвестицій
            if (chart_name == "Залежність ВВП/Інвестиції")
            {
                for (int i = 0; i < data_all.Length; i++)
                {
                    chart2.Series["Залежність ВВП/Інвестиції"].Points.AddXY(data_all[i], data_vvp[i]);
                }
            }
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
