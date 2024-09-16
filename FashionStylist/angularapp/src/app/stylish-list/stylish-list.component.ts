import { Component, OnInit } from '@angular/core';
import { Stylist } from '../model/stylist.model';
import { StylistService } from '../services/stylist.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-stylist-list',
  templateUrl: './',
  styleUrls: ['./stylist-list.component.css'],
})
export class StylistListComponent implements OnInit {
  stylists: Stylist[] = [];

  constructor(private stylistService: StylistService,private router: Router) {}

  ngOnInit(): void {
    this.getStylists();
  }

  getStylists(): void {
    try {
      this.stylistService.getStylists().subscribe(
        (res) => {
          console.log(res);
          this.stylists = res;
        },
        (err) => {
          console.log(err);
        }
      );
    } catch (err) {
      console.log('Error:', err);
    }
  }

  editStylist(id: number): void {
    this.router.navigate(['/edit', id]);
  }


  deleteStylist(id: any): void {
    this.stylistService.deleteStylist(id).subscribe(() => {
      this.stylists = this.stylists.filter((stylist) => stylist.id !== id);
    });
  }
}
