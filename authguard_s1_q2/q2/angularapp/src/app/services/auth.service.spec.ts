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
    localStorage.removeItem('loggedIn'); // Clean up localStorage after each test
  });

  fit('Authservice should be created', () => {
    expect(authService).toBeTruthy();
  });

  fit('AuthService should return true from isLoggedIn when isLoggedIn is set to true', () => {
    localStorage.setItem('loggedIn', 'true');
    expect(authService.isLoggedIn()).toBe(true);
  });

  fit('AuthService should return false from isLoggedIn when isLoggedIn is set to false', () => {
    localStorage.setItem('loggedIn', 'false');
    expect(authService.isLoggedIn()).toBe(false);
  });
});
