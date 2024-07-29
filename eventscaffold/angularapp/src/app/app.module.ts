import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CartoonEpisodeFormComponent } from './cartoonepisode-form/cartoonepisode-form.component';
import { HeaderComponent } from './header/header.component';
import { CartoonEpisodeListComponent } from './cartoonepisode-list/cartoonepisode-list.component';
import { DeleteConfirmComponent } from './delete-confirm/delete-confirm.component';

@NgModule({
  declarations: [
    AppComponent,
    CartoonEpisodeFormComponent,
    HeaderComponent,
    CartoonEpisodeListComponent,
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
