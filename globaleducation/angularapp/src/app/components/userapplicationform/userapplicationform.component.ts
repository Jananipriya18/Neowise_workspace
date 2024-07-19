import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Application } from 'src/app/models/application.model';
import { ApplicationService } from 'src/app/services/application.service';

@Component({
  selector: 'app-userapplicationform',
  templateUrl: './userapplicationform.component.html',
  styleUrls: ['./userapplicationform.component.css']
})
export class UserapplicationformComponent implements OnInit {

  applicationForm: FormGroup;
  successPopup = false;
  errorMessage = "";

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private applicationService: ApplicationService
  ) {
    this.applicationForm = this.fb.group({
      degreeName: ['', Validators.required],
      twelfthPercentage: ['', Validators.required],
      previousCollege: ['', Validators.required],
      previousCollegeCGPA: ['', Validators.required]
    });
  }

  ngOnInit(): void {
  }

  onSubmit(): void {
    if (this.applicationForm.valid) {
      const formData = this.applicationForm.value;
      const requestObject: Application = {
        ApplicationId: 0,
        UserId: Number(localStorage.getItem('userId')),
        CollegeId: Number(localStorage.getItem('collegeId')),
        DegreeName: formData.degreeName,
        TwelfthPercentage: Number(formData.twelfthPercentage),
        PreviousCollege: formData.previousCollege,
        PreviousCollegeCGPA: Number(formData.previousCollegeCGPA),
        Status: 'Pending',  // Set status to "Pending" by default
        CreatedAt: new Date().toISOString()
      };

      this.applicationService.addApplication(requestObject).subscribe(
        (response) => {
          console.log('Response:', response);
          this.successPopup = true;
        },
        (error) => {
          console.error('Error submitting application:', error);
          this.errorMessage = 'Error submitting application';
        }
      );
    } else {
      this.errorMessage = "All fields are required";
    }
  }

  handleSuccessMessage(): void {
    this.successPopup = false;
    this.router.navigate(['/user/view/college']);
  }

  navigateBack(): void {
    this.router.navigate(['/user/view/college']);
  }
}
