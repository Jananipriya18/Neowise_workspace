import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CartoonEpisodeService } from '../services/cartoonepisode.service'; 
import { CartoonEpisode } from '../models/cartoonepisode.model';

@Component({
  selector: 'app-delete-confirm',
  templateUrl: './delete-confirm.component.html',
  styleUrls: ['./delete-confirm.component.css']
})
export class DeleteConfirmComponent implements OnInit {
  episodeId: number;
  cartoonEpisode: CartoonEpisode = {} as CartoonEpisode; 

  constructor(
    private route: ActivatedRoute, 
    private router: Router,
    private cartoonEpisodeService: CartoonEpisodeService 
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.episodeId = +params['id'];
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

  confirmDelete(episodeId: number): void { 
    this.cartoonEpisodeService.deleteCartoonEpisode(episodeId).subscribe(
      () => {
        console.log('Cartoon episode deleted successfully.');
        this.router.navigate(['/viewEpisodes']); 
      },
      (error) => {
        console.error('Error deleting cartoon episode:', error);
      }
    );
  }

  cancelDelete(): void {
    this.router.navigate(['/viewEpisodes']); 
  }
}
