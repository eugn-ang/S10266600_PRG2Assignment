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
            double totalFees = 0;

            foreach (var flight in Flights.Values)
            {
                if (flight.Origin == "SIN") 
                {
                    totalFees += 800;
                }
                else if (flight.Destination == "SIN") 
                {
                    totalFees += 500;
                }

                
                if (flight is CFFTFlight cfftFlight)
                {
                    totalFees += 150;
                }
                else if (flight is DDJBFlight ddjbFlight)
                {
                    totalFees += 300; 
                }
                else if (flight is LWTTFlight lwttFlight)
                {
                    totalFees += 500; 
                }

                totalFees += 300;
                
            }
            return totalFees;
        }
        public bool RemoveFlight(Flight flight)
        {
            if (flight == null || !Flights.ContainsKey(flight.FlightNumber))
            {
                return false;
            }

            Flights.Remove(flight.FlightNumber);
            return true;
        }
        public override string ToString()
        {
            return "Name: " + Name + "\tCode: " + Code + "\tFlights: " + Flights;
        }
    }
}
