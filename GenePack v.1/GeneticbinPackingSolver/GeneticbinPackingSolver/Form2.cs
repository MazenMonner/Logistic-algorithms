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
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }
        public static int n;
        public static string[] names;
        public static string[] bhight;
        public static string[] bwidth;
        public static string hight;
        public static string width;

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
            
        }

        // painting functions 

        public static void paintBoxes(PaintEventArgs e , Panel pan )
        {
            int s = 5;
            Pen blkpen = new Pen(Color.Black);
            Rectangle r = new Rectangle();
            for (int i = 0; i < n ; i++)
            {
                r = new Rectangle((ozz[i] * (Convert.ToInt32(hight) + s) + oxx[i]), oyy[i], obh[i], obw[i]);
                e.Graphics.DrawRectangle(blkpen, r);

               
                Label l = new Label();
                l.AutoSize = true;
                l.Size = new Size(200, 32);
                l.BackColor = Color.Transparent;
                l.Location = new Point((ozz[i] * (Convert.ToInt32(hight) + s) + oxx[i]), oyy[i]+5);
                l.Text = findnames(i, obh, obw);
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

        static double mutationIndex ;

        static int[] xx = new int[n];  // collect x dims
        static int[] yy = new int[n];  // collect y dims
        static int[] zz = new int[n];  // collect z dims (which bin ? ) *** extreme importence because we make gene later

        static int[] obh = new int[n];
        static int[] obw = new int[n];

        static int[] oxx = new int[n];  // collect x dims
        static int[] oyy = new int[n];  // collect y dims
        static int[] ozz = new int[n];

        // fitness measures
        static int usedBins;

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
                allMax = max;
                allBest = best;
                oxx = Loop(xx);  // collect x dims
                oyy = Loop(yy);  // collect y dims
                ozz = Loop(zz);
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
                if (gene[i] == '1' )
                {
                    temp = bh[i];
                    bh[i] = bw[i];
                    bw[i] = temp;
                }
            }
            string[] namesclone = LoopString(names);
            // sorting 
            Array.Sort(bw, bh);
            Array.Sort(bh, bw);

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
                    for (int j = 0; j < w; j++)
                    {
                        for (int k = 0; k <h; k++)
                        {
                            byond = false;
                            empty = true;
                            for (int a = k; a < k + bh[i]; a++)
                            {
                                for (int b = j; b < j + bw[i]; b++)
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

                            if (sol[k, j, z] == null && k + bh[i] <= h && j + bw[i] <= w && empty == true)
                            { // if the curent Co-ordinations not intger (already used) and can take the hight and width 
                                x = k;
                                y = j;
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
                            sol[j, k, z] = namesclone[i];
                        }
                    }
                }
            }
            // packing 

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
        public string newLocalOptimal; 
        private void button1_Click(object sender, EventArgs e)
        {
            int populationSize =  Convert.ToInt32(textBox1.Text);
            int numOfGemerations = Convert.ToInt32(textBox2.Text);
            mutationIndex = Convert.ToDouble(textBox3.Text);

        /*
        // show me the name and dims of boxes  
        for (int i = 0; i < n; i++)
        {
            Console.Write(names[i] + "  " + bhight[i] + "  " + bwidth[i]);
            Console.WriteLine();
        }
        */

        int h = Convert.ToInt32(hight);
            int w = Convert.ToInt32(width);
            int[] bh = transTointarr(bhight);
            int[] bw = transTointarr(bwidth);
            bhClone = Loop(bh);
            bwClone = Loop(bw);

            // dims of container 
            Console.WriteLine("hight = " + h + "     width = " + w);

            // first generation 
            string[] genePool = poolMaker(n, populationSize, r);
            double[] fitnessMatrix = new double[genePool.Length];
            for (int i = 0; i < genePool.Length; i++)
            {
                string[,,] sol = huristic(names, h, w, bhClone, bwClone, genePool[i]);
                fitnessMatrix[i] = fitCalc(sol);

                // the genes and their fitness
                Console.WriteLine((i + 1) + "  " + genePool[i] + "   " + fitnessMatrix[i] + "   " + usedBins);
            }

            // evaluation of each indvidual 
            string localOptimal = recordBest(genePool, fitnessMatrix);


            // termination
            double bestcaseSenario = bestcase();
            Console.WriteLine("best case = " + bestcaseSenario);

            // paint intiation 
            Graphics g = panel1.CreateGraphics();
            g.Clear( Color.White);
            panel1.Controls.Clear();
            panel1.Refresh();

            // painting bins 
            float si = 10f;
            Pen blkpen = new Pen(Color.Black);
            for (int i = 0; i < usedBins; i++)
            {
               g.DrawRectangle(blkpen, (i * (float.Parse(hight) + si)), 0, float.Parse(hight), float.Parse(width));
            }
            Console.WriteLine("num of elements = " + n);
            Console.WriteLine("used bins = " + usedBins);

            // painting boxes 
            int s = 10;
            Random rc = new Random();
            SolidBrush br = new SolidBrush(Color.FromArgb(rc.Next(0, 256), rc.Next(0, 256), 0));
            Rectangle rct = new Rectangle();
            for (int i = 0; i < n; i++)
            {
                br = new SolidBrush(Color.FromArgb(rc.Next(0, 256), rc.Next(0, 256), 0));
                rct = new Rectangle((ozz[i] * (Convert.ToInt32(hight) + s) + oxx[i]), oyy[i], obh[i], obw[i]);
                g.DrawRectangle(blkpen, rct);
                g.FillRectangle(br, rct);

                Label l = new Label();
                l.AutoSize = true;
                l.Size = new Size(200, 32);
                l.Location = new Point((ozz[i] * (Convert.ToInt32(hight) + s) + oxx[i] + 5), oyy[i] + 5);
                l.Text = findnames(i, obh, obw);
                l.BackColor = Color.Transparent;
                panel1.Controls.Add(l);
                panel1.Show();
            }

            for (int i = 0; i < numOfGemerations; i++)
            {  // new generation  
                if (usedBins <= bestcaseSenario)
                    break;

                Console.WriteLine("gen No # " + (i + 1));
                genePool = newGeneration(genePool, fitnessMatrix);
                for (int j = 0; j < genePool.Length; j++)
                {
                    string[,,] sol = huristic(names, h, w, bhClone, bwClone, genePool[j]);
                    fitnessMatrix[j] = fitCalc(sol);
                    //Console.WriteLine((j + 1) + "  " + genePool[j] + "   " + fitnessMatrix[j] + "   " + usedBins);
                }

                newLocalOptimal = recordBest(genePool, fitnessMatrix);

                if (localOptimal != newLocalOptimal)
                {
                    localOptimal = newLocalOptimal;

                     si = 10f;
                    for (int a = 0; a < usedBins; a++)
                    {
                        g.DrawRectangle(blkpen, (a * (float.Parse(hight) + si)), 0, float.Parse(hight), float.Parse(width));
                    }
                    Console.WriteLine("num of elements = " + n);
                    Console.WriteLine("used bins = " + usedBins);

                    // painting boxes 
                    s = 10;

                    for (int a = 0; a < n; a++)
                    {
                        br = new SolidBrush(Color.FromArgb(rc.Next(0, 256), rc.Next(0, 256), 0));
                        rct = new Rectangle((ozz[a] * (Convert.ToInt32(hight) + s) + oxx[a]), oyy[a], obh[a], obw[a]);
                        g.DrawRectangle(blkpen, rct);
                        g.FillRectangle(br, rct);

                        Label l = new Label();
                        l.AutoSize = true;
                        l.Size = new Size(200, 32);
                        l.Location = new Point((ozz[i] * (Convert.ToInt32(hight) + s) + oxx[i] + 5), oyy[i] + 5);
                        l.Text = findnames(i, obh , obw);
                        panel1.Controls.Add(l);
                        l.BackColor = Color.Transparent;
                        panel1.Show();
                    }




                }

            }

        }

      public static string findnames (int x , int [] oh , int [] ow)
        {
            string th = Convert.ToString(oh[x]);
            string tw = Convert.ToString(ow[x]);

            for (int i = 0; i < n; i ++)
            {
                if (th == bhight[i] && tw == bwidth[i])
                {
                    return names[i];
                }
                else if (th == bwidth[i] && tw == bhight[i])
                {
                    return names[i];
                }

            }
            return "no name";
        }

        private void exit_btn_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int h = Convert.ToInt32(hight);
            int w = Convert.ToInt32(width);
            int[] bh = transTointarr(bhight);
            int[] bw = transTointarr(bwidth);

            int binLimit = 10;
            string[,,] sol = new string[h, w, binLimit];
            int n = bh.Length;
            int temp;

            // rotation 
            for (int i = 0; i < n; i++)
            {
                if (bh[i] < bw[i])
                {
                    temp = bh[i];
                    bh[i] = bw[i];
                    bw[i] = temp;
                }
            }
            string[] namesclone = LoopString(names);
            // sorting 
            Array.Sort(bw, bh);
            Array.Sort(bh, bw);

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
                    for (int j = 0; j < w; j++)
                    {
                        for (int k = 0; k < h; k++)
                        {
                            byond = false;
                            empty = true;
                            for (int a = k; a < k + bh[i]; a++)
                            {
                                for (int b = j; b < j + bw[i]; b++)
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

                            if (sol[k, j, z] == null && k + bh[i] <= h && j + bw[i] <= w && empty == true)
                            { // if the curent Co-ordinations not intger (already used) and can take the hight and width 
                                x = k;
                                y = j;
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
                            sol[j, k, z] = namesclone[i];
                        }
                    }
                }
            }
            // packing 

            // for (int i = 0; i < n; i++)
            //  Console.WriteLine("box {0} that has the dims of {1},{2} resides in bin {3} at coordinations {4},{5}",names[i],bh[i],bw[i],(zz[i]+1),xx[i],yy[i]);

            usedBins = zz.Max() + 1;
            //printall(sol);
            // paint intiation 
            Graphics g = panel1.CreateGraphics();
            g.Clear(Color.White);
            panel1.Controls.Clear();
            panel1.Refresh();
            // painting bins 
            float si = 10f;
            Pen blkpen = new Pen(Color.Black);
            for (int i = 0; i < usedBins; i++)
            {
                g.DrawRectangle(blkpen, (i * (float.Parse(hight) + si)), 0, float.Parse(hight), float.Parse(width));
            }
            Console.WriteLine("num of elements = " + n);
            Console.WriteLine("used bins = " + usedBins);

            // painting boxes 
            int s = 10;
            Random rc = new Random() ;
            SolidBrush br = new SolidBrush( Color.FromArgb( rc.Next(0, 256), rc.Next(0, 256), 0) ) ; 
            Rectangle rct = new Rectangle();
            for (int i = 0; i < n; i++)
            {
                 br = new SolidBrush(Color.FromArgb(rc.Next(0, 256), rc.Next(0, 256), 0));
                rct = new Rectangle((zz[i] * (Convert.ToInt32(hight) + s) + xx[i]), yy[i], bh[i], bw[i]);
                g.DrawRectangle(blkpen, rct);
                g.FillRectangle(br, rct);

                Label l = new Label();
                l.AutoSize = true;
                l.Size = new Size(200, 32);
                l.Location = new Point((zz[i] * (Convert.ToInt32(hight) + s) + xx[i] + 5), yy[i] + 5);
                l.Text = findnames(i , bh , bw);
                l.BackColor = Color.Transparent;
                panel1.Controls.Add(l);
                panel1.Show();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (label1.Visible == false)
            {
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
            }
            else if (label1.Visible == true)
            {
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                textBox1.Visible = false;
                textBox2.Visible = false;
                textBox3.Visible = false;
            }
        }
    }
}
