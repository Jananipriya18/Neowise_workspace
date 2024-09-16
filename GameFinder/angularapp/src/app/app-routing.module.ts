import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddGameComponent } from './add-game/add-game.component';
import { GameListComponent } from './game-list/game-list.component';
import { EditGameComponent } from './edit-game/edit-game.component';

const routes: Routes = [
  { path: '', redirectTo: '/gamesList', pathMatch: 'full' },
  { path: 'gamesList', component: GameListComponent },
  { path: 'addGame', component: AddGameComponent },
  { path: 'edit/:id', component: EditGameComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
