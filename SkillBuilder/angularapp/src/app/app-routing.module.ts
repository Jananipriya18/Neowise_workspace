import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddDoctorComponent } from './add-doctor/add-doctor.component';
import { DoctorListComponent } from './doctor-list/doctor-list.component';
import { EditDoctorComponent } from './edit-doctor/edit-doctor.component';

const routes: Routes = [
  { path: '', redirectTo: '/doctorsList', pathMatch: 'full' },
  { path: 'doctorsList', component: DoctorListComponent },
  { path: 'addDoctor', component: AddDoctorComponent },
  { path: 'edit/:id', component: EditDoctorComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
