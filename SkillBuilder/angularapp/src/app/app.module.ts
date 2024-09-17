import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AddSkillComponent } from './add-skill/add-skill.component';
import { SkillListComponent } from './skill-list/skill-list.component';
import { EditSkillComponent } from './edit-skill/edit-skill.component';
import { AddSkillComponent } from './add-skill/add-skill.component';

@NgModule({
  declarations: [
    AppComponent,
    AddSkillComponent,
    SkillListComponent,
    EditSkillComponent,
    AddSkillComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
