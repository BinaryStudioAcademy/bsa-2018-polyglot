import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { ProjectsComponent } from 'src/app/components/projects/projects.component';
import { TeamsComponent } from 'src/app/components/teams/teams.component';
import { GlossariesComponent } from 'src/app/components/glossaries/glossaries.component';
import { DashboardComponent } from 'src/app/components/dashboard/dashboard.component';
import { NoFoundComponent } from 'src/app/components/no-found/no-found.component';


const routes: Routes = [
  { path: '', redirectTo: '/', pathMatch: 'full' },
  {
    path: 'Dashboard', component: DashboardComponent,
    children: [
      { path: 'Projects', component: ProjectsComponent },
      { path: 'Teams', component: TeamsComponent },
      { path: 'Glossaries', component: GlossariesComponent },
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
