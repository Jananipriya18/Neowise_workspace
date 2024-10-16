import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CheeseShopService } from '../services/cheese-shop.service'; 
import { CheeseShop } from '../models/cheese-shop';

@Component({
  selector: 'app-delete-confirm',
  templateUrl: './delete-confirm.component.html',
  styleUrls: ['./delete-confirm.component.css']
})
export class DeleteConfirmComponent implements OnInit {
  shopId: number;
  cheeseShop: CheeseShop = {} as CheeseShop; 

  constructor(
    private route: ActivatedRoute, 
    private router: Router,
    private cheeseShopService: CheeseShopService 
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.shopId = +params['id'];
      this.cheeseShopService.getCheeseShop(this.shopId).subscribe(
        (cheeseShop: CheeseShop) => {
          this.cheeseShop = cheeseShop;
        },
        error => {
          console.error('Error fetching cheese shop:', error);
        }
      );
    });
  }

  confirmDelete(shopId: number): void { 
    this.cheeseShopService.deleteCheeseShop(shopId).subscribe(
      () => {
        console.log('Cheese shop deleted successfully.');
        this.router.navigate(['/viewShops']); 
      },
      (error) => {
        console.error('Error deleting cheese shop:', error);
      }
    );
  }

  cancelDelete(): void {
    this.router.navigate(['/viewShops']); 
  }
}
