using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchExtraB
{
    class Program
    {
        static void Main(string[] args)
        {
            // switch rakennetta ei voi käyttää double-tyypin kanssa
            // switch rakennetta voi käyttää int-tyypin kanssa
            // switch rakennetta voi käyttää char-tyypin kanssa
            // switch rakennetta voi käyttää string-tyypin kanssa

            #region switch char
            Console.Write("Syötä merkki: ");
            string merkkiStr = Console.ReadLine();
            // Otetaan merkkijonosta 1. merkki
            char eka = merkkiStr[0];
            switch (eka)
            {
                case 'A':
                case 'a':
                    Console.WriteLine("Syötit a:n");
                    break;
                default:
                    Console.WriteLine("Et syöttänyt a:ta");
                    break;
            }
            #endregion
            #region switch string
            Console.Write("Syötä maan nimi:");
            string maa = Console.ReadLine();
            // ToLower-komento muuttaa kirjaimet pieniksi
            switch (maa.ToLower())
            {
                case "suomi":
                    Console.WriteLine(" on Euroopassa");
                    break;
                case "libya":
                    Console.WriteLine(" on Afrikassa");
                    break;
                default:
                    Console.WriteLine(" on jossakin");
                    break; 
                    #endregion
            }
        }
    }
}
