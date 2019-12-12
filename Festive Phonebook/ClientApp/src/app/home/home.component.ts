import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { UserService } from '../services/user.service';
import { LoginModel } from '../models/login.model';
import { Router } from '@angular/router';
import { NgxSpinnerModule, NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  errors: string[] = [];
  userForm = this.formBuilder.group( {
    userName : ['', [Validators.email, Validators.required] ]
  });

  constructor(private formBuilder: FormBuilder,
    private userService: UserService,
    private router: Router,
    private spinner: NgxSpinnerService) {}

  Submit() {
    this.userForm.disable();
    this.errors = [];
    localStorage.setItem('username', this.userForm.get('userName').value);
    this.spinner.show();
    this.userService.exists(new LoginModel(this.userForm.get('userName').value, '')).subscribe(
      (res: any) => {
        this.userForm.enable();
        this.spinner.hide();
        this.router.navigateByUrl('/login');
      },
      err => {
        this.errors.push('A technical error occurred while trying to verify the account.');
        this.userForm.enable();
        this.spinner.hide();
        this.router.navigateByUrl('/signup');
      }
    );
  }
}
