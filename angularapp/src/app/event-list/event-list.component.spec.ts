import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { EventService } from '../services/event.service'; // Import EventService
import { EventListComponent } from './event-list.component'; // Adjust the import path
import { of } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Event } from '../models/event.model'; // Import Event model

describe('EventListComponent', () => {
    let component: EventListComponent;
    let fixture: ComponentFixture<EventListComponent>;
    let mockEventService: jasmine.SpyObj<EventService>; // Specify the type of mock

    beforeEach(waitForAsync(() => {
        // Create a spy object with the methods you want to mock
        mockEventService = jasmine.createSpyObj<EventService>('EventService', ['getEvents', 'deleteEvent'] as any);

        TestBed.configureTestingModule({
            declarations: [EventListComponent],
            imports: [RouterTestingModule, HttpClientTestingModule],
            providers: [
                // Provide the mock service instead of the actual service
                { provide: EventService, useValue: mockEventService }
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(EventListComponent);
        component = fixture.componentInstance;
    });

    fit('should_create_EventListComponent', () => { // Change the function name
        expect(component).toBeTruthy();
    });

    fit('EventListComponent_should_call_loadEvents_on_ngOnInit', () => { // Change the function name
        spyOn(component, 'loadEvents'); // Adjust the method name
        fixture.detectChanges();
        expect(component.loadEvents).toHaveBeenCalled(); // Adjust the method name
    });

    fit('EventListComponent_should_have_searchEvents_method', () => {
        expect(component.searchEvents).toBeDefined();
      }); 

});
