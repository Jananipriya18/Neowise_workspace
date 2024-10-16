import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { CheeseShopService } from './cheese-shop.service'; // Adjusted service import
import { CheeseShop } from '../models/cheese-shop';

describe('CheeseShopService', () => {
  let service: CheeseShopService;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CheeseShopService], // Changed service provider to CheeseShopService
    });
    service = TestBed.inject(CheeseShopService); // Changed service variable assignment to CheeseShopService
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  fit('CheeseShopService_should_be_created', () => {
    expect(service).toBeTruthy();
  });

  fit('CheeseShopService_should_add_a_shop_and_return_it', () => {
    const mockCheeseShop: CheeseShop = {
      shopId: 100,
      ownerName: 'Test Owner Name',
      cheeseSpecialties: 'Test Cheese Specialties',
      experienceYears: 10,
      storeLocation: 'Test Store Location',
      importedCountry: 'Test Imported Country',
      phoneNumber: 'Test Phone Number',
    };

    service.addCheeseShop(mockCheeseShop).subscribe((shop) => {
      expect(shop).toEqual(mockCheeseShop);
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}/api/CheeseShop`); // Adjusted API endpoint
    expect(req.request.method).toBe('POST');
    req.flush(mockCheeseShop);
  });

  fit('CheeseShopService_should_get_shops', () => {
    const mockCheeseShops: CheeseShop[] = [
      {
        shopId: 100,
        ownerName: 'Test Owner Name',
        cheeseSpecialties: 'Test Cheese Specialties',
        experienceYears: 10,
        storeLocation: 'Test Store Location',
        importedCountry: 'Test Imported Country',
        phoneNumber: 'Test Phone Number',
      }
    ];

    service.getCheeseShops().subscribe((shops) => {
      expect(shops).toEqual(mockCheeseShops);
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}/api/CheeseShop`); // Adjusted API endpoint
    expect(req.request.method).toBe('GET');
    req.flush(mockCheeseShops);
  });

  fit('CheeseShopService_should_delete_shop', () => {
    const shopId = 100;

    service.deleteCheeseShop(shopId).subscribe(() => {
      expect().nothing();
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}/api/CheeseShop/${shopId}`); // Adjusted API endpoint
    expect(req.request.method).toBe('DELETE');
    req.flush({});
  });

  fit('CheeseShopService_should_get_shop_by_id', () => {
    const shopId = 100;
    const mockCheeseShop: CheeseShop = {
      shopId: shopId,
      ownerName: 'Test Owner Name',
      cheeseSpecialties: 'Test Cheese Specialties',
      experienceYears: 10,
      storeLocation: 'Test Store Location',
      importedCountry: 'Test Imported Country',
      phoneNumber: 'Test Phone Number',
    };

    service.getCheeseShop(shopId).subscribe((shop) => {
      expect(shop).toEqual(mockCheeseShop);
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}/api/CheeseShop/${shopId}`); // Adjusted API endpoint
    expect(req.request.method).toBe('GET');
    req.flush(mockCheeseShop);
  });

  fit('CheeseShopService_should_search_cheeseShops', () => {
    const mockCheeseShops: CheeseShop[] = [
      {
        shopId: 100,
        ownerName: 'Test Owner Name',
        cheeseSpecialties: 'Test Cheese Specialties',
        experienceYears: 10,
        storeLocation: 'Test Store Location',
        importedCountry: 'Test Imported Country',
        phoneNumber: 'Test Phone Number',
      }
    ];
  
    const searchTerm = 'e';
  
    service.searchCheeseShops(searchTerm).subscribe((shops) => {
      expect(shops).toEqual(mockCheeseShops);
    });
  
    const req = httpTestingController.expectOne((request) => 
      request.url.includes(`${service['apiUrl']}/api/CheeseShop/search`) && 
      request.params.get('searchTerm') === searchTerm
    );
  
    expect(req.request.method).toBe('GET');
    req.flush(mockCheeseShops);
  }); 
  
});
