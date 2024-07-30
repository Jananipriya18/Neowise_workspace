import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MenuFormComponent } from './menu-form/menu-form.component';
import { MenuListComponent } from './menu-list/menu-list.component';
import { DeleteConfirmComponent } from './delete-confirm/delete-confirm.component';


const routes: Routes = [
  { path: 'addNewMenu', component: MenuFormComponent },
  { path: 'viewMenus', component: MenuListComponent },
  { path: 'confirmDelete/:id', component: DeleteConfirmComponent},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
