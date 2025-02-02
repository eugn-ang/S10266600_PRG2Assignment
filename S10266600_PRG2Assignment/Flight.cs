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
    abstract class Flight : IComparable<Flight>
    {
        public int CompareTo(Flight other)
        {
            if (other == null) return 1; 
            return this.ExpectedTime.CompareTo(other.ExpectedTime); 
        }

        // attributes
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }

        public DateTime ExpectedTime { get; set; }
        public string Status { get; set; }
       
       

        // ctor
        public Flight(string fn,string ori,string dest,DateTime et,string sta)
        {
            FlightNumber = fn;
            Origin = ori;
            Destination = dest;
            ExpectedTime = et;
            Status = sta;
            
        }

        // methods
        public abstract double CalcuateFees();
        public override string ToString()
        {
            return "Flight Number: " + FlightNumber + "\tOrigin: " + Origin 
                + "\tDestination: " + Destination + "\tExpected Time: " + ExpectedTime
                + "\tStatus: " + Status;
        }
    }
}
