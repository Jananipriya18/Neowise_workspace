import { Component, OnInit } from '@angular/core';
import { CheeseShop } from '../models/cheese-shop'; 
import { CheeseShopService } from '../services/cheese-shop.service'; 
import { Router } from '@angular/router';

@Component({
  selector: 'app-cheese-shop-list', 
  templateUrl: './cheese-shop-list.component.html',
  styleUrls: ['./cheese-shop-list.component.css'] 
})
export class CheeseShopListComponent implements OnInit {
  cheeseShops: CheeseShop[] = [];
  searchTerm: string = '';

  constructor(private cheeseShopService: CheeseShopService, private router: Router) { } 
  ngOnInit(): void {
    this.loadCheeseShops(); 
  }

  loadCheeseShops(): void {
    this.cheeseShopService.getCheeseShops().subscribe(cheeseShops => this.cheeseShops = cheeseShops);
  }

  deleteCheeseShop(shopId: number): void { 
    this.router.navigate(['/confirmDelete', shopId]);
  }
  
  searchCheeseShops(): void {
    if (this.searchTerm) {
      this.cheeseShopService.searchCheeseShops(this.searchTerm).subscribe(cheeseShops => this.cheeseShops = cheeseShops);
    } else {
      this.loadCheeseShops();
    }
  }
}
