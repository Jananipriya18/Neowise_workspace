import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { FormBuilder, ReactiveFormsModule, FormsModule, Validators } from '@angular/forms';
import { AddGardenerComponent } from './add-gardener.component'; // Adjust the import
import { GardenerService } from '../services/gardener.service'; // Adjust the service import
import { of } from 'rxjs';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { Router } from '@angular/router';

describe('AddGardenerComponent', () => {
  let component: AddGardenerComponent;
  let fixture: ComponentFixture<AddGardenerComponent>;
  let service: GardenerService;
  let router: Router;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddGardenerComponent],
      imports: [ReactiveFormsModule, HttpClientTestingModule, RouterTestingModule, FormsModule],
      providers: [GardenerService],
    });
    fixture = TestBed.createComponent(AddGardenerComponent);
    component = fixture.componentInstance;
    service = TestBed.inject(GardenerService);
    router = TestBed.inject(Router);
    spyOn(router, 'navigateByUrl').and.returnValue(Promise.resolve(true));
    fixture.detectChanges();
  });

  fit('should_create_AddGardenerComponent', () => {
    expect(component).toBeTruthy();
  });

  fit('should_add_a_new_gardener_when_form_is_valid', fakeAsync(() => {
    const validGardenerData = {
      name: 'Jane Doe',
      age: 35,
      task: 'Weeding',
      description: 'Weeding the garden',
      durationInMinutes: 120,
    };
    spyOn(service, 'addGardener').and.returnValue(of(validGardenerData));
    component.gardenerForm.setValue(validGardenerData);
    let value: boolean = component.gardenerForm.valid;
    component.addGardener();
    tick();
    expect(value).toBeTruthy();
    expect(service.addGardener).toHaveBeenCalledWith(validGardenerData);
  }));

  fit('should_add_all_the_required_fields', () => {
    const form = component.gardenerForm;
    form.setValue({
      name: '',
      age: '',
      task: '',
      description: '',
      durationInMinutes: '',
    });

    expect(form.valid).toBeFalsy();
    expect(form.get('name')?.hasError('required')).toBeTruthy();
    expect(form.get('age')?.hasError('required')).toBeTruthy();
    expect(form.get('task')?.hasError('required')).toBeTruthy();
    expect(form.get('description')?.hasError('required')).toBeTruthy();
    expect(form.get('durationInMinutes')?.hasError('required')).toBeTruthy();
  });

  fit('should_validate_duration', () => {
    const gardenerForm = component.gardenerForm;
    gardenerForm.setValue({
      name: 'Jane Doe',
      age: 35,
      task: 'Weeding',
      description: 'Weeding the garden',
      durationInMinutes: 120,
    });
    expect(gardenerForm.valid).toBeTruthy();

    gardenerForm.setValue({
      name: 'Jane Doe',
      age: 35,
      task: 'Weeding',
      description: 'Weeding the garden',
      durationInMinutes: -30, // Invalid duration
    });
    expect(gardenerForm.valid).toBeFalsy();
    expect(gardenerForm.get('durationInMinutes')?.hasError('min')).toBeTruthy(); // Assuming min is set to 0 in the form
  });

});
