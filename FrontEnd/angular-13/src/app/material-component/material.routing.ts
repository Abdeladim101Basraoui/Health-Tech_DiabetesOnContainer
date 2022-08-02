import { Routes } from '@angular/router';

import { ButtonsComponent } from './buttons/buttons.component';
import { GridComponent } from './grid/grid.component';
import { ListsComponent } from './lists/lists.component';
import { MenuComponent } from './menu/menu.component';
import { TabsComponent } from './tabs/tabs.component';
import { StepperComponent } from './stepper/stepper.component';
import { ExpansionComponent } from './expansion/expansion.component';
import { ChipsComponent } from './chips/chips.component';
import { ToolbarComponent } from './toolbar/toolbar.component';
import { ProgressSnipperComponent } from './progress-snipper/progress-snipper.component';
import { ProgressComponent } from './progress/progress.component';
import { DialogComponent } from './dialog/dialog.component';
import { TooltipComponent } from './tooltip/tooltip.component';
import { SnackbarComponent } from './snackbar/snackbar.component';
import { SliderComponent } from './slider/slider.component';
import { SlideToggleComponent } from './slide-toggle/slide-toggle.component';
import { ConsultationComponent } from './consultation/consultation.component';
import { GestionPatientComponent } from './gestion-patient/gestion-patient.component';
import { SuivieComponent } from './suivie/suivie.component';
import { ComplicationComponent } from './complication/complication.component';
import { AuthGuard } from '../_helper/auth.guard';
import { Component } from '@angular/core';
import { ConsultDetailsComponent } from './consultation/consult-details/consult-details.component';

export const MaterialRoutes: Routes = [
  {
    path: 'button',
    component: ButtonsComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'grid',
    component: GridComponent,
    canActivate: [AuthGuard]

  },
  {
    path: 'lists',
    component: ListsComponent,
    canActivate: [AuthGuard]

  },
  {
    path: 'menu',
    component: MenuComponent,
    canActivate: [AuthGuard]

  },
  {
    path: 'tabs',
    component: TabsComponent,
    canActivate: [AuthGuard]

  },
  {
    path: 'stepper',
    component: StepperComponent,
    canActivate: [AuthGuard]

  },
  {
    path: 'expansion',
    component: ExpansionComponent,
    canActivate: [AuthGuard]

  },
  {
    path: 'chips',
    component: ChipsComponent,
    canActivate: [AuthGuard]

  },
  {
    path: 'toolbar',
    component: ToolbarComponent,
    canActivate: [AuthGuard]

  },
  {
    path: 'progress-snipper',
    component: ProgressSnipperComponent,
    canActivate: [AuthGuard]

  },
  {
    path: 'progress',
    component: ProgressComponent,
    canActivate: [AuthGuard]

  },
  {
    path: 'dialog',
    component: DialogComponent,
    canActivate: [AuthGuard]

  },
  {
    path: 'tooltip',
    component: TooltipComponent,
    canActivate: [AuthGuard]

  },
  {
    path: 'snackbar',
    component: SnackbarComponent,
    canActivate: [AuthGuard]

  },
  {
    path: 'slider',
    component: SliderComponent,
    canActivate: [AuthGuard]

  },
  {
    path: 'slide-toggle',
    component: SlideToggleComponent,
    canActivate: [AuthGuard]

  }
  ,
  {
    path: 'consultation',
    component: ConsultationComponent,
    canActivate: [AuthGuard]

  }
  ,
  {
    path: 'gestionpatient',
    component: GestionPatientComponent,
    canActivate: [AuthGuard]

  }
  ,
  {
    path: 'suivie',
    component: SuivieComponent,
    canActivate: [AuthGuard]

  }
  ,
  {
    path: 'complication',
    component: ComplicationComponent,
    canActivate: [AuthGuard]

  },
  {
    path: 'consult-details',
    component: ConsultDetailsComponent,
    canActivate: [AuthGuard]
  }
];
