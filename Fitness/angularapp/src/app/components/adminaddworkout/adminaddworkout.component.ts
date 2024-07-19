import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { Workout } from 'src/app/models/workout.model';
import { WorkoutService } from 'src/app/services/workout.service';

@Component({
  selector: 'app-adminaddworkout',
  templateUrl: './adminaddworkout.component.html', // Update the template URL
  styleUrls: ['./adminaddworkout.component.css']
})
export class AdminaddworkoutComponent implements OnInit {

  id: string;
  formData: Workout = {
    WorkoutName: '',
    Description: '',
    DifficultyLevel: null,
    CreatedAt: '',
    TargetArea: '',
    DaysPerWeek: null,
    AverageWorkoutDurationInMinutes: null
  };
  errors: any = {};
  errorMessage: string;
  successPopup: boolean = false;

  constructor(private workoutService: WorkoutService, private router: Router) {}

  ngOnInit(): void {}

  handleChange(event: any, field: string) {
    this.formData[field] = event.target.value;
    // Validate your form here and set errors if any
  }

  onSubmit(workoutForm: NgForm) {
    console.log('Form Validity:', workoutForm.valid, this.formData);
    if (workoutForm.valid) {
      this.formData.CreatedAt = new Date().toISOString();
      this.workoutService.addWorkout(this.formData).subscribe(
        (res) => {
          this.successPopup = true;
          console.log('Workout added successfully', res);
          workoutForm.resetForm();
        },
        (err) => {
          this.errors = err.error;
          this.errorMessage = err.error.message;
          console.error('Error adding workout:', err);
        }
      );
    } else {
      this.errorMessage = 'All fields are required';
    }
  }

  handleSuccessMessage() {
    this.successPopup = false;
    this.errorMessage = '';
    this.formData = {
      WorkoutId: 0,
      WorkoutName: '',
      Description: '',
      DifficultyLevel: 0,
      CreatedAt: '',
      TargetArea: '',
      DaysPerWeek: 0,
      AverageWorkoutDurationInMinutes: 0
    };
  }
}
