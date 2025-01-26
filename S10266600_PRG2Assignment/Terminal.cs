using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace S10266600_PRG2Assignment
{
    class Terminal
    {
        // attributes
        public string TerminalName { get; set; }
        public Dictionary<string,Airline> Airlines { get; set; }
        public Dictionary<string,Flight> Flights { get; set; }
        public Dictionary<string,BoardingGate> BoardingGates { get; set; }
        public Dictionary<string,double> GateFees { get; set; }
        // ctor
        public Terminal(string terminalname)
        {
            TerminalName = terminalname;
            Airlines = new Dictionary<string,Airline>();
            Flights = new Dictionary<string,Flight>();
            BoardingGates = new Dictionary<string,BoardingGate>();
            GateFees = new Dictionary<string,double>();
        }
        // methods
        public bool AddAirlines(Airline airline)
        {
            
        }
        public bool AddBoardingGate(BoardingGate gate)
        {

        }
        public Airline GetAirlineFromFlight(Flight flight)
        {

        }
        public void PrintAirlinesFees()
        {

        }
        public override string ToString()
        {
            return "Terminal Name: " + TerminalName + "\tAirlines: " + Airlines
                + "\tFlights: " + Flights + "\tBoarding Gates: " + BoardingGates
                + "\tGate Fees: " + GateFees;
        }

    }
}
