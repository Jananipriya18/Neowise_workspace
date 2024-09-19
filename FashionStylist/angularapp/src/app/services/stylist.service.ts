import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Stylist } from '../model/stylist.model';

@Injectable({
  providedIn: 'root'
})
export class StylistService {
  public backendUrl = 'https://ide-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/proxy/3001/stylists'; 

  constructor(private http: HttpClient) { }

  getStylists(): Observable<Stylist[]> {
    return this.http.get<Stylist[]>(this.backendUrl);
  }

  addStylist(stylist: Stylist): Observable<Stylist> {
    return this.http.post<Stylist>(this.backendUrl, stylist);
  }

  getStylistById(id: number): Observable<Stylist> {
    const url = `${this.backendUrl}/${id}`;
    return this.http.get<Stylist>(url);
  }

  deleteStylist(id: number): Observable<void> {
    const url = `${this.backendUrl}/${id}`;
    return this.http.delete<void>(url);
  }
}
