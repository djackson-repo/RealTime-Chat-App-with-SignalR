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
  title = 'ClientApp';
  constructor(
    private _router: Router,
   private chatRoomService: ChatRoomService,

  ) { }


  ngOnInit() {
    this.SearchRooms();
  }

  //Navigates to the searched term search
  public SearchForUser() {
    console.log("SearchButtonPressed for: " + this.searchTerm);
    this._router.navigate(['/search', this.searchTerm]);
  }
  //Navigates to the selected chat
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
    this._router.navigate(['/newRoom']);
  }

}
