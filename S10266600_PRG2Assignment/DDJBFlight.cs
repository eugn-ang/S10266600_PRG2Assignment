using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10266600_PRG2Assignment
{
    class DDJBFlight : Flight
    {
        // attributes
        public double RequestFee { get; set; }

        // ctor
        public DDJBFlight(string fn, string ori, string dest, DateTime et, string sta, double reqF) : base(fn, ori, dest, et, sta)
        {
            RequestFee = reqF;
        }

        //methods
        public override double CalcuateFees()
        {
            double baseFee = 300; 
            return baseFee + RequestFee;
        }

        public override string ToString()
        {
            return base.ToString() + "\tRequest Fee: " + RequestFee;
        }
    }
}
