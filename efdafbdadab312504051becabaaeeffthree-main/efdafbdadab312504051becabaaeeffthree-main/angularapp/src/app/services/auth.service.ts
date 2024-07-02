// auth.service.ts

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://8080-efdafbdadab312504051becabaaeeffthree.premiumproject.examly.io';

  constructor(private http: HttpClient) {}

  login(username: string, password: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/api/login`, { username, password }).pipe(
      tap(response => {
        if (response && response.message === "Login successful") {
          localStorage.setItem('loggedIn', 'true');
        }
      })
    );
  }
  isLoggedIn(): boolean {
    return localStorage.getItem('loggedIn') === 'true';
  }
}
