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
import { ViewalldonationComponent } from './components/viewalldonation/viewalldonation.component';
import { MydonationComponent } from './components/mydonation/mydonation.component';
import { UservieworphanageComponent } from './components/uservieworphanage/uservieworphanage.component';
import { CreateorphanageComponent } from './components/createorphanage/createorphanage.component';
import { AdminvieworphanageComponent } from './components/adminvieworphanage/adminvieworphanage.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'login', component: LoginComponent },
  {path: 'signup', component: RegistrationComponent },
  {path: 'error', component: ErrorComponent },
  {path :'admin/view/feedback', component: AdminviewfeedbackComponent},
  {path :'user/view/feedback', component: UserviewfeedbackComponent},
  {path :'user/add/feedback', component: UseraddfeedbackComponent},




{path :'admin/add/orphanage', component: CreateorphanageComponent},
{path :'admin/view/orphanage', component:AdminvieworphanageComponent},

{path :'admin/view/donation', component:ViewalldonationComponent},

{path :'admin/edit/orphanage/:id', component: CreateorphanageComponent},


{path :'user/view/orphanage', component:UservieworphanageComponent},
{path :'user/view/donation', component: MydonationComponent},


  { path: '**', redirectTo: '/error' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
