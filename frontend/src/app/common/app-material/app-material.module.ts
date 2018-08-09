import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LayoutModule } from '@angular/cdk/layout';

import { MatFormFieldModule } from '@angular/material/form-field';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MdePopoverModule } from '@material-extended/mde';
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
  MatProgressBarModule,
  MatProgressSpinnerModule,

} from '@angular/material';

import { FlexLayoutModule } from '@angular/flex-layout';

import { FormsModule, ReactiveFormsModule  } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
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
    MatProgressBarModule,
    MatProgressSpinnerModule,
    
    FlexLayoutModule,
    FormsModule, 
    ReactiveFormsModule, 

    MdePopoverModule,
    BrowserAnimationsModule
  ],
  exports: [
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
    MatProgressBarModule,
    MatProgressSpinnerModule,
    
    FlexLayoutModule,
    FormsModule,
    ReactiveFormsModule,

    MdePopoverModule,
    BrowserAnimationsModule
  ]
})
export class AppMaterialModule { }
