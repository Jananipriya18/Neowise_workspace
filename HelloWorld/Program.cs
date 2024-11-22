using System;
using System.Linq;
using System.Collections.Generic;
class Program
{
    static void Main()
    {
        List<int> numbers = new List<int> { 10, 1, 7, 9, 2, 6, 5, 3, 4, 8 };    
        // Sort the numbers in ascending order
        var sortedNumbers = numbers.OrderBy(num => num);
        
        // Filter the numbers to get only the even ones
        var evenNumbers = sortedNumbers.Where(num => num % 2 == 0);
        
        // Find the sum of the even numbers
        var sumOfEvenNumbers = evenNumbers.Sum();
        
        // Output the sorted and filtered numbers
        Console.WriteLine("Sorted even numbers: ");
        foreach (var num in evenNumbers)
        {
            Console.WriteLine(num);
        }     
        Console.WriteLine("Sum of even numbers: " + sumOfEvenNumbers);
    }
}
