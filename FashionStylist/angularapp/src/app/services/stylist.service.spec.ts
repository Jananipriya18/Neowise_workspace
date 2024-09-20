import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { StylistService } from './stylist.service';
import { Stylist } from '../model/stylist.model'; // Ensure this import is correct

describe('StylistService', () => {
  let service: StylistService;
  let httpTestingController: HttpTestingController;

  const mockStylists: Stylist[] = [
    {
      id: 1,
      name: 'Jane Doe',
      expertise: 'Fashion Consulting',
      styleSignature: 'Chic and Elegant',
      availability: 'Full-time',
      hourlyRate: 100,
      location: 'New York'
    },
    {
      id: 2,
      name: 'Henry Cloe',
      expertise: 'Fashion Consulting',
      styleSignature: 'Chic and Elegant',
      availability: 'Full-time',
      hourlyRate: 100,
      location: 'New York'
    },
  ];

  let backendUrl: string;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [StylistService],
    });
    service = TestBed.inject(StylistService);
    httpTestingController = TestBed.inject(HttpTestingController);

    // Access the backendUrl from the service
    backendUrl = service.backendUrl;
  });

  afterEach(() => {
    // Ensure that there are no outstanding requests after each test
    httpTestingController.verify();
  });

  fit('should_create_the_service', () => {
    expect(service).toBeTruthy();
  });

  fit('should_retrieve_stylists_from_the_API_via_GET', () => {
    service.getStylists().subscribe((stylists) => {
      expect(stylists).toEqual(mockStylists);
    });
    const req = httpTestingController.expectOne(backendUrl);
    expect(req.request.method).toEqual('GET');
    req.flush(mockStylists);
  });

  fit('should_add_a_stylist_via_POST', () => {
    const newStylist: Stylist = {
      name: 'Jane Doe',
      expertise: 'Fashion Consulting',
      styleSignature: 'Chic and Elegant',
      availability: 'Full-time',
      hourlyRate: 100,
      location: 'New York'
    };
    service.addStylist(newStylist).subscribe((stylist) => {
      expect(stylist).toEqual(newStylist);
    });
    const req = httpTestingController.expectOne(backendUrl);
    expect(req.request.method).toEqual('POST');
    req.flush(newStylist);
  });

  fit('should_delete_a_stylist_via_DELETE', () => {
    const stylistId = 1;
    service.deleteStylist(stylistId).subscribe(() => {
      // No content to assert, but we can ensure the request was made
    });
    const req = httpTestingController.expectOne(`${backendUrl}/${stylistId}`);
    expect(req.request.method).toEqual('DELETE');
    req.flush({});
  });
});
