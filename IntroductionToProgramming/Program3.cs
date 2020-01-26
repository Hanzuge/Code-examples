using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewWriting
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "Tehtävä numero";
            int n = 12;
            // 1. tapa
            Console.WriteLine(s + " " + n);
            // 2. tapa
            Console.WriteLine("{0} {1}", s, n);
            // 3. tapa versiosta C# 6.0 lähtien
            // string interpolation (ei tenttiin)
            Console.WriteLine($"{s} {n}");
        }
    }
}
