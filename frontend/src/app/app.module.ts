import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import {FlexLayoutModule} from '@angular/flex-layout';
import { MatChipsModule, MatCheckboxModule, MatDialogModule, MatTabsModule } from '@angular/material';

import { HttpService } from './services/http.service';
import { TranslatorProfileComponent } from './components/translatorProfile/translator-profile/translator-profile.component';

import { AgmCoreModule } from '@agm/core';

import { AppMaterialModule } from './common/app-material/app-material.module';
import { ProjectsComponent } from './components/projects/projects.component';
import { TeamsComponent } from './components/teams/teams.component';
import { GlossariesComponent } from './components/glossaries/glossaries.component';
import { AppRoutingModule } from './common/app-routing-module/app-routing.module';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { NoFoundComponent } from './components/no-found/no-found.component';
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
import { ConfirmEqualValidatorDirective } from './directives/confirm-equal-validator.directive.ts';
import { ProjectMessageComponent } from './dialogs/project-message/project-message.component'
import {SnotifyModule, SnotifyService, ToastDefaults} from 'ng-snotify';
import { ProjectDetailsComponent } from './components/project-details/project-details.component';
import { ForgotPasswordDialogComponent } from './dialogs/forgot-password-dialog/forgot-password-dialog.component';
import { UploadFileComponent } from './components/upload-file/upload-file.component';
import { TabDetailComponent } from './components/workspace/key-details/tab-detail/tab-detail.component';
import { ImgDialogComponent } from './dialogs/img-dialog/img-dialog.component';
import { NewTeamComponent } from './components/teams/new-team/new-team.component';
import { LanguagesComponent } from './components/project-details/languages/languages.component';
import { DeleteProjectLanguageComponent } from './dialogs/delete-project-language/delete-project-language.component';
import { SelectProjectLanguageComponent } from './dialogs/select-project-language/select-project-language.component';
import { ConfirmDialogComponent } from './dialogs/confirm-dialog/confirm-dialog.component';
import { TabCommentsComponent } from './components/workspace/key-details/tab-comments/tab-comments.component';
import { ProjectTeamComponent } from './components/project-details/project-team/project-team.component';
import { NgxSmoothDnDModule } from 'ngx-smooth-dnd';
import { TeamAssignComponent } from './dialogs/team-assign/team-assign.component';
import { ProjectEditComponent } from './components/project-edit/project-edit.component';
import { MatRadioModule } from '@angular/material';
import { SaveStringConfirmComponent } from './dialogs/save-string-confirm/save-string-confirm.component';
import { TabHistoryComponent } from './components/workspace/key-details/tab-history/tab-history.component';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';

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
    NewTeamComponent,
    SearchComponent,
    CropperComponent,
    UserSettingsComponent,
    ConfirmEqualValidatorDirective,
    ProjectDetailsComponent,
    ProjectMessageComponent,
    ForgotPasswordDialogComponent,
    UploadFileComponent,
    TabDetailComponent,
    ImgDialogComponent,
    ProjectEditComponent,
    LanguagesComponent,
    DeleteProjectLanguageComponent,
    SelectProjectLanguageComponent,
    ConfirmDialogComponent,
    ProjectTeamComponent,
    TeamAssignComponent,
    SaveStringConfirmComponent,
    TabHistoryComponent,
    TabCommentsComponent
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
    MatSlideToggleModule,
    ImageCropperModule,
    MatCheckboxModule,
    SnotifyModule,
    MatDialogModule,
    NgxSmoothDnDModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyD_x9oQzDz-pzi_PIa9M48c_FrYGFwnImo'
    }),
    MatRadioModule,
    MatTabsModule
  ],
  entryComponents: [
    LoginDialogComponent, 
    SignupDialogComponent, 
    CropperComponent, 
    StringDialogComponent,
    ProjectMessageComponent, 
    SelectProjectLanguageComponent,
    DeleteProjectLanguageComponent,
    ForgotPasswordDialogComponent,
    ImgDialogComponent,
    ConfirmDialogComponent,
    TeamAssignComponent,
    SaveStringConfirmComponent

  ],
  providers: [HttpService, AuthService, AuthGuard,
    { provide: 'SnotifyToastConfig', useValue: ToastDefaults},
    SnotifyService],

  bootstrap: [AppComponent]


})
export class AppModule { }
