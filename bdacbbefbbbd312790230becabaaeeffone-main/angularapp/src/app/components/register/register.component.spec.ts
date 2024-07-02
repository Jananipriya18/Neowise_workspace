import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AuthService } from 'src/app/services/auth.service';
import { RegisterComponent } from './register.component';
import { RouterTestingModule } from '@angular/router/testing';
import { Router } from '@angular/router';
import { of, throwError } from 'rxjs';

describe('RegisterComponent', () => {
  let component: RegisterComponent;
  let fixture: ComponentFixture<RegisterComponent>;
  let authService: jasmine.SpyObj<AuthService>;
  let router: jasmine.SpyObj<Router>;

  beforeEach(waitForAsync(() => {
    const authServiceSpy = jasmine.createSpyObj('AuthService', ['login']);
    const routerSpy = jasmine.createSpyObj('Router', ['navigate']);

    TestBed.configureTestingModule({
      declarations: [RegisterComponent],
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
    fixture = TestBed.createComponent(RegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  fit('Register_component_should_call_authService.register_on_register_attempt', () => {
    const username = 'testUser';
    const password = 'testPassword';
    const email = 'testemail'
    const mockResponse = { message: 'Registration successful' };
  
    authService.register.and.returnValue(of(mockResponse));
  
    component.username = username;
    component.password = password;
    component.email = email;
    component.register();
  
    expect(authService.register).toHaveBeenCalledWith(username, password,email);
  });
  
  fit('Register_component_should_navigate_to_dashboard_on_successful_register', () => {
    const username = 'testUser';
    const password = 'testPassword';
    const mockResponse = { message: 'Register successful' };
  
    authService.register.and.returnValue(of(mockResponse));
  
    component.username = username;
    component.password = password;
    component.register();
  
    expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
  });

});
