using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



//==========================================================
// Student Number	: S10266600
// Student Name	: Ang Yu Heng Eugene
// Partner Name	: Lim Tzer Ee Joshua
//==========================================================

namespace S10266600_PRG2Assignment
{
    class NORMFlight : Flight
    {
        // ctor
        public NORMFlight(string fn, string ori, string dest, DateTime et, string sta) : base(fn, ori, dest, et, sta)
        {
            
        }

        // methods
        public override double CalcuateFees()
        {
            double baseFee = 300; 
            return baseFee; 
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
