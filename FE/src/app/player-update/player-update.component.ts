import {Component, OnChanges, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {TeamsServiceService} from "../services/teams-service.service";
import {Team} from "../models/teams";
import {Player} from "../models/player";

@Component({
  selector: 'app-player-update',
  templateUrl: './player-update.component.html',
  styleUrls: ['./player-update.component.css']
})
export class PlayerUpdateComponent implements OnInit {
  league: number = 0;
  teamNumber: number = 0;
  teamsFive: Team[] = [];
  teamsSix: Team[] = [];
  teamsSeven: Team[] = [];
  players: Player[] = [];

  constructor(private router:Router, private service:TeamsServiceService) {
  }

  ngOnInit() {
    this.service.getTeams(5).subscribe((result: Team[]) => (this.teamsFive = result));
    this.service.getTeams(6).subscribe((result: Team[]) => (this.teamsFive = result));
    this.service.getTeams(7).subscribe((result: Team[]) => (this.teamsFive = result));
    this.service.getPlayer(this.teamNumber).subscribe((result : Player[]) => (this.players = result));
  }

  public logout() {
    localStorage.clear();
    this.router.navigate(["/"]);
  }
}
