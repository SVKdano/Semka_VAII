import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatToolbarModule} from "@angular/material/toolbar"
import {MatTableModule} from "@angular/material/table";
import { AppRoutingModule } from './app-routing.module';
import { WelcomeComponent } from './welcome/welcome.component';
import {HttpClientModule} from "@angular/common/http";
import { LeagueNavbarComponent } from './league-navbar/league-navbar.component';
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import { InnerleagueNavbarComponent } from './innerleague-navbar/innerleague-navbar.component';
import {MatCardModule} from "@angular/material/card";
import { ScheduleComponent } from './schedule/schedule.component';
import { LeagueComponent } from './league/league.component';
import { SortPipe } from './pipe/sort.pipe';
import { PlaceComponent } from './place/place.component';
import { PlayerListComponent } from './player-list/player-list.component';
import { ContactsComponent } from './contacts/contacts.component';
import { LoginPageComponent } from './login-page/login-page.component';
import {MatFormFieldModule} from "@angular/material/form-field";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatInputModule} from "@angular/material/input";
import { LoggedUserComponent } from './logged-user/logged-user.component';
import { PlayerUpdateComponent } from './player-update/player-update.component';
import {MatSelectModule} from "@angular/material/select";
import { PlaceUpdateComponent } from './place-update/place-update.component';
import { LeagueUpdateComponent } from './league-update/league-update.component';
import { TeamUpdateComponent } from './team-update/team-update.component';
import { ScheduleUpdateComponent } from './schedule-update/schedule-update.component';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    WelcomeComponent,
    LeagueNavbarComponent,
    InnerleagueNavbarComponent,
    ScheduleComponent,
    LeagueComponent,
    SortPipe,
    PlaceComponent,
    PlayerListComponent,
    ContactsComponent,
    LoginPageComponent,
    LoggedUserComponent,
    PlayerUpdateComponent,
    PlaceUpdateComponent,
    LeagueUpdateComponent,
    TeamUpdateComponent,
    ScheduleUpdateComponent,
    AdminPanelComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MatTableModule,
    MatToolbarModule,
    AppRoutingModule,
    HttpClientModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatInputModule,
    MatSelectModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
