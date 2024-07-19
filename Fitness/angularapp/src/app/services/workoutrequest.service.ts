import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { apiUrl } from 'src/apiconfig';
import { WorkoutRequest } from '../models/workoutrequest.model';

@Injectable({
  providedIn: 'root'
})
export class WorkoutrequestService {

  public apiUrl = apiUrl;
  constructor(private http: HttpClient) { }

  addWorkoutRequest(data: WorkoutRequest): Observable<WorkoutRequest> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.post<WorkoutRequest>(`${this.apiUrl}/api/workoutrequests`, data, { headers });
  }

  getAppliedWorkouts(userId: string): Observable<WorkoutRequest[]> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.get<WorkoutRequest[]>(`${this.apiUrl}/api/workoutrequests/user/${userId}`, { headers });
  }

  deleteWorkoutApplication(requestedId: string): Observable<void> {
    console.log('deleteWorkoutId', requestedId);
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.delete<void>(`${this.apiUrl}/api/workoutrequests/${requestedId}`, { headers });
  }

  getAllWorkoutRequests(): Observable<WorkoutRequest[]> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.get<WorkoutRequest[]>(`${this.apiUrl}/api/workoutrequests`, { headers });
  }

  updateWorkoutStatus(id: string, workoutApplication: WorkoutRequest): Observable<WorkoutRequest> {
    console.log('updateWorkoutStatus', id, workoutApplication);
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.put<WorkoutRequest>(`${this.apiUrl}/api/workoutrequests/${id}`, workoutApplication, { headers });
  }

}
