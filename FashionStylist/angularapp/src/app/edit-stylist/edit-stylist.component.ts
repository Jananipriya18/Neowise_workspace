// edit-stylist.component.ts
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Stylist } from '../model/stylist.model';
import { StylistService } from '../services/stylist.service';

@Component({
  selector: 'app-edit-stylist',
  templateUrl: './edit-stylist.component.html',
  styleUrls: ['./edit-stylist.component.css']
})
export class EditStylistComponent implements OnInit {
  stylist: Stylist | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private stylistService: StylistService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.stylistService.getStylistById(+id).subscribe(
        (stylist) => (this.stylist = stylist),
        (err) => console.error(err)
      );
    }
  }

  saveStylist(): void {
    if (this.stylist) {
      this.stylistService.updateStylist(this.stylist.id, this.stylist).subscribe(
        () => {
          this.router.navigate(['/stylistsList']);
        },
        (err) => {
          console.error(err);
        }
      );
    }
  }

  cancel(): void {
    this.router.navigate(['/stylistsList']);
  }
}
