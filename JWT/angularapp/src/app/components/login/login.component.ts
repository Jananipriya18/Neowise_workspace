
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
 selector: 'app-login',
 templateUrl: './login.component.html',
 styleUrls: ['./login.component.css']
})
export class LoginComponent {

 username: string;
 password: string;

 constructor(private authService: AuthService, private router: Router) {}
 ngOnInit(): void {
  // Ensure token is destroyed when user navigates to login page
  this.authService.logout();
 }
 login(): void {
  this.authService.login(this.username, this.password).subscribe(
   response => {
    this.router.navigate(['/dashboard']);
   },
   error => {
    console.error('Login failed:', error);
    
   }
  );
 }

}
