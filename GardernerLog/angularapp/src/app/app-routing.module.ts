import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddGardenerComponent } from './add-gardener/add-gardener.component';
import { GardenerListComponent } from './gardener-list/gardener-list.component';

const routes: Routes = [
  { path: '', redirectTo: '/gardenersList', pathMatch: 'full' },
  { path: 'gardenersList', component: GardenerListComponent },
  { path: 'addGardener', component: AddGardenerComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
