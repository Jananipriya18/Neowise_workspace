import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddReviewComponent } from './add-review.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('AddReviewComponent', () => {
  let component: AddReviewComponent;
  let fixture: ComponentFixture<AddReviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddReviewComponent ],
      imports: [ReactiveFormsModule, RouterTestingModule, HttpClientTestingModule, FormsModule],
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

 fit('Frontend_should_create_the_add_review_component', () => {
  expect(component).toBeTruthy();
});

fit('Frontend_should_contain_add_party_hall_review_details_heading_in_add_review_component', () => {
  const componentHTML = fixture.debugElement.nativeElement.outerHTML;
  expect(componentHTML).toContain('Add Party Hall Review Details');
});
});
