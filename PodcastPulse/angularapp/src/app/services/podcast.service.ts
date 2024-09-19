import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Podcast } from '../model/podcast.model';

@Injectable({
  providedIn: 'root'
})
export class PodcastService {
  public backendUrl = 'https://ide-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/proxy/3001/podcasts'; 

  constructor(private http: HttpClient) { }

  getPodcast(): Observable<Podcast []> {
    return this.http.get<Podcast []>(this.backendUrl);
  }

  addPodcast (podcast : Podcast ): Observable<Podcast > {
    return this.http.post<Podcast >(this.backendUrl, podcast );
  }

  getPodcastById(id: number): Observable<Podcast > {
    const url = `${this.backendUrl}/${id}`;
    return this.http.get<Podcast >(url);
  }
  updatePodcast (id: number, podcast : Podcast ): Observable<Podcast > {
    const url = `${this.backendUrl}/${id}`;
    return this.http.put<Podcast >(url, podcast );
  }
}
