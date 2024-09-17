import {
  ComponentFixture,
  TestBed,
  fakeAsync,
  tick,
} from '@angular/core/testing';
import {
  FormBuilder,
  ReactiveFormsModule,FormsModule,
  Validators,
} from '@angular/forms';
import { AddDoctorComponent } from './add-doctor.component';
import { DoctorService } from '../services/podcast.service';
import { of } from 'rxjs';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { DebugElement } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';
import { Router } from '@angular/router';

describe('AddDoctorComponent', () => {
  let component: AddDoctorComponent;
  let fixture: ComponentFixture<AddDoctorComponent>;
  let service: DoctorService;
  let debugElement: DebugElement;
  let formBuilder: FormBuilder;
  let router: Router;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddDoctorComponent],
      imports: [ReactiveFormsModule, HttpClientTestingModule, RouterTestingModule,FormsModule],
      providers: [DoctorService],
    });
    formBuilder = TestBed.inject(FormBuilder) as any;
    fixture = TestBed.createComponent(AddDoctorComponent) as any;
    component = fixture.componentInstance as any;
    service = TestBed.inject(DoctorService) as any;
    fixture.detectChanges();
    router = TestBed.inject(Router);
    spyOn(router, 'navigateByUrl').and.returnValue(Promise.resolve(true));
  });

  fit('should_create_AddDoctorComponent', () => {
    expect(component).toBeTruthy();
  });

  fit('should_add_a_new_doctor_when_form_is_valid', fakeAsync(() => {
    const validDoctorData = {
      name: 'Dr. John Doe',
      age: 45,
      specialization: 'Cardiology',
      contactNumber: '1234567890',
      department: 'Cardiology',
    };
    spyOn((service as any), 'addDoctor').and.returnValue(of(validDoctorData));
    (component as any).doctorForm.setValue(validDoctorData); 
    let value: boolean = (component as any).doctorForm.valid;
    (component as any).addDoctor();
    tick();
    expect(value).toBeTruthy();
    expect((service as any).addDoctor).toHaveBeenCalledWith(validDoctorData);  
  }));

  fit('should_add_all_the_required_fields', () => {
    const form = (component as any).doctorForm;
    form.setValue({
      name: '',
      age: '',
      specialization: '',
      contactNumber: '',
      department: '',
    });

    expect(form.valid).toBeFalsy();
    expect(form.get('name')?.hasError('required')).toBeTruthy();
    expect(form.get('age')?.hasError('required')).toBeTruthy();
    expect(form.get('specialization')?.hasError('required')).toBeTruthy();
    expect(form.get('contactNumber')?.hasError('required')).toBeTruthy();
    expect(form.get('department')?.hasError('required')).toBeTruthy();
  });

  fit('should_validate_contact_number', () => {
    const doctorForm = (component as any).doctorForm;
    doctorForm.setValue({
      name: 'Dr. John Doe',
      age: 45,
      specialization: 'Cardiology',
      contactNumber: '1234567890',
      department: 'Cardiology',
    });
    expect(doctorForm.valid).toBeTruthy();
    expect(doctorForm.get('contactNumber')?.hasError('pattern')).toBeFalsy();
    doctorForm.setValue({
      name: 'Dr. John Doe',
      age: 45,
      specialization: 'Cardiology',
      contactNumber: '12345',
      department: 'Cardiology',
    });
    expect(doctorForm.valid).toBeFalsy();
    expect(doctorForm.get('contactNumber')?.hasError('pattern')).toBeTruthy();
  });

});
