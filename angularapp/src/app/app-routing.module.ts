import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FeedbackComponent } from './components/feedback/feedback.component';
// Import your flight components here
import { FlightListComponent } from './components/flight-list/flight-list.component';
import { FlightBookingComponent } from './components/flight-booking/flight-booking.component';

const routes: Routes = [
  { path: '', redirectTo: '/flights', pathMatch: 'full' },
  { path: 'flights', component: FlightListComponent },
  { path: 'flight/:id', component: FlightBookingComponent },
  { path: 'feedback', component: FeedbackComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
