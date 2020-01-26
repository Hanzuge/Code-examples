using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetoditJa_Parametrit
{
    class Program
    {
        static void Main(string[] args)
        {
            // Eri nimityksiä:
            // Komento, funktio, metodi
            // Vertaa matemaattinen funktio
            // WriteLine metodilla on 1 parametri
            Console.WriteLine(25*25);
            // WriteLine metodilla on 0 parametria
            Console.WriteLine(); // tyhjä rivi
            // Ceiling metodilla on yksi parametri
            double y = Math.Ceiling(2.3);
            Console.WriteLine(y); // >> 3 (Kattofunktio)
            // Pow metodilla on 2 parametria erotettuna pilkulla
            double z = Math.Pow(4, 2);
            // WriteLine metodilla on 4 parametria
            Console.WriteLine("{0} potenssiin {1} = {2}", 4, 2, z);
        }
    }
}
