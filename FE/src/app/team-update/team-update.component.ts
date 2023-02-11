import {Component, EventEmitter, Input, Output} from '@angular/core';
import { Router } from "@angular/router";
import { TeamsServiceService } from "../services/teams-service.service";
import {Team} from "../models/teams";
import {Player} from "../models/player";

@Component({
  selector: 'app-team-update',
  templateUrl: './team-update.component.html',
  styleUrls: ['./team-update.component.css']
})
export class TeamUpdateComponent {
  @Input() team: Team = {id:0, name:"", league:0 };
  teamId: number = 0;
  teamName: string = "";
  teamLeague: number = 0;

  teams: Team[] = [];

  @Output() update = new EventEmitter<Team[]>();

  constructor(private router:Router, private service:TeamsServiceService) {
  }

  fetchData() {
    this.service.getAllTeams().subscribe((result:Team[]) => (this.teams = result));
  }

  deletePlayer(Id: number) {
    this.service.deleteTeam(Id).subscribe((result: Team[]) =>
    {
      (this.update.emit(result));
      this.fetchData();
    });
  }

  initializeUpdating(id: number, name: string, leagueId: number) {
    this.teamId = id;
    this.teamName = name;
    this.teamLeague = leagueId;
  }

  updateTeam() {
    this.team.id = this.teamId;
    this.team.name = this.teamName;
    this.team.league = this.teamLeague;
    this.service.updateTeam(this.team).subscribe(
      (result:Team[]) =>
      {
        this.update.emit(result);
        this.fetchData();
      }
    );
  }

  createTeam() {
    this.team.id = 0;
    this.team.league = this.teamLeague;
    this.team.name = this.teamName;
    this.service.createTeam(this.team).subscribe(
      (result:Team[]) => {
        this.update.emit(result);
        this.fetchData();
      }
    );
  }

  logout() {
    localStorage.clear();
    this.router.navigate(["/"]);
  }

}
