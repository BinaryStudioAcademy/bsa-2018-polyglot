import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';


import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';

import { LandingComponent } from './landing.component';
import { NavigationComponent } from './components/navigation/navigation.component';

import { DataService } from './services/data.service';
import { AppMaterialModule } from './common/app-material/app-material.module';
import { ProjectsComponent } from './components/projects/projects.component';
import { TeamsComponent } from './components/teams/teams.component';
import { GlossariesComponent } from './components/glossaries/glossaries.component';
import { AppRoutingModule } from 'src/app/common/app-routing-module/app-routing.module';
import { DashboardComponent } from './components/dashboard/dashboard.component';



@NgModule({
  declarations: [
    LandingComponent,
    NavigationComponent,
    ProjectsComponent,
    TeamsComponent,
    GlossariesComponent,
    DashboardComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    AppMaterialModule,
    HttpClientModule,
    AppRoutingModule
    
  ],
  providers: [DataService],
  bootstrap: [NavigationComponent]
})
export class AppModule { }
