import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AuthService } from './auth.service';

describe('AuthService', () => {
  let service: AuthService;
  let http: HttpTestingController;

  const store: Record<string, string> = {};

  beforeEach(() => {
    spyOn(localStorage, 'getItem').and.callFake((key: string): string | null => store[key] || null);
    spyOn(localStorage, 'setItem').and.callFake((key: string, value: string): void => { store[key] = value; });
    spyOn(localStorage, 'removeItem').and.callFake((key: string): void => { delete store[key]; });

    TestBed.configureTestingModule({ imports: [HttpClientTestingModule] });
    service = TestBed.inject(AuthService);
    http = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    http.verify();
    Object.keys(store).forEach(k => delete store[k]);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('login() should store token and return response', () => {
    const mockResp = { token: 'header.' + btoa('{}') + '.sig' };

    service.login('a@a.com', 'pwd').subscribe(res => {
      expect(res).toEqual(mockResp);
      expect(localStorage.setItem).toHaveBeenCalledWith('token', mockResp.token);
    });

    const req = http.expectOne('/api/auth/login');
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual({ email: 'a@a.com', password: 'pwd' });
    req.flush(mockResp);
  });

  it('logout() should clear token', () => {
    localStorage.setItem('token', 'abc');
    service.logout();
    expect(localStorage.removeItem).toHaveBeenCalledWith('token');
    expect(service.isLoggedIn()).toBeFalse();
  });

  it('isLoggedIn() should reflect presence of token', () => {
    expect(service.isLoggedIn()).toBeFalse();
    localStorage.setItem('token', 'x.y.z');
    expect(service.isLoggedIn()).toBeTrue();
  });

  it('isAdmin() should parse role from token payload', () => {
    const payload = {
      "http://schemas.microsoft.com/ws/2008/06/identity/claims/role": 'Admin'
    };
    const token = 'hdr.' + btoa(JSON.stringify(payload)) + '.sig';
    localStorage.setItem('token', token);

    expect(service.isAdmin()).toBeTrue();
  });
});
