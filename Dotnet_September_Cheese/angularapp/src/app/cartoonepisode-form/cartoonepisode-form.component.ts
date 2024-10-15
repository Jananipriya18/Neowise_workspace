import { Component } from '@angular/core';
import { CartoonEpisode } from '../models/cartoonepisode.model';
import { CartoonEpisodeService } from '../services/cartoonepisode.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cartoonepisode-form',
  templateUrl: './cartoonepisode-form.component.html',
  styleUrls: ['./cartoonepisode-form.component.css']
})
export class CartoonEpisodeFormComponent {
  newEpisode: CartoonEpisode = {
    episodeId: 0,
    cartoonSeriesName: '',
    episodeTitle: '',
    releaseDate: '',
    directorName: '',
    duration: '',
    description: ''
  };

  formSubmitted = false; 

  constructor(private cartoonEpisodeService: CartoonEpisodeService, private router: Router) { }

  addCartoonEpisode(): void {
    this.formSubmitted = true; 

    // Validate if any required field is empty
    if (!this.newEpisode.cartoonSeriesName || !this.newEpisode.episodeTitle ||
        !this.newEpisode.releaseDate || !this.newEpisode.directorName ||
        !this.newEpisode.duration || !this.newEpisode.description) {
      console.log('Form is invalid.');
      return;
    }

    // Add the cartoon episode if the form is valid
    this.cartoonEpisodeService.addCartoonEpisode(this.newEpisode).subscribe(() => {
      console.log('Cartoon episode added successfully!');
      this.router.navigate(['/viewEpisodes']);
    });
  }
}
