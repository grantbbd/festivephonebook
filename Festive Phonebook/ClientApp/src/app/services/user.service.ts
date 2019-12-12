import { Injectable } from '@angular/core';
import { LoginModel } from '../models/login.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { SignUpModel } from '../models/signup.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  login(model: LoginModel) {
    return this.http.post(environment.apiURL + 'user/login', model);
  }

  exists(model: LoginModel) {
    return this.http.post(environment.apiURL + 'user/exists', model);
  }

  create(model: SignUpModel) {
    return this.http.post(environment.apiURL + 'user/create', model);
  }

  isLoggedIn() {
    return localStorage.getItem('token');
  }
}
