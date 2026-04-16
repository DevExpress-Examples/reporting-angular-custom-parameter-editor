import { Routes } from '@angular/router';
import { Home } from './home/home';

export const routes: Routes = [
  { path: '', component: Home, pathMatch: 'full' },
  { path: 'DocumentViewer', loadComponent: () => import('./reportviewer/report-viewer').then(m => m.ReportViewer) },
  { path: 'CustomInput', loadComponent: () => import('./custominputcomponent/custom-input-component').then(m => m.CustomInput) }
];