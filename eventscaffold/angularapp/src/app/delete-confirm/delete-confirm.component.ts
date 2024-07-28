import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CartoonEpisodeService } from '../services/cartoon-episode.service'; // Adjusted service name
import { CartoonEpisode } from '../models/cartoon-episode.model'; // Adjusted model name

@Component({
  selector: 'app-delete-confirm',
  templateUrl: './delete-confirm.component.html',
  styleUrls: ['./delete-confirm.component.css']
})
export class DeleteConfirmComponent implements OnInit {
  episodeId: number;
  cartoonEpisode: CartoonEpisode = {} as CartoonEpisode; // Initialize cartoonEpisode property with an empty object

  constructor(
    private route: ActivatedRoute, 
    private router: Router,
    private cartoonEpisodeService: CartoonEpisodeService // Adjusted service name
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.episodeId = +params['id']; // Adjust parameter name to 'id' if it matches the route parameter
      this.cartoonEpisodeService.getCartoonEpisode(this.episodeId).subscribe(
        (cartoonEpisode: CartoonEpisode) => {
          this.cartoonEpisode = cartoonEpisode;
        },
        error => {
          console.error('Error fetching cartoon episode:', error);
        }
      );
    });
  }

  confirmDelete(episodeId: number): void { // Adjust method signature
    this.cartoonEpisodeService.deleteCartoonEpisode(episodeId).subscribe(
      () => {
        console.log('Cartoon episode deleted successfully.');
        this.router.navigate(['/viewCartoonEpisodes']); // Adjust the route to navigate after deletion
      },
      (error) => {
        console.error('Error deleting cartoon episode:', error);
      }
    );
  }

  cancelDelete(): void {
    this.router.navigate(['/viewCartoonEpisodes']); // Adjust the route to navigate on cancel
  }
}
