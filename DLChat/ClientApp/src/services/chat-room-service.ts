import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ChatRoomModel } from "../app/models/chat-room-model";
import { Observable } from "rxjs";

@Injectable()
export class ChatRoomService {
  baseUrl = 'https://r8mevslq8d.execute-api.us-east-1.amazonaws.com/Prod';
  constructor(
    private http: HttpClient) { }

  public GetChatRoomUser(userId: string): Observable<ChatRoomModel[]> {
    return this.http.post<ChatRoomModel[]>(this.baseUrl + 'api/ChatRoom/GetUserChatRooms', userId);
  }
  public CreateNewRoom(chatRoom: ChatRoomModel): Observable<ChatRoomModel> {
    return this.http.post<ChatRoomModel>(this.baseUrl + 'api/ChatRoom/CreateNewRoom', chatRoom);
  }
}
