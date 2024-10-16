import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CheeseShopFormComponent } from './cheese-shop-form/cheese-shop-form.component';
import { CheeseShopListComponent } from './cheese-shop-list/cheese-shop-list.component';
import { DeleteConfirmComponent } from './delete-confirm/delete-confirm.component';

const routes: Routes = [
  { path: 'addNewShop', component: CheeseShopFormComponent },
  { path: 'viewShops', component: CheeseShopListComponent },
  { path: 'confirmDelete/:id', component: DeleteConfirmComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
