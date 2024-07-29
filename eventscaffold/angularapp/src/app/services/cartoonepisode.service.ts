import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CartoonEpisode } from '../models/cartoonepisode.model';

@Injectable({
  providedIn: 'root'
})
export class CartoonEpisodeService {
  private apiUrl = 'https://8080-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io';

  constructor(private http: HttpClient) { }

  addCartoonEpisode(cartoonEpisode: CartoonEpisode): Observable<CartoonEpisode> {
    return this.http.post<CartoonEpisode>(`${this.apiUrl}/api/CartoonEpisode`, cartoonEpisode);
  }

  getCartoonEpisodes(): Observable<CartoonEpisode[]> {
    return this.http.get<CartoonEpisode[]>(`${this.apiUrl}/api/CartoonEpisode`);
  }

  deleteCartoonEpisode(episodeId: number): Observable<void> {
    const url = `${this.apiUrl}/api/CartoonEpisode/${episodeId}`; // Adjust the URL to match your API endpoint
    return this.http.delete<void>(url);
  }

  getCartoonEpisode(episodeId: number): Observable<CartoonEpisode> {
    const url = `${this.apiUrl}/api/CartoonEpisode/${episodeId}`;
    return this.http.get<CartoonEpisode>(url);
  }

  searchCartoonEpisodes(searchTerm: string): Observable<CartoonEpisode[]> {
    const params = new HttpParams().set('searchTerm', searchTerm);
    return this.http.get<CartoonEpisode[]>(`${this.apiUrl}/api/CartoonEpisode/search`, { params });
  }
}
