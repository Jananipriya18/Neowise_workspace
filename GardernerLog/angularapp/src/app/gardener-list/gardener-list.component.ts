import { Component, OnInit } from '@angular/core';
import { Gardener } from '../model/gardener.model'; // Update the import
import { GardenerService } from '../services/gardener.service'; // Adjust the service import
import { Router } from '@angular/router';

@Component({
  selector: 'app-gardener-list', // Change the selector for better clarity
  templateUrl: './gardener-list.component.html', // Update template file
  styleUrls: ['./gardener-list.component.css'], // Update styles file
})
export class GardenerListComponent implements OnInit { // Update class name
  gardeners: Gardener[] = []; // Change the array type to Gardener

  constructor(private gardenerService: GardenerService, private router: Router) {} // Update service

  ngOnInit(): void {
    this.getGardeners(); // Update the method call
  }

  getGardeners(): void {
    try {
      this.gardenerService.getGardeners().subscribe( // Update method to use gardener service
        (res) => {
          console.log(res);
          this.gardeners = res; // Update variable to hold gardeners
        },
        (err) => {
          console.log(err);
        }
      );
    } catch (err) {
      console.log('Error:', err);
    }
  }

  deleteGardener(id: any): void { // Update method for deleting
    this.gardenerService.deleteGardener(id).subscribe(() => {
      this.gardeners = this.gardeners.filter((gardener) => gardener.id !== id); // Update filter
    });
  }
}
