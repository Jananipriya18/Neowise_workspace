import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ErrorComponent } from './components/error/error.component';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';

import { HomeComponent } from './components/home/home.component';
import { AuthGuard } from './components/authguard/auth.guard';

import { AdminviewfeedbackComponent } from './components/adminviewfeedback/adminviewfeedback.component';
import { UserviewfeedbackComponent } from './components/userviewfeedback/userviewfeedback.component';
import { UseraddfeedbackComponent } from './components/useraddfeedback/useraddfeedback.component';
import { AdminaddcollegeComponent } from './components/adminaddcollege/adminaddcollege.component';
import { AdminviewcollegeComponent } from './components/adminviewcollege/adminviewcollege.component';
import { AdmineditcollegeComponent } from './components/admineditcollege/admineditcollege.component';
import { UserviewcollegeComponent } from './components/userviewcollege/userviewcollege.component';
import { UserapplicationformComponent } from './components/userapplicationform/userapplicationform.component';
import { UserappliedcollegeComponent } from './components/userappliedcollege/userappliedcollege.component';
import { RequestedcollegeComponent } from './components/requestedcollege/requestedcollege.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'login', component: LoginComponent },
  {path: 'signup', component: RegistrationComponent },
  {path: 'error', component: ErrorComponent },
  {path :'admin/view/feedback', component: AdminviewfeedbackComponent, canActivate: [AuthGuard]},
  {path :'user/view/feedback', component: UserviewfeedbackComponent, canActivate: [AuthGuard]},
  {path :'user/add/feedback', component: UseraddfeedbackComponent, canActivate: [AuthGuard]},


  {path: 'admin/add/college', component: AdminaddcollegeComponent},
  {path: 'admin/view/college', component: AdminviewcollegeComponent},
  {path: 'admin/editcollege/:id', component: AdmineditcollegeComponent},
  {path: 'user/view/college', component: UserviewcollegeComponent},
  {path: 'user/apply/college', component: UserapplicationformComponent},
  {path: 'user/view/application', component: UserappliedcollegeComponent},
  {path: 'admin/view/requested', component: RequestedcollegeComponent},

  { path: '**', redirectTo: '/error' },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
