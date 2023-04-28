import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
})
export class LoginComponent implements OnInit {
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

}

export interface UserModel {
  name: string;
  password: string;
}
export interface AuthenticatedResponse {
  token: string;
}
