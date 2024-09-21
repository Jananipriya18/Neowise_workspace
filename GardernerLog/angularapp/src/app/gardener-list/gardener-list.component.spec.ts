import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GardenerListComponent } from './gardener-list.component';

describe('GardenerListComponent', () => {
  let component: GardenerListComponent;
  let fixture: ComponentFixture<GardenerListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GardenerListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GardenerListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
