import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { AddBookingComponent } from './add-booking.component';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('AddBookingComponent', () => {
  let component: AddBookingComponent;
  let fixture: ComponentFixture<AddBookingComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddBookingComponent],
      providers: [
        FormBuilder,
        { provide: ActivatedRoute, useValue: { snapshot: { paramMap: { get: () => '1' } } } },
        { provide: Router, useValue: {} },
      ],
      imports: [ReactiveFormsModule, RouterTestingModule, HttpClientTestingModule, FormsModule],
    }).compileComponents();

    fixture = TestBed.createComponent(AddBookingComponent);
    component = fixture.componentInstance;
  });

  fit('Frontend_should_create_the_add_booking_component', () => {
    expect(component).toBeTruthy();
  });


  fit('Frontend_should_contain_add_booking_details_heading_in_add_booking_component', () => {
    const componentHTML = fixture.debugElement.nativeElement.outerHTML;
    expect(componentHTML).toContain('Add Party Hall Booking Details');
  });

});
