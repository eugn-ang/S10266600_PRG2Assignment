using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


//==========================================================
// Student Number	: S10266600
// Student Name	: Ang Yu Heng Eugene
// Partner Name	: Lim Tzer Ee Joshua
//==========================================================


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

        // Method to load airlines from a CSV file
        public void LoadAirlines()
        {
            try
            {
                using (StreamReader reader = new StreamReader("airlines.csv"))
                {
                    // read header
                    reader.ReadLine();

                    Console.WriteLine("Loading airlines...");
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        
                        if (values.Length == 2)
                        {
                            var name = values[0].Trim();
                            var code = values[1].Trim();

                            if (!Airlines.ContainsKey(code))
                            {
                                Airlines.Add(code, new Airline(name, code, Flights));
                            }
                        }
                    }
                    Console.WriteLine("Airlines loaded successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading airlines: {ex.Message}");
            }
        }

        // method to load boarding gates from csv file
        // Method to load boarding gates from a CSV file
        public void LoadBoardingGates()
        {
            try
            {
                using (StreamReader reader = new StreamReader("boardinggates.csv"))
                {
                    // read header
                    reader.ReadLine();
                    Console.WriteLine("Loading boarding gates...");
                    while (!reader.EndOfStream)
                    {
                      

                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        // Assuming the CSV format is: GateName,SupportsCFFT,SupportsDDJB,SupportsLWTT
                        if (values.Length == 4)
                        {
                            var gateName = values[0].Trim();
                            var supportsCFFT = bool.Parse(values[1].Trim());
                            var supportsDDJB = bool.Parse(values[2].Trim());
                            var supportsLWTT = bool.Parse(values[3].Trim());

                            if (!BoardingGates.ContainsKey(gateName))
                            {
                                BoardingGates.Add(gateName, new BoardingGate(gateName, supportsCFFT, supportsDDJB, supportsLWTT, null));
                            }
                        }
                    }
                    Console.WriteLine("Boarding gates loaded successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading boarding gates: {ex.Message}");
            }
        }

        // methods
        // Add an airline to the terminal
        public bool AddAirlines(Airline airline)
        {
            if (!Airlines.ContainsKey(airline.Code))
            {
                Airlines.Add(airline.Code, airline);
                return true;
            }
            Console.WriteLine($"Airline with code {airline.Code} already exists.");
            return false;
        }
        // Add a boarding gate to the terminal
        public bool AddBoardingGate(BoardingGate gate)
        {
            if (!BoardingGates.ContainsKey(gate.GateName))
            {
                BoardingGates.Add(gate.GateName, gate);
                return true;
            }
            Console.WriteLine($"Boarding gate {gate.GateName} already exists.");
            return false;
        }
        // Get the airline associated with a flight
        public Airline GetAirlineFromFlight(Flight flight)
        {
            foreach (var airline in Airlines.Values)
            {
                if (flight.FlightNumber == airline.Name)
                {
                    return airline;
                }
            }
            Console.WriteLine($"No airline found for flight {flight.FlightNumber}.");
            return null;
        }
        public void PrintAirlinesFees()
        {
            Console.WriteLine("Airline Fees Summary:");
            foreach (var airline in Airlines.Values)
            {
                double totalFees = 0;
                foreach (var flight in Flights.Values)
                {
                    if (flight.FlightNumber == airline.Name)
                    {
                        totalFees += CalculateFlightFees(flight);
                    }
                }
                Console.WriteLine($"Airline: {airline.Name}, Total Fees: ${totalFees:F2}");
            }
        }
        // Helper method to calculate fees for a flight
        private double CalculateFlightFees(Flight flight)
        {
            double fees = 0;

            // Calculate base fees based on flight type
            if (flight.Origin == "Singapore (SIN)")
            {
                fees += 800; // Departure fee
            }
            else if (flight.Destination == "Singapore (SIN)")
            {
                fees += 500; // Arrival fee
            }

            // Add gate fees if a boarding gate is assigned
            //if (BoardingGates.TryGetValue(flight.BoardingGate, out BoardingGate gate))
            //{
            //    fees += gate.calculateFees();
            //}

            return fees;
        }
        public override string ToString()
        {
            return "Terminal Name: " + TerminalName + "\tAirlines: " + Airlines
                + "\tFlights: " + Flights + "\tBoarding Gates: " + BoardingGates
                + "\tGate Fees: " + GateFees;
        }

    }
}
