import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { forkJoin } from 'rxjs';
import { CollegeService } from 'src/app/services/college.service';

@Component({
  selector: 'app-userviewcollege',
  templateUrl: './userviewcollege.component.html',
  styleUrls: ['./userviewcollege.component.css']
})
export class UserviewcollegeComponent implements OnInit {

  availableColleges: any[] = [];
  filteredColleges = [];
  searchValue: string = '';
  sortValue: number = 0;
  page: number = 1;
  limit: number = 5;
  appliedColleges: any[] = [];
  colleges = [];

  constructor(private router: Router, private collegeService: CollegeService) {}

  ngOnInit(): void {
    this.fetchData();
  }

  fetchData() {
    const userId = localStorage.getItem('userId');

    forkJoin({
      appliedColleges: this.collegeService.getAppliedColleges(userId),
      allColleges: this.collegeService.getAllColleges()
    }).subscribe(
      ({ appliedColleges, allColleges }) => {
        this.appliedColleges = appliedColleges;
        this.availableColleges = allColleges;
        this.filteredColleges = allColleges;
        console.log('Applied colleges:', this.appliedColleges);
        console.log('Available colleges:', this.availableColleges);
      },
      (error) => {
        console.error('Error fetching data:', error);
      }
    );
  }

  searchField = '';

  handleSearchChange(searchValue: string): void {
    this.searchField = searchValue;
    this.filteredColleges = this.filterColleges(searchValue);
  }

  filterColleges(search: string) {
    const searchLower = search.toLowerCase();
    if (searchLower === '') return this.availableColleges;
    return this.availableColleges.filter(
      (college) =>
        college.CollegeName.toLowerCase().includes(searchLower) ||
        college.Location.toLowerCase().includes(searchLower) ||
        (college.Description && college.Description.toLowerCase().includes(searchLower)) ||
        (college.Website && college.Website.toLowerCase().includes(searchLower))
    );
  }

  handleApplyClick(college: any) {
    const isCollegeApplied = this.isCollegeApplied(college);
    console.log(isCollegeApplied);

    if (isCollegeApplied) {
      alert('College is already applied.');
    } else {
      this.appliedColleges.push(college);
      console.log("selectedCollege", college); // Add the applied college to the appliedColleges array
      localStorage.setItem('collegeId', college.CollegeId); // Store collegeId in local storage
      this.router.navigate(['/user/apply/college']);
    }
  }

  isCollegeApplied(college: any): boolean {
    return this.appliedColleges.some(
      (appliedCollege) => appliedCollege.CollegeId === college.CollegeId
    );
  }

  navigateToViewAppliedCollege() {
    this.router.navigate(['/appliedcollege']);
  }

  logout() {
    localStorage.clear();
    this.router.navigate(['/login']);
  }
}
