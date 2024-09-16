import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AddStylistComponent } from './add-stylist/add-stylist.component';
import { EditStylistComponent } from './edit-stylist/edit-stylist.component';
import { StylistListComponent } from './stylish-list/stylish-list.component';

@NgModule({
  declarations: [
    AppComponent,
    AddStylistComponent,
    EditStylistComponent,
    StylistListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
