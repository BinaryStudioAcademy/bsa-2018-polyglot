import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import {FlexLayoutModule} from '@angular/flex-layout';

import { DataService } from './services/data.service';
import { TranslatorProfileComponent } from './components/translatorProfile/translator-profile/translator-profile.component';



import { AppMaterialModule } from './common/app-material/app-material.module';
import { ProjectsComponent } from './components/projects/projects.component';
import { TeamsComponent } from './components/teams/teams.component';
import { GlossariesComponent } from './components/glossaries/glossaries.component';
import { AppRoutingModule } from 'src/app/common/app-routing-module/app-routing.module';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { NoFoundComponent } from './components/no-found/no-found.component';
import { UserService } from './services/user.service';
import { LoginDialogComponent } from './dialogs/login-dialog/login-dialog.component';
import { SignupDialogComponent } from './dialogs/signup-dialog/signup-dialog.component';
import { AppComponent } from './app.component';


import { environment } from '../environments/environment';
import { AngularFireModule } from 'angularfire2';
import { AngularFireDatabaseModule } from 'angularfire2/database';
import { AngularFireAuthModule } from 'angularfire2/auth';
import { AuthService } from './services/auth.service';
import { AuthGuard } from './services/guards/auth-guard.service';
import { UploadImageComponent } from './components/upload-image/upload-image.component';
import { ngfModule } from 'angular-file';
import { LandingComponent } from './components/landing/landing.component';
import { NavigationComponent } from './components/navigation/navigation.component';
import { AboutUsComponent } from './components/about-us/about-us.component';
import { ContactComponent } from './components/contact/contact.component';
import { FooterComponent } from './components/footer/footer.component';
import { NewProjectComponent } from './components/new-project/new-project.component';
import { ManagerProfileComponent } from './components/manager-profile/manager-profile.component';
import { WebStorageModule } from 'ngx-store';



@NgModule({
  declarations: [
    LandingComponent,
    NavigationComponent,
    TranslatorProfileComponent,
    LoginDialogComponent,
    SignupDialogComponent,
    ProjectsComponent,
    TeamsComponent,
    GlossariesComponent,
    DashboardComponent,
    NoFoundComponent,
    AppComponent,

    AboutUsComponent,
    ContactComponent,
    UploadImageComponent,
    FooterComponent,
    NewProjectComponent,
    ManagerProfileComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    AppMaterialModule,
    HttpClientModule,
    FlexLayoutModule,
    AppRoutingModule,
    AngularFireModule.initializeApp(environment.firebase, 'angular-auth-firebase'),
    AngularFireDatabaseModule,
    AngularFireAuthModule,
    AppRoutingModule,
    ngfModule,
    WebStorageModule
    
  ],
  entryComponents: [LoginDialogComponent, SignupDialogComponent],
  providers: [DataService, AuthService, UserService, AuthGuard],
  bootstrap: [AppComponent]


})
export class AppModule { }
