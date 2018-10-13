import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { MentionModule } from 'angular2-mentions/mention';

import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { HttpClientModule } from "@angular/common/http";
import { FlexLayoutModule } from "@angular/flex-layout";
import {
    MatChipsModule,
    MatCheckboxModule,
    MatDialogModule,
    MatSelectModule,
    MatTabsModule,
    MatSnackBarModule,
    MatBottomSheetModule,
    DateAdapter
} from "@angular/material";

import { HttpService } from "./services/http.service";
import { TranslatorProfileComponent } from "./components/translatorProfile/translator-profile/translator-profile.component";

import { AgmCoreModule } from "@agm/core";

import { AppMaterialModule } from "./common/app-material/app-material.module";
import { ProjectsComponent } from "./components/projects/projects.component";
import { TeamsComponent } from "./components/teams/teams.component";
import { GlossariesComponent } from "./components/glossaries/glossaries.component";
import { AppRoutingModule } from "./common/app-routing-module/app-routing.module";
import { DashboardComponent } from "./components/dashboard/dashboard.component";
import { NoFoundComponent } from "./components/errors/no-found/no-found.component";
import { LoginDialogComponent } from "./dialogs/login-dialog/login-dialog.component";
import { SignupDialogComponent } from "./dialogs/signup-dialog/signup-dialog.component";
import { AppComponent } from "./app.component";

import { environment } from "../environments/environment";
import { AngularFireModule } from "angularfire2";
import { AngularFireDatabaseModule } from "angularfire2/database";
import { AngularFireAuthModule } from "angularfire2/auth";
import { AuthService } from "./services/auth.service";
import { AuthGuard } from "./services/guards/auth-guard.service";
import { UploadImageComponent } from "./components/upload-image/upload-image.component";
import { ngfModule } from "angular-file";
import { LandingComponent } from "./components/landing/landing.component";
import { NavigationComponent } from "./components/navigation/navigation.component";
import { AboutUsComponent } from "./components/about-us/about-us.component";
import { ContactComponent } from "./components/contact/contact.component";
import { FooterComponent } from "./components/footer/footer.component";
import { WorkspaceComponent } from "./components/workspace/workspace.component";
import { KeyComponent } from "./components/workspace/key/key.component";
import { KeyDetailsComponent } from "./components/workspace/key-details/key-details.component";
import { StringDialogComponent } from "./dialogs/string-dialog/string-dialog.component";
import { NewProjectComponent } from "./components/new-project/new-project.component";
import { ManagerProfileComponent } from "./components/manager-profile/manager-profile.component";
import { TagsComponent } from "./components/tags/tags.component";
import { ImageCropperModule } from "ngx-img-cropper";
import { CropperComponent } from "./dialogs/cropper-dialog/cropper.component";
import { WebStorageModule } from "ngx-store";
import { MatTableModule } from "@angular/material/table";
import {
    MatPaginatorModule,
    MatProgressSpinnerModule
} from "@angular/material";
import { MatSortModule } from "@angular/material/sort";

