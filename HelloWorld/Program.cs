using System;

class Engine
{
    public void Start()
    {
        Console.WriteLine("Engine started.");
    }
}

class Car
{
    private Engine _engine;

    public Car()
    {
        // The Car class is responsible for creating the Engine
        _engine = new Engine();
    }

    public void Drive()
    {
        _engine.Start();
        Console.WriteLine("Car is driving.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Car myCar = new Car();  // Car creates its own Engine
        myCar.Drive();
    }
}
