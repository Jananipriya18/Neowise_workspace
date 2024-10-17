import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { VendorFormComponent } from './vendor-form/vendor-form.component';
import { HeaderComponent } from './header/header.component';
import { VendorListComponent } from './vendor-list/vendor-list.component';
import { DeleteConfirmComponent } from './delete-confirm/delete-confirm.component';

@NgModule({
  declarations: [
    AppComponent,
    VendorFormComponent,
    HeaderComponent,
    VendorListComponent,
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
