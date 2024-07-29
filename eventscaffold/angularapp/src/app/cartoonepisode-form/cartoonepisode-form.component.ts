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
    if (this.isFormValid()) {
      this.cartoonEpisodeService.addCartoonEpisode(this.newEpisode).subscribe(() => {
        console.log('Cartoon episode added successfully!');
        this.router.navigate(['/viewEpisodes']);
      });
    }
  }

  isFieldInvalid(fieldName: string): boolean {
    const fieldValue = this.newEpisode[fieldName];
    return !fieldValue && this.formSubmitted;
  }

  isFormValid(): boolean {
    return !this.isFieldInvalid('cartoonSeriesName') && !this.isFieldInvalid('episodeTitle') &&
      !this.isFieldInvalid('releaseDate') && !this.isFieldInvalid('directorName') &&
      !this.isFieldInvalid('duration') && !this.isFieldInvalid('genre');
  }
}
