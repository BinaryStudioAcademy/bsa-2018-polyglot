import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import {FlexLayoutModule} from '@angular/flex-layout';
import { MatChipsModule, MatCheckboxModule } from '@angular/material';

import { HttpService } from './services/http.service';
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
import { WorkspaceComponent } from './components/workspace/workspace.component';
import { KeyComponent } from './components/workspace/key/key.component';
import { KeyDetailsComponent } from './components/workspace/key-details/key-details.component';
import { StringDialogComponent } from './dialogs/string-dialog/string-dialog.component';
import { NewProjectComponent } from './components/new-project/new-project.component';
import { ManagerProfileComponent } from './components/manager-profile/manager-profile.component';
import { TagsComponent } from './components/tags/tags.component';
import { ImageCropperModule } from "ngx-img-cropper";
import { CropperComponent } from './dialogs/cropper-dialog/cropper.component';
import { WebStorageModule } from 'ngx-store';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule , MatProgressSpinnerModule,} from '@angular/material';
import { MatSortModule } from '@angular/material/sort';
import { TeamComponent } from './components/teams/team/team.component';
import { SearchComponent } from './components/search/search.component';
import { UserSettingsComponent } from './components/user-settings/user-settings.component';
import { ConfirmEqualValidatorDirective } from './directives/confirm-equal-validator.directive.ts'



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
    NewProjectComponent,
    AboutUsComponent,
    ContactComponent,
    FooterComponent,
    WorkspaceComponent,
    KeyComponent,
    KeyDetailsComponent,
    UploadImageComponent,
    FooterComponent,
    StringDialogComponent,
    TagsComponent,
    NewProjectComponent,
    ManagerProfileComponent,
    TeamComponent,
    SearchComponent,
    CropperComponent,
    UserSettingsComponent,
    ConfirmEqualValidatorDirective

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
    MatChipsModule,
    ngfModule,
    WebStorageModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatProgressSpinnerModule,
    ImageCropperModule,
    MatCheckboxModule
    
  ],
  entryComponents: [LoginDialogComponent, SignupDialogComponent, CropperComponent, StringDialogComponent],
  providers: [HttpService, AuthService, UserService, AuthGuard],
  bootstrap: [AppComponent]


})
export class AppModule { }
