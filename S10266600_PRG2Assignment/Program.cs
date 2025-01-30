
//==========================================================
// Student Number	: S10266600
// Student Name	: Ang Yu Heng Eugene
// Partner Name	: Lim Tzer Ee Joshua
//==========================================================




using System.Globalization;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace S10266600_PRG2Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of the Terminal class
            Terminal terminal = new Terminal("Terminal 5");

            // Load airlines and boarding gates
            terminal.LoadAirlines();
            terminal.LoadBoardingGates();

            // Load flights from the CSV file
            string filePath = @"flights.csv"; // Ensure this file exists in the correct path
            var flights = new Dictionary<string, Flight>();

            foreach (var line in File.ReadLines(filePath))
            {
                if (line.StartsWith("Flight Number")) // Skip the header line
                    continue;

                var fields = line.Split(',');

                if (fields.Length < 5) // Ensure the line has enough fields
                    continue;

                string flightNumber = fields[0].Trim();
                string origin = fields[1].Trim();
                string destination = fields[2].Trim();
                DateTime expectedTime = DateTime.ParseExact(fields[3].Trim(), "h:mm tt", CultureInfo.InvariantCulture);
                string specialRequestCode = fields[4].Trim();

                Flight flight;

                // Create the appropriate flight object based on special request code
                switch (specialRequestCode)
                {
                    case "CFFT":
                        flight = new CFFTFlight(flightNumber, origin, destination, expectedTime, "On Time", 150);
                        break;
                    case "DDJB":
                        flight = new DDJBFlight(flightNumber, origin, destination, expectedTime, "On Time", 300);
                        break;
                    case "LWTT":
                        flight = new LWTTFlight(flightNumber, origin, destination, expectedTime, "On Time", 500);
                        break;
                    default:
                        flight = new NORMFlight(flightNumber, origin, destination, expectedTime, "On Time");
                        break;
                }

                // Add the flight to the dictionary
                flights[flightNumber] = flight;
            }

            // Add flights to the terminal
            foreach (var flight in flights.Values)
            {
                terminal.Flights.Add(flight.FlightNumber, flight);
            }

            // Display all flights with their basic information
            Console.WriteLine("\nList of all flights with basic information:");
            Console.WriteLine("------------------------------------------------------------");

            foreach (var flight in terminal.Flights.Values)
            {
                // Retrieve the airline name using the flight number prefix
                string airlinePrefix = flight.FlightNumber.Substring(0, 2).Trim(); // Extract the airline code
                string airlineName = terminal.Airlines.TryGetValue(airlinePrefix, out var airline)
                    ? airline.Name
                    : "Unknown Airline";

                // Display flight details
                Console.WriteLine($"Flight Number: {flight.FlightNumber}");
                Console.WriteLine($"Airline Name: {airlineName}");
                Console.WriteLine($"Origin: {flight.Origin}");
                Console.WriteLine($"Destination: {flight.Destination}");
                Console.WriteLine($"Expected Departure/Arrival Time: {flight.ExpectedTime:hh:mm tt}");
                Console.WriteLine("------------------------------------------------------------");
            }
        }
    }
}
