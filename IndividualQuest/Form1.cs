using System;
using System.IO;
using System.Linq;
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
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                label1.Text = openFileDialog1.FileName;

                StreamReader sr = File.OpenText(openFileDialog1.FileName);

                string line = sr.ReadLine();

                int fileArrayLength = 0;
                while (line != null)
                {
                    fileArrayLength++;
                    line = sr.ReadLine();
                }

                dataGridView1.RowCount = fileArrayLength;

                sr.Close();

                StreamReader sri = File.OpenText(openFileDialog1.FileName);

                line = sri.ReadLine();

                int l = 0;

                while (line != null)
                {
                    l++;
                    dataGridView1.Rows[l - 1].Cells[0].Value = line;
                    line = sri.ReadLine();
                }

                sri.Close();

                for (int i = 0; i < l; i++)
                    dataGridView1.Rows[i].HeaderCell.Value = i + 1 + "";
            }
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
            {
                dataGridView2.Rows[i].Cells[0].Value = Convert.ToString(arr[i]);

                // array's numbering
                dataGridView1.Rows[i].HeaderCell.Value = i + 1 + "";
                dataGridView2.Rows[i].HeaderCell.Value = i + 1 + "";
            }
        }
    }
}