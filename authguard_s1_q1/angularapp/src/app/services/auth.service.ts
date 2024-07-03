// auth.service.ts

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://8080-aabdbffdadabafcfd314192923becabaaeeffone.premiumproject.examly.io';

  constructor(private http: HttpClient) {}

  register(username: string, password: string,email:string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/api/register`, { username, password, email }).pipe(
      tap(response => {
        if (response && response.message === "Login successful") {
          localStorage.setItem('registeredIn', 'true');
        }
      })
    );
  }
  isRegisteredIn(): boolean {
    return localStorage.getItem('registeredIn') === 'true';
  }
}
