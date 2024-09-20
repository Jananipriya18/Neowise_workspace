import {
  ComponentFixture,
  TestBed,
  fakeAsync,
  tick,
} from '@angular/core/testing';
import {
  FormBuilder,
  ReactiveFormsModule,
  FormsModule,
  Validators,
} from '@angular/forms';
import { AddStylistComponent } from './add-stylist.component';
import { StylistService } from '../services/stylist.service';
import { of } from 'rxjs';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { DebugElement } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';
import { Router } from '@angular/router';

describe('AddStylistComponent', () => {
  let component: AddStylistComponent;
  let fixture: ComponentFixture<AddStylistComponent>;
  let service: StylistService;
  let debugElement: DebugElement;
  let formBuilder: FormBuilder;
  let router: Router;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddStylistComponent],
      imports: [ReactiveFormsModule, HttpClientTestingModule, RouterTestingModule, FormsModule],
      providers: [StylistService],
    });
    formBuilder = TestBed.inject(FormBuilder) as any;
    fixture = TestBed.createComponent(AddStylistComponent) as any;
    component = fixture.componentInstance as any;
    service = TestBed.inject(StylistService) as any;
    fixture.detectChanges();
    router = TestBed.inject(Router);
    spyOn(router, 'navigateByUrl').and.returnValue(Promise.resolve(true));
  });

  fit('should_create_AddStylistComponent', () => {
    expect(component).toBeTruthy();
  });

  fit('should_add_a_new_stylist_when_form_is_valid', fakeAsync(() => {
    const validStylistData = {
      name: 'Jane Doe',
      expertise: 'Fashion Consulting',
      styleSignature: 'Chic and Elegant',
      availability: 'Full-time',
      hourlyRate: 100,
      location: 'New York',
    };
    spyOn((service as any), 'addStylist').and.returnValue(of(validStylistData));
    (component as any).stylistForm.setValue(validStylistData); 
    let value: boolean = (component as any).stylistForm.valid;
    (component as any).addStylist();
    tick();
    expect(value).toBeTruthy();
    expect((service as any).addStylist).toHaveBeenCalledWith(validStylistData);  
  }));

  fit('should_add_all_the_required_fields', () => {
    const form = (component as any).stylistForm;
    form.setValue({
      name: '',
      expertise: '',
      styleSignature: '',
      availability: '',
      hourlyRate: '',
      location: '',
    });

    expect(form.valid).toBeFalsy();
    expect(form.get('name')?.hasError('required')).toBeTruthy();
    expect(form.get('expertise')?.hasError('required')).toBeTruthy();
    expect(form.get('styleSignature')?.hasError('required')).toBeTruthy();
    expect(form.get('availability')?.hasError('required')).toBeTruthy();
    expect(form.get('hourlyRate')?.hasError('required')).toBeTruthy();
    expect(form.get('location')?.hasError('required')).toBeTruthy();
  });

  fit('should_validate_hourly_rate', () => {
    const stylistForm = (component as any).stylistForm;
    stylistForm.setValue({
      name: 'Jane Doe',
      expertise: 'Fashion Consulting',
      styleSignature: 'Chic and Elegant',
      availability: 'Full-time',
      hourlyRate: 100,
      location: 'New York',
    });
    expect(stylistForm.valid).toBeTruthy();
    expect(stylistForm.get('hourlyRate')?.hasError('min')).toBeFalsy();
    stylistForm.setValue({
      name: 'Jane Doe',
      expertise: 'Fashion Consulting',
      styleSignature: 'Chic and Elegant',
      availability: 'Full-time',
      hourlyRate: -10, // Invalid value
      location: 'New York',
    });
    expect(stylistForm.valid).toBeFalsy();
    expect(stylistForm.get('hourlyRate')?.hasError('min')).toBeTruthy();
  });
});
