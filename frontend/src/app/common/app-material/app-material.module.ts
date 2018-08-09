import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LayoutModule } from '@angular/cdk/layout';

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
  MatProgressBarModule,
  MatProgressSpinnerModule,
  MatChipsModule,
  MatPaginatorModule



} from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';

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
    MatChipsModule

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
    MatChipsModule,
    MatPaginatorModule,
    MatTableModule
  ]
})
export class AppMaterialModule { }
