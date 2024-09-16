import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { DoctorService } from './stylist.service';
import { Doctor } from '../model/doctor.model'; // Ensure this import is correct

describe('DoctorService', () => {
  let service: DoctorService;
  let httpTestingController: HttpTestingController;

  const mockDoctors: Doctor[] = [
    {
      id: 1,
      name: 'Dr. John Smith',
      age: 45,
      specialization: 'Cardiology',
      department: 'Cardiology',
      contactNumber: '1234567890'
    },
    {
      id: 2,
      name: 'Dr. Jane Doe',
      age: 38,
      specialization: 'Neurology',
      department: 'Neurology',
      contactNumber: '9876543210'
    },
  ];

  const backendUrl = 'api/doctors'; // Adjust based on your actual URL

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [DoctorService],
    });
    service = TestBed.inject(DoctorService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    // Ensure that there are no outstanding requests after each test
    httpTestingController.verify();
  });

  fit('should create the service', () => {
    expect(service).toBeTruthy();
  });

  fit('should retrieve doctors from the API via GET', () => {
    service.getDoctors().subscribe((doctors) => {
      expect(doctors).toEqual(mockDoctors);
    });
    const req = httpTestingController.expectOne(backendUrl);
    expect(req.request.method).toEqual('GET');
    req.flush(mockDoctors);
  });

  fit('should add a doctor via POST', () => {
    const newDoctor: Doctor = {
      name: 'Dr. Jane Doe',
      age: 38,
      specialization: 'Neurology',
      department: 'Neurology',
      contactNumber: '9876543210'
    };
    service.addDoctor(newDoctor).subscribe((doctor) => {
      expect(doctor).toEqual(newDoctor);
    });
    const req = httpTestingController.expectOne(backendUrl);
    expect(req.request.method).toEqual('POST');
    req.flush(newDoctor);
  });

  fit('should update a doctor via PUT', () => {
    const updatedDoctor: Doctor = {
      id: 1,
      name: 'Dr. John Smith',
      age: 46,
      specialization: 'Cardiology',
      department: 'Cardiology',
      contactNumber: '1234567890'
    };
    service.updateDoctor(updatedDoctor.id, updatedDoctor).subscribe((doctor) => {
      expect(doctor).toEqual(updatedDoctor);
    });
    const req = httpTestingController.expectOne(`${backendUrl}/${updatedDoctor.id}`);
    expect(req.request.method).toEqual('PUT');
    req.flush(updatedDoctor);
  });

  fit('should delete a doctor via DELETE', () => {
    const doctorId = 1;
    service.deleteDoctor(doctorId).subscribe(() => {
      // No content to assert, but we can ensure the request was made
    });
    const req = httpTestingController.expectOne(`${backendUrl}/${doctorId}`);
    expect(req.request.method).toEqual('DELETE');
    req.flush({});
  });
});
