import { Component, OnInit } from '@angular/core';
import { cartoonepisode } from '../models/cartoonepisode.model'; 
import { CartoonEpisodeService } from '../services/cartoon-episode.service'; 
import { Router } from '@angular/router';

@Component({
  selector: 'app-cartoon-episode-list', 
  templateUrl: './cartoon-episode-list.component.html',
  styleUrls: ['./cartoon-episode-list.component.css'] 
})
export class CartoonEpisodeListComponent implements OnInit {
  cartoonEpisodes: cartoonepisode[] = [];
  searchTerm: string = '';

  constructor(private cartoonEpisodeService: CartoonEpisodeService, private router: Router) { } 
  ngOnInit(): void {
    this.loadCartoonEpisodes(); 
  }

  loadCartoonEpisodes(): void {
    this.cartoonEpisodeService.getCartoonEpisodes().subscribe(cartoonEpisodes => this.cartoonEpisodes = cartoonEpisodes); // Adjusted the service method name
  }

  deleteCartoonEpisode(episodeId: number): void { 
    this.router.navigate(['/confirmDelete', episodeId]);
  }
  
  searchCartoonEpisodes(): void {
    if (this.searchTerm) {
      this.cartoonEpisodeService.searchCartoonEpisodes(this.searchTerm).subscribe(cartoonEpisodes => this.cartoonEpisodes = cartoonEpisodes);
    } else {
      this.loadCartoonEpisodes();
    }
  }
}
