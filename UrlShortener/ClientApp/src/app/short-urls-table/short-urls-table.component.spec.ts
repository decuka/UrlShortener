import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { UrlService } from '../services/url.service';
import { AuthService } from '../services/auth.service';

import { ShortUrlsTableComponent } from './short-urls-table.component';

describe('ShortUrlsTableComponent', () => {
  let component: ShortUrlsTableComponent;
  let fixture: ComponentFixture<ShortUrlsTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShortUrlsTableComponent ],
      providers: [
        { provide: UrlService, useValue: { getAll: () => of([]), add: () => of({}), delete: () => of({}) } },
        { provide: AuthService, useValue: { isAdmin: () => false, getEmail: () => null, isLoggedIn: () => false } }
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShortUrlsTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
