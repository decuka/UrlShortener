import { Component, OnInit } from '@angular/core';
import { UrlService, ShortUrl } from '../services/url.service';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-short-urls-table',
  templateUrl: './short-urls-table.component.html',
  styleUrls: ['./short-urls-table.component.css']
})
export class ShortUrlsTableComponent implements OnInit {
  urls: ShortUrl[] = [];
  newUrl = '';
  errorMsg = '';

  constructor(private urlSrv: UrlService, public auth: AuthService) {}

  ngOnInit() {
    this.load();
  }

  load() {
    this.urlSrv.getAll().subscribe(data => (this.urls = data));
  }

  create() {
    if (!this.newUrl) return;
    this.errorMsg = '';
    this.urlSrv.add(this.newUrl).subscribe({
      next: u => {
        // ensure owner field filled so buttons display without reload
        if (!u.createdBy) {
          u.createdBy = this.auth.getEmail() || '';
        }
        this.urls.push(u);
        this.newUrl = '';
      },
      error: err => {
        this.errorMsg = err?.error?.message || 'Failed to add URL';
      }
    });
  }

  remove(id: number) {
    this.urlSrv.delete(id).subscribe(() => {
      this.urls = this.urls.filter(x => x.id !== id);
    });
  }

  canDelete(u: ShortUrl): boolean {
    return this.auth.isAdmin() || (!!this.auth.getEmail() && u.createdBy === this.auth.getEmail());
  }

  canViewInfo(): boolean {
    return this.auth.isLoggedIn();
  }
}
