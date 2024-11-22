using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // List Example
        List<string> cities = new List<string> { "New York", "London", "Tokyo" };
        cities.Add("Paris");

        // Dictionary Example
        Dictionary<string, string> capitals = new Dictionary<string, string>
        {
            { "USA", "Washington D.C." },
            { "UK", "London" },
            { "Japan", "Tokyo" }
        };

        // Queue Example
        Queue<int> numbersQueue = new Queue<int>();
        numbersQueue.Enqueue(1);
        numbersQueue.Enqueue(2);

        // Output the collections
        Console.WriteLine("Cities: " + string.Join(", ", cities));
        Console.WriteLine("Capitals: " + string.Join(", ", capitals.Values));
        Console.WriteLine("Queue: " + string.Join(", ", numbersQueue));
    }
}
