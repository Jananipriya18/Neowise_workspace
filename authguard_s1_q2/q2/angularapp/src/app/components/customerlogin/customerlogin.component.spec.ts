import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { AuthService } from 'src/app/services/auth.service';
import { CustomerloginComponent } from './customerlogin.component';
import { RouterTestingModule } from '@angular/router/testing';
import { Router } from '@angular/router';
import { of, throwError } from 'rxjs';

describe('CustomerloginComponent', () => {
  let component: CustomerloginComponent;
  let fixture: ComponentFixture<CustomerloginComponent>;
  let authService: jasmine.SpyObj<AuthService>;
  let router: jasmine.SpyObj<Router>;

  beforeEach(waitForAsync(() => {
    const authServiceSpy = jasmine.createSpyObj('AuthService', ['customerlogin']);
    const routerSpy = jasmine.createSpyObj('Router', ['navigate']);

    TestBed.configureTestingModule({
      declarations: [CustomerloginComponent],
      imports: [RouterTestingModule],
      providers: [
        { provide: AuthService, useValue: authServiceSpy },
        { provide: Router, useValue: routerSpy }
      ]
    }).compileComponents();

    authService = TestBed.inject(AuthService) as jasmine.SpyObj<AuthService>;
    router = TestBed.inject(Router) as jasmine.SpyObj<Router>;
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomerloginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  fit('Customerlogin_component_should_be_create', () => {
    expect(component).toBeTruthy();
  });

  fit('Customerlogin_component_should_call_authService_customerlogin_on_customerlogin_attempt', () => {
    const userName = 'testUser';
    const password = 'testPassword';
    const email = 'testemail';
    const phoneNumber = '1234567890';
    const twoFactorEnabledPassCode = '1230';
    const mockResponse = { message: 'Customer Login successful' };

    authService.customerlogin.and.returnValue(of(mockResponse));

    component.customerLogin.userName = userName;
    component.customerLogin.password = password;
    component.customerLogin.email = email;
    component.customerLogin.phoneNumber = phoneNumber;
    component.customerLogin.twoFactorEnabledPassCode = twoFactorEnabledPassCode;
    component.customerlogin();

    expect(authService.customerlogin).toHaveBeenCalledWith(userName, password, email, phoneNumber, twoFactorEnabledPassCode);
  });

  fit('Customerlogin_component_should_navigate_to_dashboard_on_successful_customerlogin', () => {
    const userName = 'testUser';
    const password = 'testPassword';
    const email = 'test@gmail.com';
    const phoneNumber = '1234567890';
    const twoFactorEnabledPassCode = '1230';
    const mockResponse = { message: 'Customer Login successful' };

    authService.customerlogin.and.returnValue(of(mockResponse));

    component.customerLogin.userName = userName;
    component.customerLogin.password = password;
    component.customerLogin.email = email;
    component.customerLogin.phoneNumber = phoneNumber;
    component.customerLogin.twoFactorEnabledPassCode = twoFactorEnabledPassCode;
    component.customerlogin();

    expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
  });

});
