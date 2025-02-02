using S10266600_PRG2Assignment;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
//==========================================================
// Student Number	: S10266600
// Student Name	: Ang Yu Heng Eugene
// Partner Name	: Lim Tzer Ee Joshua
//==========================================================

// Get the current directory where the executable is located
string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

// Set the file paths for the CSV files
string flightsFilePath = Path.Combine(currentDirectory, "flights.csv");
string airlinesFilePath = Path.Combine(currentDirectory, "airlines.csv");
string boardingGatesFilePath = Path.Combine(currentDirectory, "boardinggates.csv");

// Create an instance of the Terminal class
Terminal terminal = new Terminal("Terminal 5");

// Load airlines and boarding gates
terminal.LoadAirlines();
terminal.LoadBoardingGates();

// Load flights from the CSV file
var flights = new Dictionary<string, Flight>();

foreach (var line in File.ReadLines(flightsFilePath))
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
            flight = new DDJBFlight(flightNumber, origin, destination, expectedTime, "On Time",  300);
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
    else if (choice == "3")
    {
        AssignBoardingGateToFlight(terminal);
    }
    else if (choice == "4")
    {
        CreateNewFlight(terminal, flightsFilePath);
    }
    else if (choice == "5")
    {
        DisplayAirlineFlights(terminal);
    }
    else if (choice == "6")  // Modify Flight Details
    {
        ModifyFlightDetails(terminal);
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


    void AssignBoardingGateToFlight(Terminal terminal)
    {
        Console.WriteLine("===============================================");
        Console.WriteLine("     Assign Boarding Gate to a Flight");
        Console.WriteLine("===============================================");

        // Prompt for flight number
        Console.Write("Enter Flight Number: ");
        string flightNumber = Console.ReadLine().Trim();
        
        // Prompt for boarding gate
        Console.Write("Enter Boarding Gate Name: ");
        string gateName = Console.ReadLine().Trim();

        // Check if the flight exists
        if (terminal.Flights.TryGetValue(flightNumber, out var flight))
        {
            // Check if the boarding gate exists
            if (terminal.BoardingGates.TryGetValue(gateName, out var gate))
            {
                // Check if the boarding gate is already assigned to another flight
                if (gate.Flight != null)
                {
                    Console.WriteLine($"\nBoarding Gate {gateName} is already assigned to Flight {gate.Flight.FlightNumber}.");
                }
                else
                {
                    // Validate if the boarding gate supports the flight's special request code
                    bool isGateCompatible = true;
                    if (flight is CFFTFlight && !gate.SupportsCFFT)
                    {
                        isGateCompatible = false;
                    }
                    else if (flight is DDJBFlight && !gate.SupportsDDJB)
                    {
                        isGateCompatible = false;
                    }
                    else if (flight is LWTTFlight && !gate.SupportsLWTT)
                    {
                        isGateCompatible = false;
                    }

                    if (!isGateCompatible)
                    {
                        Console.WriteLine($"\nBoarding Gate {gateName} does not support the flight's special request code.");
                    }
                    else
                    {
                        // Assign the boarding gate to the flight
                        gate.Flight = flight;
                        Console.WriteLine($"\nFlight Number: {flight.FlightNumber}");
                        Console.WriteLine($"Origin: {flight.Origin}");
                        Console.WriteLine($"Destination: {flight.Destination}");
                        Console.WriteLine($"Expected Time: {flight.ExpectedTime:dd/M/yyyy hh:mm:ss tt}");
                        Console.WriteLine($"Special Request Code: {(flight is CFFTFlight ? "CFFT" : flight is DDJBFlight ? "DDJB" : flight is LWTTFlight ? "LWTT" : "None")}");
                        Console.WriteLine($"Boarding Gate Name: {gate.GateName}");
                        Console.WriteLine($"Supports DDJB: {gate.SupportsDDJB}");
                        Console.WriteLine($"Supports CFFT: {gate.SupportsCFFT}");
                        Console.WriteLine($"Supports LWTT: {gate.SupportsLWTT}");

                        // Prompt to update flight status
                        Console.Write("\nWould you like to update the status of the flight? (Y/N): ");
                        string updateStatusChoice = Console.ReadLine().Trim().ToUpper();

                        if (updateStatusChoice == "Y")
                        {
                            Console.WriteLine("1. Delayed");
                            Console.WriteLine("2. Boarding");
                            Console.WriteLine("3. On Time");
                            Console.Write("Please select the new status of the flight: ");
                            string statusChoice = Console.ReadLine().Trim();

                            switch (statusChoice)
                            {
                                case "1":
                                    flight.Status = "Delayed";
                                    break;
                                case "2":
                                    flight.Status = "Boarding";
                                    break;
                                case "3":
                                    flight.Status = "On Time";
                                    break;
                                default:
                                    Console.WriteLine("Invalid choice. Status remains unchanged.");
                                    break;
                            }

                            Console.WriteLine($"\nFlight status updated to: {flight.Status}");
                        }
                        else
                        {
                            flight.Status = "On Time";
                        }

                        Console.WriteLine($"\nFlight {flight.FlightNumber} has been assigned to Boarding Gate {gate.GateName}!");
                    }
                }
            }
            else
            {
                Console.WriteLine($"\nBoarding Gate {gateName} not found.");
            }
        }
        else
        {
            Console.WriteLine($"\nFlight {flightNumber} not found.");
        }
    }

    void CreateNewFlight(Terminal terminal, string filePath)
    {
        Console.WriteLine("===============================================");
        Console.WriteLine("           Create a New Flight");
        Console.WriteLine("===============================================");

        while (true)
        {
            // Prompt for flight details
            Console.Write("Enter Flight Number: ");
            string flightNumber = Console.ReadLine().Trim();

            Console.Write("Enter Origin: ");
            string origin = Console.ReadLine().Trim();

            Console.Write("Enter Destination: ");
            string destination = Console.ReadLine().Trim();

            // Prompt for expected departure/arrival time
            DateTime expectedTime;
            while (true)
            {
                Console.Write("Enter Expected Departure/Arrival Time (dd/MM/yyyy HH:mm): ");
                string timeInput = Console.ReadLine();

                // Try to parse the input using the specified format
                if (DateTime.TryParseExact(timeInput, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out expectedTime))
                {
                    break; // Exit the loop if parsing is successful
                }
                else
                {
                    Console.WriteLine("Invalid date/time format. Please enter in the format dd/MM/yyyy HH:mm.");
                }
            }

            // Prompt for special request code
            Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
            string specialRequestCode = Console.ReadLine().Trim().ToUpper();
            while (specialRequestCode != "CFFT" && specialRequestCode != "DDJB" && specialRequestCode != "LWTT" && specialRequestCode != "NONE")
            {
                Console.WriteLine("Invalid special request code. Please enter CFFT, DDJB, LWTT, or None.");
                Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
                specialRequestCode = Console.ReadLine().Trim().ToUpper();
            }

            // Create the appropriate flight object based on special request code
            Flight flight;
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

            // Add the flight to the terminal
            terminal.Flights.Add(flightNumber, flight);

            // Append the flight to the CSV file
            try
            {
                using (StreamWriter writer = new StreamWriter("flights.csv", true))
                {
                    writer.WriteLine($"{flightNumber},{origin},{destination},{expectedTime:dd/MM/yyyy HH:mm},{specialRequestCode}");
                }
                Console.WriteLine($"\nFlight {flightNumber} has been added!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError writing to file: {ex.Message}");
            }

            // Prompt to add another flight
            Console.Write("Would you like to add another flight? (Y/N): ");
            string addAnotherChoice = Console.ReadLine().Trim().ToUpper();

            if (addAnotherChoice != "Y")
            {
                break;
            }
        }
    }

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
                flight.ExpectedTime.ToString("dd/M/yyyy h:mm:ss tt")); // Format date as in image

           
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

    void DisplayAirlineFlights(Terminal terminal)
    {
        // List all airlines
        Console.WriteLine("\n======================================");
        Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
        Console.WriteLine("======================================");
        Console.WriteLine("{0,-10} {1,-20}", "Airline Code", "Airline Name");

        foreach (var airline in terminal.Airlines)
        {
            Console.WriteLine("{0,-12} {1,-20}", airline.Key, airline.Value.Name);
        }

        // Prompt user for airline code
        Console.Write("\nEnter Airline Code: ");
        string airlineCode = Console.ReadLine().ToUpper();
        if (terminal.Airlines.ContainsKey(airlineCode))
        {
            Airline selectedAirline = terminal.Airlines[airlineCode];   

            // Display flights for selected airline
            Console.WriteLine("\n======================================");
            Console.WriteLine($"List of Flights for {selectedAirline.Name}");
            Console.WriteLine("======================================");
            Console.WriteLine("{0,-12} {1,-20} {2,-20} {3,-20} {4,-10}", "Flight Number", "Airline Name", "Origin", "Destination", "Expected");
            Console.WriteLine("Departure/Arrival Time");

            foreach (var flight in selectedAirline.Flights.Values)
            {
                string airlinePrefix = flight.FlightNumber.Substring(0, 2).Trim(); // Extract airline code
                if(airlinePrefix == airlineCode)
                {
                    Console.WriteLine("{0,-13} {1,-20} {2,-20} {3,-20} {4,-10}",
                    flight.FlightNumber, selectedAirline.Name, flight.Origin, flight.Destination, flight.ExpectedTime);

                }
                


            }
        }
        else
        {
            Console.WriteLine("Invalid airline code.");
        }

    }
    void ModifyFlightDetails(Terminal terminal)
    {
        // List all airlines
        Console.WriteLine("\n======================================");
        Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
        Console.WriteLine("======================================");
        Console.WriteLine("{0,-10} {1,-20}", "Airline Code", "Airline Name");

        foreach (var airline in terminal.Airlines)
        {
            Console.WriteLine("{0,-12} {1,-20}", airline.Key, airline.Value.Name);
        }

        // Prompt user for airline code
        Console.Write("\nEnter Airline Code: ");
        string airlineCode = Console.ReadLine().ToUpper();
        if (terminal.Airlines.ContainsKey(airlineCode))
        {
            Airline selectedAirline = terminal.Airlines[airlineCode];

            // Display flights for selected airline
            Console.WriteLine("\n======================================");
            Console.WriteLine($"List of Flights for {selectedAirline.Name}");
            Console.WriteLine("======================================");
            Console.WriteLine("{0,-12} {1,-20} {2,-20} {3,-20} {4,-10}", "Flight Number", "Airline Name", "Origin", "Destination", "Expected");
            Console.WriteLine("Departure/Arrival Time");

            foreach (var flight in selectedAirline.Flights.Values)
            {
                string airlinePrefix = flight.FlightNumber.Substring(0, 2).Trim(); // Extract airline code
                if (airlinePrefix == airlineCode)
                {
                    Console.WriteLine("{0,-13} {1,-20} {2,-20} {3,-20} {4,-10}",
                    flight.FlightNumber, selectedAirline.Name, flight.Origin, flight.Destination, flight.ExpectedTime);

                }
            }

            Console.Write("Choose an existing Flight to modify or delete: ");
            
            string chosenFlight = Console.ReadLine();

            // Step 4: Choose Modify or Delete
            Console.WriteLine("\n1)Modify Flight  2) Delete Flight");
            Console.Write("Enter your choice: ");
            string action = Console.ReadLine();
            if (action == "1" && chosenFlight != null) // Modify Flight
            {
                ModifyExistingFlight(selectedAirline,chosenFlight,terminal);
            }
            else if (action == "2" && chosenFlight != null) // Delete Flight
            {
                DeleteExistingFlight(selectedAirline,chosenFlight);
            }
            else
            {
                Console.WriteLine("Invalid choice. Returning to menu.");
            }
        }
        else
        {
            Console.WriteLine("Invalid airline code.");
        }
        

        

        void ModifyExistingFlight(Airline airline,string flight,Terminal terminal)
        {
            airline.Flights.TryGetValue(flight, out var data);

            Console.WriteLine("1) Modify Basic Information");
            Console.WriteLine("2) Modify Status");
            Console.WriteLine("3) Modify Special Request Code");
            Console.WriteLine("4) Modify Boarding Gate");

            Console.Write("Enter option: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.Write("Enter new Origin: ");
                    data.Origin = Console.ReadLine();
                    Console.Write("Enter new Destination: ");
                    data.Destination = Console.ReadLine();
                    Console.Write("Enter new Expected Departure/Arrival Time (dd/MM/yyyy HH:mm): ");
                    string timeInput = Console.ReadLine();
                    // Try to parse the input using the specified format
                    if (DateTime.TryParseExact(timeInput, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime newTime))
                    {
                        data.ExpectedTime = newTime;
                        airline.Flights[flight] = data; // Update the dictionary entry
                    }
                    else
                    {
                        Console.WriteLine("Invalid date/time format.");
                    }




                    break;
                
                default:
                    Console.WriteLine("Invalid option.");
                    return;
            }

            Console.WriteLine("\nFlight details updated successfully!");
            Console.WriteLine($"Flight Number: {data.FlightNumber}");
            Console.WriteLine($"Airline Name: {airline.Name}");
            Console.WriteLine($"origin: {data.Origin}");
            Console.WriteLine($"destination: {data.Destination}");
            Console.WriteLine($"Expected Departure/Arrival Time: {data.ExpectedTime}");
            Console.WriteLine($"Status: {data.Status}");


            airline.Flights.TryGetValue(flight, out var chosenFlight);
            
                // Check if Special Request Code exists based on the subclass of the flight
                if (chosenFlight is CFFTFlight)
                {
                    Console.WriteLine($"Special Request Code: CFFT");
                }
                else if (chosenFlight is DDJBFlight)
                {
                    Console.WriteLine($"Special Request Code: DDJB");
                }
                else if (chosenFlight is LWTTFlight)
                {
                    Console.WriteLine($"Special Request Code: LWTT");
                }
                else
                {
                    Console.WriteLine("Special Request Code: None");
                }

                bool found = false;
            // Check if Boarding Gate exists
            foreach (var gate in terminal.BoardingGates.Values)
            {
                if (gate.Flight != null && gate.Flight.FlightNumber != null && gate.Flight.FlightNumber == data.FlightNumber)
                {
                    Console.WriteLine($"Flight {flight} is at Boarding Gate: {gate.GateName}");
                    found = true;
                    break;
                }
                else
                {
                    continue;
                }
            }



            if (!found)
            {
                Console.WriteLine($"Flight {flight} is NOT assigned to any gate.");
            }

        }

        void DeleteExistingFlight(Airline airline,string flight)
        {
            airline.Flights.TryGetValue(flight, out var data);

            if (data.FlightNumber == flight)
            {
                string FlightNumber = data.FlightNumber;    
                

                // Ask the user if they want to delete the flight
                Console.Write("Do you want to delete this flight? (y/n): ");
                string userChoice = Console.ReadLine().ToLower();

                if (userChoice == "y")
                {
                    // Delete the flight from the dictionary
                    airline.Flights.Remove(FlightNumber);
                    Console.WriteLine($"Flight {FlightNumber} has been deleted.");
                }
                else
                {
                    Console.WriteLine("Flight not deleted.");
                }
            }
            else
            {
                Console.WriteLine("Flight not found.");
            }
        }
    }


}