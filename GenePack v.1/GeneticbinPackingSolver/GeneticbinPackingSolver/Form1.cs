using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneticbinPackingSolver
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        public string[] itemsName;
        public string[] DataBaseHight;
        public string[] DataBaseWidth;
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = tableCollection[comboBox2.SelectedItem.ToString()];
            dataGridView1.DataSource = dt;
            itemsName = dt.Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();
            DataBaseHight = dt.Rows.OfType<DataRow>().Select(k => k[1].ToString()).ToArray();
            DataBaseWidth = dt.Rows.OfType<DataRow>().Select(k => k[2].ToString()).ToArray();


        }

        DataTableCollection tableCollection;
        private void DBbtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel Workbook |*.xlsx|Excel 97-2003 workbook|*.xls" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    dataBaseFileName.Text = openFileDialog.FileName;
                    using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                            });

                            tableCollection = result.Tables;
                            comboBox2.Items.Clear();
                            foreach (DataTable table in tableCollection)
                                comboBox2.Items.Add(table.TableName);
                        }
                    }
                }
            }

            dataGridView1.Visible = true;
            comboBox2.Visible = true; 
        }
        public string[] ordername;
        public string[] orderQuantity;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt2 = tableCollection2[comboBox1.SelectedItem.ToString()];
            ordername = dt2.Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();
            orderQuantity = dt2.Rows.OfType<DataRow>().Select(k => k[1].ToString()).ToArray();
            dataGridView2.DataSource = dt2;
        }
        DataTableCollection tableCollection2;
        private void OrderBtn_Click(object sender, EventArgs e)
        {
            comboBox1.Visible = true;

            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel Workbook |*.xlsx|Excel 97-2003 workbook|*.xls" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    orderFileName.Text = openFileDialog.FileName;
                    using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                            });

                            tableCollection2 = result.Tables;
                            comboBox1.Items.Clear();
                            foreach (DataTable table in tableCollection2)
                                comboBox1.Items.Add(table.TableName);
                        }
                    }
                }
            }
            dataGridView2.Visible = true;

        }
        public static int n;
        public static string[] productNames;
        public static string[] productHights;
        public static string[] productWidths;

        public static string binH;
        public static string binW;

        private void SolveBtn_Click(object sender, EventArgs e)
        {
            


            n = SettingQuantity(orderQuantity);
            productNames = settingnames(n, ordername , orderQuantity);
            productHights = settingDims(n, productNames, itemsName, DataBaseHight);
            productWidths = settingDims(n, productNames, itemsName, DataBaseWidth);
            binH = textBox3.Text;
            binW = textBox4.Text;

            Form2 f2 = new Form2();
            f2.Show();

            
            // dims form form 1 
            /*
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("dims from form 1 ");
                Console.Write(productNames[i] + "  " + productHights[i] + "  " + productWidths[i]);
                Console.WriteLine();
            }
            */ 
        }


        public static string[] settingDims (int n , string [] productNames , string [] itemsInDB , string [] dimInDB )
        {
            string[] d = new string[n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < itemsInDB.Length; j++)
                {
                    if (productNames[i] == itemsInDB[j])
                    {
                        d[i] = dimInDB[j];
                    }
                }
            }
            return d;
        }

        public static string[] settingnames(int n, string[] names, string[] qu)
        {
            string[] x = new string[n];
            int i = 0;
            int j = 0;
            int[] q = new int[qu.Length];

            for (int k = 0; k < q.Length; k++)
                q[k] = Convert.ToInt32(qu[k]);
            while (i < n)
            {
                x[i] = names[j];
                q[j]--;
                if (q[j] == 0)
                    j++;
                i++;
            }
            /*
            Console.WriteLine("they are : ");
            for(int k = 0; k < n; k++)
                Console.Write(x[k]+"  ");
                */
            return x;
        }

            public static int SettingQuantity(string [] q )
        {
            int n = 0 ;
            int value;
            
            for (int i = 0; i < q.Length; i ++)
            {
                if (int.TryParse(q[i].Trim(), out value))
                {
                    n = n + Convert.ToInt32(q[i]);
                }
            }


            return n; 
        }

        private void exit_btn_Click(object sender, EventArgs e)
        {
            Application.Exit();
            
        }
    }
}
