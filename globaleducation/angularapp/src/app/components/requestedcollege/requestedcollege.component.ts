import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Application } from 'src/app/models/application.model';
import { ApplicationService } from 'src/app/services/application.service';

@Component({
  selector: 'app-requestedcollege',
  templateUrl: './requestedcollege.component.html',
  styleUrls: ['./requestedcollege.component.css']
})
export class RequestedcollegeComponent implements OnInit {

  applicationRequests: Application[] = [];
  searchValue = '';
  statusFilter = '-1';
  showModal = false;
  selectedApplication: Application | null = null;
  filteredApplications: Application[] = [];

  constructor(private applicationService: ApplicationService, private router: Router) {}

  ngOnInit(): void {
    this.fetchData();
  }

  fetchData(): void {
    this.applicationService.getAllApplications().subscribe(
      (response: Application[]) => {
        this.applicationRequests = response;
        this.filteredApplications = [...this.applicationRequests];
        console.log('applicationRequests:', this.applicationRequests);
      },
      (error) => {
        console.error('Error fetching applications:', error);
        // Handle error appropriately
      }
    );
  }

  handleSearchChange(event: any): void {
    this.searchValue = event.target.value.toLowerCase();
    this.filteredApplications = this.applicationRequests.filter(application =>
      application.College.CollegeName.toLowerCase().includes(this.searchValue)
    );
  }

  handleFilterChange(event: any): void {
    this.statusFilter = event.target.value;
    this.filteredApplications = this.applicationRequests.filter(application => {
      if (this.statusFilter === '-1') {
        // If the filter is set to 'All', return all applications
        return true;
      } else {
        // Otherwise, return only the applications that match the selected status
        return application.Status === this.statusFilter;
      }
    });
  }

  handleApprove(application: Application): void {
    this.updateApplicationStatus(application, 'Approved');
  }

  handleReject(application: Application): void {
    this.updateApplicationStatus(application, 'Rejected');
  }

  updateApplicationStatus(application: Application, newStatus: string): void {
    this.applicationService.updateApplicationStatus(application.ApplicationId.toString(), { ...application, Status: newStatus }).subscribe(
      (response) => {
        console.log('Response:', response);
        // Only change the status on the client side if the update was successful
        application.Status = newStatus;
        this.fetchData();
      },
      (error) => {
        console.error('Error updating application status:', error);
        // Handle error appropriately
      }
    );
  }

  handleRowExpand(index: number): void {
    const selected = this.applicationRequests[index];
    console.log('selected:', selected);
    this.selectedApplication = selected;
    this.showModal = !this.showModal;
  }

  closeApplicationDetailsModal(): void {
    this.showModal = false;
  }
}
