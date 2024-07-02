import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './components/auth.guard';
import { LoginComponent } from './components/login/login.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { AddproductComponent } from './components/addproduct/addproduct.component';

const routes: Routes = [
  {path:'dashboard', component:DashboardComponent, canActivate:[AuthGuard]},
  {path:'login',component:LoginComponent},
  {path:'addproduct',component:AddproductComponent},
  {path:'',pathMatch:'full',redirectTo:'login'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
