import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserModel } from '../../models/user-model';
import { UserService } from '../../../services/user-service';
import { ChatRoomService } from '../../../services/chat-room-service';
import { ChatRoomModel } from '../../models/chat-room-model';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  providers: [UserService, ChatRoomService]
})
export class SearchComponent {
  users: UserModel[] = [];
  title = 'ClientApp';
  userId: string = '643785174997d3872d9363a6';
  chatRooms: ChatRoomModel[] = [];
  userName = '';
  selectedRoom = '';
  public constructor(
    private _route: ActivatedRoute,
    private userService: UserService,
    private chatRoomService: ChatRoomService,
  )
  {
    
  }

  ngOnInit() {
    let id = this._route.snapshot.paramMap.get('id');
    this.userName = id;
    console.log("reload search for: " + this.userName);
    this.UsersFound();
    this.SearchRooms();
  }

  // finds all of the users using the term that is searched for
  public UsersFound() {
    console.log("UserFound")
    this.userService.GetUserByName(this.userName).subscribe(
      (result: UserModel[]) => {
        this.users = result;
        console.log('got users: ', this.users)
      },
      error => {
        console.error(error);
      }
    );
    console.log('done');
  }

  // gets all of the rooms that the logged in user is in
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

  // Create method that adds a user to a chat
  public AddUser(userId: string, chatId: string) {
    console.log("adding user:" + userId + "to chatRoom: " + chatId);
    var newChatRoom = new ChatRoomModel;
    newChatRoom.id = chatId;
    newChatRoom.users.push(userId);
  }

  // checks to see if users array is empty
  public IsEmpty() {
    var emptyArray: UserModel[] = [];
    if (this.users == emptyArray) {
      console.log("false")
      return true;
    }
    return false;
  }

}
