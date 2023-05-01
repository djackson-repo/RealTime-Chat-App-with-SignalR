import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { ChatMessageModel } from "../app/models/chat-message-model";

@Injectable()
export class ChatMessageService {
  baseUrl = 'https://r8mevslq8d.execute-api.us-east-1.amazonaws.com/Prod';
  constructor(
    private http: HttpClient) { }

  public GetChatRoomUser(chatMessages: ChatMessageModel): Observable<ChatMessageModel[]> {
    return this.http.post<ChatMessageModel[]>(this.baseUrl + 'api/ChatMessage/GetChatMessages', chatMessages);
  }
  public CreateNewMessage(chatRoom: ChatMessageModel): Observable<ChatMessageModel> {
    return this.http.post<ChatMessageModel>(this.baseUrl + 'api/ChatMessage/PostMessage', chatRoom);
  }


}
