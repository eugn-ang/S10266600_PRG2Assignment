using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10266600_PRG2Assignment
{
    class Airline
    {
        // attributes
        public string Name { get; set; }
        public string Code { get; set; }
        public Dictionary<string,Flight> Flights { get; set; }

        // ctor
        public Airline(string na, string cd, Dictionary<string,Flight> dict)
        {
            Name = na;
            Code = cd;
            Flights = dict;
            
        }

        // methods
        public bool AddFlight(Flight flight)
        {
            if (flight == null || Flights.ContainsKey(flight.FlightNumber))
            {
                return false;
            }

            Flights.Add(flight.FlightNumber, flight);
            return true;
        }
        public double CalculateFees()
        {

        }
        public bool RemoveFlight(Flight flight)
        {

        }
        public override string ToString()
        {
            return "Name: " + Name + "\tCode: " + Code + "\tFlights: " + Flights;
        }
    }
}
