using System;

class Program
{
    static void Main()
    {
        try
        {
            Console.WriteLine("Enter a number:");
            int num = int.Parse(Console.ReadLine());
            int result = 10 / num;
            Console.WriteLine("Result: " + result);
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine("Error: Cannot divide by zero!");
        }
        catch (FormatException ex)
        {
            Console.WriteLine("Error: Invalid input, please enter a valid number.");
        }
        finally
        {
            Console.WriteLine("Execution complete.");
        }
    }
}
