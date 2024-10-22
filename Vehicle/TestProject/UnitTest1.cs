using NUnit.Framework;
using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

namespace dotnetapp.Tests
{
    [TestFixture]
    public class ReflectionTests
    {
        private Assembly assembly;
        private Type vehicleType;
        private Type programType;

        [SetUp]
        public void Setup()
        {
            assembly = Assembly.LoadFrom("dotnetapp.dll");
            vehicleType = assembly.GetType("Vehicle");
            programType = assembly.GetType("Program");
        }

        [Test]
        public void VehicleClass_ShouldExist()
        {
            Assert.IsNotNull(vehicleType, "Vehicle class does not exist.");
        }

        [Test]
        public void VehicleClass_ShouldHaveCorrectProperties()
        {
            Assert.IsNotNull(vehicleType.GetProperty("VehicleName"), "Vehicle class does not have VehicleName property.");
            Assert.IsNotNull(vehicleType.GetProperty("VehicleNumber"), "Vehicle class does not have VehicleNumber property.");
            Assert.IsNotNull(vehicleType.GetProperty("Type"), "Vehicle class does not have Type property.");
            Assert.IsNotNull(vehicleType.GetProperty("DailyRentalRate"), "Vehicle class does not have DailyRentalRate property.");
            Assert.IsNotNull(vehicleType.GetProperty("SeatingCapacity"), "Vehicle class does not have SeatingCapacity property.");
        }

        [Test]
        public void ProgramClass_ShouldHaveCorrectFields()
        {
            Assert.IsNotNull(programType.GetField("vehicles", BindingFlags.NonPublic | BindingFlags.Static), "Program class does not have 'vehicles' field.");
        }

        [Test]
        public void ProgramClass_ShouldHaveCorrectMethods()
        {
            Assert.IsNotNull(programType.GetMethod("AddVehicle"), "Program class does not have AddVehicle method.");
            Assert.IsNotNull(programType.GetMethod("DisplayVehicles"), "Program class does not have DisplayVehicles method.");
            Assert.IsNotNull(programType.GetMethod("SearchBySeatingCapacity"), "Program class does not have SearchBySeatingCapacity method.");
            Assert.IsNotNull(programType.GetMethod("DeleteVehicle"), "Program class does not have DeleteVehicle method.");
        }

        [Test]
        public void ProgramClass_AddVehicleMethod_ShouldAddVehicle_WhenVehicleDoesNotExist()
        {
            var vehicle = Activator.CreateInstance(vehicleType);
            vehicleType.GetProperty("VehicleName").SetValue(vehicle, "Car A");
            vehicleType.GetProperty("VehicleNumber").SetValue(vehicle, "V001");
            vehicleType.GetProperty("Type").SetValue(vehicle, "Sedan");
            vehicleType.GetProperty("DailyRentalRate").SetValue(vehicle, 50.00m);
            vehicleType.GetProperty("SeatingCapacity").SetValue(vehicle, 5);

            var method = programType.GetMethod("AddVehicle");
            var instance = Activator.CreateInstance(programType);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                method.Invoke(instance, new object[] { "Car A", "V001", "Sedan", 50.00m, 5 });
                var output = sw.ToString().Trim();
                Assert.AreEqual("Vehicle added successfully.", output);
            }
        }

        [Test]
        public void ProgramClass_SearchBySeatingCapacityMethod_ShouldDisplayVehicleDetailsIfMatchingSeatingCapacityExists()
        {
            var addMethod = programType.GetMethod("AddVehicle");
            var instance = Activator.CreateInstance(programType);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                addMethod.Invoke(instance, new object[] { "Car A", "V001", "Sedan", 50.00m, 5 });
            }

            var searchMethod = programType.GetMethod("SearchBySeatingCapacity");
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                searchMethod.Invoke(instance, new object[] { 5 });
                var output = sw.ToString().Trim();
                StringAssert.Contains("Vehicle Name: Car A", output);
                StringAssert.Contains("Vehicle Number: V001", output);
                StringAssert.Contains("Type: Sedan", output);
                StringAssert.Contains("Daily Rental Rate: 50.00", output);
                StringAssert.Contains("Seating Capacity: 5", output);
            }
        }

        [Test]
        public void ProgramClass_SearchBySeatingCapacityMethod_ShouldDisplayNoVehiclesFoundMessageIfNoMatchingSeatingCapacityExists()
        {
            var instance = Activator.CreateInstance(programType);
            var method = programType.GetMethod("SearchBySeatingCapacity");

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                method.Invoke(instance, new object[] { 10 });
                var output = sw.ToString().Trim();
                Assert.AreEqual("No vehicles found with seating capacity of 10.", output);
            }
        }

        [Test]
        public void ProgramClass_DeleteVehicleMethod_ShouldDeleteVehicle_WhenVehicleExists()
        {
            var addMethod = programType.GetMethod("AddVehicle");
            var instance = Activator.CreateInstance(programType);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                addMethod.Invoke(instance, new object[] { "Car A", "V001", "Sedan", 50.00m, 5 });
            }

            var deleteMethod = programType.GetMethod("DeleteVehicle");
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                deleteMethod.Invoke(instance, new object[] { "V001" });
                var output = sw.ToString().Trim();
                Assert.AreEqual("Vehicle with Number V001 has been deleted.", output);
            }
        }

        [Test]
        public void ProgramClass_DeleteVehicleMethod_ShouldNotDeleteVehicle_WhenVehicleDoesNotExist()
        {
            var instance = Activator.CreateInstance(programType);
            var deleteMethod = programType.GetMethod("DeleteVehicle");

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                deleteMethod.Invoke(instance, new object[] { "V999" });
                var output = sw.ToString().Trim();
                Assert.AreEqual("No vehicle found with Number V999.", output);
            }
        }

        [Test]
        public void ProgramClass_DisplayVehiclesMethod_ShouldDisplayAllVehicles_WhenVehiclesExist()
        {
            var addMethod = programType.GetMethod("AddVehicle");
            var instance = Activator.CreateInstance(programType);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                addMethod.Invoke(instance, new object[] { "Car A", "V001", "Sedan", 50.00m, 5 });
                addMethod.Invoke(instance, new object[] { "Car B", "V002", "SUV", 70.00m, 7 });
            }

            var displayMethod = programType.GetMethod("DisplayVehicles");
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                displayMethod.Invoke(instance, null);
                var output = sw.ToString().Trim();
                StringAssert.Contains("Vehicle Name: Car A", output);
                StringAssert.Contains("Vehicle Number: V001", output);
                StringAssert.Contains("Type: Sedan", output);
                StringAssert.Contains("Daily Rental Rate: 50.00", output);
                StringAssert.Contains("Seating Capacity: 5", output);
                StringAssert.Contains("Vehicle Name: Car B", output);
                StringAssert.Contains("Vehicle Number: V002", output);
                StringAssert.Contains("Type: SUV", output);
                StringAssert.Contains("Daily Rental Rate: 70.00", output);
                StringAssert.Contains("Seating Capacity: 7", output);
            }
        }

    }
}