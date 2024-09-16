import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StylishListComponent } from './stylish-list.component';

describe('StylishListComponent', () => {
  let component: StylishListComponent;
  let fixture: ComponentFixture<StylishListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StylishListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StylishListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
