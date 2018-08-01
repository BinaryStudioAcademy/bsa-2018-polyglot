import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { LandingComponent } from './landing.component';
import { DataService } from './services/data.service';
import { NavigationComponent } from './components/navigation/navigation.component';

@NgModule({
  declarations: [
    LandingComponent,
    NavigationComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [DataService],
  bootstrap: [LandingComponent]
})
export class AppModule { }
