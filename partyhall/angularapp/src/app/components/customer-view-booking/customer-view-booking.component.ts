import { Component, OnInit } from '@angular/core';
import { Booking } from 'src/app/models/booking.model';
import { BookingService } from 'src/app/services/booking.service';

@Component({
  selector: 'app-customer-view-booking',
  templateUrl: './customer-view-booking.component.html',
  styleUrls: ['./customer-view-booking.component.css'],
})
export class CustomerViewBookingComponent implements OnInit {
  showDeletePopup = false;
  selectedBooking: Booking;
  bookings: Booking[] = [];

  constructor(private bookingService: BookingService) {}

  ngOnInit(): void {
    this.getBookingsByUserId();
  }

  getBookingsByUserId() {
    this.bookingService.getBookingsByUserId().subscribe(
      (response: Booking[]) => {
        console.log('Bookings fetched successfully', response);
        this.bookings = response;
      },
      (error) => {
        console.error('Error fetching bookings', error);
      }
    );
  }

  deleteBooking(selectedBooking: Booking) {
    this.bookingService.deleteBooking(selectedBooking.bookingId).subscribe(
      (response) => {
        console.log('Booking deleted successfully', response);
        this.getBookingsByUserId();
      },
      (error) => {
        console.error('Error deleting booking', error);
      }
    );
  }

  isEditing = false;

  editBooking(booking: Booking) {
    this.selectedBooking = booking;
    this.isEditing = true;
  }

  cancelEdit() {
    this.isEditing = false;
    this.selectedBooking = null;
  }

  updateBooking(booking: Booking): void {
    this.bookingService.updateBooking(booking).subscribe(
      (response) => {
        console.log('Booking updated successfully', response);
        this.getBookingsByUserId();
      },
      (error) => {
        console.error('Error updating booking', error);
      }
    );
    this.isEditing = false;
  }
}
