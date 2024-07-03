import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CustomerloginComponent } from './components/customerlogin/customerlogin.component';
import { AuthGuard } from './components/authguard/auth.guard';
import { DashboardComponent } from './components/dashboard/dashboard.component';

const routes: Routes = [
  { path: '', redirectTo: '/customerlogin', pathMatch: 'full' }, 
  { path: 'customerlogin', component: CustomerloginComponent },
  { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] }
  ];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
