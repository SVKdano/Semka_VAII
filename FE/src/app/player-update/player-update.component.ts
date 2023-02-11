import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Router} from "@angular/router";
import {TeamsServiceService} from "../services/teams-service.service";
import {Player} from "../models/player";

@Component({
  selector: 'app-player-update',
  templateUrl: './player-update.component.html',
  styleUrls: ['./player-update.component.css']
})
export class PlayerUpdateComponent implements OnInit {
  @Input() player: Player = {id: 0, teamId:0, name:"", surname:""};
  playerId: number = 0;
  playerTeamId: number = 0;
  playerName: string = "";
  playerSurname: string = "";

  players: Player[] = [];
  @Output() update = new EventEmitter<Player[]>();

  constructor(private router:Router, private service:TeamsServiceService) {
  }

  ngOnInit() {
  }

  deletePlayer(Id: number) {
    this.service.deletePlayer(Id).subscribe((result: Player[]) =>
    {
      (this.update.emit(result));
      this.fetchData();
    });
  }

  fetchData() {
    this.service.getAllPlayers().subscribe((result : Player[]) => (this.players = result));
    setTimeout(() => { this.fetchData() }, 2000);
  }

  initializeUpdating(id: number, name: string, surname: string, teamId: number) {
    this.playerId = id;
    this.playerName = name;
    this.playerSurname = surname;
    this.playerTeamId = teamId;
  }

  createPlayer() {
    this.player.id = 0;
    this.player.teamId = this.playerTeamId;
    this.player.name = this.playerName;
    this.player.surname = this.playerSurname;
    this.service.createPlayer(this.player).subscribe(
      (result:Player[]) => {
        this.update.emit(result);
        this.fetchData();
      }
    );
  }

  updatePlayer() {
    this.player.id = this.playerId;
    this.player.teamId = this.playerTeamId;
    this.player.name = this.playerName;
    this.player.surname = this.playerSurname;
    this.service.updatePlayer(this.player).subscribe(
      (result:Player[]) =>
      {
        this.update.emit(result);
        this.fetchData();
      }
    );
  }

  public logout() {
    localStorage.clear();
    this.router.navigate(["/"]);
  }
}
