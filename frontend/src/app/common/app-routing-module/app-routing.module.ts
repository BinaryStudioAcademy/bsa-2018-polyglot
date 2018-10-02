import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { ProjectsComponent } from '../../components/projects/projects.component';
import { TeamsComponent } from '../../components/teams/teams.component';
import { GlossariesComponent } from '../../components/glossaries/glossaries.component';
import { DashboardComponent } from '../../components/dashboard/dashboard.component';
import { LandingComponent } from '../../components/landing/landing.component';

import { AuthGuard } from '../../services/guards/auth-guard.service';
import { AboutUsComponent } from '../../components/about-us/about-us.component';
import { ContactComponent } from '../../components/contact/contact.component';
import { WorkspaceComponent } from '../../components/workspace/workspace.component';
import { KeyDetailsComponent } from '../../components/workspace/key-details/key-details.component';
import { TranslatorProfileComponent } from '../../components/translatorProfile/translator-profile/translator-profile.component';

import { NewProjectComponent } from '../../components/new-project/new-project.component';
import { ManagerProfileComponent } from '../../components/manager-profile/manager-profile.component';
import { LandingGuard } from '../../services/guards/landing-guard.service';
import { UserSettingsComponent } from '../../components/user-settings/user-settings.component';
import { ProjectDetailsComponent } from '../../components/project-details/project-details.component';
import { TeamDetailsComponent } from '../../components/team-details/team-details.component';
import { NewTeamComponent } from '../../components/teams/new-team/new-team.component';
import { GlossaryComponent } from '../../components/glossaries/glossary/glossary.component';
import { UserProfileComponent } from '../../components/user-profile/user-profile.component';
import { ChatComponent } from '../../components/chat/chat.component';
import { TranslatorGuardService } from '../../services/guards/translator-guard.service';
import { ChatWindowComponent } from '../../components/chat/chat-window/chat-window.component';
import { GuidelineComponent } from '../../components/guideline/guideline.component';
import { ForbiddenComponent } from '../../components/errors/forbidden/forbidden.component';
import { NoFoundComponent } from '../../components/errors/no-found/no-found.component';
import { InternalserverComponent } from '../../components/errors/internalserver/internalserver.component';


  
const routes: Routes = [
  { path: '',  canActivate: [LandingGuard], component: LandingComponent },
  { path: 'about-us', component: AboutUsComponent },
  { path: 'contact', component: ContactComponent },
  { path: 'profile', canActivate: [AuthGuard], component: UserProfileComponent},
  { path: 'newproject', canActivate: [AuthGuard, TranslatorGuardService], component: NewProjectComponent },
  { path: 'newteam', canActivate: [AuthGuard, TranslatorGuardService], component: NewTeamComponent },
  { path: 'profile/settings', canActivate: [AuthGuard], component: UserSettingsComponent },
  { path: 'guideline', canActivate: [AuthGuard], component: GuidelineComponent},

  {
    path: 'dashboard',
    canActivate: [AuthGuard],
    component: DashboardComponent,
    children: [
      { path: '', redirectTo: '/dashboard/projects', pathMatch: 'full' },
      { path: 'projects', component: ProjectsComponent },
      { path: 'teams', component: TeamsComponent },
      { path: 'glossaries', canActivate: [TranslatorGuardService], component: GlossariesComponent },
      { path: 'glossaries/:glossaryId', canActivate: [TranslatorGuardService], component: GlossaryComponent },
      { path: 'strings', component: NoFoundComponent }
    ]
  },
  { path: 'team/details/:teamId', canActivate: [AuthGuard], component: TeamDetailsComponent},
  { path: 'project/details/:projectId', canActivate: [AuthGuard], component: ProjectDetailsComponent },
  {
    path: 'workspace/:projectId',
    canActivate: [AuthGuard],
    component: WorkspaceComponent,
    children: [{
        path: 'key/:keyId',
        component : KeyDetailsComponent
      }
    ]
  },
  { path: 'chat', canActivate: [AuthGuard], component: ChatComponent },
  { path: 'user/:userId', canActivate: [AuthGuard], component: UserProfileComponent },
  { path: 'profile', canActivate: [AuthGuard], component: UserProfileComponent },
  { path: 'forbidden', component: ForbiddenComponent},
  { path: 'not-found', component: NoFoundComponent },
  { path: 'internal-server-error', component: InternalserverComponent },
  { path: '**', redirectTo: '/404' }
];


@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
