import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { TutorService } from '../services/tutor.service';
import { TutorListComponent } from './tutor-list.component';
import { of } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Tutor } from '../models/tutor.model';

describe('TutorListComponent', () => {
    let component: TutorListComponent;
    let fixture: ComponentFixture<TutorListComponent>;
    let mockTutorService: jasmine.SpyObj<TutorService>; // Specify the type of mock

    beforeEach(waitForAsync(() => {
        // Create a spy object with the methods you want to mock
        mockTutorService = jasmine.createSpyObj<TutorService>('TutorService', ['getTutors', 'addTutor']);

        TestBed.configureTestingModule({
            declarations: [TutorListComponent],
            imports: [RouterTestingModule, HttpClientTestingModule],
            providers: [
                // Provide the mock service instead of the actual service
                { provide: TutorService, useValue: mockTutorService }
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(TutorListComponent);
        component = fixture.componentInstance;
    });

    fit('should_create_tutor_listComponent', () => {
        expect(component).toBeTruthy();
    });

    fit('tutor_listComponent_should_call_loadTutors_on_ngOnInit', () => {
        spyOn(component, 'loadTutors');
        fixture.detectChanges();
        expect(component.loadTutors).toHaveBeenCalled();
    });

    // fit('should_add_a_tutor_to_the_database', () => {
    //     const newTutor: Tutor = { tutorId: 1, name: 'New Tutor', email: 'Email', subjectsOffered: 'SubjectsOffered', contactNumber: 'ContactNumber', availability: 'Availability' };
    //     const initialDatabaseLength = 0; // Assuming there are initially 2 tutors in the database
    
    //     mockTutorService.addTutor.and.returnValue(of()); // Mock addTutor method to return a successful response
    //     mockTutorService.getTutors.and.returnValue(of([...component.tutors, newTutor])); // Mock getTutors to return the updated list with the new tutor
    
    //     component.tutors = []; // Clear tutors array for testing
    //     component.loadTutors(); // Load tutors
    
    //     expect(mockTutorService.addTutor).toHaveBeenCalledWith(newTutor); // Verify addTutor method is called with new tutor
    //     expect(component.tutors.length).toBe(initialDatabaseLength + 1); // Check if a tutor is added by checking the length
    // });

});
