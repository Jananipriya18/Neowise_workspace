import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EventService } from '../services/event.service' // Adjusted service name
import { Event } from '../models/event.model'; // Adjusted model name

@Component({
  selector: 'app-delete-confirm',
  templateUrl: './delete-confirm.component.html',
  styleUrls: ['./delete-confirm.component.css']
})
export class DeleteConfirmComponent implements OnInit {
  eventId: number;
  event: Event = {} as Event; // Initialize event property with an empty object

  constructor(
    private route: ActivatedRoute, 
    private router: Router,
    private eventService: EventService // Adjusted service name
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.eventId = +params['id']; // Adjust parameter name to 'id' if it matches the route parameter
      this.eventService.getEvent(this.eventId).subscribe(
        (event: Event) => {
          this.event = event;
        },
        error => {
          console.error('Error fetching event:', error);
        }
      );
    });
  }

  confirmDelete(eventId: number): void { // Adjust method signature
    this.eventService.deleteEvent(eventId).subscribe(
      () => {
        console.log('Event deleted successfully.');
        this.router.navigate(['/viewEvents']); // Adjust the route to navigate after deletion
      },
      (error) => {
        console.error('Error deleting event:', error);
      }
    );
  }

  cancelDelete(): void {
    this.router.navigate(['/viewEvents']); // Adjust the route to navigate on cancel
  }
}
