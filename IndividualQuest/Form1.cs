using System;
using System.IO;
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
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //save modifed array in the user file
                FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);

                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    sw.WriteLine(dataGridView2.Rows[i].Cells[0].Value);
                sw.Close();
                MessageBox.Show("Данные сохранены!");
            }
        }
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //read array in the file
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                StreamReader sr = File.OpenText(openFileDialog1.FileName);

                string line = sr.ReadLine();

                int fileArrayLength = 0;

                while (line != null)
                {
                    fileArrayLength++;
                    line = sr.ReadLine();
                }

                dataGridView1.RowCount = fileArrayLength;
                dataGridView2.RowCount = fileArrayLength;

                sr = File.OpenText(openFileDialog1.FileName);

                line = sr.ReadLine();

                int l = 0;

                while (line != null)
                {
                    l++;
                    dataGridView1.Rows[l - 1].Cells[0].Value = line;
                    line = sr.ReadLine();
                }

                sr.Close();

                //array filling
                double[] arr = new double[fileArrayLength];
                for (int i = 0; i < fileArrayLength; i++)
                    arr[i] = Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value);

                //find value in file Array
                int count = 0, geometricMean = 1;

                for (int i = 0; i < fileArrayLength; i++)
                {
                    int j = (int)arr[i];
                    if (j < 0)
                    {
                        count++;
                        geometricMean *= j;
                    }
                }

                for (int i = 0; i < fileArrayLength; i++)
                    if (i % 2 - 1 == 0 & arr[i] > 0)
                        arr[i] = Math.Pow(geometricMean, (double)1 / count);

                //output modifed array
                for (int i = 0; i < fileArrayLength; i++)
                {
                    dataGridView2.Rows[i].Cells[0].Value = Convert.ToString(arr[i]);

                    //array's numbering
                    dataGridView1.Rows[i].HeaderCell.Value = i + 1 + "";
                    dataGridView2.Rows[i].HeaderCell.Value = i + 1 + "";
                }
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

            //output array
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

                //array's numbering
                dataGridView1.Rows[i].HeaderCell.Value = i + 1 + "";
                dataGridView2.Rows[i].HeaderCell.Value = i + 1 + "";
            }
        }
    }
}