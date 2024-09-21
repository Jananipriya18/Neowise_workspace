import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { GardenerService } from './gardener.service';

describe('GardenerService', () => {
  let service: GardenerService;
  let httpTestingController: HttpTestingController;

  const mockGardeners = [
    {
      id: 1,
      name: 'John Doe',
      age: 30,
      task: 'Planting',
      description: 'Planting new flowers',
      durationInMinutes: 60
    },
    {
      id: 2,
      name: 'Jane Smith',
      age: 28,
      task: 'Watering',
      description: 'Watering the garden',
      durationInMinutes: 45
    },
  ];

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [GardenerService],
    });
    service = TestBed.inject(GardenerService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    // Ensure that there are no outstanding requests after each test
    httpTestingController.verify();
  });

  fit('should_create_service_gardener', () => {
    expect(service).toBeTruthy();
  });

  fit('should_retrieve_gardeners_from_the_API_via_GET', () => {
    (service as any).getGardeners().subscribe((gardeners) => {
      expect(gardeners).toEqual(mockGardeners);
    });
    const req = httpTestingController.expectOne(`${service['backendUrl']}`);
    expect(req.request.method).toEqual('GET');
    req.flush(mockGardeners);
  });

  fit('should_add_a_gardener_via_POST', () => {
    const newGardener = {
      name: 'Alice Johnson',
      age: 35,
      task: 'Trimming',
      description: 'Trimming hedges',
      durationInMinutes: 30
    };
    (service as any).addGardener(newGardener).subscribe((gardener) => {
      expect(gardener).toEqual(newGardener);
    });
    const req = httpTestingController.expectOne(`${service['backendUrl']}`);
    expect(req.request.method).toEqual('POST');
    req.flush(newGardener);
  });

  fit('should_delete_a_gardener_via_DELETE', () => {
    const gardenerId = 1;
    (service as any).deleteGardener(gardenerId).subscribe(() => {
    });
    const req = httpTestingController.expectOne(`${service['backendUrl']}/${gardenerId}`);
    expect(req.request.method).toEqual('DELETE');
    req.flush({});
  });
});
