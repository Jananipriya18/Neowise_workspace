import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { apiUrl } from 'src/apiconfig';
import { Application } from '../models/application.model';

@Injectable({
  providedIn: 'root'
})
export class ApplicationService {

  public apiUrl = apiUrl;
  constructor(private http: HttpClient) { }

  addApplication(data: Application): Observable<Application> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.post<Application>(`${this.apiUrl}/api/applications`, data, { headers });
  }

  getAppliedApplications(userId: string): Observable<Application[]> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.get<Application[]>(`${this.apiUrl}/api/applications/user/${userId}`, { headers });
  }

  deleteApplication(applicationId: string): Observable<void> {
    console.log('deleteApplicationId', applicationId);
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.delete<void>(`${this.apiUrl}/api/applications/${applicationId}`, { headers });
  }

  getAllApplications(): Observable<Application[]> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.get<Application[]>(`${this.apiUrl}/api/applications`, { headers });
  }

  updateApplicationStatus(id: string, application: Application): Observable<Application> {
    console.log('updateApplicationStatus', id, application);
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.put<Application>(`${this.apiUrl}/api/applications/${id}`, application, { headers });
  }

}
