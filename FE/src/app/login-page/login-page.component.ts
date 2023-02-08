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

  register(user: User) {
    this.authService.register(user).subscribe({
      next:(response => {
        alert(response.message);
        this.loginForm.reset();
      }),
      error:(err => {
        alert(err.error.message)
    })
    });
  }

  login(user: User) {
    if (!this.loginForm.valid) {
      return;
    }
    this.authService.login(user).subscribe((token: string) => {localStorage.setItem("token", token)});

    this.router.navigate( ["/logged"]);
  }

}
