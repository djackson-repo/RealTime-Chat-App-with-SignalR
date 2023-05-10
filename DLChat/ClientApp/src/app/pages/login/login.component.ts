import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

export interface UserModel {
  name: string;
  password: string;
}

export interface AuthenticatedResponse {
  token: string;
}

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent {

  userModel: UserModel = {
    name: '',
    password: ''
  };

  constructor(private router: Router, private http: HttpClient) { }



  login(name: string, password: string) {
    // Prepare the request body

    // Make the HTTP request to the API endpoint
    this.http.post(`https://r8mevslq8d.execute-api.us-east-1.amazonaws.com/Prod/api/user/login?username=${name}&password=${password}`, {}).subscribe((response: any) => {
      // Save the token in localStorage or any other preferred storage mechanism
      localStorage.setItem('token', response.token);
      // Redirect the user to the HomeComponent
      this.router.navigate(['/home']);
    }, (error: any) => {
      console.error('Error:', error);
      // Handle the error
    });
  }
}
