import { Component, OnInit } from '@angular/core';
import { CartoonEpisode } from '../models/cartoon-episode.model'; // Import CartoonEpisode model
import { CartoonEpisodeService } from '../services/cartoon-episode.service'; // Import CartoonEpisodeService
import { Router } from '@angular/router';

@Component({
  selector: 'app-cartoon-episode-list', // Changed selector to 'app-cartoon-episode-list'
  templateUrl: './cartoon-episode-list.component.html', // Adjusted the template URL
  styleUrls: ['./cartoon-episode-list.component.css'] // Adjusted the style URL
})
export class CartoonEpisodeListComponent implements OnInit {
  cartoonEpisodes: CartoonEpisode[] = []; // Changed recipes to cartoonEpisodes
  searchTerm: string = '';

  constructor(private cartoonEpisodeService: CartoonEpisodeService, private router: Router) { } // Adjusted service name

  ngOnInit(): void {
    this.loadCartoonEpisodes(); // Adjusted the method name
  }

  loadCartoonEpisodes(): void {
    this.cartoonEpisodeService.getCartoonEpisodes().subscribe(cartoonEpisodes => this.cartoonEpisodes = cartoonEpisodes); // Adjusted the service method name
  }

  deleteCartoonEpisode(episodeId: number): void { // Adjusted the method name and parameter
    // Navigate to confirm delete page with the episode ID as a parameter
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
