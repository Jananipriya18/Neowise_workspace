// playlist-list.component.ts
import { Component, OnInit } from '@angular/core';
import { Playlist } from '../models/playlist.model'; // Import Playlist model
import { PlaylistService } from '../services/playlist.service'; // Import PlaylistService
import { Router } from '@angular/router';

@Component({
  selector: 'app-playlist-list', // Changed selector to 'app-playlist-list'
  templateUrl: './playlist-list.component.html', // Adjusted the template URL
  styleUrls: ['./playlist-list.component.css'] // Adjusted the style URL
})
export class PlaylistListComponent implements OnInit {
  playlists: Playlist[] = []; // Changed recipes to playlists
  searchTerm: string = '';

  constructor(private playlistService: PlaylistService, private router: Router) { } // Adjusted service name

  ngOnInit(): void {
    this.loadPlaylists(); // Adjusted the method name
  }

  loadPlaylists(): void {
    this.playlistService.getPlaylists().subscribe(playlists => this.playlists = playlists); // Adjusted the service method name
  }

  deletePlaylist(playlistId: number): void { // Adjusted the method name and parameter
    // Navigate to confirm delete page with the playlist ID as a parameter
    this.router.navigate(['/confirmDelete', playlistId]);
  }
  searchPlaylists(): void {
    if (this.searchTerm) {
      this.playlistService.searchPlaylists(this.searchTerm).subscribe(playlists => this.playlists = playlists);
    } else {
      this.loadPlaylists();
    }
  }
}
