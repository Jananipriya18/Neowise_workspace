import { Component } from '@angular/core';
import { Menu } from '../models/menu.model';
import { MenuService } from '../services/menu.service'; // Corrected import statement
import { Router } from '@angular/router';


@Component({
  selector: 'app-menu-form',
  templateUrl: './menu-form.component.html',
  styleUrls: ['./menu-form.component.css']
})

export class MenuFormComponent {
  newMenu: Menu = {
    menuId: 0,
    chefName: '',
    menuName: '',
    description: '',
    price: '',
    availability: ''
  };
  
  formSubmitted = false; 

  constructor(private menuService: MenuService, private router: Router) { }

  addMenu(): void {
    this.formSubmitted = true; // Set formSubmitted to true on form submission
    if (this.isFormValid()) {
      this.menuService.addMenu(this.newMenu).subscribe(() => {
        console.log('Menu added successfully!');
        this.router.navigate(['/viewMenus']);
      });
    }
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.newMenu[fieldName];
    return !field && (this.formSubmitted || this.newMenu[fieldName].touched);
  }

  isFormValid(): boolean {
    return !this.isFieldInvalid('chefName') && !this.isFieldInvalid('menuName') &&
      !this.isFieldInvalid('description') && !this.isFieldInvalid('price') &&
      !this.isFieldInvalid('availability');
  }
}

