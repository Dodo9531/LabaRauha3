using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWS3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
        }
        public int Factorial(int n)
        {
            var factorial = 1;
            for (int i = 1; i <= n; i++)
                factorial *= i;

            return factorial;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            Random random = new Random(); 
            double λ = 1;
            double t = Double.Parse(textBox1.Text);
            double numExperiments = Double.Parse(textBox2.Text);
            List<double> list = new List<double>(); // Счётчик количества отказов
            List<int> hits = new List<int>(); // Отказы в каждом эксперементе
            for (int i = 0; i < numExperiments; i++) // Просчёт кол-ва отказов в каждом эксперементе
            {
                double expt = (-Math.Log(1 - random.NextDouble()) / λ);
                hits.Add(0);
                if (expt < t)
                {
                    double a = expt;
                    while(a < t)
                    {
                        hits[i]++;
                        a += (-Math.Log(1 - random.NextDouble()) / λ);
                    }
                }
            }
            int maxHits = hits.Max();
            for (int i = 0; i < maxHits; i++) //Заполнение счётчика количества отказов
            {
                list.Add(0);
                for(int j = 0; j < hits.Count; j++)
                {
                    if (hits[j] == i)
                    {
                        list[i]++;
                    }
                }
                list[i] /= numExperiments;
            }
            
          //  double interval = (double) maxHits / 20;
            int c = 0;
            while (c <= maxHits)
            {
                
                for (int i = 0; i < maxHits; i++)
                {
                    chart1.Series[0].Points.AddXY(i, list[i]);
                    double counter = Math.Pow(λ * t, c) / Factorial(c) * Math.Exp(-λ * t);
                    chart1.Series[1].Points.AddXY(c, counter);
                }
                //c += interval;
                c++;
            }
        }
    }
}
