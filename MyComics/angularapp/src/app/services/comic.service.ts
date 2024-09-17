import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Comic } from '../model/comic.model';

@Injectable({
  providedIn: 'root'
})
export class ComicService {
  public backendUrl = 'https://ide-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/proxy/3001/comics'; 

  constructor(private http: HttpClient) { }

  getComics(): Observable<Comic[]> {
    return this.http.get<Comic[]>(this.backendUrl);
  }

  addComic(comic: Comic): Observable<Comic> {
    return this.http.post<Comic>(this.backendUrl, comic);
  }

  getComicById(id: number): Observable<Comic> {
    const url = `${this.backendUrl}/${id}`;
    return this.http.get<Comic>(url);
  }
  updateComic(id: number, comic: Comic): Observable<Comic> {
    const url = `${this.backendUrl}/${id}`;
    return this.http.put<Comic>(url, comic);
  }

  deleteComic(id: number): Observable<void> {
    const url = `${this.backendUrl}/${id}`;
    return this.http.delete<void>(url);
  }
}
