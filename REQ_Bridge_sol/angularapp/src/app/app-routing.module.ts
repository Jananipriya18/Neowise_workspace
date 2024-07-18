import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ErrorComponent } from './components/error/error.component';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { HomeComponent } from './components/home/home.component';
import { AuthGuard } from './components/authguard/auth.guard';
import { ManagerViewProjectComponent } from './components/manager-view-project/manager-view-project.component';
import { ManagerAddProjectComponent } from './components/manager-add-project/manager-add-project.component';
import { ManagerEditProjectComponent } from './components/manager-edit-project/manager-edit-project.component';
import { ManagerviewfeedbackComponent } from './components/managerviewfeedback/managerviewfeedback.component';
import { AnalystViewRequirementComponent } from './components/analyst-view-requirement/analyst-view-requirement.component';
import { AnalystAddRequirementComponent } from './components/analyst-add-requirement/analyst-add-requirement.component';
import { AnalystViewProjectComponent } from './components/analyst-view-project/analyst-view-project.component';
import { AnalystviewfeedbackComponent } from './components/analystviewfeedback/analystviewfeedback.component';
import { AnalystaddfeedbackComponent } from './components/analystaddfeedback/analystaddfeedback.component';
import { ManagerViewRequirementComponent } from './components/manager-view-requirement/manager-view-requirement.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'login', component: LoginComponent },
  {path: 'signup', component: RegistrationComponent },
  {path: 'error', component: ErrorComponent },

  {path :'analyst/add/feedback', component: AnalystaddfeedbackComponent, canActivate: [AuthGuard]},
  {path :'analyst/view/feedback', component: AnalystviewfeedbackComponent, canActivate: [AuthGuard]},
  {path: 'analyst/view/project', component: AnalystViewProjectComponent, canActivate: [AuthGuard]},
  {path: 'analyst/view/requirement', component: AnalystViewRequirementComponent, canActivate: [AuthGuard]},
  {path: 'analyst/add/requirement', component: AnalystAddRequirementComponent, canActivate: [AuthGuard]},

  {path: 'manager/view/requirement', component: ManagerViewRequirementComponent, canActivate: [AuthGuard]},
  {path: 'manager/view/project', component: ManagerViewProjectComponent, canActivate: [AuthGuard]},
  {path: 'manager/add/project', component: ManagerAddProjectComponent, canActivate: [AuthGuard]},
  {path: 'manager/edit/project/:id', component: ManagerEditProjectComponent, canActivate: [AuthGuard]},
  {path :'manager/view/feedback', component: ManagerviewfeedbackComponent, canActivate: [AuthGuard]},


  { path: '**', redirectTo: '/error' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
