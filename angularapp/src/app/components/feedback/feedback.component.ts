// import { Component } from '@angular/core';
// import { NgForm } from '@angular/forms';

// @Component({
//   selector: 'app-feedback',
//   templateUrl: './feedback.component.html',
//   styleUrls: ['./feedback.component.css']
// })
// export class FeedbackComponent {
//   passengerName: string = '';
//   rating: number = 5;
//   comment: string = '';

//   feedbackList: any[] = []; // Temporary storage for feedback data

//   submitFeedback(form: NgForm) {
//     if (form.valid) {
//       // Create a feedback object with the submitted data
//       const feedback = {
//         passengerName: this.passengerName,
//         rating: this.rating,
//         comment: this.comment
//       };

//       // Add the feedback to the list (temporary storage)
//       this.feedbackList.push(feedback);

//       // Clear the form after submission
//       form.resetForm(); // Reset form fields and set default values
//     }
//   }
// }


import { Component } from '@angular/core';

@Component({
  selector: 'app-feedback',
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.css']
})
export class FeedbackComponent {
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
