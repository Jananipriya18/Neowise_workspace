import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Gardener } from '../model/gardener.model';

@Injectable({
  providedIn: 'root'
})
export class GardenerService {
  public backendUrl = 'https://ide-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/proxy/3001/gardeners'; 

  constructor(private http: HttpClient) { }

  getGardeners(): Observable<Gardener[]> {
    return this.http.get<Gardener[]>(this.backendUrl);
  }

  addGardener(gardener: Gardener): Observable<Gardener> {
    return this.http.post<Gardener>(this.backendUrl, gardener);
  }

  getGardenerById(id: number): Observable<Gardener> {
    const url = `${this.backendUrl}/${id}`;
    return this.http.get<Gardener>(url);
  }

  deleteGardener(id: number): Observable<void> {
    const url = `${this.backendUrl}/${id}`;
    return this.http.delete<void>(url);
  }
}
