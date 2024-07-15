import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerViewReviewComponent } from './customer-view-review.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('CustomerViewReviewComponent', () => {
  let component: CustomerViewReviewComponent;
  let fixture: ComponentFixture<CustomerViewReviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CustomerViewReviewComponent ],
      imports: [ReactiveFormsModule, RouterTestingModule, HttpClientTestingModule, FormsModule]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomerViewReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

fit('Frontend_should_create_the_customer_view_review_component', () => {
  expect(component).toBeTruthy();
});

fit('Frontend_should_contain_reviews_heading_in_customer_view_review_component', () => {
  const componentHTML = fixture.debugElement.nativeElement.outerHTML;
  expect(componentHTML).toContain('Reviews');
});

});
