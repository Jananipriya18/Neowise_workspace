import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HotelListComponent } from './components/hotel-list/hotel-list.component';
import { HotelDetailComponent } from './components/hotel-detail/hotel-detail.component';
import { FeedbackComponent } from './components/feedback/feedback.component';
import { FlightListComponent } from './flight-list/flight-list.component';
import { GlightDetailComponent } from './glight-detail/glight-detail.component';
import { FlightBookingComponent } from './flight-detail/flight-detail.component';
import { FlightBookingComponent } from './flight-booking/flight-booking.component';

@NgModule({
  declarations: [
    AppComponent,
    HotelListComponent,
    HotelDetailComponent,
    FeedbackComponent,
    FlightListComponent,
    GlightDetailComponent,
    FlightBookingComponent,
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
