import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApplicationService } from 'src/app/services/application.service';
import { Application } from 'src/app/models/application.model';

@Component({
  selector: 'app-userappliedcollege',
  templateUrl: './userappliedcollege.component.html',
  styleUrls: ['./userappliedcollege.component.css']
})
export class UserappliedcollegeComponent implements OnInit {

  showDeletePopup = false;
  applicationToDelete: Application | null = null;
  appliedApplications: Application[] = [];
  filteredApplications: Application[] = [];
  searchValue = '';
  errorMessage = '';

  constructor(private applicationService: ApplicationService, private router: Router) {}

  ngOnInit(): void {
    this.fetchData();
  }

  fetchData(): void {
    const userId = localStorage.getItem('userId');
    this.applicationService.getAppliedApplications(userId).subscribe(
      (response: Application[]) => {
        this.appliedApplications = response;
        console.log('User Applied applications:', this.appliedApplications);
        this.filteredApplications = response;
      },
      (error) => {
        console.error('Error fetching data:', error);
      }
    );
  }

  filterApplications(): void {
    const searchLower = this.searchValue.toLowerCase();
    if (searchLower === '') {
      this.filteredApplications = [...this.appliedApplications];
    } else {
      this.filteredApplications = this.appliedApplications.filter((application) =>
        application.College.CollegeName.toLowerCase().includes(searchLower) ||
        application.DegreeName.toLowerCase().includes(searchLower) ||
        application.PreviousCollege.toLowerCase().includes(searchLower)
      );
    }
  }

  handleDeleteClick(application: Application): void {
    this.applicationToDelete = application;
    this.showDeletePopup = true;
  }

  handleConfirmDelete(): void {
    if (this.applicationToDelete) {
      this.applicationService
        .deleteApplication(this.applicationToDelete.ApplicationId.toString())
        .subscribe((response) => {
          console.log('Application deleted successfully', response);
          this.fetchData();
          this.closeDeletePopup();
        });
    }
  }

  closeDeletePopup(): void {
    this.applicationToDelete = null;
    this.showDeletePopup = false;
  }
}
