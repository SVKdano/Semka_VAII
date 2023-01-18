import {Component, OnInit} from '@angular/core';
import {Player} from "../models/player";
import {TeamsServiceService} from "../services/teams-service.service";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-player-list',
  templateUrl: './player-list.component.html',
  styleUrls: ['./player-list.component.css']
})
export class PlayerListComponent implements OnInit {
  players: Player[] = [];

  constructor(private teamService: TeamsServiceService,
              private route: ActivatedRoute) {
  }

  ngOnInit() : void {
    const id = Number(this.route.snapshot.paramMap.get('teamId'));
    this.teamService.getPlayer(id).subscribe((result: Player[]) => (this.players = result));
    //setTimeout(() => { this.ngOnInit() }, 1000);
  }

}
