import { JwtService } from "./jwt.service";

describe('JwtService', () => {
  let service: JwtService;

  beforeEach(() => {
    service = new JwtService();
  });

  fit('JwtService_should_save_token_to_local_storage', () => {
    const token = 'sample_token';
    service.saveToken(token);
    expect(localStorage.getItem('jwtToken')).toEqual(token);
  });

  fit('JwtService_should_get_token_from_local_storage', () => {
    const token = 'sample_token';
    localStorage.setItem('jwtToken', token);
    expect(service.getToken()).toEqual(token);
  });

  fit('JwtService_should_remove_token_from_local_storage', () => {
    const token = 'sample_token';
    localStorage.setItem('jwtToken', token);
    service.destroyToken();
    expect(localStorage.getItem('jwtToken')).toBeNull();
  });

});
