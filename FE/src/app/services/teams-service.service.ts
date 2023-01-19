import { Injectable } from '@angular/core';
import {Team} from "../models/teams";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {environment} from "../../environments/environment";
import {Schedule} from "../models/schedule";
import {Place} from "../models/place";
import {Player} from "../models/player";

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

  public getPlaces(id: number) : Observable<Place[]> {
    return this.http.get<Place[]>(`${environment.apiUrl}/place/${id}`);
  }

  public getPlayer(team: number) : Observable<Player[]> {
    return this.http.get<Player[]>(`${environment.apiUrl}/players/${team}`);
  }

  public getAllPlaces() : Observable<Place[]> {
    return this.http.get<Place[]>(`${environment.apiUrl}/places`);
  }

  public deletePlace(id: number) : Observable<Place[]> {
    return this.http.delete<Place[]>(`${environment.apiUrl}/${id}`)
  }

}
