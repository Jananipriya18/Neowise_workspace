import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './components/auth.guard';
import { RegisterComponent } from './components/register/register.component'; 
import { DashboardComponent } from './components/dashboard/dashboard.component';

const routes: Routes = [
  {path:'dashboard', component:DashboardComponent, canActivate:[AuthGuard]},
  {path:'register',component:RegisterComponent},
  {path:'',pathMatch:'full',redirectTo:'login'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
