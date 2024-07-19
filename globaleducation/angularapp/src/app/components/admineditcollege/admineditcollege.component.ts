import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CollegeService } from 'src/app/services/college.service';
import { College } from 'src/app/models/college.model';

@Component({
  selector: 'app-admineditcollege',
  templateUrl: './admineditcollege.component.html',
  styleUrls: ['./admineditcollege.component.css']
})
export class AdmineditcollegeComponent implements OnInit {
  id: string;
  errorMessage: string = '';
  formData: College = {
    CollegeId: 0,
    CollegeName: '',
    Location: '',
    Description: '',
    Website: ''
  };
  errors: any = {};
  successPopup: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private collegeService: CollegeService
  ) {}

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    this.getCollegeById();
  }

  getCollegeById() {
    this.collegeService.getCollegeById(this.id).subscribe(
      (response) => {
        console.log('College details:', response);
        this.formData = {
          CollegeId: response.CollegeId,
          CollegeName: response.CollegeName,
          Location: response.Location,
          Description: response.Description,
          Website: response.Website,
        };
      },
      (error) => {
        console.error('Error fetching college details:', error);
        this.router.navigate(['/error']);
      }
    );
  }

  handleChange(event: any, field: string) {
    this.formData[field] = event.target.value;
    this.errors[field] = ''; // Clear error when the user makes a change
  }

  handleUpdateCollege(collegeForm: NgForm) {
    if (collegeForm.valid) {
      this.collegeService.updateCollege(this.id, this.formData).subscribe(
        (response) => {
          console.log('College updated successfully', response);
          this.successPopup = true;
          this.errorMessage = '';
        },
        (error) => {
          console.error('Error updating college:', error);
          this.errorMessage = error.error.message;
        }
      );
    }
  }

  handleOkClick() {
    this.successPopup = false;
    this.router.navigate(['/admin/view/college']);
  }

  navigateToDashboard() {
    this.router.navigate(['/admin/view/college']);
  }
}
