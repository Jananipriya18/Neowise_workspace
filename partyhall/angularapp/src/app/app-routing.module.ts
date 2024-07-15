import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './components/authguard/auth.guard';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { ErrorComponent } from './components/error/error.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { AdminViewBookingComponent } from './components/admin-view-booking/admin-view-booking.component';
import { CustomerViewBookingComponent } from './components/customer-view-booking/customer-view-booking.component';
import { AddBookingComponent } from './components/add-booking/add-booking.component';
import { AddReviewComponent } from './components/add-review/add-review.component';
import { AdminViewReviewComponent } from './components/admin-view-review/admin-view-review.component';
import { CustomerViewReviewComponent } from './components/customer-view-review/customer-view-review.component';
import { AddPartyhallComponent } from './components/add-partyhall/add-partyhall.component';
import { AdminViewPartyHallComponent } from './components/admin-view-party-hall/admin-view-party-hall.component';
import {CustomerViewPartyHallComponent} from './components/customer-view-party-hall/customer-view-party-hall.component'
const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'signup', component: RegistrationComponent },
  { path: 'admin/view/review', component: AdminViewReviewComponent, canActivate: [AuthGuard]},
  { path: 'admin/view/bookings', component: AdminViewBookingComponent, canActivate: [AuthGuard]},
 
 
  { path: 'admin/add/partyhall', component: AddPartyhallComponent},
  { path: 'admin/view/partyhall', component: AdminViewPartyHallComponent},
 
 
 
 
  { path: 'customer/view/partyhall', component:CustomerViewPartyHallComponent, canActivate: [AuthGuard]},

 
  { path: 'customer/add/booking/:id', component: AddBookingComponent, canActivate: [AuthGuard]},
  { path: 'customer/view/bookings', component: CustomerViewBookingComponent, canActivate: [AuthGuard]},
    { path: 'customer/add/review', component: AddReviewComponent, canActivate: [AuthGuard]},
    { path: 'customer/view/review', component: CustomerViewReviewComponent, canActivate: [AuthGuard]},
  { path: 'error', component: ErrorComponent, data: { message: 'Oops! Something went wrong.' }},
  { path: '**', redirectTo: '/error', pathMatch: 'full' },
];
//
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
