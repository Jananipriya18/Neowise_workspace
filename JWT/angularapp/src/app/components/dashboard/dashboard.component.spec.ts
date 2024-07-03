import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardComponent } from './dashboard.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('DashboardComponent', () => {
  let component: DashboardComponent;
  let fixture: ComponentFixture<DashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DashboardComponent ],
      imports: [HttpClientTestingModule]

    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  fit('DashboardComponent_should_be_create', () => {
    expect(component).toBeTruthy();
  });
  fit('DashboardComponent_should_set_isAdmin_to_true_if_user_is_admin', () => {
    // Mock AuthService
    const authServiceMock = jasmine.createSpyObj('AuthService', ['isAdmin']);
    authServiceMock.isAdmin.and.returnValue(true);
  
    // Create DashboardComponent instance
    const component = new DashboardComponent(authServiceMock);
  
    // Call ngOnInit()
    component.ngOnInit();
  
    // Check if isAdmin is true
    expect(component.isAdmin).toBe(true);
  });
  
  
});
