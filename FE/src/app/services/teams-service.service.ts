import { Injectable } from '@angular/core';
import {Team} from "../models/teams";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class TeamsServiceService {

  constructor(private http:HttpClient) { }

  public getTeams() : Observable<Team[]> {

    return this.http.get<Team[]>(`${environment.apiUrl}/teams/5`);
  }
}
