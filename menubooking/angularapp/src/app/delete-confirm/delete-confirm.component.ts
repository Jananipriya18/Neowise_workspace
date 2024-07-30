import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MenuService } from '../services/menu.service';
import { Menu } from '../models/menu.model'; // Import Menu interface

@Component({
  selector: 'app-delete-confirm',
  templateUrl: './delete-confirm.component.html',
  styleUrls: ['./delete-confirm.component.css']
})
export class DeleteConfirmComponent implements OnInit {
  menuId: number;
  menu: Menu; // Initialize menu property with an empty object

  constructor(
    private route: ActivatedRoute, 
    private router: Router,
    private menuService: MenuService
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.menuId = +params['id'];
      this.menuService.getMenu(this.menuId).subscribe(
        (menu: Menu) => {
          this.menu = menu;
        },
        error => {
          console.error('Error fetching menu:', error);
        }
      );
    });
  }

  confirmDelete(menuId: number): void {
    this.menuService.deleteMenu(menuId).subscribe(
      () => {
        console.log('Menu deleted successfully.');
        this.router.navigate(['/viewMenus']);
      },
      (error) => {
        console.error('Error deleting menu:', error);
      }
    );
  }

  cancelDelete(): void {
    this.router.navigate(['/viewMenus']);
  }
}
