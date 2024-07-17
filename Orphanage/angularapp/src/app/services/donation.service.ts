import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { apiUrl } from 'src/apiconfig';
import { Donation } from '../models/donation.model';

@Injectable({
  providedIn: 'root'
})
export class DonationService {
  public apiUrl = apiUrl; // Update with your API URL

  constructor(private http: HttpClient) { }

  getAllDonations(): Observable<Donation[]> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.get<Donation[]>(`${this.apiUrl}/api/donations`, { headers });
  }

  getDonationById(donationId: number): Observable<Donation> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.get<Donation>(`${this.apiUrl}/api/donations/${donationId}`, { headers });
  }

  addDonation(donation: Donation): Observable<Donation> {
    console.log('Donation:', donation);
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.post<Donation>(`${this.apiUrl}/api/donations`, donation, { headers });
  }

  updateDonation(donationId: number, donation: Donation): Observable<Donation> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.put<Donation>(`${this.apiUrl}/api/donations/${donationId}`, donation, { headers });
  }

  deleteDonation(donationId: number): Observable<void> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.delete<void>(`${this.apiUrl}/api/donations/${donationId}`, { headers });
  }

  getDonationsByUserId(userId: number): Observable<Donation[]> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.get<Donation[]>(`${this.apiUrl}/api/donations/user/${userId}`, { headers });
  }

  getDonationsByOrphanageId(orphanageId: number): Observable<Donation[]> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.get<Donation[]>(`${this.apiUrl}/api/donations/orphanage/${orphanageId}`, { headers });
  }
}
