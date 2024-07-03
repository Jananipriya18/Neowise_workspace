// customerlogin.component.ts

import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { CustomerLogin } from 'src/app/models/customer-login'; // Adjust the path as per your project structure

@Component({
  selector: 'app-customerlogin',
  templateUrl: './customerlogin.component.html',
  styleUrls: ['./customerlogin.component.css']
})
export class CustomerLoginComponent {

  customerLogin: CustomerLogin = {
    userName: '',
    password: '',
    email: '',
    phoneNumber: '',
    twoFactorEnabledPassCode: ''
  };

  constructor(private authService: AuthService, private router: Router) {}

  customerlogin(): void {
    this.authService.customerlogin(this.customerLogin).subscribe(
      response => {
        this.router.navigate(['/dashboard']);
      },
      error => {
        console.error('Customer Login failed:', error);
      }
    );
  }
}
