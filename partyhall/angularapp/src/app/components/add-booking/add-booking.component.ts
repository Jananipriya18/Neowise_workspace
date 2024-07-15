import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { BookingService } from 'src/app/services/booking.service';
import { Booking } from 'src/app/models/booking.model';
import { PartyhallService } from 'src/app/services/partyhall.service';

@Component({
  selector: 'app-add-booking',
  templateUrl: './add-booking.component.html',
  styleUrls: ['./add-booking.component.css'],
})
export class AddBookingComponent implements OnInit {
  partyHall: any = [];
  bookings: any = [];
  addBookingForm: FormGroup;
  errorMessage = '';
  confirmPayment = false;
  paymentSuccess = false;

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private partyHallService: PartyhallService,
    private bookingService: BookingService,
    private router: Router
  ) {
    this.addBookingForm = this.fb.group({
      hallName: [''],
      hallLocation: [''],
      totalPrice: [''],
      capacity: [''],
      address: ['', Validators.required],
      noOfPersons: ['', [Validators.required, Validators.max(0)]], // max value will be updated dynamically
      fromDate: ['', Validators.required],
      toDate: ['', Validators.required],
    }, { validators: this.dateRangeValidator });
  }

  ngOnInit() {
    const hallId = this.route.snapshot.paramMap.get('id');
    this.getPartyHallById(parseInt(hallId));
    this.getAllBookings();

  }

  dateRangeValidator(control: AbstractControl): { [key: string]: boolean } | null {
    const fromDate = control.get('fromDate')?.value;
    const toDate = control.get('toDate')?.value;

    if (fromDate && toDate) {
      const fromDateObj = new Date(fromDate);
      const toDateObj = new Date(toDate);

      if (fromDateObj > toDateObj) {
        return { 'dateRangeError': true };
      }
    }

    return null;
  }

  getAllBookings() {

    console.log("response came");

    this.bookingService.getAllBookings().subscribe((response: any) => {

      console.log("response",response);
      this.bookings = response.filter((booking) => booking.partyHallId == this.route.snapshot.paramMap.get('id'));
    });



  }

  getPartyHallById(hallId: number) {
    this.partyHallService.getPartyHallById(hallId).subscribe((response: any) => {
      this.partyHall = response;
      this.addBookingForm.patchValue({
        hallName: response.hallName,
        hallLocation: response.hallLocation,
        totalPrice: response.price,
        capacity: response.capacity,
      });
      this.addBookingForm.get('noOfPersons').setValidators([Validators.required, Validators.max(response.capacity)]);
    });
  }

  getTodayDate(): string {
    const today = new Date();
    const month = (today.getMonth() + 1).toString().padStart(2, '0');
    const day = today.getDate().toString().padStart(2, '0');
    return `${today.getFullYear()}-${month}-${day}`;
  }

  onSubmit(): void {
    if (this.addBookingForm.valid) {
      const newBooking = this.addBookingForm.value;

      const overlappingBookings = this.bookings.filter((booking) => {
        const fromDate = new Date(newBooking.fromDate);
        const toDate = new Date(newBooking.toDate);
        const bookingFromDate = new Date(booking.fromDate);
        const bookingToDate = new Date(booking.toDate);

        return (
          (fromDate >= bookingFromDate && fromDate <= bookingToDate) ||
          (toDate >= bookingFromDate && toDate <= bookingToDate) ||
          (fromDate <= bookingFromDate && toDate >= bookingToDate)
        );
      });
    console.log("overlappingBookings",overlappingBookings);
    console.log("newBooking",newBooking);
    console.log("this.bookings",this.bookings);


      if (newBooking.noOfPersons > this.partyHall.capacity) {
        this.errorMessage = "Number of persons count exceeds party hall capacity";
      } else if (overlappingBookings.length) {
        let totalNoOfPersonsInDateRange = overlappingBookings.reduce((sum, booking) => sum + booking.noOfPersons, 0);
        if (totalNoOfPersonsInDateRange + newBooking.noOfPersons > this.partyHall.capacity) {
          this.errorMessage = 'Capacity exceeded for selected dates. Please choose different dates or reduce the number of persons.';
        } else {
          this.errorMessage = "";
          this.confirmPayment = true;
        }
      } else {
        this.errorMessage = "";
        this.confirmPayment = true;
      }
    } else {
      this.errorMessage = 'All fields are required';
    }
  }

  navigateToDashboard() {
    this.router.navigate(['/customer/view/bookings']);
  }

  makePayment() {
    const newBooking = this.addBookingForm.value;
    const requestObj: Booking = {
      userId: Number(localStorage.getItem('userId')),
      partyHallId: this.partyHall.partyHallId,
      address: newBooking.address,
      noOfPersons: newBooking.noOfPersons,
      fromDate: newBooking.fromDate,
      toDate: newBooking.toDate,
      totalPrice: newBooking.totalPrice,
      status: 'PENDING',
    };
    console.log("requestObj",{
      userId: Number(localStorage.getItem('userId')),
      partyHallId: this.partyHall.partyHallId,
      address: newBooking.address,
      noOfPersons: newBooking.noOfPersons,
      fromDate: newBooking.fromDate,
      toDate: newBooking.toDate,
      totalPrice: newBooking.totalPrice,
      status: 'PENDING',
    });
    this.bookingService.addBooking(requestObj).subscribe(
      (response) => {
        this.addBookingForm.reset();
        this.paymentSuccess = true;
        this.confirmPayment = false;
      },
      (error) => {
        console.error('Error adding booking', error);
      }
    );
  }

  cancelPayment() {
    this.confirmPayment = false;
  }
}
