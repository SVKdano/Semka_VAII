import { NgModule } from '@angular/core';
import { RouterModule, Routes } from "@angular/router";
import {WelcomeComponent} from "./welcome/welcome.component";
import {ScheduleComponent} from "./schedule/schedule.component";
import {LeagueComponent} from "./league/league.component";
import {PlaceComponent} from "./place/place.component";
import {PlayerListComponent} from "./player-list/player-list.component";
import {ContactsComponent} from "./contacts/contacts.component";
import {LoginPageComponent} from "./login-page/login-page.component";
import {LoggedUserComponent} from "./logged-user/logged-user.component";
import {AuthGuard} from "./guard/auth.guard";
import {PlayerUpdateComponent} from "./player-update/player-update.component";
import {PlaceUpdateComponent} from "./place-update/place-update.component";
import {LeagueUpdateComponent} from "./league-update/league-update.component";
import {TeamUpdateComponent} from "./team-update/team-update.component";

const routes: Routes = [
  { path: "", component: WelcomeComponent },
  { path: "contacts", component: ContactsComponent},
  { path: "league/:id", component: LeagueComponent},
  { path: "league/:id/schedule", component: ScheduleComponent},
  { path: "league/:id/place/:placeId", component: PlaceComponent},
  { path: "league/:id/players/:teamId", component: PlayerListComponent},
  { path: "login", component: LoginPageComponent},
  { path: "logged", component: LoggedUserComponent, canActivate: [AuthGuard] },
  { path: "logged/playerChanges", component: PlayerUpdateComponent, canActivate: [AuthGuard]},
  { path: "logged/placeChanges", component: PlaceUpdateComponent, canActivate: [AuthGuard]},
  { path: "logged/leagueChanges", component: LeagueUpdateComponent, canActivate: [AuthGuard]},
  { path: "logged/teamChanges", component: TeamUpdateComponent, canActivate: [AuthGuard]},
  { path: "**", redirectTo: "", pathMatch: "full"},
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
