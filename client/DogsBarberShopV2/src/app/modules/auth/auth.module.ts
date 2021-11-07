import { ResetPasswordComponent } from './../../components/auth/reset-password/reset-password.component';
import { EmailConfirmedComponent } from './../../components/auth/email-confirmed/email-confirmed.component';
import { LoadSpinnerComponent } from './../../components/shared/load-spinner/load-spinner.component';
import { LoginPageComponent } from './../../components/auth/login-page/login-page.component';
import { LoginFormComponent } from './../../components/auth/login-form/login-form.component';
import { NgModule } from '@angular/core';
import { RoutingModule } from './routing.module';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { TaigaUiModule } from '../taigaui/taigaui.module';

const inOutModules = [RoutingModule];

@NgModule({
  declarations: [
    LoginPageComponent,
    LoginFormComponent,
    EmailConfirmedComponent,
    ResetPasswordComponent,
    LoadSpinnerComponent,
  ],
  imports: [...inOutModules, CommonModule, ReactiveFormsModule, TaigaUiModule],
  exports: [...inOutModules],
})
export class AuthModule {}
