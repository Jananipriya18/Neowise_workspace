import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { JwtService } from './jwt.service';
import { Register } from './register.model'; // Assuming you have defined Register model

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public apiUrl = 'https://8080-efdafbdadab312503686bfbeafaecbcethree.premiumproject.examly.io';

  constructor(public http: HttpClient, public jwtService: JwtService) { }

  // Function to register user and save JWT token
  register(registerData: Register): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/api/register`, registerData).pipe(
      tap(response => {
        if (response && response.token) {
          this.jwtService.saveToken(response.token);
        }
      })
    );
  }

  // Function to logout user and remove JWT token
  logout(): void {
    this.jwtService.destroyToken();
  }

  // Function to check if user is logged in
  isLoggedIn(): boolean {
    return this.jwtService.isLoggedIn();
  }

  isAdmin(): boolean {
    const token = this.jwtService.getToken();
    if (!token) {
      return false;
    }
    
    const tokenParts = token.split('.');
    if (tokenParts.length !== 3) {
      return false;
    }
    
    const decodedPayload = JSON.parse(atob(tokenParts[1]));
    return decodedPayload.name === 'admin';
  }
}
