﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10266600_PRG2Assignment
{
    class LWTTFlight : Flight
    {
        // attributes
        public double RequestFee { get; set; }
        
        // ctor
        public LWTTFlight(string fn, string ori, string dest, DateTime et, string sta, double reqF) : base(fn, ori, dest, et, sta)
        {
            RequestFee = reqF;
        }

        //methods
        public override double CalcuateFees()
        {

        }

        public override string ToString()
        {
            return base.ToString() + "\tRequest Fee: " + RequestFee;
        }
    }
}
