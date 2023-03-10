import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Router} from "@angular/router";
import {TeamsServiceService} from "../services/teams-service.service";
import {League} from "../models/league";
import {FormControl, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-league-update',
  templateUrl: './league-update.component.html',
  styleUrls: ['./league-update.component.css']
})
export class LeagueUpdateComponent implements OnInit {
  @Input() league: League = {id:0, name:""};
  leagueId: number = 0;
  leagueName: string = "";

  leagueForm: FormGroup = new FormGroup(
    {
      leagueID: new FormControl(null,[Validators.required, Validators.min(5)]),
      leagueName: new FormControl(null, [Validators.required, Validators.pattern("[A-Za-zÀ-ȕ ]+")])
    }
  )

  leagues: League[] = [];

  @Output() update = new EventEmitter<League[]>();


  constructor(private router:Router, private service:TeamsServiceService) {}

  ngOnInit() {
  }

  initializeUpdating(id: number, name: string) {
    this.leagueId = id;
    this.leagueName = name;
  }

  updateLeague() {
    this.league.id = this.leagueId;
    this.league.name = this.leagueName;
    this.service.updateLeague(this.league).subscribe({
      next:((result:League[]) =>
      {
        this.update.emit(result);
        this.fetchData();
      }),
      error:(err => {
        alert(err.error.message);
      })
    });
  }

  deleteLeague(id: number) {
    this.service.deleteLeague(id).subscribe(
      (result:League[]) => {
        this.update.emit(result);
        this.fetchData();
      }
   );
  }

  createLeague() {
    this.league.id = this.leagueId;
    this.league.name = this.leagueName;
    this.service.createLeague(this.league).subscribe ( {
      next:((result:League[]) => {
        this.update.emit(result);
        this.fetchData();
      }),
      error:(
        err =>  {
          alert(err.error.message);
        }
      )
    }
  );
  }

  fetchData() {
    this.service.getAllLeagues().subscribe((result : League[]) => (this.leagues = result));
    setTimeout(() => { this.fetchData() }, 2000);
  }

  public logout() {
    localStorage.clear();
    this.router.navigate(["/"]);
  }
}
