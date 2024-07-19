import { ComponentFixture, TestBed } from '@angular/core/testing';
import { UserappliedcollegeComponent } from './userappliedcollege.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('UserappliedcollegeComponent', () => {
  let component: UserappliedcollegeComponent;
  let fixture: ComponentFixture<UserappliedcollegeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserappliedcollegeComponent ],
      imports: [ ReactiveFormsModule, RouterTestingModule, HttpClientTestingModule, FormsModule ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserappliedcollegeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  fit('Frontend_should_create_userappliedcollege_component', () => {
    expect(component).toBeTruthy();
  });

  fit('Frontend_should_contain_applied_colleges_heading_in_the_userappliedcollege_component', () => {
    const componentHTML: string = fixture.debugElement.nativeElement.outerHTML;
    expect(componentHTML).toContain('Applied Colleges');
  });

});
