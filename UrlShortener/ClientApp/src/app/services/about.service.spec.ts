import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AboutService, AboutInfo } from './about.service';

describe('AboutService', () => {
  let service: AboutService;
  let http: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({ imports: [HttpClientTestingModule] });
    service = TestBed.inject(AboutService);
    http = TestBed.inject(HttpTestingController);
  });

  afterEach(() => http.verify());

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('get() should perform GET /api/about', () => {
    const mock: AboutInfo = { description: 'Hello' };
    service.get().subscribe(data => expect(data).toEqual(mock));

    const req = http.expectOne('/api/about');
    expect(req.request.method).toBe('GET');
    req.flush(mock);
  });

  it('update() should POST description to /api/about', () => {
    service.update('New text').subscribe();

    const req = http.expectOne('/api/about');
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual({ description: 'New text' });
    req.flush({});
  });
}); 