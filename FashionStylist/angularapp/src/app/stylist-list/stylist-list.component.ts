import { Component, OnInit } from '@angular/core';
import { Stylist } from '../model/stylist.model';
import { StylistService } from '../services/stylist.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-stylist-list',
  templateUrl: './stylist-list.component.html',
  styleUrls: ['./stylist-list.component.css'],
})
export class StylistListComponent implements OnInit {
  stylists: Stylist[] = [];
  filteredStylists: Stylist[] = [];

  constructor(private stylistService: StylistService, private router: Router) {}

  ngOnInit(): void {
    this.getStylists();
  }

  getStylists(): void {
    try {
      this.stylistService.getStylists().subscribe(
        (res) => {
          console.log(res);
          this.stylists = res;
          this.filteredStylists = res; // Initialize filteredStylists with all stylists
        },
        (err) => {
          console.log(err);
        }
      );
    } catch (err) {
      console.log('Error:', err);
    }
  }

  deleteStylist(id: any): void {
    this.stylistService.deleteStylist(id).subscribe(() => {
      this.stylists = this.stylists.filter((stylist) => stylist.id !== id);
      this.filteredStylists = this.filteredStylists.filter((stylist) => stylist.id !== id);
    });
  }

  filterStylists(category: string): void {
    if (category === 'basic') {
      this.filteredStylists = this.stylists.filter(stylist => stylist.hourlyRate < 50);
    } else if (category === 'premium') {
      this.filteredStylists = this.stylists.filter(stylist => stylist.hourlyRate >= 50 && stylist.hourlyRate <= 100);
    } else if (category === 'luxury') {
      this.filteredStylists = this.stylists.filter(stylist => stylist.hourlyRate > 100);
    }
  }
}