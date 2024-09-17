import { ComponentFixture, TestBed } from '@angular/core/testing';
import { DoctorListComponent } from './doctor-list.component';
import { DoctorService } from '../services/comic.service';
import { of } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Router } from '@angular/router';
import { Doctor } from '../model/comic.model';

describe('DoctorListComponent', () => {
  let component: DoctorListComponent;
  let fixture: ComponentFixture<DoctorListComponent>;
  let service: DoctorService;
  let routerSpy: jasmine.SpyObj<Router>;

  const mockDoctors: Doctor[] = [
    { id: 1, name: 'Dr. John Smith', age: 45, specialization: 'Cardiology', department: 'Cardiology', contactNumber: '1234567890' }
  ];

  beforeEach(() => {
    const spy = jasmine.createSpyObj('Router', ['navigate']);

    TestBed.configureTestingModule({
      declarations: [DoctorListComponent],
      imports: [HttpClientTestingModule, RouterTestingModule],
      providers: [DoctorService, { provide: Router, useValue: spy }]
    });

    fixture = TestBed.createComponent(DoctorListComponent);
    component = fixture.componentInstance;
    service = TestBed.inject(DoctorService);
    routerSpy = TestBed.inject(Router) as jasmine.SpyObj<Router>;
  });

  fit('should_create_DoctorListComponent', () => {
    expect(component).toBeTruthy();
  });

  fit('should_call_getDoctors', () => {
    spyOn((service as any), 'getDoctors').and.returnValue(of(mockDoctors));
    (component as any).ngOnInit();
    expect((service as any).getDoctors).toHaveBeenCalled();
    expect((component as any).doctors).toEqual(mockDoctors);
  });

  fit('should_call_deleteDoctor', () => {
    spyOn((service as any), 'deleteDoctor').and.returnValue(of());
    (component as any).deleteDoctor(1);
    expect((service as any).deleteDoctor).toHaveBeenCalledWith(1);
  });

});
