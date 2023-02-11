import { Injectable } from '@angular/core';
import {Team} from "../models/teams";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {environment} from "../../environments/environment";
import {Schedule} from "../models/schedule";
import {Place} from "../models/place";
import {Player} from "../models/player";
import {League} from "../models/league";

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

  public getScheduleOfRound(league: number, round: number) : Observable<Schedule[]> {
    return this.http.get<Schedule[]>(`${environment.apiUrl}/schedule/${league}/${round}`);
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

  public getAllLeagues() :Observable<League[]> {
    return this.http.get<League[]>(`${environment.apiUrl}/allLeagues`);
  }

  public getAllTeams() : Observable<Team[]> {
    return this.http.get<Team[]>(`${environment.apiUrl}/allTeams`);
  }

  public getLeagueRounds(leagueId: number) : Observable<number> {
    return this.http.get<number>(`${environment.apiUrl}/rounds/${leagueId}`);
  }

  public deletePlace(id: number) : Observable<Place[]> {
    return this.http.delete<Place[]>(`${environment.apiUrl}/${id}`)
  }

  public deletePlayer(id: number) : Observable<Player[]> {
    return this.http.delete<Player[]>(`${environment.apiUrl}/delete/${id}`)
  }

  public updateLeague(league: League) : Observable<League[]> {
    return this.http.put<League[]>(`${environment.apiUrl}/leagueUpdate`, league);
  }

  public updatePlayer(hrac: Player) : Observable<Player[]> {
    return this.http.put<Player[]>(`${environment.apiUrl}/playerUpdate`, hrac);
  }

  public updateTeam(team: Team) : Observable<Team[]> {
    return this.http.put<Team[]>(`${environment.apiUrl}/teamUpdate`, team);
  }

  public deleteLeague(id: number) : Observable<League[]> {
    return this.http.delete<League[]>(`${environment.apiUrl}/leagueDelete/${id}`);
  }

  public deleteTeam(id: number) : Observable<Team[]> {
    return this.http.delete<Team[]>(`${environment.apiUrl}/deleteTeam/${id}`);
  }

  public createLeague(liga: League) : Observable<League[]> {
    return this.http.post<League[]>(`${environment.apiUrl}/newleague`,liga);
  }

  public createPlayer(hrac: Player) : Observable<Player[]> {
    return this.http.post<Player[]>(`${environment.apiUrl}/newPlayer`, hrac);
  }

  public createTeam(team: Team) : Observable<Team[]> {
    return this.http.post<Team[]>(`${environment.apiUrl}/newTeam`, team);
  }
}
