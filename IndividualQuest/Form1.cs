using System;
using System.Windows.Forms;

namespace IndividualQuest
{
    public partial class Form1 : Form
    {
        const int MaxArrayLength = 30, MinValueArray = -100, MaxValueArray = 100;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.RowCount = MaxArrayLength;
            dataGridView1.ColumnCount = 1;
            dataGridView2.RowCount = MaxArrayLength;
            dataGridView2.ColumnCount = 1;

            //array filling
            double[] arr = new double[MaxArrayLength];
            Random r = new Random();
            for (int i = 0; i < MaxArrayLength; i++)
                arr[i] = r.Next(MinValueArray, MaxValueArray);

            // output array
                for (int i = 0; i < MaxArrayLength; i++)
                    dataGridView1.Rows[i].Cells[0].Value = Convert.ToString(arr[i]);

            //find value in array
            int count = 0, geometricMean = 1;

            for (int i = 0; i < arr.Length; i++)
            {
                int j = (int)arr[i];
                if (j < 0)
                {
                    count++;
                    geometricMean *= j;
                }
            }

            for (int i = 0; i < arr.Length; i++)
                if (i % 2 - 1 == 0 & arr[i] > 0)
                    arr[i] = Math.Pow(geometricMean, (double)1 / count);

            //output modifed array
            for (int i = 0; i < MaxArrayLength; i++)
                dataGridView2.Rows[i].Cells[0].Value = Convert.ToString(arr[i]);
        }
    }
}