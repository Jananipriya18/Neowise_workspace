import { CheeseShop } from './cheese-shop';

describe('CheeseShop', () => {
  fit('should create cheese shop instance', () => {
    const shop: CheeseShop = {
      shopId: 1,
      ownerName: 'Test Owner Name',
      cheeseSpecialties: 'Test Cheese Specialties',
      experienceYears: 10,
      storeLocation: 'Test Store Location',
      importedCountry: 'Test Imported Country',
      phoneNumber: 'Test Phone Number'
    };

    // Check if the shop object exists
    expect(shop).toBeTruthy();

    // Check individual properties of the shop
    expect(shop.shopId).toBe(1);
    expect(shop.ownerName).toBe('Test Owner Name');
    expect(shop.cheeseSpecialties).toBe('Test Cheese Specialties');
    expect(shop.experienceYears).toBe(10);
    expect(shop.storeLocation).toBe('Test Store Location');
    expect(shop.importedCountry).toBe('Test Imported Country');
    expect(shop.phoneNumber).toBe('Test Phone Number');
  });
});
