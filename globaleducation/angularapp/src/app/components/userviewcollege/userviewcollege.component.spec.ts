import { ComponentFixture, TestBed } from '@angular/core/testing';
import { UserviewcollegeComponent } from './userviewcollege.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('UserviewcollegeComponent', () => {
  let component: UserviewcollegeComponent;
  let fixture: ComponentFixture<UserviewcollegeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserviewcollegeComponent ],
      imports: [ ReactiveFormsModule, RouterTestingModule, HttpClientTestingModule, FormsModule ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserviewcollegeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  fit('Frontend_should_create_userviewcollege_component', () => {
    expect(component).toBeTruthy();
  });

  fit('Frontend_should_contain_available_colleges_heading_in_the_userviewcollege_component', () => {
    const componentHTML: string = fixture.debugElement.nativeElement.outerHTML;
    expect(componentHTML).toContain('Available Colleges');
  });

});
