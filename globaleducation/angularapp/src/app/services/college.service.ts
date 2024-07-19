import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { apiUrl } from 'src/apiconfig';
import { College } from '../models/college.model';
import { Application } from '../models/application.model';

@Injectable({
  providedIn: 'root'
})
export class CollegeService {
  public apiUrl = apiUrl; // Update with your API URL

  constructor(private http: HttpClient) { }

  getAllColleges(): Observable<College[]> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.get<College[]>(`${this.apiUrl}/api/colleges`, { headers });
  }

  deleteCollege(collegeId: string): Observable<void> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.delete<void>(`${this.apiUrl}/api/colleges/${collegeId}`, { headers });
  }

  getCollegeById(id: string): Observable<College> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.get<College>(`${this.apiUrl}/api/colleges/${id}`, { headers });
  }

  addCollege(requestObject: College): Observable<College> {
    console.log(requestObject);
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.post<College>(`${this.apiUrl}/api/colleges`, requestObject, { headers });
  }

  updateCollege(id: string, requestObject: College): Observable<College> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.put<College>(`${this.apiUrl}/api/colleges/${id}`, requestObject, { headers });
  }

  getAppliedColleges(userId: string): Observable<Application[]> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.get<Application[]>(`${this.apiUrl}/api/applications/user/${userId}`, { headers });
  }

}
