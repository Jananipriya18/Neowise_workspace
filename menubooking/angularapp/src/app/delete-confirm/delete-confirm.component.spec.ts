import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { DeleteConfirmComponent } from './delete-confirm.component';
import { RouterTestingModule } from '@angular/router/testing';
import { ActivatedRoute, Router } from '@angular/router';
import { of } from 'rxjs';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TutorService } from '../services/tutor.service';
import { Tutor } from '../models/menu.model'

describe('DeleteConfirmComponent', () => {
    let component: DeleteConfirmComponent;
    let fixture: ComponentFixture<DeleteConfirmComponent>;
    let router: Router;
    let activatedRoute: ActivatedRoute;
    let mockTutorService: jasmine.SpyObj<TutorService>; // Declare mockTutorService

    beforeEach(waitForAsync(() => {
        // Create a spy object with the methods you want to mock
        mockTutorService = jasmine.createSpyObj<TutorService>('TutorService', ['getTutor', 'deleteTutor'] as any);

        TestBed.configureTestingModule({
            imports: [RouterTestingModule, HttpClientModule, FormsModule, HttpClientTestingModule], // Add HttpClientModule and HttpClientTestingModule to imports
            declarations: [DeleteConfirmComponent],
            providers: [
                // Provide the mock service instead of the actual service
                { provide: TutorService, useValue: mockTutorService }
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

    // fit('DeleteConfirmComponent_should_navigate_to_viewTutors_after_cancelDelete', () => {
    //     spyOn(router, 'navigate').and.stub(); // Spy on router.navigate method
    //     component.cancelDelete();
    //     expect(router.navigate).toHaveBeenCalledWith(['/viewTutors']); // Verify router.navigate is called with correct argument
    // });

    fit('DeleteConfirmComponent_should_call_deleteTutor_method_when_confirmDelete_is_called', () => {
        const tutorId = 1;
        
        // Spy on the deleteTutor method of the TutorService
        mockTutorService.deleteTutor.and.returnValue(of(null));

        // Call the confirmDelete method
        component.confirmDelete(tutorId);

        // Expect the deleteTutor method to have been called with the tutorId
        expect(mockTutorService.deleteTutor).toHaveBeenCalledWith(tutorId);
    });
});

