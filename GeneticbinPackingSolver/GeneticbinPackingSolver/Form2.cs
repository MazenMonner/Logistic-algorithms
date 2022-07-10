using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneticbinPackingSolver
{
    public partial class Form2 : Form
    {
       
      


        public Form2()
        {
            InitializeComponent();
           

        }
        public static int n;
        public static string[] names;
        public static string[] bhight;
        public static string[] bwidth;
        public static string hight;
        public static string width;

        public static bool change;
        public static int numOfGemerations = 1;
        private void Form2_Load(object sender, EventArgs e)
        {
            // input gathered 
            n = Form1.n;
            names = Form1.productNames;
            bhight = Form1.productHights;
            bwidth = Form1.productWidths;
            hight = Form1.binH;
            width = Form1.binW;

            


        }

       

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < n; i++)
            {
                Console.Write(names[i] + "  " + bhight[i] + "  " + bwidth[i]);
                Console.WriteLine();
            }

            int h = Convert.ToInt32(hight);
            int w = Convert.ToInt32(width);
            int[] bh = transTointarr(bhight);
            int[] bw = transTointarr(bwidth);
            bhClone = Loop(bh);
            bwClone = Loop(bw);

            Console.WriteLine("hight = " + h + "     width = " + w);
            // first generation 
            int populationSize = 5;
            string[] genePool = poolMaker(n, populationSize, r);
            double[] fitnessMatrix = new double[genePool.Length];
            for (int i = 0; i < genePool.Length; i++)
            {
                string[,,] sol = huristic(names, h, w, bhClone, bwClone, genePool[i]);
                fitnessMatrix[i] = fitCalc(sol);

                Console.WriteLine((i + 1) + "  " + genePool[i] + "   " + fitnessMatrix[i] + "   " + usedBins);
            }

            // evaluation of each indvidual 
            string localOptimal = recordBest(genePool, fitnessMatrix);
            string newLocalOptimal = "";
            string currentOpt = "";

            // termination
            double bestcaseSenario = bestcase();

            for (int i = 0; i < numOfGemerations; i++)
            {  // new generation  

                Console.WriteLine("=========================");
                genePool = newGeneration(genePool, fitnessMatrix);
                for (int j = 0; j < genePool.Length; j++)
                {
                    string[,,] sol = huristic(names, h, w, bhClone, bwClone, genePool[j]);
                    fitnessMatrix[j] = fitCalc(sol);

                    //Console.WriteLine((j + 1) + "  " + genePool[j] + "   " + fitnessMatrix[j] + "   " + usedBins);
                }

                newLocalOptimal = recordBest(genePool, fitnessMatrix);

                if (usedBins <= bestcaseSenario)
                    break; 

                /*
                Console.WriteLine();        
                printString(ons);
                print(oxx);
                print(oyy);
                print(ozz);
                print(obh);
                print(obw);    
    
                */
                // localOptimal = Compare(localOptimal, newLocalOptimal, sample);
            }


            label1.Text = string.Join(", ", oxx);
            label2.Text = string.Join(", ", oyy);
           label3.Text = string.Join(", ", ozz);

            
            label4.Text = Convert.ToString( oxx.Length);

            paintBins(e);
            paintBoxes(e , panel1);
        }

        // painting functions 

        public static void paintBoxes(PaintEventArgs e , Panel pan )
        {
            int s = 5;
            Pen blkpen = new Pen(Color.Black);
            Rectangle r = new Rectangle();
            for (int i = 0; i < n ; i++)
            {
                r = new Rectangle((ozz[i] * (Convert.ToInt32(width) + s) + oxx[i]), oyy[i], obh[i], obw[i]);
                e.Graphics.DrawRectangle(blkpen, r);

               
                Label l = new Label();
                l.BackColor = Color.Transparent;
                l.Location = new Point((ozz[i] * (Convert.ToInt32(width) + s) + oxx[i]), oyy[i]+5);
                l.Text = ons[i];
                pan.Controls.Add(l);
                pan.Show();


            }
        }
        public static void paintBins(PaintEventArgs e)
        {
            

            float s = 5f;
            Pen blkpen = new Pen(Color.Black);
            for (int i = 0; i < usedBins; i++)
            {
                e.Graphics.DrawRectangle(blkpen, (i*(float.Parse(hight)+s)), 0, float.Parse(hight), float.Parse(width));
            }
            Console.WriteLine("num of elements = " + n);
            Console.WriteLine("used bins = " + usedBins);
        }

        // the deep waters 

        static double mutationIndex = .2;

        static int[] xx = new int[n];  // collect x dims
        static int[] yy = new int[n];  // collect y dims
        static int[] zz = new int[n];  // collect z dims (which bin ? ) *** extreme importence because we make gene later

        static string[] ns = new string[n]; //  name sorting 
        static string[] ons = new string[n]; // optimal name sorting 

        static int[] obh = new int[n];
        static int[] obw = new int[n];

        static int[] oxx = new int[n];  // collect x dims
        static int[] oyy = new int[n];  // collect y dims
        static int[] ozz = new int[n];

        // fitness measures
        static int usedBins;
        static double fitness;

        static int[] bhClone = new int[n];
        static int[] bwClone = new int[n];

        public static double bestcase()
        {
            double b = 0;

            for (int i = 0; i < n; i++)
            {
                b = b + bhClone[i] * bwClone[i];
            }


            return  Math.Ceiling( (double)b / ( Convert.ToInt32( width) * Convert.ToInt32(hight)) );
        }

        public static void printString(string[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
                Console.Write(arr[i] + "    ");
            Console.WriteLine();
        }
        public static void print(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
                Console.Write(arr[i] + "    ");
            Console.WriteLine();
        }

        public static string[] newGeneration(string[] genePool, double[] fitnessMatrix)
        {
            int cuttingPoint = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(genePool[0].Length / 2)));
            string[] newGen = new string[genePool.Length];

            for (int i = 0; i < genePool.Length; i++)
            {
                // creating parents 
                string[] parents = matchRoulette(genePool, fitnessMatrix, r);
                // mating 
                string newGene = CrossOver(parents, cuttingPoint, r);
                newGen[i] = newGene;
            }

            Console.WriteLine("=========================");

            return newGen;

        }

        public static string best = "";
        public static double max = 0;
        public static string allBest = "";
        public static double allMax = 0;

        public static string recordBest(string[] genePool, double[] fitnessMatrix)
        {
            change = false;
            best = "";
            double max = 0;
            for (int i = 0; i < fitnessMatrix.Length; i++)
            {
                if (max < fitnessMatrix[i])
                {
                    max = fitnessMatrix[i];
                    best = genePool[i];
                }
            }
            Console.WriteLine("best solution in this generation  = " + best + " whose fitness = " + max);
            if (allMax < max)
            {
                change = true;
                allMax = max;
                allBest = best;
                oxx = Loop(xx);  // collect x dims
                oyy = Loop(yy);  // collect y dims
                ozz = Loop(zz);
                ons = LoopString(ns);
                obh = Loop(bhClone);
                obw = Loop(bwClone);
            }
            Console.WriteLine("best solution all over = " + allBest + "whose fitness = " + allMax);
            return best;
        }

        public static string CrossOver(string[] parents, int cuttingPoint, Random r)
        {
            double m = r.NextDouble();
            string newGene = "";

            if (m >= mutationIndex)
            {
                for (int i = 0; i < parents[0].Length; i++)
                {
                    if (i <= cuttingPoint)
                        newGene = newGene + parents[0][i];
                    else
                        newGene = newGene + parents[1][i];

                }
                //Console.WriteLine("new gene = " + newGene);
            }
            else
            {
                for (int j = 0; j < n; j++)
                {

                    int x = r.Next(0, 2);
                    newGene = newGene + Convert.ToString(x);

                }
            }
            return newGene;
        }

        public static Random r = new Random();

        public static string[] matchRoulette(string[] genePool, double[] fitnessMatrix, Random r)
        {
            double sum = Sum(fitnessMatrix);
            double[] weights = cummulative(fitnessMatrix);
            string[] parent = { " ", " " };
            int x = r.Next(0, Convert.ToInt32(sum));
            for (int i = 0; i < weights.Length; i++)
            {
                if (x < weights[i] || x == sum)
                {

                    parent[0] = genePool[i];
                    break;
                }
            }

            x = r.Next(0, Convert.ToInt32(sum));
            for (int i = 0; i < weights.Length; i++)
            {
                if (x < weights[i] || x == sum)
                {
                    parent[1] = genePool[i];
                    break;
                }
            }
            return parent;
        }

        public static double[] cummulative(double[] s)
        {
            double[] c = new double[s.Length];
            c[0] = s[0];

            for (int i = 1; i < s.Length; i++)
            {
                c[i] = s[i] + c[i - 1];
            }
            return c;
        }
        public static double Sum(double[] Arr)
        {
            double sum = 0;
            for (int i = 0; i < Arr.Length; i++)
            {
                sum = sum + Arr[i];
            }
            return sum;
        }


        public static string[] poolMaker(int n, int populationSize, Random Rand)
        {
            string gene = "";
            string[] genePool = new string[populationSize];
            for (int j = 0; j < populationSize; j++)
            {
                gene = "";
                for (int i = 0; i < n; i++)
                {
                    int x = Rand.Next(0, 2);
                    gene = gene + Convert.ToString(x);
                }
                genePool[j] = gene;
            }

            return genePool;
        }
        public static double fitCalc(string[,,] sol)
        {
            double[] sum = new double[usedBins];
            for (int i = 0; i < usedBins; i++)
            {
                sum[i] = 0;
                for (int j = 0; j < sol.GetLength(0); j++)
                {
                    for (int k = 0; k < sol.GetLength(1); k++)
                    {
                        if (sol[j, k, i] != null)
                            sum[i]++;
                    }
                }
                sum[i] = (double)sum[i] / (sol.GetLength(0) * sol.GetLength(1));

            }
            double u = 0;
            for (int i = 0; i < sum.Length; i++)
                u = u + sum[i];

            double fit = Math.Pow((double)u, 2) / usedBins;

            return fit;
        }



        public static string[,,] huristic(string[] names, int h, int w, int[] bh, int[] bw, string gene)
        {
            int binLimit = 10;
            string[,,] sol = new string[h, w, binLimit];
            int n = bh.Length;
            int temp;

            // rotation 
            for (int i = 0; i < n; i++)
            {
                if (gene[i] == '1' && (bh[i] <= w || bw[i] <= h))
                {
                    temp = bh[i];
                    bh[i] = bw[i];
                    bw[i] = temp;
                }
                else if (gene[i] == '1' && (bh[i] > w || bw[i] > h))
                {
                    return sol;
                }
            }

            // sorting 
            int[] t = Loop(bw);
            Array.Sort(bw, bh);
            Array.Sort(t, names);

            t = Loop(bh);
            Array.Sort(bh, bw);
            Array.Sort(t, names);

            Array.Reverse(t);
            Array.Reverse(bw);
            Array.Reverse(bh);

            // new vars
            int x = 0;
            int y = 0;
            int z = 0;

            xx = new int[n];  // collect x dims
            yy = new int[n];  // collect y dims
            zz = new int[n];  // collect z dims (which bin ? ) *** extreme importence because we make gene later
            bool packed;    // bool for if the item is packed yet 
            bool empty = true;
            bool byond;
            // pointer 
            for (int i = 0; i < n; i++)
            {
                z = 0;
                packed = false;
                while (packed == false)
                {
                    for (int j = 0; j < h; j++)
                    {
                        for (int k = 0; k < w; k++)
                        {
                            byond = false;
                            empty = true;
                            for (int a = j; a < j + bh[i]; a++)
                            {
                                for (int b = k; b < k + bw[i]; b++)
                                {
                                    if (a == h || b == w)
                                    {
                                        byond = true;
                                        break;
                                    }
                                    if (sol[a, b, z] != null)
                                    {
                                        empty = false;
                                        break;
                                    }
                                }
                                if (empty == false || byond == true)
                                    break;
                            }

                            if (sol[j, k, z] == null && j + bh[i] <= h && k + bw[i] <= w && empty == true)
                            { // if the curent Co-ordinations not intger (already used) and can take the hight and width 
                                x = j;
                                y = k;
                                packed = true;   // pack ; 
                                break;  // beak the H loop 

                            }
                        }
                        if (packed == true)
                        {
                            break;  // break the W Loop 
                        }
                    }

                    if (i != 0 && packed == false)
                    {
                        z++;
                    }
                    xx[i] = x;
                    yy[i] = y;
                    zz[i] = z;
                }

                for (int j = x; j < h; j++)
                {  //vasualising 
                    for (int k = y; k < w; k++)
                    {
                        if (j < x + bh[i] && k < y + bw[i])
                        {
                            sol[j, k, z] = names[i];
                        }
                    }
                }
            }
            // packing 

            ns = LoopString(names);
            // for (int i = 0; i < n; i++)
            //  Console.WriteLine("box {0} that has the dims of {1},{2} resides in bin {3} at coordinations {4},{5}",names[i],bh[i],bw[i],(zz[i]+1),xx[i],yy[i]);

            usedBins = zz.Max() + 1;
            //printall(sol);

            return sol;
        }

        static int[] Loop(int[] a)
        {
            int[] x = new int[a.Length];

            for (int i = 0; i < a.Length; i++)
            {
                x[i] = a[i];
            }

            return x;
        }

        public static string[] LoopString(string[] a)
        {
            string[] x = new string[a.Length];

            for (int i = 0; i < a.Length; i++)
            {
                x[i] = a[i];
            }

            return x;
        }
        public static void printall(string[,,] arr)
        {
            for (int i = 0; i < arr.GetLength(2); i++)
            {
                for (int j = 0; j < arr.GetLength(0); j++)
                {
                    for (int k = 0; k < arr.GetLength(1); k++)
                    {
                        Console.Write(arr[j, k, i] + "  ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        public static int[] transTointarr(string[] arr)
        {
            int[] ans = new int[arr.Length];

            for (int i = 0; i < arr.Length; i++)
                ans[i] = Convert.ToInt32(arr[i]);

            return ans;
        }

      
    }
}
