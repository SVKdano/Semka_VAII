import { Injectable } from '@angular/core';
import {Team} from "../models/teams";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {environment} from "../../environments/environment";
import {Schedule} from "../models/schedule";
import {Place} from "../models/place";
import {Player} from "../models/player";
import {League} from "../models/league";
import {LeagueUpdateComponent} from "../league-update/league-update.component";

@Injectable({
  providedIn: 'root'
})
export class TeamsServiceService {

  constructor(private http:HttpClient) { }

  public getTeams(league: number) : Observable<Team[]> {

    return this.http.get<Team[]>(`${environment.apiUrl}/teams/${league}`);
  }

  public getSchedule(league: number) : Observable<Schedule[]> {
    return this.http.get<Schedule[]>(`${environment.apiUrl}/schedule/${league}`);
  }

  public getPlaces(teamId: number) : Observable<Place[]> {
    return this.http.get<Place[]>(`${environment.apiUrl}/place/${teamId}`);
  }

  public getPlayer(team: number) : Observable<Player[]> {
    return this.http.get<Player[]>(`${environment.apiUrl}/players/${team}`);
  }

  public getAllPlaces() : Observable<Place[]> {
    return this.http.get<Place[]>(`${environment.apiUrl}/places`);
  }

  public getAllPlayers(): Observable<Player[]> {
    return this.http.get<Player[]>(`${environment.apiUrl}/players`);
  }

  public deletePlace(id: number) : Observable<Place[]> {
    return this.http.delete<Place[]>(`${environment.apiUrl}/${id}`)
  }

  public deletePlayer(id: number) : Observable<Player[]> {
    return this.http.delete<Player[]>(`${environment.apiUrl}/delete/${id}`)
  }

  public getAllLeagues() :Observable<League[]> {
    return this.http.get<League[]>(`${environment.apiUrl}/allLeagues`);
  }

  public updateLeague(league: League) : Observable<League[]> {
    return this.http.get<League[]>(`${environment.apiUrl}/update`);
  }

  public deleteLeague(id: number) : Observable<League[]> {
    return this.http.delete<League[]>(`${environment.apiUrl}/leagueDelete/${id}`);
  }

  public createLeague(liga: League) : Observable<League[]> {
  return this.http.post<League[]>(`${environment.apiUrl}/newleague`,liga);
  }
}