import { SearchComponent } from "./components/search/search.component";
import { UserSettingsComponent } from "./components/user-settings/user-settings.component";
import { ConfirmEqualValidatorDirective } from "./directives/confirm-equal-validator.directive.ts";
import { NoWhiteSpaceDirective } from './directives/noWhiteSpace.directive';
import { ProjectMessageComponent } from "./dialogs/project-message/project-message.component";
import { SnotifyModule, SnotifyService, ToastDefaults } from "ng-snotify";
import { ProjectDetailsComponent } from "./components/project-details/project-details.component";
import { ForgotPasswordDialogComponent } from "./dialogs/forgot-password-dialog/forgot-password-dialog.component";
import { UploadFileComponent } from "./components/upload-file/upload-file.component";
import { TabDetailComponent } from "./components/workspace/key-details/tab-detail/tab-detail.component";
import { ImgDialogComponent } from "./dialogs/img-dialog/img-dialog.component";
import { NewTeamComponent } from "./components/teams/new-team/new-team.component";
import { LanguagesComponent } from "./components/project-details/languages/languages.component";
import { DeleteProjectLanguageComponent } from "./dialogs/delete-project-language/delete-project-language.component";
import { SelectProjectLanguageComponent } from "./dialogs/select-project-language/select-project-language.component";
import { ConfirmDialogComponent } from "./dialogs/confirm-dialog/confirm-dialog.component";
import { TabCommentsComponent } from "./components/workspace/key-details/tab-comments/tab-comments.component";
import { ProjectTeamComponent } from "./components/project-details/project-team/project-team.component";
import { NgxSmoothDnDModule } from "ngx-smooth-dnd";
import { TeamAssignComponent } from "./dialogs/team-assign/team-assign.component";
import { ProjectEditComponent } from "./components/project-edit/project-edit.component";
import { MatRadioModule } from "@angular/material";
import { SaveStringConfirmComponent } from "./dialogs/save-string-confirm/save-string-confirm.component";
import { TabHistoryComponent } from "./components/workspace/key-details/tab-history/tab-history.component";
import { DownloadFileComponent } from "./components/project-details/download-file/download-file.component";
import { MatSlideToggleModule } from "@angular/material/slide-toggle";
import { ChooseRoleDialogComponent } from "./dialogs/choose-role-dialog/choose-role-dialog.component";
import { GlossaryService } from "./services/glossary.service";
import { GlossaryCreateDialogComponent } from "./dialogs/glossary-create-dialog/glossary-create-dialog.component";
import { GlossaryEditDialogComponent } from "./dialogs/glossary-edit-dialog/glossary-edit-dialog.component";
import { GlossaryComponent } from "./components/glossaries/glossary/glossary.component";
import { GlossaryStringDialogComponent } from "./dialogs/glossary-string-dialog/glossary-string-dialog.component";

//Ngx-Charts
import { NgxChartsModule } from "@swimlane/ngx-charts";
import { ReportsComponent } from "./components/reports/reports.component";
import { NgxInfiniteScrollerModule } from "ngx-infinite-scroller";
import { InfiniteScrollModule } from "ngx-infinite-scroll";

import { ProjectActivitiesComponent } from "./components/project-details/project-activities/project-activities.component";
import { TabReviewComponent } from './components/translatorProfile/tab-review/tab-review.component';
import { StarRatingComponent } from './components/translatorProfile/star-rating/star-rating.component';
import { MachineTranslationMenuComponent } from './dialogs/machine-translation-menu/machine-translation-menu.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { SnotifyGlobalConfig } from './common/SnotifyGlobalConfig';
import { AssignGlossariesComponent } from './components/project-details/assign-glossaries/assign-glossaries.component';
import { ListTranslatorsComponent } from './dialogs/list-translators/list-translators.component';
import { TranslatorGuardService } from './services/guards/translator-guard.service';
import { TabOptionalComponent } from './components/workspace/key-details/tab-optional/tab-optional.component';
import { SelectColorDialogComponent } from './dialogs/select-color-dialog/select-color-dialog.component';


import { ChatComponent } from "./components/chat/chat.component";
import { ChatContactsComponent } from './components/chat/chat-contacts/chat-contacts.component';
import { ChatWindowComponent } from './components/chat/chat-window/chat-window.component';
import { TeamProjectComponent } from "./components/team-details/team-project/team-project.component";
import { TeamMembersComponent } from "./components/team-details/team-members/team-members.component";
import { TeamDetailsComponent } from "./components/team-details/team-details.component";
import { NotificationsComponent } from './components/notifications/notifications.component';
import { TeamAddMemberComponent } from './dialogs/team-add-member/team-add-member.component';
import { ChooseProficiencyDialogComponent } from './dialogs/choose-proficiency-dialog/choose-proficiency-dialog.component';
import { TranslatorSearchByNamePipe } from './pipes/translator-search-by-name.pipe';
import { AddRemoveLanguagesDialogComponent } from './dialogs/add-remove-languages-dialog/add-remove-languages-dialog.component';
import { GuidelineComponent } from './components/guideline/guideline.component';
import { TranslatorGuideComponent } from './components/guideline/translator-guide/translator-guide.component';
import { ManagerGuideComponent } from './components/guideline/manager-guide/manager-guide.component';
import { CommentsPipe } from './common/pipes/comments.pipe';
import { OptionalTranslationMenuComponent } from './dialogs/optional-translation-menu/optional-translation-menu.component';
import { InternalserverComponent } from './components/errors/internalserver/internalserver.component';
import { ForbiddenComponent } from './components/errors/forbidden/forbidden.component';


