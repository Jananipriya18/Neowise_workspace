import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddPodcastComponent } from './add-podcast/add-podcast.component';
import { PodcastListComponent } from './podcast-list/podcast-list.component';
import { EditPodcastComponent } from './edit-podcast/edit-podcast.component';

const routes: Routes = [
  { path: '', redirectTo: '/podcasts', pathMatch: 'full' },
  { path: 'podcastsList', component: PodcastListComponent },
  { path: 'addPodcast', component: AddPodcastComponent },
  { path: 'edit/:id', component: EditPodcastComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
