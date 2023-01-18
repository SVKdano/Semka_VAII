import { NgModule } from '@angular/core';
import { RouterModule, Routes } from "@angular/router";
import {WelcomeComponent} from "./welcome/welcome.component";
import {ScheduleComponent} from "./schedule/schedule.component";
import {LeagueComponent} from "./league/league.component";

const routes: Routes = [
  { path: "", component: WelcomeComponent },
  { path: "league/:id", component: LeagueComponent},
  { path: "league/:id/schedule", component: ScheduleComponent},
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