@NgModule({
  exports: [
    MatSnackBarModule
  ],
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
    NewTeamComponent,
    SearchComponent,
    CropperComponent,
    UserSettingsComponent,
    ConfirmEqualValidatorDirective,
    NoWhiteSpaceDirective,
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
    TabCommentsComponent,
    TabHistoryComponent,
    ChooseRoleDialogComponent,
    ProjectTeamComponent,
    TeamProjectComponent,
    TeamAssignComponent,
    SaveStringConfirmComponent,
    TabHistoryComponent,
    TabCommentsComponent,
    GlossaryCreateDialogComponent,
    GlossaryEditDialogComponent,
    GlossaryComponent,
    GlossaryStringDialogComponent,
    ProjectActivitiesComponent,
    TabReviewComponent,
    DownloadFileComponent,
    StarRatingComponent,
    MachineTranslationMenuComponent,
    ReportsComponent,
    ProjectActivitiesComponent,
    TabReviewComponent,
    DownloadFileComponent,
    StarRatingComponent,
    UserProfileComponent,
    AssignGlossariesComponent,
    TabOptionalComponent,
    SelectColorDialogComponent,
    TeamDetailsComponent,
    TeamMembersComponent,
    ChatComponent,
    ChatContactsComponent,
    ChatWindowComponent,
    ListTranslatorsComponent,
    TabOptionalComponent,
    NotificationsComponent,
    TeamAddMemberComponent,
    ChooseProficiencyDialogComponent,
    TranslatorSearchByNamePipe,
    ChooseProficiencyDialogComponent,
    AddRemoveLanguagesDialogComponent,
    GuidelineComponent,
    TranslatorGuideComponent,
    ManagerGuideComponent,
    CommentsPipe,
    OptionalTranslationMenuComponent,
    InternalserverComponent,
    ForbiddenComponent
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
    MatBottomSheetModule,
    ImageCropperModule,
    MatCheckboxModule,
    MatSelectModule,
    SnotifyModule,
    MatDialogModule,
    NgxSmoothDnDModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyD_x9oQzDz-pzi_PIa9M48c_FrYGFwnImo'
    }),
    MatRadioModule,
    MatTabsModule,
    NgxChartsModule,
    NgxInfiniteScrollerModule,
    InfiniteScrollModule,
    MentionModule,
    InfiniteScrollModule

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
    ChooseRoleDialogComponent,
    TeamAssignComponent,
    SaveStringConfirmComponent,
    GlossaryCreateDialogComponent,
    GlossaryEditDialogComponent,
    GlossaryStringDialogComponent,
    SaveStringConfirmComponent,
    SelectColorDialogComponent,
    TeamAddMemberComponent,
    ChooseProficiencyDialogComponent,
    AddRemoveLanguagesDialogComponent

  ],
  providers: [HttpService, AuthService, AuthGuard,
    {
      provide: 'SnotifyToastConfig',
      useValue: SnotifyGlobalConfig
    },
    SnotifyService, GlossaryService, TranslatorGuardService],

  bootstrap: [AppComponent]

})
export class AppModule {
    constructor(private dateAdapter: DateAdapter<Date>) {
        dateAdapter.setLocale("en-in"); // DD/MM/YYYY
    }
}
