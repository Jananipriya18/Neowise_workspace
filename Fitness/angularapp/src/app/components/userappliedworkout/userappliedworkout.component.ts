import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { WorkoutrequestService } from 'src/app/services/workoutrequest.service';

@Component({
  selector: 'app-userappliedworkout',
  templateUrl: './userappliedworkout.component.html',
  styleUrls: ['./userappliedworkout.component.css']
})
export class UserappliedworkoutComponent implements OnInit {

  showDeletePopup = false;
  workoutToDelete: any = null;
  appliedWorkouts: any[] = [];
  filteredWorkouts: any[] = [];
  searchValue = '';
  sortValue = 0;
  page = 1;
  limit = 5;
  maxRecords = 1;

  constructor(private workoutRequestService: WorkoutrequestService, private router: Router) {}

  ngOnInit(): void {
    this.fetchData();
  }

  fetchData(): void {
    const userId = localStorage.getItem('userId');
    this.workoutRequestService.getAppliedWorkouts(userId).subscribe(
      (response: any) => {
        this.appliedWorkouts = response;
        console.log('User Applied workouts:', this.appliedWorkouts);
        this.filteredWorkouts = response;
        this.maxRecords = response.length;
      },
      (error) => {
        console.error('Error fetching data:', error);
      }
    );
  }

  totalPages(): number {
    return Math.ceil(this.maxRecords / this.limit);
  }

  filterWorkouts(): void {
    const searchLower = this.searchValue ? this.searchValue.toLowerCase() : '';
    if (searchLower === '') {
      this.filteredWorkouts = [...this.appliedWorkouts];
    } else {
      this.filteredWorkouts = this.appliedWorkouts.filter((workout) =>
        workout.Workout && workout.Workout.WorkoutName && workout.Workout.WorkoutName.toLowerCase().includes(searchLower)
      );
    }
    this.maxRecords = this.filteredWorkouts.length;
  }

  toggleSort(order: number): void {
    this.sortValue = order;

    this.filteredWorkouts.sort((a, b) => {
      return order === 1
        ? new Date(a.RequestedDate).getTime() - new Date(b.RequestedDate).getTime()
        : order === -1
        ? new Date(b.RequestedDate).getTime() - new Date(a.RequestedDate).getTime()
        : 0;
    });
  }

  handleDeleteClick(workout: any): void {
    this.workoutToDelete = workout;
    this.showDeletePopup = true;
  }

  handleConfirmDelete(): void {
    this.workoutRequestService
        .deleteWorkoutApplication(this.workoutToDelete.WorkoutRequestId)
        .subscribe((response) => {
            console.log('Workout deleted successfully', response);
            this.fetchData();
            this.closeDeletePopup();
        });
  }

  closeDeletePopup(): void {
    this.workoutToDelete = null;
    this.showDeletePopup = false;
  }
}
