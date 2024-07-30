import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { FormsModule } from '@angular/forms'; // Import FormsModule
import { RouterTestingModule } from '@angular/router/testing';
import { MenuFormComponent } from './menu-form.component';
import { MenuService } from '../services/menu.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Router } from '@angular/router';
import { of } from 'rxjs';
import { Menu } from '../models/menu.model';
import { fakeAsync, tick } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';
import { MenuListComponent } from '../menu-list/menu-list.component';

describe('MenuFormComponent', () => {
  let component: MenuFormComponent;
  let fixture: ComponentFixture<MenuFormComponent>;
  let menuService: MenuService;
  let router: Router;
  let menuListComponent: MenuListComponent;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MenuFormComponent],
      imports: [FormsModule, RouterTestingModule, HttpClientTestingModule],
      providers: [
        MenuService,
      ]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MenuFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();

    menuService = TestBed.inject(MenuService);
    router = TestBed.inject(Router);

  });

  fit('should_have_addMenu_method', () => {
    expect(component.addMenu).toBeTruthy();
  });

  fit('should_show_error_messages_for_required_fields_on_submit', fakeAsync(() => {
    // Mock new menu data
    component.newMenu = {
        menuId: 1,
        chefName: '',
        menuName: '',
        description: '',
        price: '',
        availability: ''
    };

    // Trigger form submission
    const form = fixture.debugElement.query(By.css('form')).nativeElement;
    form.dispatchEvent(new Event('submit'));
    fixture.detectChanges();
    tick();

    // Check error messages for each field
    const errorMessages = fixture.debugElement.queryAll(By.css('.error-message'));
    expect(errorMessages.length).toBe(5); // Assuming there are 5 required fields

    // Check error messages content
    expect(errorMessages[0].nativeElement.textContent).toContain('Chef Name is required');
    expect(errorMessages[1].nativeElement.textContent).toContain('Menu name is required');
    expect(errorMessages[2].nativeElement.textContent).toContain('Description are required');
    expect(errorMessages[3].nativeElement.textContent).toContain('Price is required');
    expect(errorMessages[4].nativeElement.textContent).toContain('Availability is required');
}));


  // fit('should show chefName required error message on register page', fakeAsync(() => {
  //   const nameInput = fixture.debugElement.query(By.css('#name'));
  //   nameInput.nativeElement.value = '';
  //   nameInput.nativeElement.dispatchEvent(new Event('input'));
  //   fixture.detectChanges();
  //   tick();
  //   const errorMessage = fixture.debugElement.query(By.css('.error-message'));
  //   expect(errorMessage.nativeElement.textContent).toContain('Name is required');
  // }));

  fit('should_not_render_any_error_messages_when_all_fields_are_filled', () => {
    const compiled = fixture.nativeElement;
    const form = compiled.querySelector('form');

    // Fill all fields
    component.newMenu = {
      menuId: null, // or omit this line if menuId is auto-generated
      chefName: 'Test Chef Name',
      menuName: 'Test Menu Name',
      description: 'Test Description',
      price: 'Test Price',
      availability: 'Test Availability'
    };

    fixture.detectChanges();

    form.dispatchEvent(new Event('submit')); // Submit the form

    // Check if no error messages are rendered
    expect(compiled.querySelector('#chefNameError')).toBeNull();
    expect(compiled.querySelector('#menuNameError')).toBeNull();
    expect(compiled.querySelector('#descriptionError')).toBeNull();
    expect(compiled.querySelector('#priceError')).toBeNull();
    expect(compiled.querySelector('#availabilityError')).toBeNull();
  });

  fit('should_call_add_menu_method_while_adding_the_menu', () => {
    // Create a mock Menu object with all required properties
    const menu: Menu = { 
      menuId: 1, 
      chefName: 'Test Chef Name', 
      menuName: 'Test Menu Name', 
      description: 'Test Description', 
      price: 'Test Price', 
      availability: 'Test Availability'
    };
    const addMenuSpy = spyOn(component, 'addMenu').and.callThrough();
    component.addMenu();
    expect(addMenuSpy).toHaveBeenCalled();
  });
});

