import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { ChatMessageModel } from "../app/models/chat-message-model";

@Injectable()
export class ChatMessageService {
/*  baseUrl = 'https://localhost:59446/';*/
  baseUrl = 'https://r8mevslq8d.execute-api.us-east-1.amazonaws.com/Prod/';
  constructor(
    private http: HttpClient) { }

  public GetChatMessages(chatMessages: ChatMessageModel): Observable<ChatMessageModel[]> {
    var url = this.baseUrl + 'api/ChatMessage/GetChatMessages';
    return this.http.post<ChatMessageModel[]>(url, chatMessages);
  }
  public CreateNewMessage(chatMessage: ChatMessageModel): Observable<ChatMessageModel> {
    var url = this.baseUrl + 'api/ChatMessage/';
    return this.http.post<ChatMessageModel>(url, chatMessage);
  }


}
