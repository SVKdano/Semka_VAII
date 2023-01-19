import { Component } from '@angular/core';
import {Router} from "@angular/router";

@Component({
  selector: 'app-logged-user',
  templateUrl: './logged-user.component.html',
  styleUrls: ['./logged-user.component.css']
})
export class LoggedUserComponent {
  constructor(private router:Router) {
  }

  public logout() {
    localStorage.clear();
    this.router.navigate(["/"]);
  }
}
