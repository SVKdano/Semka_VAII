import {Component, EventEmitter, OnChanges, OnInit, Output} from '@angular/core';
import {Router} from "@angular/router";
import {TeamsServiceService} from "../services/teams-service.service";
import {Player} from "../models/player";

@Component({
  selector: 'app-player-update',
  templateUrl: './player-update.component.html',
  styleUrls: ['./player-update.component.css']
})
export class PlayerUpdateComponent implements OnInit {
  players: Player[] = [];
  playerId: number = 0;
  @Output() update = new EventEmitter<Player[]>();

  constructor(private router:Router, private service:TeamsServiceService) {
  }

  ngOnInit() {
    this.service.getAllPlayers().subscribe((result : Player[]) => (this.players = result));
  }

  deletePlayer(Id: number) {
    this.service.deletePlayer(this.playerId).subscribe((result: Player[]) => (this.update.emit(result)));
  }

  public logout() {
    localStorage.clear();
    this.router.navigate(["/"]);
  }
}
