import {Component, OnInit} from '@angular/core';
import {Team} from "../models/teams";
import {TeamsServiceService} from "../services/teams-service.service";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-league',
  templateUrl: './league.component.html',
  styleUrls: ['./league.component.css']
})
export class LeagueComponent implements OnInit {
  teams: Team[] = [];

  constructor(private teamService: TeamsServiceService,
              private route: ActivatedRoute) {
  }

  ngOnInit() : void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.teamService.getTeams(id).subscribe((result: Team[]) => (this.teams = result));
    setTimeout(() => { this.ngOnInit() }, 1000);
  }
}
