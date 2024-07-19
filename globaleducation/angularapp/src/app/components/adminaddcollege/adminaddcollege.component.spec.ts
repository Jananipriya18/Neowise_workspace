import { ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { AdminaddcollegeComponent } from './adminaddcollege.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('AdminaddcollegeComponent', () => {
  let component: AdminaddcollegeComponent;
  let fixture: ComponentFixture<AdminaddcollegeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminaddcollegeComponent ],
      imports: [ ReactiveFormsModule, RouterTestingModule, HttpClientTestingModule, FormsModule ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminaddcollegeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  fit('Frontend_should_create_adminaddcollege_component', () => {
    expect(component).toBeTruthy();
  });

  fit('Frontend_should_contain_create_new_college_heading_in_the_adminaddcollege_component', () => {
    const componentHTML: string = fixture.debugElement.nativeElement.outerHTML;
    expect(componentHTML).toContain('Create New College');
  });

});
