import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Game } from '../model/game.model';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  public backendUrl = 'https://ide-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/proxy/3001/doctors'; 

  constructor(private http: HttpClient) { }

  getGames(): Observable<Game[]> {
    return this.http.get<Game[]>(this.backendUrl);
  }

  addGame(doctor: Game): Observable<Game> {
    return this.http.post<Game>(this.backendUrl, doctor);
  }

  getGameById(id: number): Observable<Game> {
    const url = `${this.backendUrl}/${id}`;
    return this.http.get<Game>(url);
  }
  updateGame(id: number, doctor: Game): Observable<Game> {
    const url = `${this.backendUrl}/${id}`;
    return this.http.put<Game>(url, doctor);
  }

  deleteGame(id: number): Observable<void> {
    const url = `${this.backendUrl}/${id}`;
    return this.http.delete<void>(url);
  }
}
