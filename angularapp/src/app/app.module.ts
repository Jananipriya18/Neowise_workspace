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
import { FlightDetailComponent } from './flight-detail/flight-detail.component';

@NgModule({
  declarations: [
    AppComponent,
    HotelListComponent,
    HotelDetailComponent,
    FeedbackComponent,
    FlightListComponent,
    GlightDetailComponent,
    FlightDetailComponent,
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
