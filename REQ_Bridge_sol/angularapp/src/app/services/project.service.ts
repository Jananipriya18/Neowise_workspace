import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Project } from '../models/project.model'; // Adjust the path as necessary
import { apiUrl } from 'src/apiconfig'; // Adjust the path as necessary

@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  apiUrl = apiUrl;

  constructor(private http: HttpClient) {}

  private getHeaders() {
    return new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
  }

  getAllProjects(): Observable<Project[]> {
    return this.http.get<Project[]>(`${this.apiUrl}/api/projects`, { headers: this.getHeaders() });
  }

  getProjectById(projectId: number): Observable<Project> {
    return this.http.get<Project>(`${this.apiUrl}/api/projects/${projectId}`, { headers: this.getHeaders() });
  }

  addProject(project: Project): Observable<any> {
    return this.http.post(`${this.apiUrl}/api/projects`, project, { headers: this.getHeaders() });
  }

  updateProject(projectId: number, project: Project): Observable<any> {
    return this.http.put(`${this.apiUrl}/api/projects/${projectId}`, project, { headers: this.getHeaders() });
  }

  deleteProject(projectId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/api/projects/${projectId}`, { headers: this.getHeaders() });
  }
}
