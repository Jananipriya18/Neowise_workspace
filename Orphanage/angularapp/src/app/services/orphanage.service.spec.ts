import { TestBed } from '@angular/core/testing';

import { OrphanageService } from './orphanage.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('OrphanageService', () => {
  let service: OrphanageService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],

    });
    service = TestBed.inject(OrphanageService);
  });


  fit('Frontend_should_create_orphanage_service', () => {
    expect(service).toBeTruthy();
  });
});
