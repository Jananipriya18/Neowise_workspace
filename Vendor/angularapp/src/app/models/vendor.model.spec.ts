import { Vendor } from './vendor.model'; 

describe('Vendor', () => { 
  fit('should_create_vendor_instance', () => { 
    const vendor: Vendor = { 
      vendorId: 1, 
      name: 'Test Vendor Name', 
      productOfferings: 'Test Product Offerings',
      experience: '5 years',
      storeLocation: 'Test Store Location',
      operatingHours: '9 AM - 5 PM', 
      phoneNumber: '1234567890' 
    }as any;

    // Check if the vendor object exists
    expect(vendor).toBeTruthy();

    // Check individual properties of the vendor
    expect(vendor.vendorId).toBe(1); 
    expect(vendor.name).toBe('Test Vendor Name'); 
    expect(vendor.productOfferings).toBe('Test Product Offerings'); 
    expect(vendor.experience).toBe('5 years'); 
    expect(vendor.storeLocation).toBe('Test Store Location'); 
    expect(vendor.operatingHours).toBe('9 AM - 5 PM'); 
    expect(vendor.phoneNumber).toBe('1234567890'); 
  });
});