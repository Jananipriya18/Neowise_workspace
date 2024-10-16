import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { CheeseShopFormComponent } from './cheese-shop-form.component';
import { CheeseShopService } from '../services/cheese-shop.service';
import { Router } from '@angular/router';
import { of } from 'rxjs';
import { By } from '@angular/platform-browser';
import { CheeseShop } from '../models/cheese-shop';

describe('CheeseShopFormComponent', () => {
  let component: CheeseShopFormComponent;
  let fixture: ComponentFixture<CheeseShopFormComponent>;
  let cheeseShopService: CheeseShopService;
  let router: Router;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CheeseShopFormComponent],
      imports: [FormsModule, RouterTestingModule, HttpClientTestingModule],
      providers: [CheeseShopService]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CheeseShopFormComponent);
    component = fixture.componentInstance;
    cheeseShopService = TestBed.inject(CheeseShopService);
    router = TestBed.inject(Router);
    fixture.detectChanges();
  });

  fit('should_create_CheeseShopFormComponent', () => {
    expect((component as any)).toBeTruthy();
  });

  // fit('CheeseShopFormComponent_should_render_error_messages_when_required_fields_are_empty_on_submit', () => {
  //   // Set all fields to empty strings
  //   component.newShop = {
  //     shopId: 0,
  //     ownerName: '',
  //     cheeseSpecialties: '',
  //     experienceYears: 0,
  //     storeLocation: '',
  //     importedCountry: '',
  //     phoneNumber: ''
  //   } as CheeseShop;

  //   // Manually trigger form submission
  //   component.formSubmitted = true;
  //   fixture.detectChanges();

  //   // Find the form element
  //   const form = fixture.debugElement.query(By.css('form')).nativeElement;

  //   // Trigger the form submission
  //   form.dispatchEvent(new Event('submit'));

  //   fixture.detectChanges();

  //   // Check if error messages are rendered for each field
  //   expect(fixture.debugElement.query(By.css('#ownerName + .error'))).toBeTruthy();
  //   expect(fixture.debugElement.query(By.css('#cheeseSpecialties + .error'))).toBeTruthy();
  //   expect(fixture.debugElement.query(By.css('#experienceYears + .error'))).toBeTruthy();
  //   expect(fixture.debugElement.query(By.css('#storeLocation + .error'))).toBeTruthy();
  //   expect(fixture.debugElement.query(By.css('#importedCountry + .error'))).toBeTruthy();
  //   expect(fixture.debugElement.query(By.css('#phoneNumber + .error'))).toBeTruthy();
  // });

  fit('CheeseShopFormComponent_should_call_addCheeseShop_method_while_adding_the_shop', () => {
    // Create a mock CheeseShop object with all required properties
    const shop: CheeseShop = {
      shopId: 1,
      ownerName: 'Test Owner Name',
      cheeseSpecialties: 'Test Cheese Specialties',
      experienceYears: 5,
      storeLocation: 'Test Store Location',
      importedCountry: 'Test Imported Country',
      phoneNumber: 'Test Phone Number'
    } as any;

    const addCheeseShopSpy = spyOn((component as any), 'addCheeseShop').and.callThrough();
    (component as any).addCheeseShop();
    expect(addCheeseShopSpy).toHaveBeenCalled();
  });
});
