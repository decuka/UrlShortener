import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { AboutService } from '../services/about.service';
import { AuthService } from '../services/auth.service';

import { AboutComponent } from './about.component';

describe('AboutComponent', () => {
  let component: AboutComponent;
  let fixture: ComponentFixture<AboutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AboutComponent ],
      providers: [
        { provide: AboutService, useValue: { get: () => of({ description: 'demo' }), update: () => of({}) } },
        { provide: AuthService, useValue: { isAdmin: () => false } }
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AboutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
