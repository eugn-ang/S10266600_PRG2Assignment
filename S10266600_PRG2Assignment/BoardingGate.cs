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
    class BoardingGate
    {
        // attributes
        public string GateName { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsCFFT { get; set; }
        public bool SupportsLWTT { get; set; }
        public Flight Flight { get; set; }

        // ctor
        public BoardingGate(string gn, bool sDDJB, bool sCFFT, bool sLWTT, Flight fl)
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
            double totalFees = 300; // Base Boarding Gate fee

            // Additional fees based on the special request code
            if (Flight != null)
            {
                switch (Flight)
                {
                    case CFFTFlight:
                        totalFees += 150; // CFFT fee
                        break;
                    case DDJBFlight:
                        totalFees += 300; // DDJB fee
                        break;
                    case LWTTFlight:
                        totalFees += 500; // LWTT fee
                        break;
                }
            }

            return totalFees;
        }
        public override string ToString()
        {
            return "Gate Name: " + GateName + "\tSupports CFFT: " + SupportsCFFT
                + "\tSupports DDJB: " + SupportsDDJB + "\tSupports LWTT: " + SupportsLWTT;
            //+ "Flight: " + Flight;
        }
    }
}
