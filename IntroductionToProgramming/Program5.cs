using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace For2
{
    class Program
    {
        static void Main(string[] args)
        {
            string s1 = "Java", s2 = "Basic";
            // kahden indeksin for-silmukka
            // Huom! Viimeistä merkkiä ei saa ylittää
            for (int i = 0, j = s2.Length - 1; i < s1.Length; i++, j--)
            {
                // Huom! Merkkijonoon voi liittää merkkejä
                // Kahden merkin lisääminen laskee merkkien koodien summan
                Console.WriteLine("" + s1[i] + s2[j]);
            }
            Console.WriteLine(" " + s2[0]);
        }
    }
}
