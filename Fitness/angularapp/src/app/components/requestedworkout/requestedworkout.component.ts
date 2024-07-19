import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { WorkoutRequest } from 'src/app/models/workoutrequest.model';
import { WorkoutrequestService } from 'src/app/services/workoutrequest.service';

@Component({
  selector: 'app-requestedworkout',
  templateUrl: './requestedworkout.component.html',
  styleUrls: ['./requestedworkout.component.css']
})
export class RequestedworkoutComponent implements OnInit {

  workoutRequests: any[] = [];
  searchValue = '';
  statusFilter = '-1';
  showModal = false;
  selectedWorkout: any = null;

  constructor(private workoutService: WorkoutrequestService, private router: Router) {}

  ngOnInit(): void {
    this.fetchData();
  }

  fetchData(): void {
    this.workoutService.getAllWorkoutRequests().subscribe(
      (response) => {
        this.workoutRequests = response;
        this.filteredWorkouts = [...this.workoutRequests];
        console.log('workoutRequests:', this.workoutRequests);
      },
      (error) => {
        console.error('Error fetching workouts:', error);
        // Handle error appropriately
      }
    );
  }
  filteredWorkouts = [];

  handleSearchChange(event: any): void {
    this.searchValue = event.target.value.toLowerCase();
    this.filteredWorkouts = this.workoutRequests.filter(workout =>
      workout.Workout.WorkoutName.toLowerCase().includes(this.searchValue)
    );
  }

  handleFilterChange(event: any): void {
    this.statusFilter = event.target.value;
    this.filteredWorkouts = this.workoutRequests.filter(workout => {
      if (this.statusFilter === '-1') {
        // If the filter is set to 'All', return all workouts
        return true;
      } else {
        // Otherwise, return only the workouts that match the selected status
        return workout.RequestStatus === this.statusFilter;
      }
    });
  }

  handleApprove(workoutRequest: any): void {
    this.updateWorkoutStatus(workoutRequest, 'Approved');
  }

  handleReject(workoutRequest: any): void {
    this.updateWorkoutStatus(workoutRequest, 'Rejected');
  }

  updateWorkoutStatus(workoutRequest: any, newStatus: string): void {
    this.workoutService.updateWorkoutStatus(workoutRequest.WorkoutRequestId, { ...workoutRequest, RequestStatus: newStatus }).subscribe(
      (response) => {
        console.log('Response:', response);
        // Only change the status on the client side if the update was successful
        workoutRequest.RequestStatus = newStatus;
        this.fetchData();
      },
      (error) => {
        console.error('Error updating workout status:', error);
        // Handle error appropriately
      }
    );
  }

  handleRowExpand(index: number): void {
    const selected = this.workoutRequests[index];
    console.log('selected:', selected);
    this.selectedWorkout = selected;
    this.showModal = !this.showModal;
  }

  closeWorkoutDetailsModal(): void {
    this.showModal = false;
  }
}
