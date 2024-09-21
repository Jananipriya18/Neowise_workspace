import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddGardenerComponent } from './add-gardener.component';

describe('AddGardenerComponent', () => {
  let component: AddGardenerComponent;
  let fixture: ComponentFixture<AddGardenerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddGardenerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddGardenerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
