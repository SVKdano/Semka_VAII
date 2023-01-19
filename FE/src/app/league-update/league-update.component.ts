import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Router} from "@angular/router";
import {TeamsServiceService} from "../services/teams-service.service";
import {League} from "../models/league";
import {Place} from "../models/place";

@Component({
  selector: 'app-league-update',
  templateUrl: './league-update.component.html',
  styleUrls: ['./league-update.component.css']
})
export class LeagueUpdateComponent implements OnInit {
  @Input() league!: League;
  leagueId: number = 0;
  leagueName: string = "";
  leagues: League[] = [];
  @Output() update = new EventEmitter<League[]>();

  constructor(private router:Router, private service:TeamsServiceService) {}

  ngOnInit() {
    this.service.getAllLeagues().subscribe((result : League[]) => (this.leagues = result));
  }

  updateLeague() {
    this.service.updateLeague(this.league).subscribe((result:League[]) => this.update.emit(result));
  }

  deleteLeague() {
    this.service.deleteLeague(this.league.id).subscribe((result:League[]) => this.update.emit(result));
  }

  createLeague() {
    this.service.updateLeague(this.league).subscribe((result:League[]) => this.update.emit(result));
  }

  public logout() {
    localStorage.clear();
    this.router.navigate(["/"]);
  }
}
