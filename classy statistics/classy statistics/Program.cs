using System;

namespace classy_statistics
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();

            //----------------------
            stat O = new stat();
            O.dataEntry();
            O.printData();
            O.computation();
            O.output();
            //----------------------
            Console.ReadKey();
        }
    }

    class stat
    {   // fields 
        private int[] arr;
        private double mean;
        private double std;
        private int max;
        private int min;

        private int odd;
        private int even;
        private int seq;
        private int index;
        private int maxSeq;
        private int repVal;
        private double[] counting;
        private double[] persentage;

        // constractors 

      
        // methods 
        public void computation()
            {
              average();
             Std(mean);
             maxnmin();
             oddEven();
             unique();
             freqoutput();

            }
            public void output ()
        {
            Console.WriteLine("mean = " + mean);
            Console.WriteLine("standard dev = " + std);
            Console.WriteLine("the biggest number is {0} and the smalles number is {1} " , min , max);
            Console.WriteLine("the number of odd numbers is {0} and the number of even {1}" , odd , even);
            Console.WriteLine("the biggest seq = {0} as it was repeted {1} " , repVal , maxSeq );
            Console.WriteLine();
            freq2();
        }
        private void freqOutput ()
        {

        }
        private void intro()
        {
            Console.WriteLine('\t' + "  " + '\t' + "this prog calcs the statistical data");

        }

        public void dataEntry()
        {
            intro();

            Console.WriteLine("do you want to input the data through keyboard[K] or through a file [F]");
            char ans = Convert.ToChar(Console.ReadLine());

            if (ans == 'k' || ans == 'K')
            {
                readFromUser();
            }
            else if (ans == 'F' || ans == 'f')
            {
                readFromFile();
            }
            else
            {
                dataEntry();
            }


        }

        private void readFromFile()
        {

        }
        private void readFromUser()
        {
            Console.WriteLine("please input the data and keep a single (,) between each number ");
            string entry = Console.ReadLine();

            string[] nums = entry.Split(',');
            arr = new int[nums.Length];
            for (int i = 0; i < nums.Length; i++)
            {
                arr[i] = Convert.ToInt32(nums[i]);
            }

        }
        public  void printData()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t" + " n" + '\t' + "|" + "\t" + "value");
            Console.WriteLine("------------------------------");
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine("\t" + i + 1 + "\t" + "|" + "\t" + arr[i]);
            }
        }

        private void average()
        {
            Console.WriteLine();
            mean = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                mean += arr[i];
            }

            mean = mean / arr.Length;
        }

        private void Std(double mean)
        {
            std = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                std += (mean - arr[i]) * (mean - arr[i]);
            }

            std = Math.Sqrt (std / (arr.Length - 1));
        }

        private void maxnmin()
        {
            max = arr[0];
            min = arr[0];

            for (int i = 0; i < arr.Length; i++)
            {
                if (max > arr[i])
                    max = arr[i];

                if (min < arr[i])
                    min = arr[i];
            }

        }


        private void Seq()
        {// seq 
             seq = 0;
             index = 0;
             maxSeq = 0;
             repVal = 0;
              
            for (int i = 0; i < arr.Length - 1; i++)
            {
                if (arr[i] == arr[i + 1])
                {
                    seq++;
                }
                else
                {
                    seq = 0;
                }

                if (seq > maxSeq)
                {
                    maxSeq = seq;
                    index = i - seq + 1;
                    repVal = arr[i];
                }
            }
        }

        // analysis
        private void oddEven()
        { odd = 0; even = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] % 2 == 0)
                    even++;

                else
                    odd++;

            }
        }

        // unique 

        private string u;
        private void unique()
        {
            u = "";
            bool unique = true;
            int[] un = new int[arr.Length];

            Console.WriteLine("the unique numbers are : ");
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr.Length; j++)
                {
                    if (arr[i] == un[j])
                    {
                        unique = false;
                        break;
                    }

                }

                if (unique == true)
                {
                    u = u + Convert.ToString(arr[i]) + '\t';
                    un[i] = arr[i];
                }

                unique = true;
            }
        }


        private void freq1()
            {
                // freqency with arrays
                string[] nums = u.Split('\t');
            int[] uniqueNums = new int[nums.Length - 1];
            for (int i = 0; i < uniqueNums.Length; i++)
            {
                uniqueNums[i] = Convert.ToInt32(nums[i]);
            }
            int[] counting = new int[uniqueNums.Length];
            double[] persentage = new double[uniqueNums.Length];
            for (int i = 0; i < uniqueNums.Length; i++)
            {
                for (int j = 0; j < arr.Length; j++)
                {
                    if (arr[j] == uniqueNums[i])
                        counting[i] = counting[i] + 1;
                }

                persentage[i] = ((100) * counting[i]) / arr.Length;
            }


        }

        private void freqoutput ()
        {
            Console.WriteLine( "number" + '\t' + "frq" +'\t'+"persentage"+'\t'+"diagram" );

            for (int i = 0; i < 0; i++)
            {
                Console.Write(arr[i] + 't' + counting[i] + '\t' + persentage[i] + '\t');

                for (int k = 0; k < persentage[i]; k++)
                {
                    Console.Write("###");
                }
                Console.WriteLine();
            }
        }

        // freqency but printing is implemented within 

        private void freq2()
        {
            int counter = 0;
            int persent;
            Console.WriteLine("number" + '\t' + "freq" + '\t' + "%" + '\t' + "diagram");
            Console.WriteLine("---------------------------------------------------------------");
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr.Length; j++)
                {
                    if (arr[i] == arr[j] && arr[i] != -404)
                    {
                        counter++;
                        if (counter > 1)
                        {
                            arr[j] = -404;
                        }
                    }
                }
                if (arr[i] != -404)
                {
                    persent = (100 * counter) / arr.Length;

                    Console.Write(arr[i] + "" + '\t' + counter + '\t' + persent + "% " + '\t');
                    for (int k = 0; k < persent; k++)
                    {
                        Console.Write("###");
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                }

                counter = 0;
            }
        }
           
    }
}
