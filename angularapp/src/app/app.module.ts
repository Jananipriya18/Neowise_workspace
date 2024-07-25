import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { EventFormComponent } from './event-form/event-form.component';
import { HeaderComponent } from './header/header.component';
import { EventListComponent } from './event-list/event-list.component';
import { DeleteConfirmComponent } from './delete-confirm/delete-confirm.component';




@NgModule({
  declarations: [
    AppComponent,
    EventFormComponent,
    HeaderComponent,
    EventListComponent,
    DeleteConfirmComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
