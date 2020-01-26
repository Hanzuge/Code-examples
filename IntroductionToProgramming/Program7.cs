using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kertoma
{
    class Program
    {
        static void Main(string[] args)
        {
            // Kertoma: 3! = 1*2*3
            // Long-tyyppi on iso kokonaisluku
            int n = 3;
            long tulos = 1;
            for (int i = 1; i <= n; i++)
            {
                tulos = tulos * i; // tulos *=i;
            }
            Console.WriteLine("{0}! = {1}", n, tulos);
        }
    }
}
