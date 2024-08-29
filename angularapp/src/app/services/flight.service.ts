import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class FlightService {
  private flights = [
    {
      id: 1,
      name: 'Beluga XL',
      airline: 'Airbus',
      description: 'The Airbus Beluga XL is a unique cargo aircraft designed to transport large aircraft components. It features an enormous cargo hold and is known for its distinctive whale-like appearance. The Beluga XL offers a state-of-the-art cargo capacity and is a key player in Airbus’s logistics and transportation operations.',
      price: 100000, // Example price for leasing or using the aircraft
      availableSeats: 0, // Not applicable as it's a cargo aircraft
      departureDate: '2024-09-15', // Adjusted date
      arrivalDate: '2024-09-15', // Adjusted date
      imageSrc: '../assets/Beluga_XL.jpg' // Ensure the image path is correct
    },    
    {
      id: 2,
      name: 'A330',
      airline: 'Emirates',
      description: 'Flight 202 offers a premium flying experience from City C to City D. Enjoy business class seats with gourmet meals and complimentary beverages. The Airbus A330 provides a comfortable and luxurious journey with state-of-the-art amenities.',
      price: 450, // Price per ticket
      availableSeats: 20, // Number of available seats
      departureDate: '2023-09-18', // Date of departure
      arrivalDate: '2023-09-18', // Date of arrival
      imageSrc: 'assets/A330.jpg' // Path to the Airbus A330 image
    },    
    {
      id: 3,
      name: 'Flight 303',
      airline: 'Airline C',
      description: 'Flight 303 provides economy and business class options for a comfortable journey from City E to City F. Departure at 02:00 PM, arrival at 06:00 PM.',
      price: 250,
      availableSeats: 60,
      departureDate: '2023-09-24',
      arrivalDate: '2023-09-24',
      imageSrc: 'assets/flight3.jpg'
    },
    {
      id: 4,
      name: 'Flight 404',
      airline: 'Airline D',
      description: 'Flight 404 is a direct flight from City G to City H with various in-flight amenities. Departure at 10:00 AM, arrival at 02:00 PM.',
      price: 320,
      availableSeats: 40,
      departureDate: '2023-09-19',
      arrivalDate: '2023-09-19',
      imageSrc: 'assets/flight4.jpg'
    },
    {
      id: 5,
      name: 'Flight 505',
      airline: 'Airline E',
      description: 'Flight 505 offers a luxurious flying experience from City I to City J, with fully reclining seats and personal entertainment systems.',
      price: 600,
      availableSeats: 15,
      departureDate: '2023-09-25',
      arrivalDate: '2023-09-25',
      imageSrc: 'assets/flight5.jpg'
    },
    {
      id: 6,
      name: 'Flight 606',
      airline: 'Airline F',
      description: 'Flight 606 is a budget-friendly option from City K to City L, providing basic amenities with no-frills service.',
      price: 150,
      availableSeats: 100,
      departureDate: '2023-09-18',
      arrivalDate: '2023-09-18',
      imageSrc: 'assets/flight6.jpg'
    }
    // Add more flights as needed...
  ];

  private reservations: any[] = [
    {
      flightId: 1,
      departureDate: '2023-09-15',
      arrivalDate: '2023-09-15',
      seatsReserved: 5
    },
    {
      flightId: 2,
      departureDate: '2023-09-18',
      arrivalDate: '2023-09-18',
      seatsReserved: 2
    },
    // Add more reservations here
  ];

  getAllFlights() {
    return this.flights;
  }

  getFlightById(id: number) {
    return this.flights.find((flight) => flight.id === id);
  }

  checkAvailability(flightId: number, departureDate: string): boolean {
    const flight = this.flights.find(f => f.id === flightId);

    if (!flight) {
      return false; // Flight not found
    }

    const reservationsForFlight = this.reservations.filter(r => r.flightId === flightId);
    const flightDate = new Date(departureDate);

    // Check if seats are available on the specified date
    let totalReservedSeats = 0;

    for (const reservation of reservationsForFlight) {
      const resDepartureDate = new Date(reservation.departureDate);

      // Check if there's any reservation on the requested date
      if (flightDate.getTime() === resDepartureDate.getTime()) {
        totalReservedSeats += reservation.seatsReserved;
      }
    }

    const availableSeats = flight.availableSeats - totalReservedSeats;

    return availableSeats > 0;
  }

  reserveSeats(flightId: number, seatsToReserve: number): void {
    const flight = this.flights.find(f => f.id === flightId);

    if (flight) {
      if (seatsToReserve > 0 && seatsToReserve <= flight.availableSeats) {
        // Check if there are enough available seats to reserve
        flight.availableSeats -= seatsToReserve; // Update availableSeats
      } else {
        console.log(`Not enough available seats to reserve on ${flight.name}.`);
      }
    } else {
      console.log(`Flight with ID ${flightId} not found.`);
    }
  }
}
