import { Component } from '@angular/core';
import { ChatRoomModel } from '../../models/chat-room-model';
import { ChatRoomService } from '../../../services/chat-room-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signup',
  templateUrl: './newRoom.component.html',
  providers: [ChatRoomService]
})
export class newRoomComponent {
  userId: string = '643785174997d3872d9363a6';
  rooms: ChatRoomModel[];
  newChatName: string = '';
  constructor(
    private chatRoomService: ChatRoomService,
    private _router: Router,
  ) { }


  //creates the room and the moment that the room is posted to the database it sends you back to home
  public CreateRoom() {
    console.log("Creating a new room with:" + this.newChatName);
    var newChatRoom: ChatRoomModel = new ChatRoomModel();
    newChatRoom.chatName = this.newChatName;
    newChatRoom.admins = [this.userId]
    newChatRoom.users = [this.userId]
    this.chatRoomService.CreateNewRoom(newChatRoom).subscribe((result) => {
      console.log("entering new ChatRoom as: " + newChatRoom.chatName);
      this._router.navigate(['/home']);
    },
      error => {
        console.error(error);
      })
    
  }
}
