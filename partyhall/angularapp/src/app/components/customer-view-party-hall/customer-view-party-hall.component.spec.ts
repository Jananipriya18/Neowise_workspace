import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerViewPartyHallComponent } from './customer-view-party-hall.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('CustomerViewPartyHallComponent', () => {
  let component: CustomerViewPartyHallComponent;
  let fixture: ComponentFixture<CustomerViewPartyHallComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CustomerViewPartyHallComponent ],
      imports: [ReactiveFormsModule, RouterTestingModule, HttpClientTestingModule, FormsModule]

    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomerViewPartyHallComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });


  fit('Frontend_should_create_the_customer_view_party_hall_component', () => {
    expect(component).toBeTruthy();
  });
  
  fit('Frontend_should_contain_party_hall_details_heading_in_customer_view_party_hall_component', () => {
    const componentHTML = fixture.debugElement.nativeElement.outerHTML;
    expect(componentHTML).toContain('Party Hall Details');
  });
});
