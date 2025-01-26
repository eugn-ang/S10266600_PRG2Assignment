using S10266600_PRG2Assignment;
using System.Globalization;


// test
// Create an instance of the Terminal class
Terminal terminal = new Terminal("Terminal 5");
terminal.LoadAirlines();

foreach (var kvp in terminal.Airlines)
{
    Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
}

Console.WriteLine();
terminal.LoadBoardingGates();

foreach (var kvp in terminal.BoardingGates)
{
    Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
}



string filePath = @"flights.csv"; // Ensure this file exists in the correct path
var flights = new Dictionary<string, Flight>();

// Feature 2
foreach (var line in File.ReadLines(filePath))
{
    if (line.StartsWith("Flight Number"))
        continue;

    var fields = line.Split(',');

    if (fields.Length < 5)
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

// Display the loaded flights
Console.WriteLine("Flights loaded from CSV:");
foreach (var flight in flights.Values)
{
    Console.WriteLine(flight.ToString());
}

