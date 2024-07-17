import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OrphanageService } from 'src/app/services/orphanage.service';
import { DonationService } from 'src/app/services/donation.service';

@Component({
  selector: 'app-uservieworphanage',
  templateUrl: './uservieworphanage.component.html',
  styleUrls: ['./uservieworphanage.component.css']
})
export class UservieworphanageComponent implements OnInit {

  availableOrphanages: any[] = [];
  showDonationPopup = false;
  status: string = '';
  errorMessage: string = '';
  totalDonationAmount: number = 0;
  donationAmount: number = 0;
  orphanageToDonate: number | null = null;

  constructor(private router: Router, private orphanageService: OrphanageService, private donationService: DonationService) {}

  ngOnInit(): void {
    this.fetchAvailableOrphanages();
  }

  fetchAvailableOrphanages() {
    this.orphanageService.getAllOrphanages().subscribe(
      (data: any) => {
        this.availableOrphanages = data;
        this.status = 'loaded';
      },
      (error) => {
        this.status = 'error';
        console.error('Error fetching orphanages:', error);
      }
    );
  }

  openDonationPopup(orphanageId: number) {
    this.orphanageToDonate = orphanageId;
    this.showDonationPopup = true;
  }

  handleAddDonation() {
    if (this.orphanageToDonate && this.donationAmount > 0) {
      const newDonation = {
        OrphanageId: this.orphanageToDonate,
        Amount: this.donationAmount,
        DonationDate: new Date(),
        UserId: parseInt(localStorage.getItem("userId"))
      };

      this.donationService.addDonation(newDonation).subscribe(
        (response) => {
          console.log('Donation added successfully', response);
          this.closeDonationPopup();
          this.donationAmount = 0;
          this.errorMessage = '';
        },
        (error) => {
          console.error('Error adding donation:', error);
          this.errorMessage = error.error.message;
        }
      );
    } else {
      this.errorMessage = 'Please enter a valid donation amount';
    }
  }

  closeDonationPopup() {
    this.showDonationPopup = false;
    this.orphanageToDonate = null;
    this.donationAmount = 0;
    this.errorMessage = '';
  }
}
