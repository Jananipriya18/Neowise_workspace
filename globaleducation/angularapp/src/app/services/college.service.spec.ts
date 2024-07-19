import { TestBed } from '@angular/core/testing';

import { CollegeService } from './college.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('CollegeService', () => {
  let service: CollegeService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
    });
    service = TestBed.inject(CollegeService);
  });

  fit('Frontend_should_create_college_service', () => {
    expect(service).toBeTruthy();
  });
});
