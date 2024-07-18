import { Component, OnInit } from '@angular/core';
import { FeedbackService } from 'src/app/services/feedback.service';

@Component({
  selector: 'app-analystviewfeedback',
  templateUrl: './analystviewfeedback.component.html',
  styleUrls: ['./analystviewfeedback.component.css']
})


export class AnalystviewfeedbackComponent implements OnInit {
  feedbacks: any[] = [];
  showDeletePopup = false;
  feedbackToDelete: string;

  constructor(private feedbackService: FeedbackService) { }

  ngOnInit(): void {
    this.getAllLoansByUserId();
  }

  getAllLoansByUserId() {
    const userId = localStorage.getItem('userId');
    this.feedbackService.getAllfeedbacksByUserId(userId).subscribe(
      (data: any) => {
        this.feedbacks = data;
        console.log(this.feedbacks);
      },
      (err) => {
        console.log(err);
      }
    );
  }

  deleteFeedback(feedbackId: string) {
    this.feedbackService.deleteFeedback(feedbackId).subscribe(
      (response) => {
        console.log('Feedback deleted:', response);
        // Refresh the feedbacks
        this.getAllLoansByUserId();
      },
      (error) => {
        console.error('Error:', error);
      }
    );
  }

  openDeletePopup(feedbackId: string) {
    this.showDeletePopup = true;
    this.feedbackToDelete = feedbackId;
  }

  closeDeletePopup() {
    this.showDeletePopup = false;
  }

  handleConfirmDelete() {
    this.deleteFeedback(this.feedbackToDelete);
    this.closeDeletePopup();
  }
}
