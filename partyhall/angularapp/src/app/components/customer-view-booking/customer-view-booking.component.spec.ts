import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerViewBookingComponent } from './customer-view-booking.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('CustomerViewBookingComponent', () => {
  let component: CustomerViewBookingComponent;
  let fixture: ComponentFixture<CustomerViewBookingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CustomerViewBookingComponent ],
      imports: [ReactiveFormsModule, RouterTestingModule, HttpClientTestingModule, FormsModule]

    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomerViewBookingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

fit('Frontend_should_create_the_customer_view_booking_component', () => {
  expect(component).toBeTruthy();
});

fit('Frontend_should_contain_party_hall_booking_details_heading_in_customer_view_booking_component', () => {
  const componentHTML = fixture.debugElement.nativeElement.outerHTML;
  expect(componentHTML).toContain('Party Hall Booking Details');
});
});
