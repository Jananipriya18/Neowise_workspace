import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AdmineditcollegeComponent } from './admineditcollege.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('AdmineditcollegeComponent', () => {
  let component: AdmineditcollegeComponent;
  let fixture: ComponentFixture<AdmineditcollegeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdmineditcollegeComponent ],
      imports: [ ReactiveFormsModule, RouterTestingModule, HttpClientTestingModule, FormsModule ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdmineditcollegeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  fit('Frontend_should_create_admineditcollege_component', () => {
    expect(component).toBeTruthy();
  });

  fit('Frontend_should_contain_edit_college_heading_in_the_admineditcollege_component', () => {
    const componentHTML: string = fixture.debugElement.nativeElement.outerHTML;
    expect(componentHTML).toContain('Edit College');
  });

});
