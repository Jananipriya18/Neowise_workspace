import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProjectRequirement } from '../models/projectRequirement.model'; // Adjust the path as necessary
import { apiUrl } from 'src/apiconfig'; // Adjust the path as necessary

@Injectable({
  providedIn: 'root'
})
export class ProjectRequirementService {
  apiUrl = apiUrl;

  constructor(private http: HttpClient) {}

  private getHeaders() {
    return new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
  }

  getAllProjectRequirements(): Observable<ProjectRequirement[]> {
    return this.http.get<ProjectRequirement[]>(`${this.apiUrl}/api/projectrequirements`, { headers: this.getHeaders() });
  }

  getProjectRequirementById(requirementId: number): Observable<ProjectRequirement> {
    return this.http.get<ProjectRequirement>(`${this.apiUrl}/api/projectrequirements/${requirementId}`, { headers: this.getHeaders() });
  }

  getProjectRequirementsByUserId(userId: number): Observable<ProjectRequirement[]> {
    return this.http.get<ProjectRequirement[]>(`${this.apiUrl}/api/projectrequirements/user/${userId}`, { headers: this.getHeaders() });
  }

  addProjectRequirement(projectRequirement: ProjectRequirement): Observable<any> {
    return this.http.post(`${this.apiUrl}/api/projectrequirements`, projectRequirement, { headers: this.getHeaders() });
  }

  updateProjectRequirement(requirementId: number, projectRequirement: ProjectRequirement): Observable<any> {
    return this.http.put(`${this.apiUrl}/api/projectrequirements/${requirementId}`, projectRequirement, { headers: this.getHeaders() });
  }

  deleteProjectRequirement(requirementId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/api/projectrequirements/${requirementId}`, { headers: this.getHeaders() });
  }
}
