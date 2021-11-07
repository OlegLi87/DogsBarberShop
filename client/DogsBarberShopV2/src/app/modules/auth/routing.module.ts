import { ResetPasswordGuardService } from './../../infastructure/routeGuards/resetPasswordGuard.service';
import { ResetPasswordComponent } from './../../components/auth/reset-password/reset-password.component';
import { EmailConfirmedGuardService } from './../../infastructure/routeGuards/emailConfirmedGuard.service';
import { EmailConfirmedComponent } from './../../components/auth/email-confirmed/email-confirmed.component';
import { LoginPageComponent } from './../../components/auth/login-page/login-page.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    component: LoginPageComponent,
  },
  {
    path: 'emailConfirmed',
    component: EmailConfirmedComponent,
    canActivate: [EmailConfirmedGuardService],
  },
  {
    path: 'resetPassword',
    component: ResetPasswordComponent,
    canActivate: [ResetPasswordGuardService],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class RoutingModule {}
