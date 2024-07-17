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
import { CommonModule } from '@angular/common';
import { AdminviewfeedbackComponent } from './components/adminviewfeedback/adminviewfeedback.component';
import { UserviewfeedbackComponent } from './components/userviewfeedback/userviewfeedback.component';
import { UseraddfeedbackComponent } from './components/useraddfeedback/useraddfeedback.component';
import { AdminnavComponent } from './components/adminnav/adminnav.component';
import { UsernavComponent } from './components/usernav/usernav.component';

import { MydonationComponent } from './components/mydonation/mydonation.component';
import { ViewalldonationComponent } from './components/viewalldonation/viewalldonation.component';
import { AdminvieworphanageComponent } from './components/adminvieworphanage/adminvieworphanage.component';
import { CreateorphanageComponent } from './components/createorphanage/createorphanage.component';
import { UservieworphanageComponent } from './components/uservieworphanage/uservieworphanage.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegistrationComponent,
    ErrorComponent,

    NavbarComponent,

    AdminviewfeedbackComponent,
    UserviewfeedbackComponent,
    UseraddfeedbackComponent,
    AdminnavComponent,
    UsernavComponent,
    MydonationComponent,
    ViewalldonationComponent,
    AdminvieworphanageComponent,
    CreateorphanageComponent,
    UservieworphanageComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    CommonModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
