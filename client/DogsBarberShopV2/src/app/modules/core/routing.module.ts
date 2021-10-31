import { NotFoundPageComponent } from './../../components/shared/not-found-page/not-found-page.component';
import { MainPageComponent } from '../../components/main/main-page/main-page.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', component: MainPageComponent },
  {
    path: 'login',
    loadChildren: () =>
      import('../auth/auth.module').then((mod) => mod.AuthModule),
  },
  { path: '**', component: NotFoundPageComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class RoutingModule {}
