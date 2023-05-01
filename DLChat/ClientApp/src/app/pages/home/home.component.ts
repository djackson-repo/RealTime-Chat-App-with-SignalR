import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ChatRoomService } from '../../../services/chat-room-service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  providers: [ChatRoomService]
})
export class HomeComponent {
  searchTerm: string;
  title = 'ClientApp';
  constructor(
    private _router: Router,
   private chatRoomService: ChatRoomService,

  ) { }

  public searchFor() {
    // Perform the search using the searchTerm
    console.log('Search term:', this.searchTerm);
    // Add your search logic here
  }
  public SearchForUser() {
    console.log("SearchButtonPressed");
    this._router.navigate(['/search', this.searchTerm]);
  }

  public NavigateToChat(chatId: string) {
    console.log("NavToChatButton");
    this._router.navigate(['/chatRoom', chatId]);
  }


}
