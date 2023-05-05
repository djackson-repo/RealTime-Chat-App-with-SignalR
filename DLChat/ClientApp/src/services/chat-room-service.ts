import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ChatRoomModel } from "../app/models/chat-room-model";
import { Observable } from "rxjs";
import * as e from "express";

@Injectable()
export class ChatRoomService {
  baseUrl = 'https://localhost:59446/';
  //baseUrl = 'https://r8mevslq8d.execute-api.us-east-1.amazonaws.com/Prod';
  constructor(
    private http: HttpClient) { }

  public GetChatRoomUser(userId: string): Observable<ChatRoomModel[]> {
    let url = this.baseUrl + 'api/ChatRoom/GetUserChatRooms/' + userId;
    return this.http.get<ChatRoomModel[]>(url);
  }
  public CreateNewRoom(chatRoom: ChatRoomModel): Observable<ChatRoomModel> {
    let url = this.baseUrl + 'api/ChatRoom/CreateNewRoom' + chatRoom;
    return this.http.post<ChatRoomModel>(this.baseUrl + 'api/ChatRoom/CreateNewRoom', chatRoom);
  }
}
