import { Component } from '@angular/core';
import { Event } from '../models/event.model';
import { EventService } from '../services/event.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-event-form',
  templateUrl: './event-form.component.html',
  styleUrls: ['./event-form.component.css']
})
export class EventFormComponent {
  newEvent: Event = {
    playlistId: 0,
    playlistName: '',
    songName: '',
    yearOfRelease: '',
    artistName: '',
    genre: '',
    MovieName: ''
  }; // Initialize newEvent with empty fields

  formSubmitted = false; // Track form submission

  constructor(private eventService: EventService, private router: Router) { }

  addEvent(): void {
    this.formSubmitted = true; // Set formSubmitted to true on form submission
    if (this.isFormValid()) {
      this.eventService.addEvent(this.newEvent).subscribe(() => {
        console.log('Event added successfully!');
        this.router.navigate(['/viewEvents']);
      });
    }
  }

  isFieldInvalid(fieldName: string): boolean {
    const fieldValue = this.newEvent[fieldName];
    return !fieldValue && this.formSubmitted;
  }

  isFormValid(): boolean {
    return !this.isFieldInvalid('playlistName') && !this.isFieldInvalid('songName') &&
      !this.isFieldInvalid('yearOfRelease') && !this.isFieldInvalid('artistName') &&
      !this.isFieldInvalid('genre') && !this.isFieldInvalid('MovieName');
  }
}
