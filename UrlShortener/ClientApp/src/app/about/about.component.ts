import { Component, OnInit } from '@angular/core';
import { AboutService, AboutInfo } from '../services/about.service';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css']
})
export class AboutComponent implements OnInit {
  info: AboutInfo = { description: '' };
  editText = '';
  loading = true;

  constructor(private aboutSrv: AboutService, public auth: AuthService) {}

  ngOnInit() {
    this.aboutSrv.get().subscribe(d => {
      this.info = d;
      this.editText = d.description;
      this.loading = false;
    });
  }

  save() {
    this.aboutSrv.update(this.editText).subscribe({
      next: () => {
        this.info.description = this.editText;
        // last edited info no longer displayed, so not updating
      },
      error: err => {
        alert(err?.error?.error || 'Failed to save');
      }
    });
  }
}
