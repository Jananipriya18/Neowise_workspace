import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { VendorService } from './vendor.service';
import { Vendor } from '../models/vendor.model';

describe('VendorService', () => {
  let service: VendorService;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [VendorService],
    });
    service = TestBed.inject(VendorService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  fit('VendorService_should_be_created', () => {
    expect((service as any)).toBeTruthy();
  });

  fit('VendorService_should_add_a_vendor_and_return_it', () => {
    const mockVendor: Vendor = {
      vendorId: 100,
      name: 'Test Vendor Name',
      productOfferings: 'Test Product Offerings',
      experience: '5 years',
      storeLocation: 'Test Store Location',
      operatingHours: '9 AM - 5 PM',
      phoneNumber: '1234567890'
    }as any;

    (service as any).addVendor(mockVendor).subscribe((vendor) => {
      expect(vendor).toEqual(mockVendor);
    });

    const req = httpTestingController.expectOne(`${(service as any)['apiUrl']}/api/Vendor`);
    expect(req.request.method).toBe('POST');
    req.flush(mockVendor);
  });

  fit('VendorService_should_get_vendors', () => {
    const mockVendors: Vendor[] = [
      {
        vendorId: 100,
        name: 'Test Vendor Name',
        productOfferings: 'Test Product Offerings',
        experience: '5 years',
        storeLocation: 'Test Store Location',
        operatingHours: '9 AM - 5 PM',
        phoneNumber: '1234567890'
      }
    ]as any;

    (service as any).getVendors().subscribe((vendors) => {
      expect(vendors).toEqual(mockVendors);
    });

    const req = httpTestingController.expectOne(`${(service as any)['apiUrl']}/api/Vendor`);
    expect(req.request.method).toBe('GET');
    req.flush(mockVendors);
  });

  fit('VendorService_should_delete_vendor', () => {
    const vendorId = 100;

    (service as any).deleteVendor(vendorId).subscribe(() => {
      expect().nothing();
    });

    const req = httpTestingController.expectOne(`${(service as any)['apiUrl']}/api/Vendor/${vendorId}`);
    expect(req.request.method).toBe('DELETE');
    req.flush({});
  });

  fit('VendorService_should_get_vendor_by_id', () => {
    const vendorId = 100;
    const mockVendor: Vendor = {
      vendorId: vendorId,
      name: 'Test Vendor Name',
      productOfferings: 'Test Product Offerings',
      experience: '5 years',
      storeLocation: 'Test Store Location',
      operatingHours: '9 AM - 5 PM',
      phoneNumber: '1234567890'
    }as any;

    (service as any).getVendor(vendorId).subscribe((vendor) => {
      expect(vendor).toEqual(mockVendor);
    });

    const req = httpTestingController.expectOne(`${(service as any)['apiUrl']}/api/Vendor/${vendorId}`);
    expect(req.request.method).toBe('GET');
    req.flush(mockVendor);
  });

  fit('VendorService_should_search_vendors', () => {
    const mockVendors: Vendor[] = [
      {
        vendorId: 100,
        name: 'Test Vendor Name',
        productOfferings: 'Test Product Offerings',
        experience: '5 years',
        storeLocation: 'Test Store Location',
        operatingHours: '9 AM - 5 PM',
        phoneNumber: '1234567890'
      }
    ]as any;
  
    const searchTerm = 'Test Vendor Name';
  
    (service as any).searchVendors(searchTerm).subscribe((vendors) => {
      expect(vendors).toEqual(mockVendors);
    });
  
    const req = httpTestingController.expectOne((request) => 
      request.url.includes(`${(service as any)['apiUrl']}/api/Vendor/search`) && 
      request.params.get('searchTerm') === searchTerm
    );
  
    expect(req.request.method).toBe('GET');
    req.flush(mockVendors);
  }); 
});