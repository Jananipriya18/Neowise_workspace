import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { WorkoutService } from 'src/app/services/workout.service';

@Component({
  selector: 'app-adminviewworkout',
  templateUrl: './adminviewworkout.component.html',
  styleUrls: ['./adminviewworkout.component.css']
})
export class AdminviewworkoutComponent implements OnInit {

  availableWorkouts: any[] = [];
  showDeletePopup = false;
  workoutToDelete: string | null = null;
  searchValue = '';
  sortValue = 0;
  page: number = 1;
  limit = 5;
  maxRecords = 1;
  totalPages = 1;
  status: string = '';
  filteredWorkouts = [];
  searchField = '';
  errorMessage: string = '';
  allWorkouts: any[] = [];

  constructor(private router: Router, private workoutService: WorkoutService) {}

  ngOnInit(): void {
    console.log('Admin view workout component initialized');
    this.fetchAvailableWorkouts();
  }

  fetchAvailableWorkouts() {
    this.status = 'loading';
    this.workoutService.getAllWorkouts().subscribe(
      (data: any) => {
        this.availableWorkouts = data;
        this.maxRecords = this.availableWorkouts.length;
        this.allWorkouts = data;
        this.totalPages = Math.ceil(this.maxRecords / this.limit);
        this.status = '';
        console.log('Available workouts:', this.availableWorkouts);
      },
      (error) => {
        console.error('Error fetching workouts:', error);
        this.status = 'error';
      }
    );
  }

  handleDeleteClick(workoutId: string) {
    this.workoutToDelete = workoutId;
    this.showDeletePopup = true;
  }

  navigateToEditWorkout(id: string) {
    this.router.navigate(['/admin/editworkout', id]);
  }

  handleConfirmDelete() {
    if (this.workoutToDelete) {
      this.workoutService.deleteWorkout(this.workoutToDelete).subscribe(
        (response) => {
          console.log('Workout deleted successfully', response);
          this.closeDeletePopup();
          this.fetchAvailableWorkouts();
          this.errorMessage = '';
        },
        (error) => {
          console.error('Error deleting workout:', error);
          this.errorMessage = error.error.message;
        }
      );
    }
  }

  closeDeletePopup() {
    this.workoutToDelete = null;
    this.showDeletePopup = false;
    this.errorMessage = '';
  }

  updateAvailableWorkouts(newWorkouts: any[]) {
    this.availableWorkouts = newWorkouts;
  }

  handleSearchChange(searchValue: string): void {
    this.searchField = searchValue;
    if (searchValue) {
      this.availableWorkouts = this.filterWorkouts(searchValue);
    } else {
      this.availableWorkouts = this.allWorkouts;
    }
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
}
