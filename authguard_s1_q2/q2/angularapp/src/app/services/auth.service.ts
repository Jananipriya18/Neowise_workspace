// auth.service.ts

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})  
export class AuthService {
  private apiUrl = 'https://8080-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io';

  constructor(private http: HttpClient) {}

  customerlogin(username: string, password: string, email: string, phoneNumber:string, twoFactorEnabledPassCode:string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/api/customerlogin`, { username, password, email, phoneNumber, twoFactorEnabledPassCode }).pipe(
      tap(response => {
        if (response && response.message === "Customer Login successful") {
          localStorage.setItem('loggedIn', 'true');
        }
      })
    );
  }
  isLoggedIn(): boolean {
    return localStorage.getItem('loggedIn') === 'true';
  }
}
