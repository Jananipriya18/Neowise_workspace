// menu-list.component.ts
import { Component, OnInit } from '@angular/core';
import { Menu } from '../models/menu.model';
import { MenuService } from '../services/menu.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-menu-list',
  templateUrl: './menu-list.component.html',
  styleUrls: ['./menu-list.component.css']
})
export class MenuListComponent implements OnInit {
  menus: Menu[] = [];

  constructor(private menuService: MenuService,private router: Router) { }

  ngOnInit(): void {
    this.loadMenus();
  }

  loadMenus(): void {
    this.menuService.getMenus().subscribe(menus => this.menus = menus);
  }

  Delete(menuId: number): void {
    // Navigate to confirm delete page with the menu ID as a parameter
    this.router.navigate(['/confirmDelete', menuId]);
  }
}

