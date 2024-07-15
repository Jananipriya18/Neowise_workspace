import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PartyhallService } from 'src/app/services/partyhall.service';

@Component({
  selector: 'app-customer-view-party-hall',
  templateUrl: './customer-view-party-hall.component.html',
  styleUrls: ['./customer-view-party-hall.component.css']
})
export class CustomerViewPartyHallComponent implements OnInit {
  partyHalls: any = [];

  constructor(private partyhallService: PartyhallService, private router: Router) { }

  ngOnInit(): void {
    this.getAllPartyHalls();
  }

  getAllPartyHalls() {
    this.partyhallService.getAllPartyHalls().subscribe((response: any) => {
      console.log("All party halls", response);
      this.partyHalls = response;
    });
  }

  navigateToAddBooking(partyHall) {
    localStorage.setItem("capacity", partyHall.capacity);
    this.router.navigate(['/customer/add/booking', partyHall.partyHallId]);
  }
}
