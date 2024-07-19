import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { forkJoin } from 'rxjs';
import { WorkoutService } from 'src/app/services/workout.service';

@Component({
  selector: 'app-userviewworkout',
  templateUrl: './userviewworkout.component.html',
  styleUrls: ['./userviewworkout.component.css']
})
export class UserviewworkoutComponent implements OnInit {

  availableWorkouts: any[] = [];
  filteredWorkouts = [];
  searchValue: string = '';
  sortValue: number = 0;
  page: number = 1;
  limit: number = 5;
  appliedWorkouts: any[] = [];
  workouts = [];

  constructor(private router: Router, private workoutService: WorkoutService) {}

  ngOnInit(): void {
    this.fetchData();
  }

  fetchData() {
    const userId = localStorage.getItem('userId');

    forkJoin({
      appliedWorkouts: this.workoutService.getAppliedWorkouts(userId),
      allWorkouts: this.workoutService.getAllWorkouts()
    }).subscribe(
      ({ appliedWorkouts, allWorkouts }) => {
        this.appliedWorkouts = appliedWorkouts;
        this.availableWorkouts = allWorkouts;
        this.filteredWorkouts = allWorkouts;
        console.log('Applied workouts:', this.appliedWorkouts);
        console.log('Available workouts:', this.availableWorkouts);
      },
      (error) => {
        console.error('Error fetching data:', error);
      }
    );
  }

  searchField = '';

  handleSearchChange(searchValue: string): void {
    this.searchField = searchValue;
    this.filteredWorkouts = this.filterWorkouts(searchValue);
  }

  filterWorkouts(search: string) {
    const searchLower = search.toLowerCase();
    if (searchLower === '') return this.availableWorkouts;
    return this.availableWorkouts.filter(
      (workout) =>
        workout.WorkoutName.toLowerCase().includes(searchLower) ||
        workout.Description.toLowerCase().includes(searchLower)
    );
  }

  handleApplyClick(workout: any) {
    const isWorkoutApplied = this.isWorkoutApplied(workout);
    console.log(isWorkoutApplied);

    if (isWorkoutApplied) {
      alert('Workout is already applied.');
    } else {
      this.appliedWorkouts.push(workout);
      console.log("selectedWorkout", workout); // Add the applied workout to the appliedWorkouts array
      localStorage.setItem('workoutId', workout.WorkoutId); // Store workoutId in local storage
      this.router.navigate(['/user/apply/workout']);
    }
  }

  isWorkoutApplied(workout: any): boolean {
    return this.appliedWorkouts.some(
      (appliedWorkout) => appliedWorkout.WorkoutId === workout.WorkoutId
    );
  }

  navigateToViewAppliedWorkout() {
    this.router.navigate(['/appliedworkout']);
  }

  logout() {
    localStorage.clear();
    this.router.navigate(['/login']);
  }
}
