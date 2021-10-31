import { LoginPageComponent } from './../../components/auth/login-page/login-page.component';
import { LoginFormComponent } from './../../components/auth/login-form/login-form.component';
import { NgModule } from '@angular/core';
import { RoutingModule } from './routing.module';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { TaigaUiModule } from '../taigaui/taigaui.module';

const inOutModules = [RoutingModule];

@NgModule({
  declarations: [LoginPageComponent, LoginFormComponent],
  imports: [...inOutModules, CommonModule, ReactiveFormsModule, TaigaUiModule],
  exports: [...inOutModules],
})
export class AuthModule {}
