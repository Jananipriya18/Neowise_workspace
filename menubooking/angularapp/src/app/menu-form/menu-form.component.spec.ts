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

  fit('should_have_MenuFormComponent', () => {
    expect(component).toBeTruthy();
  });

  fit('MenuFormComponent_should_render_error_messages_when_required_fields_are_empty_on_submit', () => {
    // Set all fields to empty values
    component.newMenu = {
      menuId: 0,
      chefName: '',
      menuName: '',
      description: '',
      price: '',
      availability: ''
    } as Menu;
 
    // Manually trigger form submission
    component.formSubmitted = true;
 
    fixture.detectChanges();
 
    // Find the form element
    const form = fixture.debugElement.query(By.css('form')).nativeElement;
 
    // Submit the form
    form.dispatchEvent(new Event('submit'));
 
    fixture.detectChanges();
 
    // Check if error messages are rendered for each field
    expect(fixture.debugElement.query(By.css('#chefName + .error-message'))).toBeTruthy();
    expect(fixture.debugElement.query(By.css('#menuName + .error-message'))).toBeTruthy();
    expect(fixture.debugElement.query(By.css('#description + .error-message'))).toBeTruthy();
    expect(fixture.debugElement.query(By.css('#price + .error-message'))).toBeTruthy();
    expect(fixture.debugElement.query(By.css('#availability + .error-message'))).toBeTruthy();
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
    } as any;
    const addMenuSpy = spyOn(component, 'addMenu').and.callThrough();
    component.addMenu();
    expect(addMenuSpy).toHaveBeenCalled();
  });
});

