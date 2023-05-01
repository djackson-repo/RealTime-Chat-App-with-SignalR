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
  users: string[] = [];
  title = 'ClientApp';
  userName = ''
  public constructor(
    private _route: ActivatedRoute,
    private userService: UserService,
  )
  {
    let id = this._route.snapshot.paramMap.get('id');
    this.userName = id;
  }

  ngOnInit() {
    this.UsersFound();
  }

  public UsersFound() {
    for (let i = 0; i < this.users.length; i++) {
      this.userService.GetUserByName(this.userName).subscribe(
        (result: string[]) => {
          this.users = result;
          console.log('got users: ', this.users)
        },
        error => {
          console.error(error);
        }
      );
      console.log('done');
    }
  }
}
