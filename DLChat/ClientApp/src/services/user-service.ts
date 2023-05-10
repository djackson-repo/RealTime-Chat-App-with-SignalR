import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { UserModel } from "../app/pages/login/login.component";

@Injectable()
export class UserService {
  baseUrl = 'https://localhost:59446/';
/*  baseUrl = 'https://r8mevslq8d.execute-api.us-east-1.amazonaws.com/Prod/';*/
  constructor(
    private http: HttpClient) { }

  public GetAsync(username: string): Observable<UserModel[]> {

    return this.http.post<UserModel[]>(this.baseUrl + 'api/user/GetAsync', username);
  }

  public GetUserByName(username: string): Observable<UserModel[]> {
    let url = this.baseUrl + "api/user/GetUserByName/" + username;
    console.log("calling: " + url);
    return this.http.get<UserModel[]>(url);
  }
}
