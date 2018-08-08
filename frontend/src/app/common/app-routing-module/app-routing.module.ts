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
import { TranslatorProfileComponent } from '../../components/translatorProfile/translator-profile/translator-profile.component';

import { NewProjectComponent } from '../../components/new-project/new-project.component';
import { ManagerComponent } from '../../components/manager/manager.component';
import { ManagerProfileComponent } from '../../components/manager-profile/manager-profile.component';
import { LandingGuard } from '../../services/guards/landing-guard.service';
  
const routes: Routes = [
  { path: '',  canActivate: [LandingGuard], component: LandingComponent },
  { path: 'about-us', component: AboutUsComponent },
  { path: 'contact', component: ContactComponent },
  { path: 'newproject', component: NewProjectComponent },

  {
    path: 'dashboard',
    canActivate: [AuthGuard],
    component: DashboardComponent,
    children: [
      { path: '', redirectTo: '/dashboard/projects', pathMatch: 'full' },
      { path: 'projects', component: ProjectsComponent },
      { path: 'teams', component: TeamsComponent },
      { path: 'glossaries', component: GlossariesComponent },
      { path: 'strings', component: NoFoundComponent },
    ]
  },
  { path: 'translator', component: TranslatorProfileComponent },
  { path: 'manager', component: ManagerProfileComponent },
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
