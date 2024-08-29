import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class FlightService {
  private flights = [
    {
      id: 1,
      name: 'Airbus A320',
      airline: 'Air India',
      description: 'The Airbus A320 operated by Air India offers a comfortable and reliable experience for short to medium-haul flights. Enjoy complimentary meals, Wi-Fi, and in-flight entertainment in a spacious cabin with a focus on passenger comfort. Fly from Mumbai (BOM) to Delhi (DEL) and experience Air India’s renowned service.',
      price: 300, // Price per ticket
      availableSeats: 150, // Number of available seats
      departureDate: '2023-09-15', // Departure date
      arrivalDate: '2023-09-15', // Arrival date
      imageSrc: '../assets/AirIndia.jpg' // Image for Air India Airbus A320
    },
    {
      id: 2,
      name: 'Airbus A330',
      airline: 'Emirates',
      description: 'The Airbus A330 operated by Emirates provides a luxurious flying experience from Dubai (DXB) to London (LHR). Enjoy spacious business class seats, gourmet meals, and a wide range of entertainment options on this long-haul flight. Emirates ensures a comfortable and premium journey across continents.',
      price: 450, // Price per ticket
      availableSeats: 20, // Number of available seats
      departureDate: '2023-09-18', // Date of departure
      arrivalDate: '2023-09-18', // Date of arrival
      imageSrc: 'assets/A330.jpg' // Path to the Airbus A330 image
    },
    {
      id: 3,
      name: 'Airbus A220-300',
      airline: 'Swiss International Air Lines',
      description: 'The Airbus A220-300 by Swiss International Air Lines offers a modern and comfortable flying experience from Zurich (ZRH) to Vienna (VIE). Enjoy spacious seating, quiet cabins, and advanced technology, with options for both economy and business class. Ideal for a pleasant and efficient journey in Europe.',
      price: 250,
      availableSeats: 60,
      departureDate: '2023-09-24',
      arrivalDate: '2023-09-24',
      imageSrc: 'assets/a220-300.png'
    },
    {
      id: 4,
      name: 'IndiGo A320',
      airline: 'IndiGo',
      description: 'IndiGo’s A320 offers a direct and convenient flight from Mumbai (BOM) to Delhi (DEL). This budget-friendly service focuses on providing essential amenities and reliable service. Ideal for travelers looking for efficiency and value on their domestic journey in India.',
      price: 320,
      availableSeats: 40,
      departureDate: '2023-09-19',
      arrivalDate: '2023-09-19',
      imageSrc: 'assets/indigo_airlines.png' // Image for IndiGo Airlines
    },
    {
      id: 5,
      name: 'Airbus A380',
      airline: 'Emirates',
      description: 'Experience unparalleled luxury on Emirates’ Airbus A380 from New York (JFK) to Dubai (DXB). This flight offers fully reclining seats, personal entertainment systems, and world-class amenities, making it perfect for long-haul travelers seeking comfort and elegance.',
      price: 600,
      availableSeats: 15,
      departureDate: '2023-09-25',
      arrivalDate: '2023-09-25',
      imageSrc: 'assets/Airbus A380 Emirates.jpg' // Image for Emirates Airbus A380
    },
    {
      id: 6,
      name: 'Qantas A380',
      airline: 'Qantas',
      description: 'Qantas’s A380 flight from Sydney (SYD) to Los Angeles (LAX) offers a premium travel experience with spacious seating and high-quality in-flight services. Enjoy a comfortable journey across the Pacific with Qantas, known for its exceptional service and attention to detail.',
      price: 150,
      availableSeats: 100,
      departureDate: '2023-09-18',
      arrivalDate: '2023-09-18',
      imageSrc: 'assets/Qantas Airbus A380.jpg' // Image for Qantas Airbus A380
    },
    {
      id: 7,
      name: 'Flydubai Boeing 737',
      airline: 'Flydubai',
      description: 'Flydubai offers an economical option from Dubai (DXB) to Istanbul (IST) on its Boeing 737. This budget-friendly flight provides essential amenities for a comfortable journey while keeping costs low. Perfect for travelers seeking affordability on their trip between these two vibrant cities.',
      price: 150,
      availableSeats: 100,
      departureDate: '2023-09-18',
      arrivalDate: '2023-09-18',
      imageSrc: 'assets/flydubai.jpg' // Image for Flydubai Boeing 737
    },
    {
      id: 8,
      name: 'Red Wings Boeing 777',
      airline: 'Red Wings',
      description: 'Red Wings’ Boeing 777 offers a cost-effective flight from Moscow (DME) to Sochi (AER). This no-frills service provides the basics for a comfortable travel experience, focusing on efficiency and value for travelers between Russia’s major cities.',
      price: 150,
      availableSeats: 100,
      departureDate: '2023-09-18',
      arrivalDate: '2023-09-18',
      imageSrc: 'assets/red_wings.jpg' // Image for Red Wings Boeing 777
    }
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
