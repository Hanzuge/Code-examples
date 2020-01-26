using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pelit
{
    public class Peli
    {
        private Pelialusta alusta = new Pelialusta();
        private uint pisteet;
        private ushort hutit;
        private Random random = new Random();

        public uint Pisteet { get => pisteet; }
        public ushort Hutit { get => hutit; }
        public int rnd;

        public bool OnkoOsuma(int ruutu)
        {
            if (alusta.OnkoOsuma(ruutu) == true)
            {
                pisteet++;
                return true;
            }
            else
            {
                hutit++;
                return false;
            }
        }
        public void Init()
        {
            pisteet = 0;
            hutit = 0;
            alusta.Init();
            Siirto();
        }
        public bool OnkoValmis()
        {
            if (hutit >= 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Siirto()
        {
            int ruutu = random.Next(0, 9);
            rnd = ruutu;
            alusta.Siirto(ruutu);
        }
    }
}
