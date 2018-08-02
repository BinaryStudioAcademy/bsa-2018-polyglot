import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { LandingComponent } from './landing.component';
import { DataService } from './services/data.service';
import { NavigationComponent } from './components/navigation/navigation.component';

@NgModule({
  declarations: [
    LandingComponent,
    NavigationComponent
  ],
  imports: [
    HttpClientModule,
    BrowserModule
  ],
  providers: [DataService],
  bootstrap: [LandingComponent]
})
export class AppModule { }
