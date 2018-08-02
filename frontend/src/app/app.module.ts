import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {
  MatButtonModule,
  MatToolbarModule,
  MatSidenavModule,
  MatIconModule,
  MatListModule

} from '@angular/material';

import { LayoutModule } from '@angular/cdk/layout';

import { LandingComponent } from './landing.component';
import { NavigationComponent } from './components/navigation/navigation.component';

import { DataService } from './services/data.service';


@NgModule({
  declarations: [
    LandingComponent,
    NavigationComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    
    
  ],
  providers: [DataService],
  bootstrap: [NavigationComponent]
})
export class AppModule { }
