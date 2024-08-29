import { Component } from '@angular/core';

@Component({
  selector: 'app-flight-feedback',
  templateUrl: './flight-feedback.component.html',
  styleUrls: ['./flight-feedback.component.css']
})
export class FlightFeedbackComponent {
  passengerName: string = '';
  rating: number = 5;
  comment: string = '';

  feedbackList: any[] = []; // Temporary storage for feedback data

  submitFeedback() {
    // Create a feedback object with the submitted data
    const feedback = {
      passengerName: this.passengerName,
      rating: this.rating,
      comment: this.comment
    };

    // Add the feedback to the list (temporary storage)
    this.feedbackList.push(feedback);

    // Clear the form after submission
    this.passengerName = '';
    this.rating = 1;
    this.comment = '';
  }
}
