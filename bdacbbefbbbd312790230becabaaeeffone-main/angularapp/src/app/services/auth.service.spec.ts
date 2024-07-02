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

  fit('Authservice_should_be_created', () => {
    expect(authService).toBeTruthy();
  });

  fit('AuthService_should_return_true_from_isLoggedIn_when_loggedIn_is_set_to_true', () => {
    localStorage.setItem('loggedIn', 'true');
    expect(authService.isLoggedIn()).toBe(true);
  });

  fit('AuthService_should_return_false_from_isLoggedIn_when_loggedIn_is_set_to_false', () => {
    localStorage.setItem('loggedIn', 'false');
    expect(authService.isLoggedIn()).toBe(false);
  });
});
