import { Routes } from '@angular/router';
import { AuthGuard } from '../_helper/auth.guard';

import { DashboardComponent } from './dashboard.component';

export const DashboardRoutes: Routes = [{
  path: '',
  component: DashboardComponent,
  canActivate:[AuthGuard]
}];
