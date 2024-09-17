import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddComicComponent } from './add-comic/add-comic.component';
import { ComicListComponent } from './comic-list/comic-list.component';
import { EditComicComponent } from './edit-comic/edit-comic.component';

const routes: Routes = [
  { path: '', redirectTo: '/comicsList', pathMatch: 'full' },
  { path: 'comicsList', component: ComicListComponent },
  { path: 'addComic', component: AddComicComponent },
  { path: 'edit/:id', component: EditComicComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
