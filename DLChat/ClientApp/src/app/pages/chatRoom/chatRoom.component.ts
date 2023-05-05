import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

@Component({
  selector: 'app-chatRoom',
  templateUrl: './chatRoom.component.html',
})
export class ChatRoomComponent {
  private hubConnection: HubConnection;
  chatRoomId = "";
  public constructor(
    private _route: ActivatedRoute)
  {
    
  }


  public sendMessage() {
    this.hubConnection.invoke('SendMessage', 'testmessage');

  }

  ngOnInit() {
    let id = this._route.snapshot.paramMap.get('id');
    this.chatRoomId = id;
    this.ConnectHub();
  }

  // create method that adds a message using the userId and roomId

  // create method that actively updates the user messages

  public ConnectHub() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('https://localhost:59446/chatHub')
      .build();

    this.hubConnection.start().then(() => {
      console.log('Hub connection started');
    });

    this.hubConnection.on('ReceiveMessage', (message: string) => {
      console.log('Received message: ' + message);
    });
  }
}
