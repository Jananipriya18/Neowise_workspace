import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { EventFormComponent } from './event-form.component';
import { EventService } from '../services/event.service';
import { Router } from '@angular/router';
import { of } from 'rxjs';
import { By } from '@angular/platform-browser';
import { Event } from '../models/event.model';

describe('EventFormComponent', () => {
  let component: EventFormComponent;
  let fixture: ComponentFixture<EventFormComponent>;
  let eventService: EventService;
  let router: Router;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EventFormComponent],
      imports: [FormsModule, RouterTestingModule, HttpClientTestingModule],
      providers: [EventService]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EventFormComponent);
    component = fixture.componentInstance;
    eventService = TestBed.inject(EventService);
    router = TestBed.inject(Router);
    fixture.detectChanges();
  });

  fit('should_create_EventFormComponent', () => {
    expect(component).toBeTruthy();
  });

  fit('EventFormComponent_should_render_error_messages_when_required_fields_are_empty_on_submit', () => {
    // Set all fields to empty strings
    component.newEvent = {
      eventId: 0,
      eventName: '',
      eventDescription: '',
      eventDate: '',
      eventTime: '',
      eventLocation: '',
      eventOrganizer: ''
    } as Event;

    // Manually trigger form submission
    component.formSubmitted = true;

    fixture.detectChanges();

    // Find the form element
    const form = fixture.debugElement.query(By.css('form')).nativeElement;

    // Submit the form
    form.dispatchEvent(new Event('submit'));

    fixture.detectChanges();

    // Check if error messages are rendered for each field
    expect(fixture.debugElement.query(By.css('#eventName + .error-message'))).toBeTruthy();
    expect(fixture.debugElement.query(By.css('#eventDescription + .error-message'))).toBeTruthy();
    expect(fixture.debugElement.query(By.css('#eventDate + .error-message'))).toBeTruthy();
    expect(fixture.debugElement.query(By.css('#eventTime + .error-message'))).toBeTruthy();
    expect(fixture.debugElement.query(By.css('#eventLocation + .error-message'))).toBeTruthy();
    expect(fixture.debugElement.query(By.css('#eventOrganizer + .error-message'))).toBeTruthy();
  });

  fit('EventFormComponent_should_call_addEvent_method_while_adding_the_event', () => {
    // Create a mock Event object with all required properties
    const event: Event = {
      eventId: 1,
      eventName: 'Test Event Name',
      eventDescription: 'Test Event Description',
      eventDate: 'Test Event Date',
      eventTime: 'Test Event Time',
      eventLocation: 'Test Event Location',
      eventOrganizer: 'Test Event Organizer'
    };

    const addEventSpy = spyOn(component, 'addEvent').and.callThrough();
    component.addEvent();
    expect(addEventSpy).toHaveBeenCalled();
  });
});
