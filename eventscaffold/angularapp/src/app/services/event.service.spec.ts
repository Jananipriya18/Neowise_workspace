import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { EventService } from './event.service'; // Adjusted service import
import { Event } from '../models/event.model';

describe('EventService', () => {
  let service: EventService;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [EventService], // Changed service provider to EventService
    });
    service = TestBed.inject(EventService); // Changed service variable assignment to EventService
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  fit('EventService_should_be_created', () => {
    expect(service).toBeTruthy();
  });

  fit('EventService_should_add_an_event_and_return_it', () => {
    const mockEvent: Event = {
      playlistId: 100,
      playlistName: 'Test Event Name',
      songName: 'Test Event Description',
      yearOfRelease: 'Test Event Date',
      artistName: 'Test Event Time',
      genre: 'Test Event Location',
      MovieName: 'Test Event Organizer'
    };

    service.addEvent(mockEvent).subscribe((event) => {
      expect(event).toEqual(mockEvent);
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}/api/Event`); // Adjusted API endpoint
    expect(req.request.method).toBe('POST');
    req.flush(mockEvent);
  });

  fit('EventService_should_get_events', () => {
    const mockEvents: Event[] = [
      {
        playlistId: 100,
        playlistName: 'Test Event Name',
        songName: 'Test Event Description',
        yearOfRelease: 'Test Event Date',
        artistName: 'Test Event Time',
        genre: 'Test Event Location',
        MovieName: 'Test Event Organizer'
      }
    ];

    service.getEvents().subscribe((events) => {
      expect(events).toEqual(mockEvents);
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}/api/Event`); // Adjusted API endpoint
    expect(req.request.method).toBe('GET');
    req.flush(mockEvents);
  });

  fit('EventService_should_delete_event', () => {
    const playlistId = 100;

    service.deleteEvent(playlistId).subscribe(() => {
      expect().nothing();
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}/api/Event/${playlistId}`); // Adjusted API endpoint
    expect(req.request.method).toBe('DELETE');
    req.flush({});
  });

  fit('EventService_should_get_event_by_id', () => {
    const playlistId = 100;
    const mockEvent: Event = {
      playlistId: playlistId,
      playlistName: 'Test Event Name',
      songName: 'Test Event Description',
      yearOfRelease: 'Test Event Date',
      artistName: 'Test Event Time',
      genre: 'Test Event Location',
      MovieName: 'Test Event Organizer'
    };

    service.getEvent(playlistId).subscribe((event) => {
      expect(event).toEqual(mockEvent);
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}/api/Event/${playlistId}`); // Adjusted API endpoint
    expect(req.request.method).toBe('GET');
    req.flush(mockEvent);
  });

});
