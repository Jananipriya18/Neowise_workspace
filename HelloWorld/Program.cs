using System;

class Program
{
    // Step 1: Declare a delegate
    public delegate void TextDelegate(string text);

    // Step 2: Declare an event using the delegate
    public static event TextDelegate TextEvent;

    static void Main(string[] args)
    {
        // Step 3: Subscribe to the event
        TextEvent += PrintText;

        // Step 4: Trigger the event
        TextEvent?.Invoke("Hello using an event!");
    }

    static void PrintText(string text)
    {
        Console.WriteLine(text);
    }
}
