import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Doctor } from '../model/stylist.model';

@Injectable({
  providedIn: 'root'
})
export class DoctorService {
  public backendUrl = 'https://ide-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/proxy/3001/doctors'; 

  constructor(private http: HttpClient) { }

  getDoctors(): Observable<Doctor[]> {
    return this.http.get<Doctor[]>(this.backendUrl);
  }

  addDoctor(doctor: Doctor): Observable<Doctor> {
    return this.http.post<Doctor>(this.backendUrl, doctor);
  }

  getDoctorById(id: number): Observable<Doctor> {
    const url = `${this.backendUrl}/${id}`;
    return this.http.get<Doctor>(url);
  }
  updateDoctor(id: number, doctor: Doctor): Observable<Doctor> {
    const url = `${this.backendUrl}/${id}`;
    return this.http.put<Doctor>(url, doctor);
  }

  deleteDoctor(id: number): Observable<void> {
    const url = `${this.backendUrl}/${id}`;
    return this.http.delete<void>(url);
  }
}
