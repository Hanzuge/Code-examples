// Otetaan käyttöön nimiavaruudet
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Namespaces
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine();
            #region Turku ja Raisio
            string s1 = "Turku", s2 = "Raisio";
            #endregion
            double x = 1, y = 2.34, z = 11.2345;
            //jos käytössä on System-nimiavaruus
            //ei tarvitse kirjoittaa System.Console
            Console.WriteLine(s1 + ", " + s2);
            // 2. tulostus
            Console.WriteLine("{0}, {1}", s1, s2);
            // Tulosta samaa
            Console.WriteLine("{0}\t{0}\n\t\t{0}", s1);
            // pi 2:n desimaalin tarkkuudella
            Console.WriteLine("{0:f2}", Math.PI);
        }
    }
}
