import { NgModule } from '@angular/core';
import { RouterModule, Routes } from "@angular/router";
import { LeadersTableComponent } from "./leaders-table/leaders-table.component";
import {WelcomeComponent} from "./welcome/welcome.component";

const routes: Routes = [
  { path: "leaders", component: LeadersTableComponent },
  { path: "", component: WelcomeComponent }
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
