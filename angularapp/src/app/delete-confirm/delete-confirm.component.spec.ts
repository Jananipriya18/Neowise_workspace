import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { DeleteConfirmComponent } from './delete-confirm.component';
import { RouterTestingModule } from '@angular/router/testing';
import { ActivatedRoute, Router } from '@angular/router';
import { of } from 'rxjs';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { EventService } from '../services/event.service'; // Adjusted service name
import { Event } from '../models/event.model'; // Adjusted model name

describe('DeleteConfirmComponent', () => {
    let component: DeleteConfirmComponent;
    let fixture: ComponentFixture<DeleteConfirmComponent>;
    let router: Router;
    let activatedRoute: ActivatedRoute;
    let mockEventService: jasmine.SpyObj<EventService>; // Adjusted service name

    beforeEach(waitForAsync(() => {
        mockEventService = jasmine.createSpyObj<EventService>('EventService', ['getEvent', 'deleteEvent'] as any); // Adjusted service name and methods

        TestBed.configureTestingModule({
            imports: [RouterTestingModule, HttpClientModule, FormsModule, HttpClientTestingModule],
            declarations: [DeleteConfirmComponent],
            providers: [
                { provide: EventService, useValue: mockEventService } // Adjusted service name
            ]
        }).compileComponents();
        router = TestBed.inject(Router);
        activatedRoute = TestBed.inject(ActivatedRoute);
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(DeleteConfirmComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    fit('should_create_DeleteConfirmComponent', () => {
        expect(component).toBeTruthy();
    });

    fit('DeleteConfirmComponent_should_call_deleteEvent_method_when_confirmDelete_is_called', () => {
        const eventId = 1; // Adjusted ID name
        
        mockEventService.deleteEvent.and.returnValue(of(null)); // Adjusted method name

        component.confirmDelete(eventId); // Adjusted parameter name

        expect(mockEventService.deleteEvent).toHaveBeenCalledWith(eventId); // Adjusted method name and parameter
    });
});

