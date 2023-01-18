import { Component } from '@angular/core';
import {Team} from "../models/teams";
import {TeamsServiceService} from "../services/teams-service.service";

@Component({
  selector: 'app-liga-sedem',
  templateUrl: './liga-sedem.component.html',
  styleUrls: ['./liga-sedem.component.css']
})
export class LigaSedemComponent {
  teams: Team[] = [];

  constructor(private teamService: TeamsServiceService) {
  }

  ngOnInit() : void {
    this.teamService.getTeams(7).subscribe((result: Team[]) => (this.teams = result));
    setTimeout(() => { this.ngOnInit() }, 1000);
  }
}
