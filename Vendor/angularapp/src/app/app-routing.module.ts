import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PlaylistFormComponent } from './vendor-form/vendor-form.component';
import { PlaylistListComponent } from './vendor-list/vendor-list.component';
import { DeleteConfirmComponent } from './delete-confirm/delete-confirm.component';


const routes: Routes = [
  { path: 'addNewPlaylist', component: PlaylistFormComponent },
  { path: 'viewPlaylists', component: PlaylistListComponent },
  { path: 'confirmDelete/:id', component: DeleteConfirmComponent},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
