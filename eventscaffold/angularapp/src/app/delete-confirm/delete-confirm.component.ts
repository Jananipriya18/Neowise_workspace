import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PlaylistService } from '../services/event.service' // Adjusted service name
import { Playlist } from '../models/event.model'; // Adjusted model name

@Component({
  selector: 'app-delete-confirm',
  templateUrl: './delete-confirm.component.html',
  styleUrls: ['./delete-confirm.component.css']
})
export class DeleteConfirmComponent implements OnInit {
  playlistId: number;
  event: Playlist = {} as Playlist; // Initialize event property with an empty object

  constructor(
    private route: ActivatedRoute, 
    private router: Router,
    private eventService: PlaylistService // Adjusted service name
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.playlistId = +params['id']; // Adjust parameter name to 'id' if it matches the route parameter
      this.eventService.getPlaylist(this.playlistId).subscribe(
        (event: Playlist) => {
          this.event = event;
        },
        error => {
          console.error('Error fetching event:', error);
        }
      );
    });
  }

  confirmDelete(playlistId: number): void { // Adjust method signature
    this.eventService.deletePlaylist(playlistId).subscribe(
      () => {
        console.log('Playlist deleted successfully.');
        this.router.navigate(['/viewPlaylists']); // Adjust the route to navigate after deletion
      },
      (error) => {
        console.error('Error deleting event:', error);
      }
    );
  }

  cancelDelete(): void {
    this.router.navigate(['/viewPlaylists']); // Adjust the route to navigate on cancel
  }
}
