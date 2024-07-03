// jwtservice.service.ts

import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class JwtService {

  constructor() { }

  // Function to save JWT token to local storage
  saveToken(token: string): void {
    localStorage.setItem('jwtToken', token);
    console.log(token);
  }

  // Function to get JWT token from local storage
  getToken(): string | null {
    return localStorage.getItem('jwtToken');
  }

  // Function to remove JWT token from local storage
  destroyToken(): void {
    localStorage.removeItem('jwtToken'); 
  }

  // Function to check if user is logged in
  isLoggedIn(): boolean {
    return !!this.getToken();
  }
}
