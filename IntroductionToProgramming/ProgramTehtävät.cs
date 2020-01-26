using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetodiKutsuja
{
    class Program
    {
        static void Main(string[] args)
        {
            // Tehtävä 1
            // Metodikutsu 
            Calculate(13.7);

            // Tehtävä 2
            Char c = FirstChar("bcd");
            Console.WriteLine(c);

            // Tehtävä 3
            int[] arr1 = { 3, 9, 15, 6, 8 };
            int max = Greatest(arr1);
            Console.WriteLine(max);
            // int max = Greatest(new int[] { 3, 9, 15, 6, 8 }); nimetön taulukko
        }

        private static int Greatest(int[] arr1) // Tehtävä 3
        {
            int max = arr1[0];
            for (int i = 1; i < arr1.Length; i++)
            {
                if (arr1[i] > max)
                {
                    max = arr1[i];
                }
            }
            return max;
        }

        private static char FirstChar(string v) // Tehtävä 2
        {
            return v[0];
        }

        private static void Calculate(double v) // Tehtävä 1
        {
            Console.WriteLine(2 * v);
        }
    }
}
