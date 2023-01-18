import { Injectable } from '@angular/core';
import {Team} from "../models/teams";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {environment} from "../../environments/environment";
import {Schedule} from "../models/schedule";

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
}
