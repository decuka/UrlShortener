import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UrlService, ShortUrl } from '../services/url.service';

@Component({
  selector: 'app-short-url-info',
  templateUrl: './short-url-info.component.html',
  styleUrls: ['./short-url-info.component.css']
})
export class ShortUrlInfoComponent implements OnInit {
  data?: ShortUrl;
  loading = true;

  constructor(private route: ActivatedRoute, private urlSrv: UrlService) {}

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.urlSrv.getById(id).subscribe(d => {
      this.data = d;
      this.loading = false;
    });
  }
}
