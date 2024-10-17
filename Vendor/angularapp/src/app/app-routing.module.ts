import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { VendorFormComponent } from './vendor-form/vendor-form.component';
import { VendorListComponent } from './vendor-list/vendor-list.component';
import { DeleteConfirmComponent } from './delete-confirm/delete-confirm.component';


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
