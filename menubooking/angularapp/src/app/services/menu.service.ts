// src/app/services/menu.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Menu } from '../models/menu.model';


@Injectable({
  providedIn: 'root'
})
export class MenuService {
  private apiUrl = 'https://8080-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/'; 

  constructor(private http: HttpClient) { }

  addMenu(menu: Menu): Observable<Menu> {
    return this.http.post<Menu>(`${this.apiUrl}api/MenuBooking`, menu);
  }

  getMenus(): Observable<Menu[]> {
    return this.http.get<Menu[]>(`${this.apiUrl}api/MenuBooking`);
  }

  deleteMenu(menuId: number): Observable<void> {
    const url = `${this.apiUrl}api/MenuBooking/${menuId}`; 
    return this.http.delete<void>(url);
  }

  getMenu(menuId: number): Observable<Menu> {
    const url = `${this.apiUrl}api/MenuBooking/${menuId}`;
    return this.http.get<Menu>(url);
  }
}
