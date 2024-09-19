import { Component, OnInit } from '@angular/core';
import { StylistService } from '../services/stylist.service';
import { Router } from '@angular/router';
import { Stylist } from '../model/stylist.model';

@Component({
  selector: 'app-delete-stylist',
  templateUrl: './delete-stylist.component.html',
  styleUrls: ['./delete-stylist.component.css']
})
export class DeleteStylistComponent implements OnInit {
  stylists: Stylist[] = [];

  constructor(private stylistService: StylistService,private router: Router) {}

  ngOnInit(): void {
  }

  deleteStylist(id: any): void {
    this.stylistService.deleteStylist(id).subscribe(() => {
      this.stylists = this.stylists.filter((stylist) => stylist.id !== id);
    });
  }  
}


  

  