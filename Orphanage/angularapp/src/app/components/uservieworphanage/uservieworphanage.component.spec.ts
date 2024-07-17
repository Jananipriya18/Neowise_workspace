import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UservieworphanageComponent } from './uservieworphanage.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('UservieworphanageComponent', () => {
  let component: UservieworphanageComponent;
  let fixture: ComponentFixture<UservieworphanageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UservieworphanageComponent ],
      imports: [ReactiveFormsModule, RouterTestingModule, HttpClientTestingModule, FormsModule]

    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UservieworphanageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  fit('Frontend_should_create_uservieworphanage_component', () => {
    expect(component).toBeTruthy();
  });

  fit('Frontend_should_contain_orphanages_heading_in_the_uservieworphanage_component', () => {
    const componentHTML = fixture.debugElement.nativeElement.outerHTML;
    expect(componentHTML).toContain('Orphanages');
  });
});
