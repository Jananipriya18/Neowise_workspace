import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { apiUrl } from 'src/apiconfig';
import { Orphanage } from '../models/orphanage.model';

@Injectable({
  providedIn: 'root'
})
export class OrphanageService {
  public apiUrl = apiUrl; // Update with your API URL

  constructor(private http: HttpClient) { }

  getAllOrphanages(): Observable<Orphanage[]> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.get<Orphanage[]>(`${this.apiUrl}/api/orphanages`, { headers });
  }

  getOrphanageById(orphanageId: number): Observable<Orphanage> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.get<Orphanage>(`${this.apiUrl}/api/orphanages/${orphanageId}`, { headers });
  }

  addOrphanage(orphanage: Orphanage): Observable<any> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.post<any>(`${this.apiUrl}/api/orphanages`, orphanage, { headers });
  }

  updateOrphanage(orphanageId: number, orphanage: Orphanage): Observable<any> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.put<any>(`${this.apiUrl}/api/orphanages/${orphanageId}`, orphanage, { headers });
  }

  deleteOrphanage(orphanageId: number): Observable<any> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.delete<any>(`${this.apiUrl}/api/orphanages/${orphanageId}`, { headers });
  }
}
