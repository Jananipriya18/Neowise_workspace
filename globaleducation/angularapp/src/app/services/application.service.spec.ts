import { TestBed } from '@angular/core/testing';

import { ApplicationService } from './application.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('ApplicationService', () => {
  let service: ApplicationService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
    });
    service = TestBed.inject(ApplicationService);
  });

  fit('Frontend_should_create_application_service', () => {
    expect(service).toBeTruthy();
  });
});
