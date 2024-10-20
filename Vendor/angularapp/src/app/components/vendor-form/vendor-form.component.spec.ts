import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { VendorFormComponent } from './vendor-form.component';
import { VendorService } from '../../services/vendor.service';
import { Router } from '@angular/router';
import { of } from 'rxjs';
import { By } from '@angular/platform-browser';
import { Vendor } from '../../models/vendor.model';

describe('VendorFormComponent', () => {
  let component: VendorFormComponent;
  let fixture: ComponentFixture<VendorFormComponent>;
  let vendorService: VendorService;
  let router: Router;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [VendorFormComponent],
      imports: [FormsModule, RouterTestingModule, HttpClientTestingModule],
      providers: [VendorService]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VendorFormComponent);
    component = fixture.componentInstance;
    vendorService = TestBed.inject(VendorService);
    router = TestBed.inject(Router);
    fixture.detectChanges();
  });

  fit('should_create_VendorFormComponent', () => {
    expect((component as any)).toBeTruthy();
  });

  fit('VendorFormComponent_should_call_addVendor_method_while_adding_the_vendor', () => {
    // Create a mock Vendor object with all required properties
    const vendor: Vendor = {
      vendorId: 1,
      name: 'Test Vendor Name',
      productOfferings: 'Test Product Offerings',
      experience: '5 years',
      storeLocation: 'Test Store Location',
      operatingHours: '9 AM - 5 PM',
      phoneNumber: '1234567890'
    }as any;

    const addVendorSpy = spyOn(component, 'addVendor').and.callThrough();
    (component as any).addVendor();
    expect(addVendorSpy).toHaveBeenCalled();
  });
 
});