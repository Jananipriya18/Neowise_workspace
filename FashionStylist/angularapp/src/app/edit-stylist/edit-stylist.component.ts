// edit-stylist.component.ts
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Stylish } from '../model/stylist.model';
import { StylishService } from '../services/stylist.service';

@Component({
  selector: 'app-edit-stylist',
  templateUrl: './edit-stylist.component.html',
  styleUrls: ['./edit-stylist.component.css']
})
export class EditStylishComponent implements OnInit {
  stylist: Stylish | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private stylistService: StylishService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.stylistService.getStylishById(+id).subscribe(
        (stylist) => (this.stylist = stylist),
        (err) => console.error(err)
      );
    }
  }

  saveStylish(): void {
    if (this.stylist) {
      this.stylistService.updateStylish(this.stylist.id, this.stylist).subscribe(
        () => {
          this.router.navigate(['/stylists']);
        },
        (err) => {
          console.error(err);
        }
      );
    }
  }

  cancel(): void {
    this.router.navigate(['/stylists']);
  }
}
