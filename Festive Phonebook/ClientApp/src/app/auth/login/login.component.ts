import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { LoginModel } from 'src/app/models/login.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  errors: string[] = [];
  loginForm = this.formBuilder.group({
    userName: new FormControl(
      { value: localStorage.getItem('username'), disabled: true },
      [Validators.required, Validators.email]
    ),
    password: ['', Validators.required]
  });

  constructor(
    private formBuilder: FormBuilder,
    private userService: UserService,
    private router: Router
  ) {}

  ngOnInit() {}

  Submit() {
    this.errors = [];
    this.loginForm.disable();
    this.userService
      .login(
        new LoginModel(
          this.loginForm.get('userName').value,
          this.loginForm.get('password').value
        )
      )
      .subscribe(
        (res: any) => {
          localStorage.setItem('token', res.token);
          this.router.navigateByUrl('/phonebook');
        },
        (err) => {
         if (err.error) {
           this.errors.push(err.error.message);
         } else {
           this.errors.push('A technical error occurred trying to login.');
         }
         this.loginForm.enable();
        }
      );
  }
}
