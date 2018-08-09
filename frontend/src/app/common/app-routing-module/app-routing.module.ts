import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { ProjectsComponent } from '../../components/projects/projects.component';
import { TeamsComponent } from '../../components/teams/teams.component';
import { GlossariesComponent } from '../../components/glossaries/glossaries.component';
import { DashboardComponent } from '../../components/dashboard/dashboard.component';
import { NoFoundComponent } from '../../components/no-found/no-found.component';
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
  
const routes: Routes = [
  { path: '',  canActivate: [LandingGuard], component: LandingComponent },
  { path: 'about-us', component: AboutUsComponent },
  { path: 'contact', component: ContactComponent },
  { path: 'profile', component: ManagerProfileComponent},
  { path: 'profile/newproject', component: NewProjectComponent },
  { path: 'profile/settings', component: UserSettingsComponent },

  {
    path: 'dashboard',
    canActivate: [AuthGuard],
    component: DashboardComponent,
    children: [
      { path: '', redirectTo: '/dashboard/projects', pathMatch: 'full' },
      { path: 'projects', component: ProjectsComponent },
      { path: 'teams', component: TeamsComponent },
      { path: 'glossaries', component: GlossariesComponent },
      { path: 'newproject', component: NewProjectComponent },
      { path: 'strings', component: NoFoundComponent },
    ]
  },
  {
    path: 'workspace/:projectId',
    component: WorkspaceComponent,
    children: [
      {
        path: 'key/:keyId',
        component: KeyDetailsComponent
      }
    ]
  },
  { path: 'translator', component: TranslatorProfileComponent },
  { path: '404', component: NoFoundComponent },
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
