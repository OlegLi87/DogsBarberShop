import { NotFoundPageComponent } from './../../components/shared/not-found-page/not-found-page.component';
import { MainPageComponent } from '../../components/main/main-page/main-page.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginAndMainPageGuardService } from 'src/app/infastructure/routeGuards/loginAndMainPageGuard.service';

const routes: Routes = [
  {
    path: 'main',
    component: MainPageComponent,
    canActivate: [LoginAndMainPageGuardService],
  },
  {
    path: 'login',
    loadChildren: () =>
      import('../auth/auth.module').then((mod) => mod.AuthModule),
    canLoad: [LoginAndMainPageGuardService],
  },
  { path: '', redirectTo: 'main', pathMatch: 'full' },
  { path: '**', component: NotFoundPageComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class RoutingModule {}
