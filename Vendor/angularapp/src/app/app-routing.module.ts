import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { VendorFormComponent } from './components/vendor-form/vendor-form.component';
import { VendorListComponent } from './components/vendor-list/vendor-list.component';
import { DeleteConfirmComponent } from './components/delete-confirm/delete-confirm.component';


const routes: Routes = [
  { path: 'addNewVendor', component: VendorFormComponent },
  { path: 'viewVendors', component: VendorListComponent },
  { path: 'confirmDelete/:id', component: DeleteConfirmComponent},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
