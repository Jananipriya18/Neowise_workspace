import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Vendor } from '../models/vendor.model';

@Injectable({
  providedIn: 'root'
})
export class VendorService {
  private apiUrl = 'https://8080-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io'; // Replace this with your API endpoint

  constructor(private http: HttpClient) { }

  addVendor(Vendor: Vendor): Observable<Vendor> {
    return this.http.post<Vendor>(`${this.apiUrl}/api/Vendor`, Vendor);
  }

  getVendors(): Observable<Vendor[]> {
    return this.http.get<Vendor[]>(`${this.apiUrl}/api/Vendor`);
  }

  deleteVendor(VendorId: number): Observable<void> {
    const url = `${this.apiUrl}/api/Vendor/${VendorId}`; // Adjust the URL to match your API endpoint
    return this.http.delete<void>(url);
  }

  getVendor(VendorId: number): Observable<Vendor> {
    const url = `${this.apiUrl}/api/Vendor/${VendorId}`;
    return this.http.get<Vendor>(url);
  }

  searchVendors(searchTerm: string): Observable<Vendor[]> {
    const params = new HttpParams().set('searchTerm', searchTerm);
    return this.http.get<Vendor[]>(`${this.apiUrl}/api/Vendor/search`, { params });
  }
}
