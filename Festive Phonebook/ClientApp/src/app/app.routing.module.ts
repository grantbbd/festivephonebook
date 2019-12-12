import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { HomeComponent } from './home/home.component';
import { SignUpComponent } from './auth/sign-up/sign-up.component';
import { LoginComponent } from './auth/login/login.component';
import { PhonebookComponent } from './phonebook/phonebook.component';
import { AuthGuard } from './auth/auth.guard';
import { PhonebookEntryComponent } from './phonebook-entry/phonebook-entry.component';

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'signup', component: SignUpComponent },
  { path: 'login', component: LoginComponent },
  { path: 'phonebook', component: PhonebookComponent, canActivate: [AuthGuard] },
  { path: 'entry', component: PhonebookEntryComponent, canActivate: [AuthGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
