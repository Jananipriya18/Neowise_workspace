using System;

class Program
{
    // Step 1: Declare a delegate
    public delegate void MessageDelegate(string message);

    // Step 2: Declare an event using the delegate
    public static event MessageDelegate MessageEvent;

    static void Main(string[] args)
    {
        // Step 3: Subscribe to the event
        MessageEvent += PrintMessage;

        // Step 4: Trigger the event
        MessageEvent?.Invoke("Hello using an event!");
    }

    static void PrintMessage(string message)
    {
        Console.WriteLine(message);
    }
}
