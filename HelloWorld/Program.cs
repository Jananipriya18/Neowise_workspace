using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Program started.");
        await SimulateTaskAsync();
        Console.WriteLine("Program ended.");
    }

    static async Task SimulateTaskAsync()
    {
        Console.WriteLine("Task started...");
        await Task.Delay(2000); // Simulates a delay
        Console.WriteLine("Task completed.");
    }
}
