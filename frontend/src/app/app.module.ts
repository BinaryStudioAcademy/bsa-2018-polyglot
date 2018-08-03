import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';

import { LandingComponent } from './landing.component';
import { NavigationComponent } from './components/navigation/navigation.component';

import { DataService } from './services/data.service';
import { AppMaterialModule } from './common/app-material/app-material.module';


@NgModule({
  declarations: [
    LandingComponent,
    NavigationComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    AppMaterialModule,
    HttpClientModule,
    
  ],
  providers: [DataService],
  bootstrap: [NavigationComponent]
})
export class AppModule { }
