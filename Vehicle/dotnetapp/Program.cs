﻿using System;
using System.Collections.Generic;
using System.Linq;

public class Vehicle
{
    public string VehicleName { get; set; }
    public string VehicleNumber { get; set; }
    public string Type { get; set; }
    public decimal DailyRentalRate { get; set; }
    public int SeatingCapacity { get; set; }

    public void DisplayVehicleDetails()
    {
        Console.WriteLine($"Vehicle Name: {VehicleName}");
        Console.WriteLine($"Vehicle Number: {VehicleNumber}");
        Console.WriteLine($"Type: {Type}");
        Console.WriteLine($"Daily Rental Rate: {DailyRentalRate}");
        Console.WriteLine($"Seating Capacity: {SeatingCapacity}");
    }
}

public class Program
{
    private static List<Vehicle> vehicles = new List<Vehicle>();

    public static void AddVehicle(string name, string number, string type, decimal rentalRate, int seatingCapacity)
    {
        Vehicle vehicle = new Vehicle
        {
            VehicleName = name,
            VehicleNumber = number,
            Type = type,
            DailyRentalRate = rentalRate,
            SeatingCapacity = seatingCapacity
        };

        vehicles.Add(vehicle);
        Console.WriteLine("Vehicle added successfully.");
    }

    public static void DisplayVehicles()
    {
        if (vehicles.Count == 0)
        {
            Console.WriteLine("No vehicles available.");
        }
        else
        {
            Console.WriteLine("Vehicle Details:");
            foreach (var vehicle in vehicles)
            {
                vehicle.DisplayVehicleDetails();
                Console.WriteLine(); // Add a blank line for better readability
            }
        }
    }

    public static void SearchBySeatingCapacity(int seatingCapacity)
    {
        var matchingVehicles = vehicles.Where(v => v.SeatingCapacity == seatingCapacity).ToList();

        if (matchingVehicles.Count > 0)
        {
            Console.WriteLine($"Vehicles with seating capacity of {seatingCapacity}:");
            foreach (var vehicle in matchingVehicles)
            {
                vehicle.DisplayVehicleDetails();
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine($"No vehicles found with seating capacity of {seatingCapacity}.");
        }
    }

    public static void DeleteVehicle(string numberToDelete)
    {
        if (vehicles.Count == 0)
        {
            Console.WriteLine("No vehicles available to delete.");
            return;
        }

        Vehicle vehicleToDelete = vehicles.FirstOrDefault(v => v.VehicleNumber == numberToDelete);

        if (vehicleToDelete != null)
        {
            vehicles.Remove(vehicleToDelete);
            Console.WriteLine($"Vehicle with Number {numberToDelete} has been deleted.");
        }
        else
        {
            Console.WriteLine($"No vehicle found with Number {numberToDelete}.");
        }
    }

    public static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Main Menu:");
            Console.WriteLine("1. Add Vehicle");
            Console.WriteLine("2. Display Vehicles");
            Console.WriteLine("3. Search by Seating Capacity");
            Console.WriteLine("4. Delete Vehicle");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": // Add vehicle
                    string name = Console.ReadLine();
                    string number = Console.ReadLine();
                    string type = Console.ReadLine();
                    decimal rentalRate = decimal.Parse(Console.ReadLine());
                    int seatingCapacity = int.Parse(Console.ReadLine());

                    AddVehicle(name, number, type, rentalRate, seatingCapacity);
                    break;

                case "2": // Display vehicles
                    DisplayVehicles();
                    break;

                case "3": // Search by seating capacity
                    Console.Write("Enter the seating capacity to search for: ");
                    if (int.TryParse(Console.ReadLine(), out int capacity))
                    {
                        SearchBySeatingCapacity(capacity);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid seating capacity.");
                    }
                    break;

                case "4": // Delete vehicle
                    // Console.Write("Enter the Vehicle Number to delete: ");
                    string numberToDelete = Console.ReadLine();
                    DeleteVehicle(numberToDelete);
                    break;

                case "5": // Exit
                    Console.WriteLine("Exiting program...");
                    return;

                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        }
    }
}