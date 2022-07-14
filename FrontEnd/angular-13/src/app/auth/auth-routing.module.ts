import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FullComponent } from '../layouts/full/full.component';
import { AuthGuard } from '../_helper/auth.guard';
import { RoleAccessGuard } from '../_helper/role-access.guard';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';

const routes: Routes = [
 { path: '',
  component: FullComponent,
  children: [
    {
      path: '',
      redirectTo: '/dashboard',
      pathMatch: 'full'
    },
    {
      path: '',
      loadChildren:
        () => import('../material-component/material.module').then(m => m.MaterialComponentsModule)
    },
    {
      path: 'dashboard',
      loadChildren: () => import('../dashboard/dashboard.module').then(m => m.DashboardModule)
    }
  ]/*,canActivate:[AuthGuard]*/
},
  {path:'login',
  component:LoginComponent
},
  {path:'register',
component:RegisterComponent,canActivate:[RoleAccessGuard]
}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule { }
