import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CheeseShop } from '../models/cheese-shop';

@Injectable({
  providedIn: 'root'
})
export class CheeseShopService {
  private apiUrl = 'https://8080-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io';

  constructor(private http: HttpClient) { }

  addCheeseShop(cheeseShop: CheeseShop): Observable<CheeseShop> {
    return this.http.post<CheeseShop>(`${this.apiUrl}/api/CheeseShop`, cheeseShop);
  }

  getCheeseShops(): Observable<CheeseShop[]> {
    return this.http.get<CheeseShop[]>(`${this.apiUrl}/api/CheeseShop`);
  }

  deleteCheeseShop(shopId: number): Observable<void> {
    const url = `${this.apiUrl}/api/CheeseShop/${shopId}`;
    return this.http.delete<void>(url);
  }

  getCheeseShop(shopId: number): Observable<CheeseShop> {
    const url = `${this.apiUrl}/api/CheeseShop/${shopId}`;
    return this.http.get<CheeseShop>(url);
  }

  searchCheeseShops(searchTerm: string): Observable<CheeseShop[]> {
    const params = new HttpParams().set('searchTerm', searchTerm);
    return this.http.get<CheeseShop[]>(`${this.apiUrl}/api/CheeseShop/search`, { params });
  }
}
