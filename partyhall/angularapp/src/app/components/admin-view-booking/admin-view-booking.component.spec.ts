import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminViewBookingComponent } from './admin-view-booking.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('AdminViewBookingComponent', () => {
  let component: AdminViewBookingComponent;
  let fixture: ComponentFixture<AdminViewBookingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminViewBookingComponent ],
      imports: [ReactiveFormsModule, RouterTestingModule, HttpClientTestingModule, FormsModule]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminViewBookingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

 fit('Frontend_should_create_the_admin_view_booking_component', () => {
  expect(component).toBeTruthy();
});

fit('Frontend_should_contain_party_hall_booking_details_heading_in_admin_view_booking_component', () => {
  const componentHTML = fixture.debugElement.nativeElement.outerHTML;
  expect(componentHTML).toContain('Party Hall Booking Details');
});
});
