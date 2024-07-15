import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { apiUrl } from 'src/apiconfig';
import { PartyHall } from '../models/partyHall.model';
import { Review } from '../models/review.model';

@Injectable({
  providedIn: 'root'
})
export class PartyhallService {
  private apiUrl = apiUrl;

  constructor(private http: HttpClient) {}

  private getHeaders(): HttpHeaders {
    const token = localStorage.getItem('token');
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}` // Adjust based on the token type used
    });
  }

  addPartyHall(partyHall: PartyHall): Observable<any> {
    const headers = this.getHeaders();
    return this.http.post(`${this.apiUrl}/api/partyhall`, partyHall, { headers });
  }

  getAllPartyHalls(): Observable<PartyHall[]> {
    const headers = this.getHeaders();
    return this.http.get<PartyHall[]>(`${this.apiUrl}/api/partyhall`, { headers });
  }

  getPartyHallById(partyHallId: number): Observable<PartyHall> {
    const headers = this.getHeaders();
    return this.http.get<PartyHall>(`${this.apiUrl}/api/partyhall/${partyHallId}`, { headers });
  }

  updatePartyHall(partyHall: PartyHall): Observable<any> {
    const headers = this.getHeaders();
    return this.http.put(`${this.apiUrl}/api/partyhall/${partyHall.partyHallId}`, partyHall, { headers });
  }

  deletePartyHall(partyHallId: number): Observable<any> {
    const headers = this.getHeaders();
    return this.http.delete(`${this.apiUrl}/api/partyhall/${partyHallId}`, { headers });
  }

    addReview(review: Review): Observable<any> {
    const token = localStorage.getItem('token');
    // console.log(token)
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
    });

    return this.http.post(`${this.apiUrl}/api/review`, review, { headers });
  }

  getAllReviews(){
    const token = localStorage.getItem('token');
    // console.log(token)
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
    });
    return this.http.get(`${this.apiUrl}/api/review`, { headers });
  }

  getReviewsByUserId(){
    const userId = localStorage.getItem('userId');
    const token = localStorage.getItem('token');
    // console.log(token)
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
    });
    return this.http.get(`${this.apiUrl}/api/review/${userId}`, { headers });
  }
}
