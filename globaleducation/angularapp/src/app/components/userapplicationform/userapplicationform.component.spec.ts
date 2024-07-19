import { ComponentFixture, TestBed } from '@angular/core/testing';
import { UserapplicationformComponent } from './userapplicationform.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('UserapplicationformComponent', () => {
  let component: UserapplicationformComponent;
  let fixture: ComponentFixture<UserapplicationformComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserapplicationformComponent ],
      imports: [ ReactiveFormsModule, RouterTestingModule, HttpClientTestingModule, FormsModule ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserapplicationformComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  fit('Frontend_should_create_userapplicationform_component', () => {
    expect(component).toBeTruthy();
  });

  fit('Frontend_should_contain_college_application_form_heading_in_the_userapplicationform_component', () => {
    const componentHTML: string = fixture.debugElement.nativeElement.outerHTML;
    expect(componentHTML).toContain('College Application Form');
  });

});
