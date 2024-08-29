import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FeedbackComponent } from './components/feedback/feedback.component';
import { FlightListComponent } from './components/flight-list/flight-list.component';
import { FlightBookingComponent } from './components/flight-booking/flight-booking.component';


@NgModule({
  declarations: [
    AppComponent,
    FeedbackComponent,
    FlightListComponent,
    FlightBookingComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
