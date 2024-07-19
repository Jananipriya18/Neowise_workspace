import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AdminviewcollegeComponent } from './adminviewcollege.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('AdminviewcollegeComponent', () => {
  let component: AdminviewcollegeComponent;
  let fixture: ComponentFixture<AdminviewcollegeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminviewcollegeComponent ],
      imports: [ ReactiveFormsModule, RouterTestingModule, HttpClientTestingModule, FormsModule ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminviewcollegeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  fit('Frontend_should_create_adminviewcollege_component', () => {
    expect(component).toBeTruthy();
  });

  fit('Frontend_should_contain_colleges_heading_in_the_adminviewcollege_component', () => {
    const componentHTML: string = fixture.debugElement.nativeElement.outerHTML;
    expect(componentHTML).toContain('Colleges');
  });

});
