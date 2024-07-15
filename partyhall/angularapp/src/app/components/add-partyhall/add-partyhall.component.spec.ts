import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddPartyhallComponent } from './add-partyhall.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('AddPartyhallComponent', () => {
  let component: AddPartyhallComponent;
  let fixture: ComponentFixture<AddPartyhallComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddPartyhallComponent ],
      imports: [ReactiveFormsModule, RouterTestingModule, HttpClientTestingModule, FormsModule],

    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddPartyhallComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  fit('Frontend_should_create_the_add_partyhall_component', () => {
    expect(component).toBeTruthy();
  });

  fit('Frontend_should_contain_add_new_party_hall_heading_in_add_partyhall_component', () => {
    const componentHTML = fixture.debugElement.nativeElement.outerHTML;
    expect(componentHTML).toContain('Add New Party Hall');
  });

});
