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

    // Validate if any required field is empty
    if (!this.newPlaylist.playlistName || !this.newPlaylist.songName || 
        !this.newPlaylist.yearOfRelease || !this.newPlaylist.artistName || 
        !this.newPlaylist.genre || !this.newPlaylist.MovieName) {
      console.log('Form is invalid.');
      return;
    }

    // If form is valid, add the playlist
    this.playlistService.addPlaylist(this.newPlaylist).subscribe(() => {
      console.log('Playlist added successfully!');
      this.router.navigate(['/viewPlaylists']);
    });
  }
}
