import { TestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AuthService } from './auth.service';

describe('AuthService', () => {
  let authService: AuthService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [AuthService]
    });

    authService = TestBed.inject(AuthService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify(); // Verifies that no requests are outstanding after each test
    localStorage.removeItem('registeredIn'); // Clean up localStorage after each test
  });

  fit('Authservice should be created', () => {
    expect(authService).toBeTruthy();
  });

  fit('AuthService should return true from isRegisteredIn when isRegisteredIn is set to true', () => {
    localStorage.setItem('registeredIn', 'true');
    expect(authService.isRegisteredIn()).toBe(true);
  });

  fit('AuthService should return false from isRegisteredIn when isRegisteredIn is set to false', () => {
    localStorage.setItem('registeredIn', 'false');
    expect(authService.isRegisteredIn()).toBe(false);
  });
});
