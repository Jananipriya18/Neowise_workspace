import { Component, OnInit } from '@angular/core';
import { CartoonEpisode } from '../models/cartoonepisode.model'; 
import { CartoonEpisodeService } from '../services/cartoonepisode.service'; 
import { Router } from '@angular/router';

@Component({
  selector: 'app-cartoonepisode-list', 
  templateUrl: './cartoonepisode-list.component.html',
  styleUrls: ['./cartoonepisode-list.component.css'] 
})
export class CartoonEpisodeListComponent implements OnInit {
  cartoonEpisodes: CartoonEpisode[] = [];
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
