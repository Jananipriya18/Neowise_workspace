import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';

import { Menu } from '../models/menu.model';
import { MenuService } from './menu.service';

describe('MenuService', () => {
  let service: MenuService;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [MenuService],
    });
    service = TestBed.inject(MenuService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  fit('MenuService_should_be_created', () => {
    expect(service).toBeTruthy();
  });

  fit('MenuService_should_add_a_menu_and_return_it', () => {
    const mockMenu: Menu = {
      menuId: 100,
      chefName: 'Test Chef Name',
      menuName: 'Test Menu Name',
      description: 'Test Description',
      price: 'Test Price',
      availability: 'Test Availability'
    };

    service.addMenu(mockMenu).subscribe((menu) => {
      expect(menu).toEqual(mockMenu);
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}api/MenuBooking`);
    expect(req.request.method).toBe('POST');
    req.flush(mockMenu);
  });

  fit('MenuService_should_get_menus', () => {
    const mockMenus: Menu[] = [
      {
        menuId: 100,
        chefName: 'Test Menu 1',
        menuName: 'Test Email',
        description: 'Test SubjectsOffered',
        price: 'Test ContactNumber',
        availability: 'Test Availability'
      }
    ];

    service.getMenus().subscribe((Menus) => {
      expect(Menus).toEqual(mockMenus);
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}api/MenuBooking`);
    expect(req.request.method).toBe('GET');
    req.flush(mockMenus);
  });

  fit('MenuService_should_delete_Menu', () => {
    const menuId = 100;

    service.deleteMenu(menuId).subscribe(() => {
      expect().nothing();
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}api/MenuBooking/${menuId}`);
    expect(req.request.method).toBe('DELETE');
    req.flush({});
  });

  fit('MenuService_should_get_Menu_by_id', () => {
    const menuId = 100;
    const mockMenu: Menu = {
      menuId: menuId,
      chefName: 'Test Menu',
      menuName: 'Test Email',
      description: 'Test SubjectsOffered',
      price: 'Test ContactNumber',
      availability: 'Test Availability'
    };

    service.getMenu(menuId).subscribe((menu) => {
      expect(menu).toEqual(mockMenu);
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}api/MenuBooking/${menuId}`);
    expect(req.request.method).toBe('GET');
    req.flush(mockMenu);
  });
});
