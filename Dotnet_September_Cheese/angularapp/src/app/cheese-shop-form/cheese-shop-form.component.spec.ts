import { Component } from '@angular/core';
import { CheeseShop } from '../models/cheese-shop';
import { CheeseShopService } from '../services/cheese-shop.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cheese-shop-form',
  templateUrl: './cheese-shop-form.component.html',
  styleUrls: ['./cheese-shop-form.component.css']
})
export class CheeseShopFormComponent {
  newShop: CheeseShop = {
    shopId: 0,
    ownerName: '',
    cheeseSpecialties: '',
    experienceYears: 0,
    storeLocation: '',
    importedCountry: '',
    phoneNumber: ''
  };

  formSubmitted = false; 

  constructor(private cheeseShopService: CheeseShopService, private router: Router) { }

  addCheeseShop(): void {
    this.formSubmitted = true; 

    // Validate if any required field is empty
    if (!this.newShop.ownerName || !this.newShop.cheeseSpecialties ||
        !this.newShop.experienceYears || !this.newShop.storeLocation ||
        !this.newShop.importedCountry || !this.newShop.phoneNumber) {
      console.log('Form is invalid.');
      return;
    }

    // Add the cheese shop if the form is valid
    this.cheeseShopService.addCheeseShop(this.newShop).subscribe(() => {
      console.log('Cheese shop added successfully!');
      this.router.navigate(['/viewShops']);
    });
  }
}
