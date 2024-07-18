import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { ErrorComponent } from './components/error/error.component';
import { HttpClientModule } from '@angular/common/http';
import { NavbarComponent } from './components/navbar/navbar.component';
import { CommonModule, DatePipe } from '@angular/common';

import { ManagerViewProjectComponent } from './components/manager-view-project/manager-view-project.component';
import { ManagerAddProjectComponent } from './components/manager-add-project/manager-add-project.component';
import { ManagerEditProjectComponent } from './components/manager-edit-project/manager-edit-project.component';
import { ManagernavComponent } from './components/managernav/managernav.component';
import { ManagerviewfeedbackComponent } from './components/managerviewfeedback/managerviewfeedback.component';
import { AnalystViewProjectComponent } from './components/analyst-view-project/analyst-view-project.component';
import { AnalystAddRequirementComponent } from './components/analyst-add-requirement/analyst-add-requirement.component';
import { AnalystViewRequirementComponent } from './components/analyst-view-requirement/analyst-view-requirement.component';
import { AnalystaddfeedbackComponent } from './components/analystaddfeedback/analystaddfeedback.component';
import { AnalystviewfeedbackComponent } from './components/analystviewfeedback/analystviewfeedback.component';
import { AnalystnavComponent } from './components/analystnav/analystnav.component';
import { ManagerViewRequirementComponent } from './components/manager-view-requirement/manager-view-requirement.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegistrationComponent,
    ErrorComponent,
    NavbarComponent,
    ManagerViewProjectComponent,
    ManagerAddProjectComponent,
    ManagerEditProjectComponent,
    ManagernavComponent,
    ManagerviewfeedbackComponent,
    AnalystViewProjectComponent,
    AnalystAddRequirementComponent,
    AnalystViewRequirementComponent,
    AnalystaddfeedbackComponent,
    AnalystviewfeedbackComponent,
    AnalystnavComponent,
    ManagerViewRequirementComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    CommonModule
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
