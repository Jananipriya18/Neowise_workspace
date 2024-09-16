import { ComponentFixture, TestBed } from '@angular/core/testing';
import { EditDoctorComponent } from './edit-doctor.component';
import { DoctorService } from '../services/doctor.service';
import { of, throwError } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Doctor } from '../model/stylist.model';

describe('EditDoctorComponent', () => {
  let component: EditDoctorComponent;
  let fixture: ComponentFixture<EditDoctorComponent>;
  let service: DoctorService;
  let routerSpy: jasmine.SpyObj<Router>;

  const mockDoctor: Doctor = {
    id: 1,
    name: 'Dr. John Smith',
    age: 45,
    specialization: 'Cardiology',
    department: 'Cardiology',
    contactNumber: '1234567890'
  };

  beforeEach(() => {
    const spy = jasmine.createSpyObj('Router', ['navigate']);

    TestBed.configureTestingModule({
      declarations: [EditDoctorComponent],
      imports: [HttpClientTestingModule, RouterTestingModule, FormsModule],
      providers: [
        DoctorService,
        { provide: ActivatedRoute, useValue: { snapshot: { paramMap: { get: () => '1' } } } },
        { provide: Router, useValue: spy }
      ]
    });

    fixture = TestBed.createComponent(EditDoctorComponent);
    component = fixture.componentInstance;
    service = TestBed.inject(DoctorService);
    routerSpy = TestBed.inject(Router) as jasmine.SpyObj<Router>;
  });

  fit('should_create_EditDoctorComponent', () => {
    expect((component as any)).toBeTruthy();
  });

  fit('should_load_doctor_details_on_init', () => {
    spyOn((service as any), 'getDoctorById').and.returnValue(of(mockDoctor));
    (component as any).ngOnInit();
    fixture.detectChanges();

    expect((service as any).getDoctorById).toHaveBeenCalledWith(1);
    expect((component as any).doctor).toEqual(mockDoctor);
  });

  fit('should_save_doctor_details', () => {
    spyOn((service as any), 'updateDoctor').and.returnValue(of(mockDoctor));
    (component as any).doctor = { ...mockDoctor };
    (component as any).saveDoctor();
    
    expect((service as any).updateDoctor).toHaveBeenCalledWith(mockDoctor.id, mockDoctor);
  });

});
