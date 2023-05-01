import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
})
export class LoginComponent {


}

export interface UserModel {
  name: string;
  password: string;
}
export interface AuthenticatedResponse {
  token: string;
}
