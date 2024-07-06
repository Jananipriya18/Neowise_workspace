import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AuthService } from './auth.service';
import { JwtService } from './jwt.service';
import { Register } from './register.model'; // Assuming you have defined Register model

describe('AuthService', () => {
  let service: AuthService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [AuthService, JwtService]
    });
    service = TestBed.inject(AuthService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  fit('should call API to register user and save token', () => {
    const mockResponse = { token: 'sample_token' };
    const registerData: Register = {
      username: 'testUser',
      password: 'testPassword',
      confirmPassword: 'testPassword',
      email: 'testUser@example.com',
      firstName: 'Test',
      lastName: 'User'
    };

    service.register(registerData).subscribe(response => {
      expect(response).toEqual(mockResponse);
      expect(service.isLoggedIn()).toBeTruthy();
    });

    const request = httpMock.expectOne(`${service.apiUrl}/api/register`);
    expect(request.request.method).toBe('POST');
    expect(request.request.body).toEqual(registerData); // Ensure the request body matches the registerData
    request.flush(mockResponse);
  });

});
