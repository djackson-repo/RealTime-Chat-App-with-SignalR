import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ChatRoomModel } from "../app/models/chat-room-model";
import { Observable } from "rxjs";
import * as e from "express";

@Injectable()
export class ChatRoomService {
/*  baseUrl = 'https://localhost:59446/';*/
  baseUrl = 'https://r8mevslq8d.execute-api.us-east-1.amazonaws.com/Prod/';
  constructor(
    private http: HttpClient) { }

  public GetRooms(): Observable<ChatRoomModel[]> {
    let url = this.baseUrl + 'api/ChatRoom/';
    return this.http.get<ChatRoomModel[]>(url);
  }

  public GetChatRoomUser(userId: string): Observable<ChatRoomModel[]> {
    let url = this.baseUrl + 'api/ChatRoom/GetUserChatRooms/' + userId;
    return this.http.get<ChatRoomModel[]>(url);
  }

  public CreateNewRoom(chatRoom: ChatRoomModel): Observable<ChatRoomModel> {
    console.log("creating new room:" + chatRoom.id);
    let url = this.baseUrl + 'api/ChatRoom/';
    return this.http.post<ChatRoomModel>(url, chatRoom);
  }

  public GetRoomInfo(chatRoomId: string): Observable<ChatRoomModel> {
    let url = this.baseUrl + 'api/ChatRoom/GetRoomInfo/' + chatRoomId;
    return this.http.get<ChatRoomModel>(url);
  }

  public Update(chatId: string, userId: string): Observable<ChatRoomModel> {
    console.log("creating new room:" + chatId);
    let url = this.baseUrl + 'api/ChatRoom/' + chatId;
    return this.http.put<ChatRoomModel>(url, userId);
  }

}
