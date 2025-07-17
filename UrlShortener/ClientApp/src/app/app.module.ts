import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { ShortUrlsTableComponent } from './short-urls-table/short-urls-table.component';
import { ShortUrlInfoComponent } from './short-url-info/short-url-info.component';
import { AboutComponent } from './about/about.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './auth.interceptor';
import { AuthGuard } from './guards/auth.guard';
import { AdminGuard } from './guards/admin.guard';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ShortUrlsTableComponent,
    ShortUrlInfoComponent,
    AboutComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: ShortUrlsTableComponent },
      { path: 'login', component: LoginComponent },
      { path: 'info/:id', component: ShortUrlInfoComponent, canActivate: [AuthGuard] },
      { path: 'about', component: AboutComponent }
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
