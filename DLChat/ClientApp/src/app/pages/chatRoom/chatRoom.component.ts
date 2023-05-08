import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { ChatMessageModel } from '../../models/chat-message-model';
import { ChatMessageService } from '../../../services/chat-message-service';
import { ChatRoomModel } from '../../models/chat-room-model';
import { ChatRoomService } from '../../../services/chat-room-service';

@Component({
  selector: 'app-chatRoom',
  templateUrl: './chatRoom.component.html',
  providers: [ChatMessageService, ChatRoomService]
})
export class ChatRoomComponent {
  private hubConnection: HubConnection;
  chatRoomId = "";
  newMessageText = "";
  userId: string = '643785174997d3872d9363a6';
  chatMessages: string[] = [];
  roomInfo: ChatRoomModel = new ChatRoomModel;

  public constructor(
    private _route: ActivatedRoute,
    private chatMessageService: ChatMessageService,
    private chatRoomService: ChatRoomService,
  )
  {}


  public sendMessage() {
    this.hubConnection.invoke('SendMessage', this.newMessageText);
    console.log(this.chatMessages);
    this.newMessageText = '';


  }

  ngOnInit() {
    let id = this._route.snapshot.paramMap.get('id');
    this.chatRoomId = id;
    this.ConnectHub();
    this.RoomInfo();

  }

  // create method that adds a message using the userId and roomId
  public CreateMessage() {
    var newMessage = new ChatMessageModel();
    this.hubConnection.invoke('SendMessage', this.userId + ': ' + this.newMessageText);
      newMessage.message = this.newMessageText;
      newMessage.chatRoomId = this.chatRoomId;
      newMessage.userId = this.userId;
      this.newMessageText = '';
      this.chatMessageService.CreateNewMessage(newMessage).subscribe((result) => {
        console.log("entering new ChatRoom as: " + newMessage.message);
      },
        error => {
          console.error(error);
        })

   
    
  }

  // create method that actively updates the user messages
  public GetMessages() {
    console.log("get messages with:" + this.roomInfo.chatName)
    for (var i = 0; i < this.roomInfo.users.length; i++) {
      var tempModel = new ChatMessageModel;
      tempModel.userId = this.roomInfo.users[i]
      tempModel.chatRoomId = this.chatRoomId;
      this.chatMessageService.GetChatMessages(tempModel).subscribe(
        (result: ChatMessageModel[]) =>
        {
          for (var j = 0; j < result.length; j++) {
            this.chatMessages.push(result[j].userId + ': ' + result[j].message);
            console.log('got messages: ' + result[i].message)
          }
          
        },
        error => {
          console.error(error); 
        })
    }
  }

  public RoomInfo() {
    this.chatRoomService.GetRoomInfo(this.chatRoomId).subscribe(
      (result: ChatRoomModel) => {
        this.roomInfo = result;
        console.log("got roomInfo " + this.roomInfo.chatName);
        this.GetMessages();
      },
      error => {
        console.error(error);
      }
    )
  }


  public ConnectHub() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('https://localhost:59446/chatHub')
      .build();

    this.hubConnection.start().then(() => {
      console.log('Hub connection started');
    });

    this.hubConnection.on('ReceiveMessage', (message: string) => {
      console.log('Received message: ' + message);
      this.chatMessages.push(message);
    });
  }
}
