import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddSkillComponent } from './add-skill/add-skill.component';
import { SkillListComponent } from './skill-list/skill-list.component';
import { EditSkillComponent } from './edit-skill/edit-skill.component';

const routes: Routes = [
  { path: '', redirectTo: '/skillsList', pathMatch: 'full' },
  { path: 'skillsList', component: SkillListComponent },
  { path: 'addSkill', component: AddSkillComponent },
  { path: 'edit/:id', component: EditSkillComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
