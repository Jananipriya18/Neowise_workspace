import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CartoonEpisodeFormComponent } from './cartoon-episode-form/cartoon-episode-form.component';
import { CartoonEpisodeListComponent } from './cartoon-episode-list/cartoon-episode-list.component';
import { DeleteConfirmComponent } from './delete-confirm/delete-confirm.component'; // Assuming this component can be reused

const routes: Routes = [
  { path: 'addNewEpisode', component: CartoonEpisodeFormComponent },
  { path: 'viewEpisodes', component: CartoonEpisodeListComponent },
  { path: 'confirmDelete/:id', component: DeleteConfirmComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
