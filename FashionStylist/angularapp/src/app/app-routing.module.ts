import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddStylistComponent } from './add-stylist/add-stylist.component';
import { StylistListComponent } from './stylish-list/stylish-list.component';
import { EditStylistComponent } from './edit-stylist/edit-stylist.component';

const routes: Routes = [
  { path: '', redirectTo: '/stylistsList', pathMatch: 'full' },
  { path: 'stylistsList', component: StylistListComponent },
  { path: 'addStylist', component: AddStylistComponent },
  { path: 'edit/:id', component: EditStylistComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
