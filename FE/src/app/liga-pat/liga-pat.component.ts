import { Component } from '@angular/core';
import {TeamsServiceService} from "../services/teams-service.service";
import {Team} from "../models/teams";

@Component({
  selector: 'app-liga-pat',
  templateUrl: './liga-pat.component.html',
  styleUrls: ['./liga-pat.component.css']
})
export class LigaPatComponent {
  teams: Team[] = [];

  constructor(private teamService: TeamsServiceService) {
  }

  ngOnInit() : void {
    this.teamService.getTeams(5).subscribe((result: Team[]) => (this.teams = result));
    setTimeout(() => { this.ngOnInit() }, 1000);
  }

}
