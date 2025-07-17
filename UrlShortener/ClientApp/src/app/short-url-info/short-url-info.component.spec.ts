import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { UrlService } from '../services/url.service';

import { ShortUrlInfoComponent } from './short-url-info.component';

describe('ShortUrlInfoComponent', () => {
  let component: ShortUrlInfoComponent;
  let fixture: ComponentFixture<ShortUrlInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShortUrlInfoComponent ],
      providers: [
        { provide: ActivatedRoute, useValue: { snapshot: { paramMap: { get: () => '1' } } } },
        { provide: UrlService, useValue: { getById: () => of({ id: 1, originalUrl: 'http://a', shortCode: 'test', createdBy: '', createdAt: '' }) } }
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShortUrlInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
