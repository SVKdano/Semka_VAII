import {Component, EventEmitter, Input, Output} from '@angular/core';
import {Router} from "@angular/router";

import {User} from "../models/user";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {TeamsServiceService} from "../services/teams-service.service";
import {AuthentificationService} from "../services/authentification.service";

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent {
  @Input() user:User = { id:0, email:"", password:"" };
  userEmail: string = "";
  userPassword: string = "";


  users:User[] = [];

  userForm: FormGroup = new FormGroup(
    {
      userMail: new FormControl(null, [Validators.required, Validators.email]),
      userPasswd: new FormControl(null, [Validators.required])
    }
  )

  @Output() update = new EventEmitter<User[]>();

  constructor(private router:Router, private service:TeamsServiceService, private authService: AuthentificationService) {
  }

  fetchData() {
    this.service.getUsers().subscribe((result:User[]) => (this.users=result));
  }

  deleteUser(id: number) {
    this.service.deleteUser(id).subscribe((result:User[]) => {
      (this.update.emit(result));
      this.fetchData();
    })
  }

  createUser() {
    this.user.email = this.userEmail;
    this.user.password = this.userPassword;
    this.authService.register(this.user).subscribe( {
      next:((result:User[]) => {
        this.update.emit(result);
        this.fetchData();
      }),
      error:(err =>
      {
        alert(err.error.message);
      })
    });
  }

  public logout() {
    localStorage.clear();
    this.router.navigate(["/"]);
  }
}
