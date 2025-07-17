import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface ShortUrl {
  id: number;
  originalUrl: string;
  shortCode: string;
  createdBy: string;
  createdAt: string;
}

@Injectable({ providedIn: 'root' })
export class UrlService {
  private api = '/api/shorturls';

  constructor(private http: HttpClient) {}

  getAll(): Observable<ShortUrl[]> {
    return this.http.get<ShortUrl[]>(this.api);
  }

  add(originalUrl: string): Observable<ShortUrl> {
    return this.http.post<ShortUrl>(this.api, { originalUrl });
  }

  delete(id: number) {
    return this.http.delete(`${this.api}/${id}`);
  }

  getById(id: number): Observable<ShortUrl> {
    return this.http.get<ShortUrl>(`${this.api}/${id}`);
  }
} 