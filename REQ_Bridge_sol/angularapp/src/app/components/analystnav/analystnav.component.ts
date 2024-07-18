import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-analystnav',
  templateUrl: './analystnav.component.html',
  styleUrls: ['./analystnav.component.css']
})

export class AnalystnavComponent implements OnInit {

  isLoggedIn: boolean = false;
  isCustomer: boolean = false;

  constructor(private authService: AuthService) {
    this.authService.isAuthenticated$.subscribe((authenticated: boolean) => {
      this.isLoggedIn = authenticated;
      if (this.isLoggedIn) {
        this.isCustomer = this.authService.isCustomer();
        console.log(this.isCustomer);

      } else {
        this.isCustomer = false;
      }
    });
  }

  ngOnInit(): void {
    // Initialize the properties on component initialization
    this.isLoggedIn = this.authService.isAuthenticated();
    if (this.isLoggedIn) {
      this.isCustomer = this.authService.isCustomer();
    }
  }
}