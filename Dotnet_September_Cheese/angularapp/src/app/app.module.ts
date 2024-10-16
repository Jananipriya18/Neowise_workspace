import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HeaderComponent } from './header/header.component';
import { DeleteConfirmComponent } from './delete-confirm/delete-confirm.component';
import { CheeseShopFormComponent } from './cheese-shop-form/cheese-shop-form.component';
import { CheeseShopListComponent } from './cheese-shop-list/cheese-shop-list.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    DeleteConfirmComponent,
    CheeseShopFormComponent,
    CheeseShopListComponent
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
