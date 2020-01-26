using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex32
{
    class Program
    {
        static void Main(string[] args)
        {
            // Luodaan MyIntArray-olio
            MyIntArray mia = new MyIntArray();
            // Asetetaan ominaisuudelle Array1 arvo
            int[] arr1 = { 7, 1, 4 };
            mia.Array1 = arr1;
            // Kutsutaan Write-metodia
            mia.Write(", ");
            // T33. Jos WriteStatic on staattinen
            // MyIntArray.WriteStatic(arr1, ", ");
            // Vertaa taulukon lajittelumetodi
            Array.Sort(arr1);
            // T34. Luo Dice -olio ja anna arvo ominaisuudelle Ran


        }
    }
}
