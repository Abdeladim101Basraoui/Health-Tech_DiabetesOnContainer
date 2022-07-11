import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NbAuthComponent, NbLoginComponent, NbLogoutComponent, NbRegisterComponent, NbRequestPasswordComponent, NbResetPasswordComponent } from '@nebular/auth';
import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { LogComponent } from './log/log.component';
import { MainUIComponent } from './main-ui/main-ui.component';

const routes: Routes = [
  {
    path: 'auth',
    component: NbAuthComponent,
    children: [
      {
        path: '',
        component: NbLoginComponent,
      },
      {
        path: 'login',
        component: NbLoginComponent,
      },
      {
        path: 'register',
        component: NbRegisterComponent,
      },
      {
        path: 'logout',
        component: NbLogoutComponent,
      },
      {
        path: 'request-password',
        component: NbRequestPasswordComponent,
      },
      {
        path: 'reset-password',
        component: NbResetPasswordComponent,
      }
    ]
    }
    ,
    {
      path:'test',
      component:LogComponent
    }
    ,
    { 
      path:'',
      component:MainUIComponent
    }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
