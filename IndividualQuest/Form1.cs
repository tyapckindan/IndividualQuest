using System;
using System.Windows.Forms;

namespace IndividualQuest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.RowCount = 30;
            dataGridView1.ColumnCount = 1;
            dataGridView2.RowCount = 30;
            dataGridView2.ColumnCount = 1;

            double[] arr = new double[30];
            Random r = new Random();
            for (int i = 0; i < 30; i++)
                arr[i] = r.Next(-100, 100);

                for (int i = 0; i < 30; i++)
                    dataGridView1.Rows[i].Cells[0].Value = Convert.ToString(arr[i]);

            int count = 0, geometricMean = 1;

            for (int i1 = 0; i1 < arr.Length; i1++)
            {
                int i = (int)arr[i1];
                if (i < 0)
                {
                    count++;
                    geometricMean *= i;
                }
            }

            for (int i = 0; i < arr.Length; i++)
                if (i % 2 - 1 == 0 & arr[i] > 0)
                    arr[i] = Math.Pow(geometricMean, (double)1 / count);

            for (int i = 0; i < 30; i++)
                dataGridView2.Rows[i].Cells[0].Value = Convert.ToString(arr[i]);
        }
    }
}