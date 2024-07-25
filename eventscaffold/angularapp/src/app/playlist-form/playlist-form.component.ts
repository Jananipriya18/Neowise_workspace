import { Component } from '@angular/core';
import { Playlist } from '../models/playlist.model';
import { PlaylistService } from '../services/playlist.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-playlist-form',
  templateUrl: './playlist-form.component.html',
  styleUrls: ['./playlist-form.component.css']
})
export class PlaylistFormComponent {
  newPlaylist: Playlist = {
    playlistId: 0,
    playlistName: '',
    songName: '',
    yearOfRelease: '',
    artistName: '',
    genre: '',
    MovieName: ''
  }; // Initialize newPlaylist with empty fields

  formSubmitted = false; // Track form submission

  constructor(private playlistService: PlaylistService, private router: Router) { }

  addPlaylist(): void {
    this.formSubmitted = true; // Set formSubmitted to true on form submission
    if (this.isFormValid()) {
      this.playlistService.addPlaylist(this.newPlaylist).subscribe(() => {
        console.log('Playlist added successfully!');
        this.router.navigate(['/viewPlaylists']);
      });
    }
  }

  isFieldInvalid(fieldName: string): boolean {
    const fieldValue = this.newPlaylist[fieldName];
    return !fieldValue && this.formSubmitted;
  }

  isFormValid(): boolean {
    return !this.isFieldInvalid('playlistName') && !this.isFieldInvalid('songName') &&
      !this.isFieldInvalid('yearOfRelease') && !this.isFieldInvalid('artistName') &&
      !this.isFieldInvalid('genre') && !this.isFieldInvalid('MovieName');
  }
}
