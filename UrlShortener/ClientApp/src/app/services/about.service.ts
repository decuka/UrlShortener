import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface AboutInfo {
  description: string;
}

@Injectable({ providedIn: 'root' })
export class AboutService {
  private api = '/api/about';
  constructor(private http: HttpClient) {}

  get(): Observable<AboutInfo> {
    return this.http.get<AboutInfo>(this.api);
  }

  update(description: string) {
    return this.http.post(this.api, { description });
  }
} 