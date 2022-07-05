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

namespace LFTFsolver
{
    public partial class Form1 : Form
    {
        public string[] input;
        public Form1()
        {
            InitializeComponent();
        }

        DataTableCollection TableCollection;
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel workbook|*.xlsx|Excel 97-2003 workbook|*.xls" })

            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFilename.Text = openFileDialog.FileName;
                    using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                            });
                            TableCollection = result.Tables;
                            cboSheet.Items.Clear();
                            foreach (DataTable table in TableCollection)
                                cboSheet.Items.Add(table.TableName);//add sheet to combobox 

                            
                        }
                    }
                }
            }
        }

        private void cbosheet_selected(object sender, EventArgs e)
        {
            DataTable dt = TableCollection[cboSheet.SelectedItem.ToString()];
            dataGridView1.DataSource = dt;
             input = dt.Rows.OfType<DataRow>().Select(k => k[1].ToString()).ToArray();
            

        }

        

        private void calcBtn_Click(object sender, EventArgs e)
        {
            


            // input 
            int[] arr = new int[input.Length];
            int temp;
            bool success = false;
            for (int k = 0; k<arr.Length;k++)
            {
                 success = Int32.TryParse(input[k], out temp) ;
                if (success == true)
                    arr[k] = Convert.ToInt32(input[k]);
                else
                {
                    MessageBox.Show("please select integer data");
                    break;
                }
            }

            int max = arr[0];
            int min = arr[0];
            //finding m optimal 
            double cf=0;
            double ch=0;
            double cv=0;

            success = Int32.TryParse(FCtxtbx.Text.Trim() , out temp);
            if (success == true)
                cf = Convert.ToDouble(FCtxtbx.Text.Trim());

            else
                MessageBox.Show("input correct fixed cost");

            success = Int32.TryParse(HCtxtbx.Text.Trim(), out temp);
            if (success == true)
                ch = Convert.ToDouble(HCtxtbx.Text.Trim());
            else
                MessageBox.Show("input correct hiring cost");


            success = Int32.TryParse(VCtxtbx.Text.Trim(), out temp);
            if (success == true)
                cv = Convert.ToDouble(VCtxtbx.Text.Trim());
            else
                MessageBox.Show("input correct variable cost");

            

            Console.WriteLine("arr length = " + arr.Length);
            double n = Convert.ToDouble(arr.Length);
            int mo = Convert.ToInt32(Math.Ceiling((n * cf) / (ch - cv)));

            Console.WriteLine("m optimal = "+ mo);

            // finding the length 
            for (int k = 0; k < arr.Length; k++)
            {
                if (max < arr[k])
                    max = arr[k];

                if (min > arr[k])
                    min = arr[k];
            }
            Console.WriteLine("max value = " + max + "     min value = " + min);
            int l = max - min + 1;

            //inetializing 
            Array.Sort(arr);
            Array.Reverse(arr);
            int[] vt = new int[l];
            int[] f = new int[l];
            vt[0] = arr[0]; int i;
            int j = 0;

            //freqency matrix 
            for (i = 0; i < arr.Length; i++)
            {
                if (vt[j] == arr[i])
                    f[j]++;

                else
                {
                    j++;
                    vt[j] = arr[i];
                    f[j]++;
                }

            }

            
            Console.WriteLine();
            Console.Write( "vt" + "\t");
            for (int k = 0; k < l; k++)
                Console.Write(vt[k] + "\t");
            Console.WriteLine();
            Console.Write("ft" + "\t");
            for (int k = 0; k < l; k++)
                Console.Write(f[k] + "\t");
            Console.WriteLine();
            

            // mt array
            int[] mt = new int[l];
            mt[0] = 0;

            for (int k = 1; k < l; k++)
            {
                mt[k] = mt[k - 1] + f[k - 1];
            }

           
            Console.Write("mt" + "\t");
            for (int k = 0; k < l; k++)
                Console.Write(mt[k] + "\t");
               

            // calculating V optimal

            int vo = 0;
            int vindex = 0;
            for (int k = 0; k < l; k++)
            {
                if (mt[k] >= mo)
                {
                    vo = vt[k];
                    vindex = k;
                    break;
                }
            }

            Console.WriteLine();

            // vt - vo 
            int[] vt_vo = new int[l];
            for (int k = 0; k < l; k++)
            {
                vt_vo[k] = vt[k] - vo;
                if (vt_vo[k] == 0)
                    break;
            }

            
            Console.WriteLine();
            Console.Write("vt-vo" + "\t");
            for (int k = 0; k < l; k++)
                Console.Write(vt_vo[k] + "\t");

            Console.WriteLine();
            Console.WriteLine("v optimal = " + vo);
            VoptTxtBx.Visible = true;
            VoptTxtBx.Text ="Optimal number of vehicles = " + Convert.ToString(vo);


            // calculating cost 
            double c1 = cf * vo * n;
            double c2 = cv * ((mt[vindex + 1] * vo) + sum(vt, f, vindex + 1));   
            double c3 = ch * sum(vt_vo, f, 0);
            double tc = c1 + c2 + c3;

            Console.WriteLine("vi index = " + vindex);

            Console.WriteLine( "total fixed cost = " + c1);
            Console.WriteLine("total variable cost = " + c2);
            Console.WriteLine("total hiring cost = " + c3);
            Console.WriteLine("the total cost of the problem =" + tc);
            
            tfcTxt.Visible = true;
            tvcTxt.Visible = true;
            thcTxt.Visible = true;
            tcTxt.Visible = true;



            tfcTxt.Text="Total Fixed Cost = " +'\t'+ Convert.ToString(c1);
            tvcTxt.Text = "Total Variable Cost = "+'\t' + Convert.ToString(c2);
            thcTxt.Text = "Total Hiring Cost = " + '\t' + Convert.ToString(c3);
            tcTxt.Text = "Total Transportation Cost = " + '\t' + Convert.ToString(tc);





            // comparing 
            int uv = 0;
            int uvindex = 0;
            int[] vt_uv = new int[l];
            double cc1 = 0;
            double cc2 = 0;
            double cc3 = 0;
            double tcc = 0;

            if (currentLbl.Visible == true)
            {
                uv = Convert.ToInt16(ctxtBx.Text);
                for (int k = 0; k < l; k++)
                {
                    if (vt[k] <= uv)
                    {
                        uvindex = k;
                        break;
                    }
                }

                // new used v table 
                for (int k = 0; k < l; k++)
                {
                    vt_uv[k] = vt[k] - uv;
                    if (vt_uv[k] == 0)
                        break;
                }


                
                Console.WriteLine();
                Console.Write("vt-uv" + '\t');
                for (int k = 0; k < l; k++)
                    Console.Write(vt_uv[k] + "  ");
                

                // calculating current cost 
                cc1 = cf * uv * n;
                
                cc2= cv * ((mt[uvindex + 1] * uv) + sum(vt, f, uvindex + 1));
                cc3 = ch * sum(vt_uv, f, 0);
                tcc = cc1 + cc2 + cc3;

               


                Console.WriteLine(cv* mt[uvindex + 1]);
                Console.WriteLine(cv* uv);
               


                ctfcTxt.Visible = true;
                ctvcTxt.Visible = true;
                cthcTxt.Visible = true;
                ctcTxt.Visible = true;

                ctfcTxt.Text = Convert.ToString( cc1);
                ctvcTxt.Text = Convert.ToString(cc2);
                cthcTxt.Text = Convert.ToString(cc3);
                ctcTxt.Text = Convert.ToString(tcc);

                Console.WriteLine();
                Console.WriteLine("total current fixed cost = " + cc1);
                Console.WriteLine("total variable cost = " + cc2);
                Console.WriteLine("total hiring cost = " + cc3);
                Console.WriteLine("the total cost of the problem =" + tcc);


                Console.WriteLine( cv * ((mt[uvindex + 1] * uv) + sum(vt, f, uvindex + 1)));

                Console.WriteLine("cv="+cv);
                Console.WriteLine("uv=" + uv);
                Console.WriteLine("mt[uvindex + 1]=" + mt[uvindex + 1]);
                Console.WriteLine("sum=" +sum(vt, f, uvindex + 1));
                Console.WriteLine(uvindex + 1);







                // the comparestion
                coTxt.Visible = true;
                if (tc >= tcc)
                {
                    coTxt.Text = "the current number of vehicles is optimal";
                }
                else
                {
                    coTxt.Text = "the perposed soltion will save "+'\t' + (tcc-tc)+'\t' + "  cost unit of transportation costs with a delta equal to " + Math.Round( 100*((tcc-tc)/tcc),3)+"%";
                }

            }
            else
            {
                Console.WriteLine("thanks");
            }

        }


        static double sum(int[] vt, int[] f, int l)
        {
            int count = 0;
            for (int i = l; i < f.Length; i++)
            {
                count = count + vt[i] * f[i];
            }

            return Convert.ToDouble(count);

        }

        private void CompRevBtn_Click(object sender, EventArgs e)
        {
            currentLbl.Visible = true;
            ctxtBx.Visible = true;
            // ozymandias 7,5,22
        }

        
    }
}
                  
