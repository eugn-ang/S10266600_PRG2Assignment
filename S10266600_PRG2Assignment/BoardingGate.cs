using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10266600_PRG2Assignment
{
    class BoardingGate
    {
        // attributes
        public string GateName { get; set; }
        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWTT { get; set; }
        public Flight Flight { get; set; }

        // ctor
        public BoardingGate(string gn, bool sCFFT, bool sDDJB,bool sLWTT,Flight fl)
        {
            GateName = gn;
            SupportsCFFT = sCFFT;
            SupportsDDJB = sDDJB;
            SupportsLWTT = sLWTT;
            Flight = fl;

            
        }
        // methods
        public double calculateFees()
        {

        }
        public override string ToString()
        {
            return "Gate Name: " + GateName + "\tSupports CFFT: " + SupportsCFFT
                + "\tSupports DDJB: " + SupportsDDJB + "\tSupports LWTT: " + SupportsLWTT
                + "Flight: " + Flight;
        }
    }
}
