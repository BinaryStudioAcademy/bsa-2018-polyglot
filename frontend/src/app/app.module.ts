import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { LandingComponent } from './landing.component';
import { DataService } from './services/data.service';
import { NavigationComponent } from './components/navigation/navigation.component';

import { environment } from '../environments/environment';
import { AngularFireModule } from 'angularfire2';
import { AngularFireDatabaseModule } from 'angularfire2/database';
import { AngularFireAuthModule } from 'angularfire2/auth';
import { AuthService } from './services/auth.service';

@NgModule({
  declarations: [
    LandingComponent,
    NavigationComponent
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    AngularFireModule.initializeApp(environment.firebase, 'angular-auth-firebase'),
    AngularFireDatabaseModule,
    AngularFireAuthModule
  ],
  providers: [DataService, AuthService],
  bootstrap: [LandingComponent]
})
export class AppModule { }
