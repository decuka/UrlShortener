import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { UrlService, ShortUrl } from './url.service';

describe('UrlService', () => {
  let service: UrlService;
  let http: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({ imports: [HttpClientTestingModule] });
    service = TestBed.inject(UrlService);
    http = TestBed.inject(HttpTestingController);
  });

  afterEach(() => http.verify());

  it('getAll() should GET /api/shorturls', () => {
    const mock: ShortUrl[] = [
      { id: 1, originalUrl: 'http://a', shortCode: 'abcd1234', createdBy: 'user', createdAt: 'now' }
    ];
    service.getAll().subscribe(data => expect(data).toEqual(mock));
    const req = http.expectOne('/api/shorturls');
    expect(req.request.method).toBe('GET');
    req.flush(mock);
  });

  it('add() should POST to /api/shorturls with body', () => {
    const mockResp: ShortUrl = { id: 2, originalUrl: 'http://b', shortCode: 'efgh5678', createdBy: 'u', createdAt: 'now' };
    service.add('http://b').subscribe(data => expect(data).toEqual(mockResp));
    const req = http.expectOne('/api/shorturls');
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual({ originalUrl: 'http://b' });
    req.flush(mockResp);
  });

  it('delete() should DELETE /api/shorturls/:id', () => {
    service.delete(3).subscribe();
    const req = http.expectOne('/api/shorturls/3');
    expect(req.request.method).toBe('DELETE');
    req.flush({});
  });

  it('getById() should GET /api/shorturls/:id', () => {
    const mock: ShortUrl = { id: 4, originalUrl: 'http://d', shortCode: 'ijkl9012', createdBy: 'x', createdAt: 'now' };
    service.getById(4).subscribe(data => expect(data).toEqual(mock));
    const req = http.expectOne('/api/shorturls/4');
    expect(req.request.method).toBe('GET');
    req.flush(mock);
  });
}); 