import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';

import { Tutor } from '../models/tutor.model';
import { TutorService } from './tutor.service';

describe('TutorService', () => {
  let service: TutorService;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [TutorService],
    });
    service = TestBed.inject(TutorService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  fit('TutorService_should_be_created', () => {
    expect(service).toBeTruthy();
  });

  fit('TutorService_should_add_a_tutor_and_return_it', () => {
    const mockTutor: Tutor = {
      tutorId: 100,
      name: 'Test Tutor',
      email: 'Test Email',
      subjectsOffered: 'Test SubjectsOffered',
      contactNumber: 'Test ContactNumber',
      availability: 'Test Availability'
    };

    service.addTutor(mockTutor).subscribe((tutor) => {
      expect(tutor).toEqual(mockTutor);
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}api/Tutoring`);
    expect(req.request.method).toBe('POST');
    req.flush(mockTutor);
  });

  fit('TutorService_should_get_tutors', () => {
    const mockTutors: Tutor[] = [
      {
        tutorId: 100,
        name: 'Test Tutor 1',
        email: 'Test Email',
        subjectsOffered: 'Test SubjectsOffered',
        contactNumber: 'Test ContactNumber',
        availability: 'Test Availability'
      }
    ];

    service.getTutors().subscribe((Tutors) => {
      expect(Tutors).toEqual(mockTutors);
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}api/Tutoring`);
    expect(req.request.method).toBe('GET');
    req.flush(mockTutors);
  });

  fit('TutorService_should_delete_Tutor', () => {
    const tutorId = 100;

    service.deleteTutor(tutorId).subscribe(() => {
      expect().nothing();
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}api/Tutoring/${tutorId}`);
    expect(req.request.method).toBe('DELETE');
    req.flush({});
  });

  fit('TutorService_should_get_Tutor_by_id', () => {
    const tutorId = 100;
    const mockTutor: Tutor = {
      tutorId: tutorId,
      name: 'Test Tutor',
      email: 'Test Email',
      subjectsOffered: 'Test SubjectsOffered',
      contactNumber: 'Test ContactNumber',
      availability: 'Test Availability'
    };

    service.getTutor(tutorId).subscribe((tutor) => {
      expect(tutor).toEqual(mockTutor);
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}api/Tutoring/${tutorId}`);
    expect(req.request.method).toBe('GET');
    req.flush(mockTutor);
  });
});
