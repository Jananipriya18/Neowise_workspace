import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddStylistComponent } from './add-stylist/add-stylist.component';
import { StylistListComponent } from './stylist-list/stylist-list.component';

const routes: Routes = [
  { path: '', redirectTo: '/stylistsList', pathMatch: 'full' },
  { path: 'stylistsList', component: StylistListComponent },
  { path: 'addStylist', component: AddStylistComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
