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

        private void SaveExcelToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //App
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook ExcelWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet;

            //Book
            ExcelWorkBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value);

            //Table
            ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);

            for (int i = 0; i < dataGridView2.Rows.Count; i++)
                for (int j = 0; j < dataGridView2.ColumnCount; j++)
                    ExcelApp.Cells[i + 1, j + 1] = dataGridView2.Rows[i].Cells[j].Value;

            //Calling the created excel
            ExcelApp.Visible = true;
            ExcelApp.UserControl = true;
        }

        private void OpenExcelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Файл Excel|*.XLSX;*.XLS";
            opf.ShowDialog();
            System.Data.DataTable tb = new System.Data.DataTable();
            string filename = opf.FileName;
 
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook ExcelWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet;
 
            ExcelWorkBook = ExcelApp.Workbooks.Open(filename, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false,
                false, 0, true, 1, 0);
            ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);

            for (int i = 1; i < MaxArrayLength + 1; i++)
                dataGridView1.Rows[i - 1].Cells[0].Value = ExcelApp.Cells[i, 1].Value;
        }

        private void OutputArrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView2.RowCount = MaxArrayLength;
            dataGridView2.ColumnCount = 1;

            double[] arr = new double[MaxArrayLength];

            for (int i = 0; i < MaxArrayLength; i++)
                arr[i] = Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value);

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

        private void NumberingArrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //array's numbering
            for (int i = 0; i < MaxArrayLength; i++)
            {
                dataGridView1.Rows[i].HeaderCell.Value = i + 1 + "";
                dataGridView2.Rows[i].HeaderCell.Value = i + 1 + "";
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //read array in the file
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                StreamReader sr = File.OpenText(openFileDialog1.FileName);

                string line = sr.ReadLine();

                for (int i = 0; i < MaxArrayLength; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = line;
                    line = sr.ReadLine();
                }

                sr.Close();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.RowCount = MaxArrayLength;
            dataGridView1.ColumnCount = 1;

            //array filling
            double[] arr = new double[MaxArrayLength];
            Random r = new Random();
            for (int i = 0; i < MaxArrayLength; i++)
                arr[i] = r.Next(MinValueArray, MaxValueArray);

            //output array
                for (int i = 0; i < MaxArrayLength; i++)
                    dataGridView1.Rows[i].Cells[0].Value = Convert.ToString(arr[i]);
        }
    }
}