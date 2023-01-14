import { Component } from '@angular/core';
import {Team} from "./models/teams";
import {TeamsServiceService} from "./services/teams-service.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'semestralkaVAII';
  teams: Team[] = [];

  constructor(private teamService: TeamsServiceService) {
    this.teamService.getTeams().subscribe((result: Team[]) => (this.teams = result));
  }
}
