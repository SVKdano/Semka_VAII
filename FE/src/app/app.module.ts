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
import { LigaPatComponent } from './liga-pat/liga-pat.component';
import { LigaSestComponent } from './liga-sest/liga-sest.component';
import { LigaSedemComponent } from './liga-sedem/liga-sedem.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    WelcomeComponent,
    LeagueNavbarComponent,
    LigaPatComponent,
    LigaSestComponent,
    LigaSedemComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MatTableModule,
    MatToolbarModule,
    AppRoutingModule,
    HttpClientModule,
    MatButtonModule,
    MatIconModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
