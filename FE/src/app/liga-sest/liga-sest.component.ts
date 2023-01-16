import { Component } from '@angular/core';
import {Team} from "../models/teams";
import {TeamsServiceService} from "../services/teams-service.service";

@Component({
  selector: 'app-liga-sest',
  templateUrl: './liga-sest.component.html',
  styleUrls: ['./liga-sest.component.css']
})
export class LigaSestComponent {
  teams: Team[] = [];

  constructor(private teamService: TeamsServiceService) {
    this.teamService.getTeams(6).subscribe((result: Team[]) => (this.teams = result));
  }
}
