import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FlightService } from '../../services/flight.service'; // Import your service

@Component({
  selector: 'app-flight-detail',
  templateUrl: './flight-detail.component.html',
  styleUrls: ['./flight-detail.component.css']
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
  }

  bookFlight() {
    // Implement booking logic here, e.g., send a request to a backend API
    // and handle the booking process.
    alert('Flight booked successfully: ' + this.flight.name);
  }

  makeReservation() {
    // Check availability for the specified dates
    const available = this.flightService.checkAvailability(
      this.flight.id,
      this.departureDate,
      this.arrivalDate
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
