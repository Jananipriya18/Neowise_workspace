import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { DoctorService } from './skill.service';

describe('DoctorService', () => {
  let service: DoctorService;
  let httpTestingController: HttpTestingController;

  const mockDoctors = [
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

  fit('should_create_service_doctor', () => {
    expect(service).toBeTruthy();
  });

  fit('should_retrieve_doctors_from_the_API_via_GET', () => {
    (service as any).getDoctors().subscribe((doctors) => {
      expect(doctors).toEqual(mockDoctors);
    });
    const req = httpTestingController.expectOne(`${service['backendUrl']}`);
    expect(req.request.method).toEqual('GET');
    req.flush(mockDoctors);
  });

  fit('should_add_a_doctor_via_POST', () => {
    const newDoctor = {
      name: 'Dr. Jane Doe',
      age: 38,
      specialization: 'Neurology',
      department: 'Neurology',
      contactNumber: '9876543210'
    };
    (service as any).addDoctor(newDoctor).subscribe((doctor) => {
      expect(doctor).toEqual(newDoctor);
    });
    const req = httpTestingController.expectOne(`${service['backendUrl']}`);
    expect(req.request.method).toEqual('POST');
    req.flush(newDoctor);
  });


  fit('should_update_a_doctor_via_PUT', () => {
    const updatedDoctor = {
      id: 1,
      name: 'Dr. John Smith',
      age: 46,
      specialization: 'Cardiology',
      department: 'Cardiology',
      contactNumber: '1234567890'
    };
    (service as any).updateDoctor(updatedDoctor.id, updatedDoctor).subscribe((doctor) => {
      expect(doctor).toEqual(updatedDoctor);
    });
    const req = httpTestingController.expectOne(`${service['backendUrl']}/${updatedDoctor.id}`);
    expect(req.request.method).toEqual('PUT');
    req.flush(updatedDoctor);
  });



  fit('should_delete_a_doctor_via_DELETE', () => {
    const doctorId = 1;
    (service as any).deleteDoctor(doctorId).subscribe(() => {
    });
    const req = httpTestingController.expectOne(`${service['backendUrl']}/${doctorId}`);
    expect(req.request.method).toEqual('DELETE');
    req.flush({});
  });
});
