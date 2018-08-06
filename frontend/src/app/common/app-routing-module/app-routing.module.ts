import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { ProjectsComponent } from '../../components/projects/projects.component';
import { TeamsComponent } from '../../components/teams/teams.component';
import { GlossariesComponent } from '../../components/glossaries/glossaries.component';
import { DashboardComponent } from '../../components/dashboard/dashboard.component';
import { NoFoundComponent } from '../../components/no-found/no-found.component';
import { LandingComponent } from '../../components/landing/landing.component';
import { HomeComponent } from '../../components/landing/home/home.component';
import { AboutUsComponent } from '../../components/landing/about-us/about-us.component';
import { ContactComponent } from '../../components/landing/contact/contact.component';
import { AuthGuard } from '../../services/auth-guard.service';
import { LandingGuard } from '../../components/landing/landing.guard.service';

const routes: Routes = [
  { // TODO landing routes should be extracted to separate routing module. 
    path: '', 
    component: LandingComponent,
    
    children: [
      { path: '', component: HomeComponent },
      { path: 'about-us', component: AboutUsComponent },
      { path: 'contact', component: ContactComponent },
    ]
  },
  {
    path: 'dashboard',
    canActivate: [LandingGuard],
    component: DashboardComponent,
    children: [
      { path: '', redirectTo: '/dashboard/projects', pathMatch: 'full' },
      { path: 'projects', component: ProjectsComponent },
      { path: 'teams', component: TeamsComponent },
      { path: 'glossaries', component: GlossariesComponent },
      { path: 'strings', component: NoFoundComponent },
    ]
  },
  {path: '404', component: NoFoundComponent},
  {path: '**', redirectTo: '/404'}
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
