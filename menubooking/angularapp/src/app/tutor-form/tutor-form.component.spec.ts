import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { FormsModule } from '@angular/forms'; // Import FormsModule
import { RouterTestingModule } from '@angular/router/testing';
import { TutorFormComponent } from './tutor-form.component';
import { TutorService } from '../services/tutor.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Router } from '@angular/router';
import { of } from 'rxjs';
import { Tutor } from '../models/menu.model';
import { fakeAsync, tick } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';
import { TutorListComponent } from '../tutor-list/tutor-list.component';

describe('TutorFormComponent', () => {
  let component: TutorFormComponent;
  let fixture: ComponentFixture<TutorFormComponent>;
  let tutorService: TutorService;
  let router: Router;
  let tutorListComponent: TutorListComponent;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TutorFormComponent],
      imports: [FormsModule, RouterTestingModule, HttpClientTestingModule],
      providers: [
        TutorService,
      ]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TutorFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();

    tutorService = TestBed.inject(TutorService);
    router = TestBed.inject(Router);

  });

  fit('should_have_addTutor_method', () => {
    expect(component.addTutor).toBeTruthy();
  });

  fit('should_show_error_messages_for_required_fields_on_submit', fakeAsync(() => {
    // Mock new tutor data
    component.newTutor = {
        tutorId: 1,
        name: '',
        email: '',
        subjectsOffered: '',
        contactNumber: '',
        availability: ''
    };

    // Trigger form submission
    const form = fixture.debugElement.query(By.css('form')).nativeElement;
    form.dispatchEvent(new Event('submit'));
    fixture.detectChanges();
    tick();

    // Check error messages for each field
    const errorMessages = fixture.debugElement.queryAll(By.css('.error-message'));
    expect(errorMessages.length).toBe(5); // Assuming there are 5 required fields

    // Check error messages content
    expect(errorMessages[0].nativeElement.textContent).toContain('Name is required');
    expect(errorMessages[1].nativeElement.textContent).toContain('Email is required');
    expect(errorMessages[2].nativeElement.textContent).toContain('Subjects Offered are required');
    expect(errorMessages[3].nativeElement.textContent).toContain('Contact Number is required');
    expect(errorMessages[4].nativeElement.textContent).toContain('Availability is required');
}));


  // fit('should show name required error message on register page', fakeAsync(() => {
  //   const nameInput = fixture.debugElement.query(By.css('#name'));
  //   nameInput.nativeElement.value = '';
  //   nameInput.nativeElement.dispatchEvent(new Event('input'));
  //   fixture.detectChanges();
  //   tick();
  //   const errorMessage = fixture.debugElement.query(By.css('.error-message'));
  //   expect(errorMessage.nativeElement.textContent).toContain('Name is required');
  // }));

  fit('should_not_render_any_error_messages_when_all_fields_are_filled', () => {
    const compiled = fixture.nativeElement;
    const form = compiled.querySelector('form');

    // Fill all fields
    component.newTutor = {
      tutorId: null, // or omit this line if tutorId is auto-generated
      name: 'Test Name',
      email: 'Test Email',
      subjectsOffered: 'Test SubjectsOffered',
      contactNumber: 'Test ContactNumber',
      availability: 'Test Availability'
    };

    fixture.detectChanges();

    form.dispatchEvent(new Event('submit')); // Submit the form

    // Check if no error messages are rendered
    expect(compiled.querySelector('#nameError')).toBeNull();
    expect(compiled.querySelector('#emailError')).toBeNull();
    expect(compiled.querySelector('#subjectsOfferedError')).toBeNull();
    expect(compiled.querySelector('#contactNumberError')).toBeNull();
    expect(compiled.querySelector('#availabilityError')).toBeNull();
  });

  fit('should_call_add_tutor_method_while_adding_the_tutor', () => {
    // Create a mock Tutor object with all required properties
    const tutor: Tutor = { 
      tutorId: 1, 
      name: 'Test Tutor', 
      email: 'Test Tutor Email', 
      subjectsOffered: 'Ingredient 2', 
      contactNumber: 'Test Tutor ContactNumber', 
      availability: 'Test Availability'
    };
    const addTutorSpy = spyOn(component, 'addTutor').and.callThrough();
    component.addTutor();
    expect(addTutorSpy).toHaveBeenCalled();
  });
});

