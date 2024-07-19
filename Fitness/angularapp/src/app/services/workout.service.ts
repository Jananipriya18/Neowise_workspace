import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { apiUrl } from 'src/apiconfig';
import { Workout } from '../models/workout.model';
import { WorkoutRequest } from '../models/workoutrequest.model';

@Injectable({
  providedIn: 'root'
})
export class WorkoutService {
  public apiUrl = apiUrl; // Update with your API URL

  constructor(private http: HttpClient) { }

  getAllWorkouts(): Observable<Workout[]> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.get<Workout[]>(`${this.apiUrl}/api/workout`, { headers });
  }

  deleteWorkout(workoutId: string): Observable<void> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.delete<void>(`${this.apiUrl}/api/workout/${workoutId}`, { headers });
  }

  getWorkoutById(id: string): Observable<Workout> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.get<Workout>(`${this.apiUrl}/api/workout/${id}`, { headers });
  }

  addWorkout(requestObject: Workout): Observable<Workout> {
    console.log(requestObject);
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.post<Workout>(`${this.apiUrl}/api/workout`, requestObject, { headers });
  }

  updateWorkout(id: string, requestObject: Workout): Observable<Workout> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.put<Workout>(`${this.apiUrl}/api/workout/${id}`, requestObject, { headers });
  }

  getAppliedWorkouts(userId: string): Observable<WorkoutRequest[]> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.get<WorkoutRequest[]>(`${this.apiUrl}/api/workoutrequests/user/${userId}`, { headers });
  }
}
