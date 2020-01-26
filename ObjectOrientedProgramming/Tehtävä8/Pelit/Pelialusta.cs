using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pelit
{
    class Pelialusta
    {
        private bool[] ruudut = new bool[9];

        public bool OnkoOsuma(int ruutu)
        {
            if (ruudut[ruutu-1] == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Siirto(int ruutu)
        {
            for (int i = 0; i < 9; i++)
            {
                ruudut[i] = false;
            }
            ruudut[ruutu] = true;
        }
        public void Init()
        {
            for (int i = 0; i < 9; i++)
            {
                ruudut[i] = false;
            }
        }
    }
}
