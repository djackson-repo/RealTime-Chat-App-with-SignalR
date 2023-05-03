import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserModel } from '../../models/user-model';
import { UserService } from '../../../services/user-service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  providers: [UserService]
})
export class SearchComponent {
  users: UserModel[] = [];
  title = 'ClientApp';
  userName = ''
  public constructor(
    private _route: ActivatedRoute,
    private userService: UserService,
  )
  {
    
  }

  ngOnInit() {
    let id = this._route.snapshot.paramMap.get('id');
    this.userName = id;
    console.log("reload search for: " + this.userName);
    this.UsersFound();
  }

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

  // Create method that adds a user to a chat
}
