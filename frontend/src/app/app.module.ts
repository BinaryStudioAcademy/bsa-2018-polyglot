import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatFormFieldModule } from '@angular/material/form-field';
import {
  MatToolbarModule,
  MatButtonModule,
  MatSidenavModule,
  MatIconModule,
  MatListModule,
  MatSelectModule,
  MatInputModule,
  MatAutocompleteModule,
  MatBadgeModule,
  MatCardModule,
  MatDatepickerModule,
  MatGridListModule,
  MatMenuModule,
  MatNativeDateModule,
  MatTableModule,
  MatTabsModule,
  MatTooltipModule,

} from '@angular/material';

import { LayoutModule } from '@angular/cdk/layout';
import { HttpClientModule } from '@angular/common/http';

import { LandingComponent } from './landing.component';
import { NavigationComponent } from './components/navigation/navigation.component';

import { UserService } from './services/user.service';
import { LoginDialogComponent } from './dialogs/login-dialog/login-dialog.component';


@NgModule({
  declarations: [
    LandingComponent,
    NavigationComponent,
    LoginDialogComponent
  ],
  imports: [

    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    MatSelectModule,
    MatInputModule,
    MatAutocompleteModule,
    MatBadgeModule,
    MatCardModule,
    MatDatepickerModule,
    MatGridListModule,
    MatMenuModule,
    MatNativeDateModule,
    MatTableModule,
    MatTabsModule,
    MatTooltipModule,
    MatFormFieldModule,
    HttpClientModule,


  ],
  entryComponents: [LoginDialogComponent],
  providers: [UserService],
  bootstrap: [LandingComponent]
})
export class AppModule { }
