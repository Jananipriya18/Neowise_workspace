import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AddPodcastComponent } from './add-podcast/add-podcast.component';
import { EditPodcastComponent } from './edit-podcast/edit-podcast.component';
import { PodcastListComponent } from './podcast-list/podcast-list.component';

@NgModule({
  declarations: [
    AppComponent,
    AddPodcastComponent,
    EditPodcastComponent,
    PodcastListComponent
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
