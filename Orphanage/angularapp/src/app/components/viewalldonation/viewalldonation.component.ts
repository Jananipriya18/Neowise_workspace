import { Component, OnInit } from '@angular/core';
import { DonationService } from 'src/app/services/donation.service';

@Component({
  selector: 'app-viewalldonation',
  templateUrl: './viewalldonation.component.html',
  styleUrls: ['./viewalldonation.component.css']
})
export class ViewalldonationComponent implements OnInit {

  donations: any[] = [];
  status: string = '';
  errorMessage: string = '';

  constructor(private donationService: DonationService) { }

  ngOnInit(): void {
    this.fetchAllDonations();
  }

  fetchAllDonations() {
    this.status = 'loading';
    this.donationService.getAllDonations().subscribe(
      (data: any[]) => {
        this.donations = data;
        this.status = 'loaded';
      },
      (error) => {
        this.status = 'error';
        console.error('Error fetching donations:', error);
        this.errorMessage = error.error.message;
      }
    );
  }
}
