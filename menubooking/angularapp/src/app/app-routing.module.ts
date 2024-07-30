import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TutorFormComponent } from './menu-form/menu-form.component';
import { TutorListComponent } from './tutor-list/tutor-list.component';
import { DeleteConfirmComponent } from './delete-confirm/delete-confirm.component';


const routes: Routes = [
  { path: 'addNewTutor', component: TutorFormComponent },
  { path: 'viewTutors', component: TutorListComponent },
  { path: 'confirmDelete/:id', component: DeleteConfirmComponent},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
