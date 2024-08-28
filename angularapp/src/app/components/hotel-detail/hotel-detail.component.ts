import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HotelService } from '../../services/hotel.service'; // Import your service

@Component({
  selector: 'app-hotel-detail',
  templateUrl: './hotel-detail.component.html',
  styleUrls: ['./hotel-detail.component.css']
})
export class HotelDetailComponent implements OnInit {
  hotel: any;
  checkInDate: string=''; // Declare checkInDate property
  checkOutDate: string='';// Define the hotel property

  constructor(
    private route: ActivatedRoute,
    private hotelService: HotelService // Inject your service
  ) {}

  ngOnInit() {
    const hotelId = +this.route.snapshot.paramMap.get('id')! // Assuming 'id' is the route parameter
    this.hotel = this.hotelService.getHotelById(hotelId); // Fetch the hotel data from your service
  }

  bookHotel() {
    // Implement booking logic here, e.g., send a request to a backend API
    // and handle the booking process.
    alert('Hotel booked successfully: ' + this.hotel.name);
  }

  makeReservation() {
    // Check availability for the specified dates
    const available = this.hotelService.checkAvailability(
      this.hotel.id,
      this.checkInDate,
      this.checkOutDate
    );

    if (available) {
      // Reserve rooms if available
      this.hotelService.reserveRooms(this.hotel.id, 1); // Assuming reserving one room
      alert('Reservation successful!');
    } else {
      alert('Rooms are not available for the selected dates.');
    }
  }
}
