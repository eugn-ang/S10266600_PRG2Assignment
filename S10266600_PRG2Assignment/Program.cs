using S10266600_PRG2Assignment;


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