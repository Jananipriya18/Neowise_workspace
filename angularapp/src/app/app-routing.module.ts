import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FeedbackComponent } from './components/feedback/feedback.component';
// Import your components here
import { HotelListComponent } from './components/hotel-list/hotel-list.component';
import { HotelDetailComponent } from './components/hotel-detail/hotel-detail.component';

const routes: Routes = [
  { path: '', redirectTo: '/hotels', pathMatch: 'full' },
  { path: 'hotels', component: HotelListComponent },
  { path: 'hotel/:id', component: HotelDetailComponent },
  { path: 'feedback', component: FeedbackComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
