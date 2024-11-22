using System;

class Program
{
    static void Main()
    {
        int number = 25;

        // If Statement
        if (number > 20)
        {
            Console.WriteLine("The number is greater than 20.");
        }

        // If-Else Statement
        if (number % 2 == 0)
        {
            Console.WriteLine("The number is even.");
        }
        else
        {
            Console.WriteLine("The number is odd.");
        }

        // If-Else If-Else Statement
        if (number < 10)
        {
            Console.WriteLine("The number is less than 10.");
        }
        else if (number < 30)
        {
            Console.WriteLine("The number is between 10 and 30.");
        }
        else
        {
            Console.WriteLine("The number is 30 or more.");
        }

        // Switch Statement
        switch (number)
        {
            case 10:
                Console.WriteLine("The number is 10.");
                break;
            case 20:
                Console.WriteLine("The number is 20.");
                break;
            case 25:
                Console.WriteLine("The number is 25.");
                break;
            default:
                Console.WriteLine("The number is something else.");
                break;
        }
    }
}
