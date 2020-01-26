using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            // Tulosta ruudulle Hello world
            Console.WriteLine("Hello world");
            // Erikoismerkit
            // \n = rivinvaihto
            // \t = tabulaattori
            // \" = lainausmerkki
            // \a = hälytysääni
            Console.WriteLine("\t\"Hello\"\nworld\a");
            // Laskutoimituksia +,-,*,/
            Console.WriteLine(25 * 25); // >> 625
            Console.WriteLine((5 + 2) * (5 - 2)); // >> 21
            // Huom! Jakolasku
            // Kun kokonaisluku jaettuna kokonaisluvulla = kokonaisluku
            Console.WriteLine(3 / 7); // >> 0
            // Huom! Desimaalipiste, ei pilkku
            // 3.0 on ns. kaksinkertaisen tarkkuuden liukuluku (double)
            Console.WriteLine(3.0 / 7);
            // Muuttujat (variable)
            int a = 5; // a on kokonaislukutyyppiä (var, voi olla mikä vaan näistä)
            double x = 4.2; // x on liukulukutyyppiä
            string nimi = "ICT"; // nimi on merkkijono
            char me = 'W'; // me on merkkityyppiä
            // + merkkijonoille liittää ne yhteen
            Console.WriteLine(me + nimi + " " + (a * x));
            // 2. tapa tulostaa
            Console.WriteLine("{0}{1} {2} ", me, nimi, a * x);

        }
    }
}
