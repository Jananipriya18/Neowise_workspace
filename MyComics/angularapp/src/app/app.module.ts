import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AddComicComponent } from './add-comic/add-comic.component';
import { ComicListComponent } from './comic-list/comic-list.component';
import { EditComicComponent } from './edit-comic/edit-comic.component';

@NgModule({
  declarations: [
    AppComponent,
    AddComicComponent,
    ComicListComponent,
    EditComicComponent
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
