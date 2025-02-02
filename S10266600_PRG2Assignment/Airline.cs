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
            double totalDiscounts = 0;
            int flightCount = Flights.Count;

            foreach (var flight in Flights.Values)
            {
                // Base Fees
                double flightFee = 300; // Boarding Gate Base Fee
                if (flight.Origin == "SIN")
                    flightFee += 800;
                else if (flight.Destination == "SIN")
                    flightFee += 500;

                // Special Request Code Fees
                if (flight is CFFTFlight) flightFee += 150;
                else if (flight is DDJBFlight) flightFee += 300;
                else if (flight is LWTTFlight) flightFee += 500;
                else totalDiscounts += 50; // No Special Request Code Discount

                // Apply Discounts
                if (flight.ExpectedTime.Hour < 11 || flight.ExpectedTime.Hour >= 21)
                    totalDiscounts += 110; // Time-based discount

                if (flight.Origin == "DXB" || flight.Origin == "BKK" || flight.Origin == "NRT")
                    totalDiscounts += 25; // Origin-based discount

                

                totalFees += flightFee;
            }

            // Apply Bulk Discounts
            totalDiscounts += (flightCount / 3) * 350; // $350 discount per 3 flights

            if (flightCount > 5)
                totalDiscounts += totalFees * 0.03; // 3% discount on total bill

            // Final Fee Calculation
            double finalTotal = totalFees - totalDiscounts;

            // Display Breakdown
            Console.WriteLine($"Total Fees before Discount: ${totalFees}");
            Console.WriteLine($"Total Discounts Applied: -${totalDiscounts}");
            Console.WriteLine($"Final Total Fee: ${finalTotal}");

            return finalTotal;
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
            return "Name: " + Name; //+ "\tCode: " + Code + "\tFlights: " + Flights;
        }
    }
}
