import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormControl } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { SignUpModel } from 'src/app/models/signup.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
  
  errors: string[] = [];
  signUpForm = this.formBuilder.group({
    userName: new FormControl(
      { value: localStorage.getItem('username'), disabled: true },
      [Validators.required, Validators.email]
    ),
    firstName: ['', Validators.required],
    surname: ['', Validators.required],
    password: ['', Validators.required],
    confirmPassword: ['', Validators.required]
  });

  constructor(
    private formBuilder: FormBuilder,
    private userService: UserService,
    private router: Router
  ) {}

  ngOnInit() {}

  Submit() {
    this.errors = [];
    if (
      this.signUpForm.get('password').value !==
      this.signUpForm.get('confirmPassword').value
    ) {
      this.errors.push('Passwords must match.');
    } else {
      this.signUpForm.disable();
      this.userService
        .create(
          new SignUpModel(
            this.signUpForm.get('userName').value,
            this.signUpForm.get('firstName').value,
            this.signUpForm.get('surname').value,
            this.signUpForm.get('password').value
          )
        )
        .subscribe(
          (res: any) => {
            this.signUpForm.enable();
            localStorage.setItem('token', res.token);
            this.router.navigateByUrl('/phonebook');
          },
          (err: any) => {
            console.log(err);
            if (err.error.errors) {
              err.error.errors.forEach(element => {
                this.errors.push(element.description);
              });
            } else {
              this.errors.push('A technical error occurred while creating the user.');
            }
            this.signUpForm.enable();
          }
        );
    }
  }
}
