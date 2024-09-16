import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditStylistComponent } from './edit-stylist.component';

describe('EditStylistComponent', () => {
  let component: EditStylistComponent;
  let fixture: ComponentFixture<EditStylistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditStylistComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditStylistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
