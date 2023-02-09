import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {Team} from "../models/teams";
import {TeamsServiceService} from "../services/teams-service.service";

@Component({
  selector: 'app-innerleague-navbar',
  templateUrl: './innerleague-navbar.component.html',
  styleUrls: ['./innerleague-navbar.component.css']
})
export class InnerleagueNavbarComponent implements OnInit {
  teams: Team[] = [];
  league: number = 0;

  constructor(private teamService: TeamsServiceService,
              private route: ActivatedRoute) {
  }

  ngOnInit() : void {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    this.teamService.getTeams(id).subscribe((result: Team[]) =>
      {
        (this.teams = result);
        this.league = result[0].league;
      }
    );
    //setTimeout(() => { this.ngOnInit() }, 1000);
  }

}
