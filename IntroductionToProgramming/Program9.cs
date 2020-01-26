using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operaattorit
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 3, b = 5;
            a--; // Tämän jälkeen a = 2
            b++; // Tämän jälkeen b = 6
            Console.WriteLine(a + " " + b); // >> 2 6
            --a; // Tämän jälkeen a = 1
            ++b; // Tämän jälkeen b = 7
            Console.WriteLine(a + " " + b); // >> 1 7
            int c = (a++ + b--) * --b; // Tämän jälkeen a = 2, b = 5, c = 40 ,m Vertaa ViLLE
            Console.WriteLine(a + " " + b + " " + c); // >> 2 5 40
            c += 10; // eli c = c + 10;
            Console.WriteLine(c + 1); // >> 51
            Console.WriteLine(c); // >> 50
        }
    }
}
