import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RequestedcollegeComponent } from './requestedcollege.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('RequestedcollegeComponent', () => {
  let component: RequestedcollegeComponent;
  let fixture: ComponentFixture<RequestedcollegeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RequestedcollegeComponent ],
      imports: [ ReactiveFormsModule, RouterTestingModule, HttpClientTestingModule, FormsModule ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RequestedcollegeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  fit('Frontend_should_create_requestedcollege_component', () => {
    expect(component).toBeTruthy();
  });

  fit('Frontend_should_contain_college_application_requests_for_approval_heading_in_the_requestedcollege_component', () => {
    const componentHTML: string = fixture.debugElement.nativeElement.outerHTML;
    expect(componentHTML).toContain('College Application Requests for Approval');
  });

});
