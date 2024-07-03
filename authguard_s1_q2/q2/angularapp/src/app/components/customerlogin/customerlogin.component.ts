import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { CustomerLogin } from 'src/app/models/customerlogin.model';

@Component({
  selector: 'app-customerlogin',
  templateUrl: './customerlogin.component.html',
  styleUrls: ['./customerlogin.component.css']
})
export class CustomerloginComponent {

  customerLogin: CustomerLogin = {
    userName: '',
    password: '',
    email: '',
    phoneNumber: '',
    twoFactorEnabledPassCode: ''
  };

  constructor(private authService: AuthService, private router: Router) {}

  customerlogin(): void {
    const { userName, password, email, phoneNumber, twoFactorEnabledPassCode } = this.customerLogin;
    this.authService.customerlogin(userName, password, email, phoneNumber, twoFactorEnabledPassCode).subscribe(
      response => {
        if (response.message === "Customer Login successful") {
          this.router.navigate(['/dashboard']);
        }
      },
      error => {
        console.error('Customer Login failed:', error);
      }
    );
  }
}
