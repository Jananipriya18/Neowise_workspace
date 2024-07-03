import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { LoginComponent } from './login.component';
import { AuthService } from 'src/app/services/auth.service';
import { JwtService } from 'src/app/services/jwt.service';
import { Router } from '@angular/router';
import { of } from 'rxjs';

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let authService: jasmine.SpyObj<AuthService>;
  let router: jasmine.SpyObj<Router>;

  beforeEach(waitForAsync(() => {
    const authServiceSpy = jasmine.createSpyObj('AuthService', ['logout', 'login']);
    const routerSpy = jasmine.createSpyObj('Router', ['navigate']);

    TestBed.configureTestingModule({
      declarations: [LoginComponent],
      imports: [RouterTestingModule],
      providers: [
        { provide: AuthService, useValue: authServiceSpy },
        { provide: Router, useValue: routerSpy },
        JwtService
      ]
    }).compileComponents();

    authService = TestBed.inject(AuthService) as jasmine.SpyObj<AuthService>;
    router = TestBed.inject(Router) as jasmine.SpyObj<Router>;
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  fit('LoginComponent_should_be_created', () => {
    expect(component).toBeTruthy();
  });

  fit('LoginComponent_should_call_logout_and_login_methods_of_AuthService_on_login', () => {
    const username = 'testUser';
    const password = 'testPassword';
    const mockResponse = { message: 'Login successful' };

    authService.login.and.returnValue(of(mockResponse));

    component.username = username;
    component.password = password;
    component.login();

    expect(authService.logout).toHaveBeenCalled();
    expect(authService.login).toHaveBeenCalledWith(username, password);
  });

  fit('LoginComponent_should_navigate_to_dashboard_on_successful_login', () => {
    const username = 'testUser';
    const password = 'testPassword';
    const mockResponse = { message: 'Login successful' };

    authService.login.and.returnValue(of(mockResponse));

    component.username = username;
    component.password = password;
    component.login();

    expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
  });
});
