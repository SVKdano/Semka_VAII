import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import {AuthentificationService} from "../services/authentification.service";
import {User} from "../models/user";

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent {
  user = new User();

  loginForm: FormGroup = new FormGroup({
    email: new FormControl(null, [Validators.required, Validators.email]),
    password: new FormControl(null, [Validators.required]),
  });

  constructor(
    private router: Router,
    private authService: AuthentificationService
  ) { }

  login(user: User) {

    if (!this.loginForm.valid) {
      return;
    }
    this.authService.login(user).subscribe({
      next:((token: string) => {
        localStorage.setItem("token", token)
      }),
      error:(_ => {
        alert("Wrong email or password");
      })
    });

    this.router.navigate(["/logged"]);

  }

}
