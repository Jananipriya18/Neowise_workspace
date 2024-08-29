// import { Component, OnInit } from '@angular/core';
// import { ActivatedRoute } from '@angular/router';
// import { FlightService } from '../../services/flight.service'; // Import your service

// @Component({
//   selector: 'app-flight-booking',
//   templateUrl: './flight-booking.component.html',
//   styleUrls: ['./flight-booking.component.css']
// })
// export class FlightBookingComponent implements OnInit {
//   flight: any;
//   departureDate: string = ''; // Declare departureDate property
//   arrivalDate: string = ''; // Declare arrivalDate property

//   constructor(
//     private route: ActivatedRoute,
//     private flightService: FlightService // Inject your service
//   ) {}

//   ngOnInit() {
//     const flightId = +this.route.snapshot.paramMap.get('id')!; // Assuming 'id' is the route parameter
//     this.flight = this.flightService.getFlightById(flightId); // Fetch the flight data from your service
//     // Optionally initialize departureDate and arrivalDate if needed
//     if (this.flight) {
//       this.departureDate = this.flight.departureDate;
//       this.arrivalDate = this.flight.arrivalDate;
//     }
//   }

//   bookFlight() {
//     // Implement booking logic here, e.g., send a request to a backend API
//     // and handle the booking process.
//     alert('Flight booked successfully: ' + this.flight.name);
//   }

//   makeReservation() {
//     if (!this.flight) {
//       alert('No flight selected.');
//       return;
//     }

//     // Check availability for the specified departure date
//     const available = this.flightService.checkAvailability(
//       this.flight.id,
//       this.departureDate
//     );

//     if (available) {
//       // Reserve seats if available
//       this.flightService.reserveSeats(this.flight.id, 1); // Assuming reserving one seat
//       alert('Reservation successful!');
//     } else {
//       alert('Seats are not available for the selected dates.');
//     }
//   }
// }
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FlightService } from '../../services/flight.service'; // Import your service

@Component({
  selector: 'app-flight-booking',
  templateUrl: './flight-booking.component.html',
  styleUrls: ['./flight-booking.component.css']
})
export class FlightBookingComponent implements OnInit {
  flight: any;
  departureDate: string = ''; // Declare departureDate property
  arrivalDate: string = ''; // Declare arrivalDate property

  constructor(
    private route: ActivatedRoute,
    private flightService: FlightService // Inject your service
  ) {}

  ngOnInit() {
    const flightId = +this.route.snapshot.paramMap.get('id')!; // Assuming 'id' is the route parameter
    this.flight = this.flightService.getFlightById(flightId); // Fetch the flight data from your service

    if (!this.flight) {
      // Handle the case where the flight is not found
      alert('Flight not found.');
    } else {
      // Initialize departureDate and arrivalDate if the flight is found
      this.departureDate = this.flight.departureDate;
      this.arrivalDate = this.flight.arrivalDate;
    }
  }

  bookFlight() {
    if (!this.flight) {
      alert('No flight selected.');
      return;
    }

    // Implement booking logic here, e.g., send a request to a backend API
    // and handle the booking process.
    alert('Flight booked successfully: ' + this.flight.name);
  }

  makeReservation() {
    if (!this.flight) {
      alert('No flight selected.');
      return;
    }

    // Check availability for the specified departure date
    const available = this.flightService.checkAvailability(
      this.flight.id,
      this.departureDate
    );

    if (available) {
      // Reserve seats if available
      this.flightService.reserveSeats(this.flight.id, 1); // Assuming reserving one seat
      alert('Reservation successful!');
    } else {
      alert('Seats are not available for the selected dates.');
    }
  }
}
