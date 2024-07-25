import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Event } from '../models/event.model';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  private apiUrl = 'https://8080-bfdeeddcedfa313953410ebabbcadeeefceacone.premiumproject.examly.io'; // Replace this with your API endpoint

  constructor(private http: HttpClient) { }

  addEvent(event: Event): Observable<Event> {
    return this.http.post<Event>(`${this.apiUrl}/api/Event`, event);
  }

  getEvents(): Observable<Event[]> {
    return this.http.get<Event[]>(`${this.apiUrl}/api/Event`);
  }

  deleteEvent(playlistId: number): Observable<void> {
    const url = `${this.apiUrl}/api/Event/${playlistId}`; // Adjust the URL to match your API endpoint
    return this.http.delete<void>(url);
  }

  getEvent(playlistId: number): Observable<Event> {
    const url = `${this.apiUrl}/api/Event/${playlistId}`;
    return this.http.get<Event>(url);
  }

  searchEvents(searchTerm: string): Observable<Event[]> {
    const params = new HttpParams().set('searchTerm', searchTerm);
    return this.http.get<Event[]>(`${this.apiUrl}/api/Event/search`, { params });
  }
}
