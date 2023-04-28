import { Component, OnInit } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

@Component({
  selector: 'app-chatRoom',
  templateUrl: './chatRoom.component.html',
})
export class ChatRoomComponent {
  private hubConnection: HubConnection;

  public sendMessage() {
    this.hubConnection.invoke('SendMessage', 'testmessage');

  }

  ngOnInit() {
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
