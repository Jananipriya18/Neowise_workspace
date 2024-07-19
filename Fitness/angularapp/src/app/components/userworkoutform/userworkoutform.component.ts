import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { WorkoutRequest } from 'src/app/models/workoutrequest.model';
import { WorkoutrequestService } from 'src/app/services/workoutrequest.service';

@Component({
  selector: 'app-userworkoutform',
  templateUrl: './userworkoutform.component.html',
  styleUrls: ['./userworkoutform.component.css']
})
export class UserworkoutformComponent implements OnInit {

  workoutForm: FormGroup;
  successPopup = false;
  errorMessage = "";

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private workoutRequestService: WorkoutrequestService
  ) {
    this.workoutForm = this.fb.group({
      age: ['', Validators.required],
      bmi: ['', Validators.required],
      gender: ['', Validators.required],
      dietaryPreferences: ['', Validators.required],
      medicalHistory: ['', Validators.required],
    });
  }

  ngOnInit(): void {
  }

  onSubmit(): void {
    if (this.workoutForm.valid) {
      const formData = this.workoutForm.value;
      const requestObject: WorkoutRequest = {
        WorkoutRequestId: 0,
        UserId: Number(localStorage.getItem('userId')),
        WorkoutId: Number(localStorage.getItem('workoutId')),
        Age: Number(formData.age),
        BMI: Number(formData.bmi),
        Gender: formData.gender,
        DietaryPreferences: formData.dietaryPreferences,
        MedicalHistory: formData.medicalHistory,
        RequestedDate: new Date().toISOString(),
        RequestStatus: "Pending",
      };

      this.workoutRequestService.addWorkoutRequest(requestObject).subscribe(
        (response) => {
          console.log('Response:', response);
          this.successPopup = true;
        },
        (error) => {
          console.error('Error submitting workout request:', error);
          this.errorMessage = 'Error submitting workout request';
        }
      );
    } else {
      this.errorMessage = "All fields are required";
    }
  }

  handleSuccessMessage(): void {
    this.successPopup = false;
    this.router.navigate(['/user/view/workout']);
  }

  navigateBack(): void {
    this.router.navigate(['/user/view/workout']);
  }
}
