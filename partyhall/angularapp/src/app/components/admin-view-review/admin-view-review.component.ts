import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Review } from 'src/app/models/review.model'; // Assuming you have a Review model
import { PartyhallService } from 'src/app/services/partyhall.service';

@Component({
  selector: 'app-admin-view-review',
  templateUrl: './admin-view-review.component.html',
  styleUrls: ['./admin-view-review.component.css']
})
export class AdminViewReviewComponent implements OnInit {
  showDeletePopup = false;
  selectedReview: Review;
  isEditing = false;
  reviews: Review[] = [];

  constructor(private router: Router, private partyHallService: PartyhallService) { } // Update to PartyHallService

  ngOnInit(): void {
    this.getAllReviews();
  }

  getAllReviews() {
    this.partyHallService.getAllReviews().subscribe( // Update to PartyHallService
      (data: Review[]) => {
        this.reviews = data;
        console.log(this.reviews);
      },
      (err) => {
        console.log(err);
      }
    );
  } 
}
