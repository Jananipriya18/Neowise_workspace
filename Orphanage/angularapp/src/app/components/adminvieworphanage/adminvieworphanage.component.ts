import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OrphanageService } from 'src/app/services/orphanage.service';
import { DonationService } from 'src/app/services/donation.service';

@Component({
  selector: 'app-adminvieworphanage',
  templateUrl: './adminvieworphanage.component.html',
  styleUrls: ['./adminvieworphanage.component.css']
})
export class AdminvieworphanageComponent implements OnInit {

  availableOrphanages: any[] = [];
  showDeletePopup = false;
  showDonationPopup = false;
  orphanageToDelete: number | null = null;
  status: string = '';
  errorMessage: string = '';
  totalDonationAmount: number = 0;
  allOrphanages: any[] = [];

  constructor(private router: Router, private orphanageService: OrphanageService, private donationService: DonationService) {}

  ngOnInit(): void {
    this.fetchAvailableOrphanages();
  }

  fetchAvailableOrphanages() {
    this.status = 'loading';
    this.orphanageService.getAllOrphanages().subscribe(
      (data: any) => {
        this.availableOrphanages = data;
        this.allOrphanages = data;
        this.status = 'loaded';
      },
      (error) => {
        this.status = 'error';
        console.error('Error fetching orphanages:', error);
      }
    );
  }

  handleDeleteClick(orphanageId: number) {
    this.orphanageToDelete = orphanageId;
    this.showDeletePopup = true;
  }

  navigateToEditOrphanage(id: number) {
    this.router.navigate(['admin/edit/orphanage/', id]);
  }

  handleDonation(orphanageId: number) {
    // Call the backend service to get donation details
    this.donationService.getDonationsByOrphanageId(orphanageId).subscribe(
      (donationData: any[]) => {
        // Sum up the total donation amount
        this.totalDonationAmount = donationData.reduce((total, donation) => total + donation.Amount, 0);
        // Show the donation popup
        this.showDonationPopup = true;
      },
      (error) => {
        console.error('Error fetching donation details:', error);
        this.errorMessage = error.error.message;
      }
    );
  }

  handleConfirmDelete() {
    if (this.orphanageToDelete) {
      this.orphanageService.deleteOrphanage(this.orphanageToDelete).subscribe(
        (response) => {
          console.log('Orphanage deleted successfully', response);
          this.closeDeletePopup();
          this.fetchAvailableOrphanages();
          this.errorMessage = '';
        },
        (error) => {
          console.error('Error deleting orphanage:', error);
          this.errorMessage = error.error.message;
        }
      );
    }
  }

  closeDonationPopup() {
    this.showDonationPopup = false;
  }

  closeDeletePopup() {
    this.orphanageToDelete = null;
    this.showDeletePopup = false;
    this.errorMessage = '';
  }
}
