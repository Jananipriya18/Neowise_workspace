import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Playlist } from '../models/event.model';

@Injectable({
  providedIn: 'root'
})
export class PlaylistService {
  private apiUrl = 'https://8080-bfdeeddcedfa313953410ebabbcadeeefceacone.premiumproject.examly.io'; // Replace this with your API endpoint

  constructor(private http: HttpClient) { }

  addPlaylist(event: Playlist): Observable<Playlist> {
    return this.http.post<Playlist>(`${this.apiUrl}/api/Playlist`, event);
  }

  getPlaylists(): Observable<Playlist[]> {
    return this.http.get<Playlist[]>(`${this.apiUrl}/api/Playlist`);
  }

  deletePlaylist(playlistId: number): Observable<void> {
    const url = `${this.apiUrl}/api/Playlist/${playlistId}`; // Adjust the URL to match your API endpoint
    return this.http.delete<void>(url);
  }

  getPlaylist(playlistId: number): Observable<Playlist> {
    const url = `${this.apiUrl}/api/Playlist/${playlistId}`;
    return this.http.get<Playlist>(url);
  }

  searchPlaylists(searchTerm: string): Observable<Playlist[]> {
    const params = new HttpParams().set('searchTerm', searchTerm);
    return this.http.get<Playlist[]>(`${this.apiUrl}/api/Playlist/search`, { params });
  }
}
