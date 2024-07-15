import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Review } from 'src/app/models/review.model'; // Assuming you have the Review model
import { BookingService } from 'src/app/services/booking.service';
import { PartyhallService } from 'src/app/services/partyhall.service';

@Component({
  selector: 'app-add-review',
  templateUrl: './add-review.component.html',
  styleUrls: ['./add-review.component.css'],
})
export class AddReviewComponent implements OnInit {
  addReviewForm: FormGroup;
  errorMessage = '';
  partyhalls: any[] = [];

  constructor(private fb: FormBuilder, private partyhallService: PartyhallService, private bookingService: BookingService, private router: Router) {
    this.addReviewForm = this.fb.group({
      userName: [localStorage.getItem('userName'), Validators.required],
      subject: ['', Validators.required],
      body: ['', Validators.required],
      rating: ['', Validators.required],
      dateCreated: [this.getCurrentDate(), Validators.required],
    });
  }

  ngOnInit() {
    console.log(localStorage.getItem('userName'))
    this.getBookingsByUserId();
  }

  getBookingsByUserId() {
    this.bookingService.getBookingsByUserId().subscribe(
      (data: any) => {
        this.partyhalls = data;
        console.log(this.partyhalls)
      },
      (err) => {
        console.log(err);
      }
    );
  }

  getCurrentDate(): string {
    const currentDate = new Date();
    const year = currentDate.getFullYear();
    const month = ('0' + (currentDate.getMonth() + 1)).slice(-2);
    const day = ('0' + currentDate.getDate()).slice(-2);
    return `${year}-${month}-${day}`;
  }

  onSubmit(): void {
    if (this.addReviewForm.valid) {
      const newReview = this.addReviewForm.value;
      const requestObj: Review = {
        userId: Number(localStorage.getItem('userId')),
        subject: newReview.subject,
        body: newReview.body,
        rating: newReview.rating,
        dateCreated: newReview.dateCreated,
      };
      console.log(requestObj);

      this.partyhallService.addReview(requestObj).subscribe(
        (response) => {
          console.log('Review added successfully', response);
          this.router.navigate(['/customer/view/review']);
          this.addReviewForm.reset(); // Reset the form
        },
        (error) => {
          console.error('Error adding review', error);
        }
      );
    } else {
      this.errorMessage = 'All fields are required';
    }
  }
}
