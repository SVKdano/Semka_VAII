import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {User} from "../models/user";
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class AuthentificationService {

  constructor(private httpClient: HttpClient) { }

  public register(user: User): Observable<any> {
    return this.httpClient.post<any>(`${environment.apiUrl}/register`, user);
  }

  public login(user: User): Observable<string> {
    return this.httpClient.post(`${environment.apiUrl}/login`, user, {responseType: "text"});
  }

  public isLoggedIn() {
    return localStorage.getItem("token") != null;
  }

}
