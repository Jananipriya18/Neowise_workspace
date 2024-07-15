import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminViewReviewComponent } from './admin-view-review.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('AdminViewReviewComponent', () => {
  let component: AdminViewReviewComponent;
  let fixture: ComponentFixture<AdminViewReviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReactiveFormsModule, RouterTestingModule, HttpClientTestingModule, FormsModule],
      declarations: [ AdminViewReviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminViewReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

fit('Frontend_should_create_the_admin_view_review_component', () => {
  expect(component).toBeTruthy();
});

fit('Frontend_should_contain_party_hall_reviews_heading_in_admin_view_review_component', () => {
  const componentHTML = fixture.debugElement.nativeElement.outerHTML;
  expect(componentHTML).toContain('Party Hall Reviews');
});
});
