// event-list.component.ts
import { Component, OnInit } from '@angular/core';
import { Playlist } from '../models/event.model'; // Import Playlist model
import { PlaylistService } from '../services/event.service'; // Import PlaylistService
import { Router } from '@angular/router';

@Component({
  selector: 'app-event-list', // Changed selector to 'app-event-list'
  templateUrl: './event-list.component.html', // Adjusted the template URL
  styleUrls: ['./event-list.component.css'] // Adjusted the style URL
})
export class PlaylistListComponent implements OnInit {
  events: Playlist[] = []; // Changed recipes to events
  searchTerm: string = '';

  constructor(private eventService: PlaylistService, private router: Router) { } // Adjusted service name

  ngOnInit(): void {
    this.loadPlaylists(); // Adjusted the method name
  }

  loadPlaylists(): void {
    this.eventService.getPlaylists().subscribe(events => this.events = events); // Adjusted the service method name
  }

  deletePlaylist(playlistId: number): void { // Adjusted the method name and parameter
    // Navigate to confirm delete page with the event ID as a parameter
    this.router.navigate(['/confirmDelete', playlistId]);
  }
  searchPlaylists(): void {
    if (this.searchTerm) {
      this.eventService.searchPlaylists(this.searchTerm).subscribe(events => this.events = events);
    } else {
      this.loadPlaylists();
    }
  }
}
