import { Component, OnInit } from '@angular/core';
import { Booking } from 'src/app/models/booking.model';
import { BookingService } from 'src/app/services/booking.service';

@Component({
  selector: 'app-admin-view-booking',
  templateUrl: './admin-view-booking.component.html',
  styleUrls: ['./admin-view-booking.component.css']
})
export class AdminViewBookingComponent implements OnInit {

  showDeletePopup = false;
  selectedBooking: Booking;
  partyHalls: any[] = []; // Declare the 'partyHalls' property as an array of any type

  constructor(private bookingService: BookingService) { }

  ngOnInit(): void {
    this.getAllBookings();
  }

  getAllBookings() {
    this.bookingService.getAllBookings().subscribe((response: any) => {
      this.partyHalls = response;
      console.log("partyHalls", this.partyHalls);
      console.log(Array.isArray(this.partyHalls));
    });
  }

  deleteBooking(bookingId: number) {
    this.bookingService.deleteBooking(bookingId).subscribe(
      (response) => {
        console.log('Booking deleted successfully', response);
        this.getAllBookings();
      },
      (error) => {
        console.error('Error deleting booking', error);
      }
    );
  }

  approveBooking(booking): void {
    booking.status = 'APPROVED';
    this.bookingService.updateBooking(booking).subscribe(
      (response) => {
        console.log('Booking updated successfully', response);
        this.getAllBookings();
      },
      (error) => {
        console.error('Error updating booking', error);
      }
    );
  }

  rejectBooking(booking): void {
    booking.status = 'REJECTED';
    this.bookingService.updateBooking(booking).subscribe(
      (response) => {
        console.log('Booking updated successfully', response);
        this.getAllBookings();
      },
      (error) => {
        console.error('Error updating booking', error);
      }
    );
  }
}
