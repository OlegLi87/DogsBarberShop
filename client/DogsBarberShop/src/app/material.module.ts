import { NgModule } from '@angular/core';

import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';

import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';

const modules = [
  MatButtonModule,
  MatCardModule,
  MatFormFieldModule,
  MatInputModule,
  MatIconModule,
  NgxMaterialTimepickerModule,
  MatDatepickerModule,
  MatNativeDateModule,
];

@NgModule({
  imports: modules,
  exports: modules,
})
export class MaterialModule {}
