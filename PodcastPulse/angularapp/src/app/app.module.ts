import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AddDoctorComponent } from './add-doctor/add-doctor.component';
import { DoctorListComponent } from './doctor-list/doctor-list.component';
import { EditDoctorComponent } from './edit-doctor/edit-doctor.component';
import { AddPodcastComponent } from './add-podcast/add-podcast.component';
import { EditPodcastComponent } from './edit-podcast/edit-podcast.component';
import { PodcasListtComponent } from './podcas-listt/podcas-listt.component';
import { PodcasListComponent } from './podcas-list/podcas-list.component';
import { PodcastListComponent } from './podcast-list/podcast-list.component';

@NgModule({
  declarations: [
    AppComponent,
    AddDoctorComponent,
    DoctorListComponent,
    EditDoctorComponent,
    AddPodcastComponent,
    EditPodcastComponent,
    PodcasListtComponent,
    PodcasListComponent,
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
