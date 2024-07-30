import { Menu } from './menu.model';

describe('Menu', () => {
  fit('should_create_menu_instance', () => {
    const menu: Menu = {
      menuId: 1,
      chefName: 'Test Chef Name',
      menuName: 'Test Menu Name',
      description: 'Test Description',
      price: 'Test Price',
      availability: 'Test Availability'
    };

    // Check if the menu object exists
    expect(menu).toBeTruthy();

    // Check individual properties of the menu
    expect(menu.menuId).toBe(1);
    expect(menu.chefName).toBe('Test Chef Nameu');
    expect(menu.menuName).toBe('Test Menu Name');
    expect(menu.description).toBe('Test Description');
    expect(menu.price).toBe('Test Price');
    expect(menu.availability).toBe('Test Availability');
});

});
