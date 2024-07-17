import { Component, OnInit } from '@angular/core';
import { DonationService } from 'src/app/services/donation.service';

@Component({
  selector: 'app-mydonation',
  templateUrl: './mydonation.component.html',
  styleUrls: ['./mydonation.component.css']
})
export class MydonationComponent implements OnInit {

  donations: any[] = [];
  showOrphanagePopup: boolean = false;
  selectedOrphanage: any;

  constructor(private donationService: DonationService) { }

  ngOnInit(): void {
    this.getDonationsByUserId();
  }

  getDonationsByUserId(): void {
    const userId = localStorage.getItem('userId');

    if (userId) {
      this.donationService.getDonationsByUserId(parseInt(userId)).subscribe(
        data => {this.donations = data;},
        error => console.error(error)
      );
console.log(this.donations,"this.donations",userId)

    } else {
      console.error('User ID not found in local storage');
    }
  }

  showOrphanageDetails(orphanage: any): void {
    this.selectedOrphanage = orphanage;
    this.showOrphanagePopup = true;
  }

  closeOrphanagePopup(): void {
    this.showOrphanagePopup = false;
  }
}
