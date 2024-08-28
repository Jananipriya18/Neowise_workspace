// feedback.component.ts
import { Component } from '@angular/core';

@Component({
  selector: 'app-feedback',
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.css']
})
export class FeedbackComponent {
  guestName: string = '';
  rating: number = 5;
  comment: string = '';

  feedbackList: any[] = []; // Temporary storage for feedback data

  submitFeedback() {
    // Create a feedback object with the submitted data
    const feedback = {
      guestName: this.guestName,
      rating: this.rating,
      comment: this.comment
    };

    // Add the feedback to the list (temporary storage)
    this.feedbackList.push(feedback);

    // Clear the form after submission
    this.guestName = '';
    this.rating = 1;
    this.comment = '';
  }
}
