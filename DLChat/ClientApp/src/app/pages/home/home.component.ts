import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ChatRoomService } from '../../../services/chat-room-service';
import { ChatRoomModel } from '../../models/chat-room-model';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  providers: [ChatRoomService]
})
export class HomeComponent {
  searchTerm: string;
  userId: string = '643785174997d3872d9363a6';
  chatRooms: ChatRoomModel[];
  newChatName: string;
  title = 'ClientApp';
  constructor(
    private _router: Router,
   private chatRoomService: ChatRoomService,

  ) { }


  ngOnInit() {
    this.SearchRooms();
  }

  public SearchForUser() {
    console.log("SearchButtonPressed for: " + this.searchTerm);
    this._router.navigate(['/search', this.searchTerm]);
  }
  public NavigateToChat(chatId: string) {
    console.log("NavToChatButton for: " + chatId);
    this._router.navigate(['/chatRoom', chatId]);
  }

  //create a method that gets all of the chat rooms that the user is in
  public SearchRooms() {
    console.log("SearchRooms with user: " + this.userId)
    this.chatRoomService.GetChatRoomUser(this.userId).subscribe(
      (result: ChatRoomModel[]) => {
        this.chatRooms = result;
        console.log('got users: ', this.chatRooms)
      },
      error => {
        console.error(error);
      }
    );
    console.log('done');
  }

  //create a method to create a new chatRoom
  public CreateRoom() {
    console.log("Creating a new room with");
    var newChatRoom: ChatRoomModel;
    newChatRoom.chatName = this.newChatName
    newChatRoom.admins = [this.userId]
    newChatRoom.users = [this.userId]
    this.chatRoomService.CreateNewRoom(newChatRoom).subscribe((result) =>
    {
      console.log("entering new ChatRoom as: " + newChatRoom.chatName);
    },
      error => {
        console.error(error);
      })
  }

}
