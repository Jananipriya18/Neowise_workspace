// event-list.component.ts
import { Component, OnInit } from '@angular/core';
import { Event } from '../models/event.model'; // Import Event model
import { EventService } from '../services/event.service'; // Import EventService
import { Router } from '@angular/router';

@Component({
  selector: 'app-event-list', // Changed selector to 'app-event-list'
  templateUrl: './event-list.component.html', // Adjusted the template URL
  styleUrls: ['./event-list.component.css'] // Adjusted the style URL
})
export class EventListComponent implements OnInit {
  events: Event[] = []; // Changed recipes to events
  searchTerm: string = '';

  constructor(private eventService: EventService, private router: Router) { } // Adjusted service name

  ngOnInit(): void {
    this.loadEvents(); // Adjusted the method name
  }

  loadEvents(): void {
    this.eventService.getEvents().subscribe(events => this.events = events); // Adjusted the service method name
  }

  deleteEvent(playlistId: number): void { // Adjusted the method name and parameter
    // Navigate to confirm delete page with the event ID as a parameter
    this.router.navigate(['/confirmDelete', playlistId]);
  }
  searchEvents(): void {
    if (this.searchTerm) {
      this.eventService.searchEvents(this.searchTerm).subscribe(events => this.events = events);
    } else {
      this.loadEvents();
    }
  }
}
