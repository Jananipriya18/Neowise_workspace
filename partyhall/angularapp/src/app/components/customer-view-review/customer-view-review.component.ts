import { Component, OnInit } from '@angular/core';
import { PartyhallService } from 'src/app/services/partyhall.service';

@Component({
  selector: 'app-customer-view-review',
  templateUrl: './customer-view-review.component.html',
  styleUrls: ['./customer-view-review.component.css']
})
export class CustomerViewReviewComponent implements OnInit {
  reviews: any[] = [];

  constructor( private PartyhallService:PartyhallService ) { }

  ngOnInit(): void {
    this.getReviewsByUserId();
  }

  getReviewsByUserId() {
    this.PartyhallService.getReviewsByUserId().subscribe(
      (data: any) => {
        this.reviews = data;
        console.log(this.reviews)
      },
      (err) => {
        console.log(err);
      }
    );
  }

}
