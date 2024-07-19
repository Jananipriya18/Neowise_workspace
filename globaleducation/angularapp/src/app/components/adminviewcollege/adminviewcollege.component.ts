import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CollegeService } from 'src/app/services/college.service';

@Component({
  selector: 'app-adminviewcollege',
  templateUrl: './adminviewcollege.component.html',
  styleUrls: ['./adminviewcollege.component.css']
})
export class AdminviewcollegeComponent implements OnInit {

  availableColleges: any[] = [];
  showDeletePopup = false;
  collegeToDelete: string | null = null;
  searchValue = '';
  sortValue = 0;
  page: number = 1;
  limit = 5;
  maxRecords = 1;
  totalPages = 1;
  status: string = '';
  filteredColleges = [];
  searchField = '';
  errorMessage: string = '';
  allColleges: any[] = [];

  constructor(private router: Router, private collegeService: CollegeService) {}

  ngOnInit(): void {
    console.log('Admin view college component initialized');
    this.fetchAvailableColleges();
  }

  fetchAvailableColleges() {
    this.status = 'loading';
    this.collegeService.getAllColleges().subscribe(
      (data: any) => {
        this.availableColleges = data;
        this.maxRecords = this.availableColleges.length;
        this.allColleges = data;
        this.totalPages = Math.ceil(this.maxRecords / this.limit);
        this.status = '';
        console.log('Available colleges:', this.availableColleges);
      },
      (error) => {
        console.error('Error fetching colleges:', error);
        this.status = 'error';
      }
    );
  }

  handleDeleteClick(collegeId: string) {
    this.collegeToDelete = collegeId;
    this.showDeletePopup = true;
  }

  navigateToEditCollege(id: string) {
    this.router.navigate(['/admin/editcollege', id]);
  }

  handleConfirmDelete() {
    if (this.collegeToDelete) {
      this.collegeService.deleteCollege(this.collegeToDelete).subscribe(
        (response) => {
          console.log('College deleted successfully', response);
          this.closeDeletePopup();
          this.fetchAvailableColleges();
          this.errorMessage = '';
        },
        (error) => {
          console.error('Error deleting college:', error);
          this.errorMessage = error.error.message;
        }
      );
    }
  }

  closeDeletePopup() {
    this.collegeToDelete = null;
    this.showDeletePopup = false;
    this.errorMessage = '';
  }

  updateAvailableColleges(newColleges: any[]) {
    this.availableColleges = newColleges;
  }

  handleSearchChange(searchValue: string): void {
    this.searchField = searchValue;
    if (searchValue) {
      this.availableColleges = this.filterColleges(searchValue);
    } else {
      this.availableColleges = this.allColleges;
    }
  }

  filterColleges(search: string) {
    const searchLower = search.toLowerCase();
    if (searchLower === '') return this.availableColleges;
    return this.availableColleges.filter(
      (college) =>
        college.CollegeName.toLowerCase().includes(searchLower) ||
        college.Location.toLowerCase().includes(searchLower)
    );
  }
}
