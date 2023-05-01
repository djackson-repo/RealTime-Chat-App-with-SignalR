import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { UserModel } from "../app/pages/login/login.component";

@Injectable()
export class UserService {
  baseUrl = 'https://r8mevslq8d.execute-api.us-east-1.amazonaws.com/Prod';
  constructor(
    private http: HttpClient) { }

  public GetUserByName(username: string): Observable<string[]> {
    return this.http.post<string[]>(this.baseUrl + 'api/User/GetUserByName', username);
  }
}
