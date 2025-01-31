using S10266600_PRG2Assignment;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
//==========================================================
// Student Number	: S10266600
// Student Name	: Ang Yu Heng Eugene
// Partner Name	: Lim Tzer Ee Joshua
//==========================================================


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

while (true)
{
    // Display header
    Console.WriteLine("===============================================");
    Console.WriteLine("     Welcome to Changi Airport Terminal 5");
    Console.WriteLine("===============================================");
    // Display menu options
    Console.WriteLine("1. List All Flights");
    Console.WriteLine("2. List Boarding Gates");
    Console.WriteLine("3. Assign a Boarding Gate to a Flight");
    Console.WriteLine("4. Create Flight");
    Console.WriteLine("5. Display Airline Flights");
    Console.WriteLine("6. Modify Flight Details");
    Console.WriteLine("7. Display Flight Schedule");
    Console.WriteLine("0. Exit");

    Console.Write("\nPlease select your option: ");
    string choice = Console.ReadLine();
    // choice
    if (choice == "1")
    {
        DisplayFlights(terminal);
    }
    else if (choice == "2")
    {
        DisplayBoardingGates(terminal);
    }
    else if (choice == "0")
    {
        Console.WriteLine("Exiting program. Goodbye!");
        return;
    }
    else
    {
        Console.WriteLine("Invalid option. Please try again.");
    }

    Console.WriteLine("\nPress any key to return to the main menu...");
    Console.ReadKey();

    

    void DisplayFlights(Terminal terminal)
    {
        Console.WriteLine("===============================================");
        Console.WriteLine("  List of Flights for Changi Airport Terminal 5");
        Console.WriteLine("===============================================\n");

        // Print column headers with proper alignment
        Console.WriteLine("{0,-12} {1,-20} {2,-20} {3,-20} {4,-10}",
            "Flight No", "Airline Name", "Origin", "Destination", "Expected");

        // Print a separator line
        Console.WriteLine(new string('-', 85));

        foreach (var flight in terminal.Flights.Values)
        {
            // Retrieve the airline name using the flight number prefix
            string airlinePrefix = flight.FlightNumber.Substring(0, 2).Trim(); // Extract airline code
            string airlineName = terminal.Airlines.TryGetValue(airlinePrefix, out var airline)
                ? airline.Name
                : "Unknown Airline";

            // Print flight details in aligned format
            Console.WriteLine("{0,-12} {1,-20} {2,-20} {3,-20} {4,-10}",
                flight.FlightNumber, airlineName, flight.Origin, flight.Destination,
                flight.ExpectedTime.ToString("dd/M/yyyy")); // Format date as in image

            // Print departure/arrival time below flight number
            Console.WriteLine("{0,-12}", flight.ExpectedTime.ToString("h:mm:ss tt"));
        }

    }

    // Method to display all boarding gates
    void DisplayBoardingGates(Terminal terminal)
    {
  
        Console.WriteLine("===============================================");
        Console.WriteLine(" List of Boarding Gates for Changi Airport Terminal 5");
        Console.WriteLine("===============================================\n");

        Console.WriteLine("{0,-10} {1,-10} {2,-10} {3,-10}",
            "Gate Name", "DDJB", "CFFT", "LWTT");

        Console.WriteLine("-------------------------------------------------------------");

        foreach (var gateEntry in terminal.BoardingGates)
        {
            BoardingGate gate = gateEntry.Value;

            Console.WriteLine("{0,-10} {1,-10} {2,-10} {3,-10}",
                gate.GateName,
                gate.SupportsDDJB,
                gate.SupportsCFFT,
                gate.SupportsLWTT
                );
        }

        
    }




}